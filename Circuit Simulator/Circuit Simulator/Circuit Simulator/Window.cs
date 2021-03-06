﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Circuit_Simulator
{
    public partial class Window : Form
    {
        #region Constants
        public const string Title = "Circuit Simulator";
        public const string Filter = "Logic Circuit |*.circ";
        public const string TagFont = "Consolas";
        public const string NameFont = "Microsoft YaHei UI";
        public const string DataFormat = "circ";
        public const float ScaleFactor = 2/3f;
        public const float ConnectionClickRange = 3.5f;
        public const float PenWidth = 0.05f;
        public const float GridSize = 20f;
        public const int MaxUndoBufferSize = 32;
        public const int Version = 108;
        #endregion
        #region Fields and Properties
        bool selecting, objectMoved;
        static bool msgboxActive, mousedown;
        PointF lastClick, lastPos, lastLocation;
        List<byte> clickState;
        CablePoint pointClicked;

        List<Gate> clipboard = new List<Gate>();

        float offsetX, offsetY, scale = GridSize;
        bool saved;
        string filePath;
        Connection clickedConnection;

        List<byte>[] undoBuffer = new List<byte>[MaxUndoBufferSize];
        int undoIndex, redoIndex, undoBufferUsed;

        List<Gate> gates = new List<Gate>();
        List<Gate> selected = new List<Gate>();

        List<CablePoint> Points
        {
            get {
                List<CablePoint> points = new List<CablePoint>();
                foreach (var gate in gates)
                    foreach (var input in gate.input)
                        if (!input.empty) points.Add(input.Point);
                return points;
            }
        }
        bool AnySelected
        {
            get {
                return selected.Count > 0;
            }
        }
        bool AnyCopied
        {
            get {
                return clipboard.Count > 0;
            }
        }
        bool ButtonSelected
        {
            get {
                return selected.Any(gate => gate.type == Gate.Type.Switch);
            }
        }
        bool CanRemoveConnection
        {
            get {
                return clickedConnection != null && clickedConnection.self != null && !clickedConnection.isOutput
                    && clickedConnection.self.input.Count > clickedConnection.self.minInputs;
            }
        }
        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var win = new Window();
            if (args.Length == 1 && new FileInfo(args[0]).Exists)
                if (!win.OpenFile(args[0]))
                    return;
            Application.Run(win);
        }
        public Window()
        {
            InitializeComponent();
            LoadGateList();
            ResetAll();
            UpdateButtonVisibility();
            UpdateTitle();
        }

        #region IO
        List<byte> GenerateState()
        {
            List<Connection> connectionList = new List<Connection>();
            List<CablePoint> pointList = Points.ToList();

            foreach (var gate in gates)
                foreach (var conn in gate.Connections)
                    if (!conn.empty)
                        connectionList.Add(conn);

            List<byte> stream = new List<byte>();

            stream.AddByte(Version);
            stream.AddInt(pointList.Count);
            foreach (var point in pointList)
            {
                stream.AddFloat(point.x);
                stream.AddFloat(point.y);
            }

            stream.AddInt(AmtInputs());
            stream.AddInt(AmtOutputs());

            foreach (string name in GetSorted(Gate.Type.Switch).Select(g => g.name))
                stream.AddString(name);
            foreach (string name in GetSorted(Gate.Type.Light).Select(g => g.name))
                stream.AddString(name);
            foreach (bool bt in GenerateTruthTable())
                stream.AddBool(bt);

            foreach (var gate in gates)
            {
                stream.AddInt((int) gate.type);
                stream.AddBool(gate.isSelected);
                stream.AddBool(gate.state);
                stream.AddFloat(gate.x);
                stream.AddFloat(gate.y);
                stream.AddFloat(gate.w);
                stream.AddFloat(gate.h);

                stream.AddString(gate.name);

                stream.AddInt(gate.amtInputs);
                stream.AddInt(gate.amtOutputs);

                if (gate.type == Gate.Type.Custom)
                {
                    foreach (string name in gate.nameList)
                        stream.AddString(name);
                    foreach (bool bt in gate.truthTable)
                        stream.AddBool(bt);
                }

                foreach (var conn in gate.Connections)
                {
                    stream.AddInt(conn.index);
                    stream.AddBool(conn.inverted);
                    stream.AddBool(conn.isOutput);
                    stream.AddBool(conn.empty);

                    if (!conn.empty)
                    {
                        if (!conn.isOutput)
                            stream.AddInt(pointList.IndexOf(conn.Point));
                        stream.AddInt(conn.OtherConnections.Count);
                        foreach (var c in conn.OtherConnections)
                            stream.AddInt(connectionList.IndexOf(c));
                    }
                }
            }
            return stream;
        }

        bool LoadState(List<byte> bytes)
        {
            try
            {
                List<Gate> gates = new List<Gate>();
                List<Gate> selected = new List<Gate>();
                List<Connection> connectionList = new List<Connection>();
                List<CablePoint> pointList = new List<CablePoint>();
                List<int> refList = new List<int>();

                int i = 1;
                int amtPoints = bytes.ReadInt(ref i);
                for (int j = 0; j < amtPoints; j++)
                {
                    var point = new CablePoint(bytes.ReadFloat(ref i), bytes.ReadFloat(ref i));
                    pointList.Add(point);
                }

                int amtAllInputs = bytes.ReadInt(ref i);
                int amtAllOutputs = bytes.ReadInt(ref i);
                for (int j = 0; j < amtAllInputs + amtAllOutputs; j++)
                {
                    var skip = bytes.ReadInt(ref i); i += skip;
                    //i += bytes.ReadInt(ref i);
                }
                i += (1 << amtAllInputs) * amtAllOutputs;

                while (i < bytes.Count)
                {
                    Gate.Type type = (Gate.Type) bytes.ReadInt(ref i);
                    Gate gate = new Gate(0, 0, type);
                    gate.ResetAllConnections();
                    if (bytes.ReadBool(ref i)) selected.Add(gate);
                    gate.state = bytes.ReadBool(ref i);
                    gate.x = bytes.ReadFloat(ref i);
                    gate.y = bytes.ReadFloat(ref i);
                    gate.w = bytes.ReadFloat(ref i);
                    gate.h = bytes.ReadFloat(ref i);
                    gate.name = bytes.ReadString(ref i);

                    int amtInputs = bytes.ReadInt(ref i);
                    int amtOutputs = bytes.ReadInt(ref i);

                    if (type == Gate.Type.Custom)
                    {
                        gate.ChangeTo(Gate.Type.Custom, gate);
                        gate.nameList = new string[amtInputs + amtOutputs];
                        gate.truthTable = new bool[(1 << amtInputs) * amtOutputs];

                        for (int j = 0; j < amtInputs + amtOutputs; j++)
                        {
                            gate.nameList[j] = bytes.ReadString(ref i);
                        }
                        for (int j = 0; j < (1 << amtInputs) * amtOutputs; j++)
                        {
                            gate.truthTable[j] = bytes.ReadBool(ref i);
                        }
                    }

                    for (int j = 0; j < amtInputs + amtOutputs; j++)
                    {
                        var connection = new Connection(gate, 0, false, false);
                        connection.index = bytes.ReadInt(ref i);
                        connection.inverted = bytes.ReadBool(ref i);
                        connection.isOutput = bytes.ReadBool(ref i);
                        connection.empty = bytes.ReadBool(ref i);
                        if (connection.isOutput)
                        {
                            gate.output.Add(connection);
                            gate.amtOutputs++;
                        }
                        else
                        {
                            gate.input.Add(connection);
                            gate.amtInputs++;
                            if (!connection.empty)
                                connection.Point = pointList[bytes.ReadInt(ref i)];
                        }
                        if (!connection.empty)
                        {
                            connectionList.Add(connection);
                            int connectionCount = bytes.ReadInt(ref i);
                            for (int k = 0; k < connectionCount; k++)
                            {
                                connection.OtherConnections.Add(null);
                                refList.Add(bytes.ReadInt(ref i));
                            }
                        }
                    }
                    gates.Add(gate);
                }

                int relativeIndex = 0;
                foreach (Gate gate in gates)
                    foreach (var conns in gate.Connections)
                        if (!conns.empty)
                            for (int x = 0; x < conns.OtherConnections.Count; x++)
                                conns.OtherConnections[x] = connectionList[refList[relativeIndex++]];

                ClearGates();
                foreach (Gate gate in gates)
                    AddGate(gate);
                foreach (Gate gate in selected)
                    Select(gate);
                return true;
            }
            catch (Exception ex)
            {
                MsgBox(
                    (bytes[0] == Version ? ex.GetType().Name : $"Unsupported File Format ({bytes[0]})")
                        + "\r\n" + ex.StackTrace,
                    "File Parsing Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return false;
            }
        }
        Gate ImportState(List<byte> bytes)
        {
            int i = 1;
            int skipOverPoints = bytes.ReadInt(ref i);
            i += 2 * 4 * skipOverPoints;

            int amtInputs = bytes.ReadInt(ref i);
            int amtOutputs = bytes.ReadInt(ref i);
            if (amtInputs == 0 || amtOutputs == 0) return null;

            Gate gate = new Gate(0, 0, Gate.Type.Custom);

            gate.amtInputs = amtInputs;
            gate.amtOutputs = amtOutputs;

            gate.nameList = new string[amtInputs + amtOutputs];
            gate.truthTable = new bool[(1 << amtInputs) * amtOutputs];

            for (int j = 0; j < amtInputs + amtOutputs; j++)
            {
                gate.nameList[j] = bytes.ReadString(ref i);
            }
            for (int j = 0; j < gate.truthTable.Length; j++)
            {
                gate.truthTable[j] = bytes.ReadBool(ref i);
            }
            return gate;
        }

        bool[] GenerateTruthTable()
        {
            List<Gate> inputGates = GetSorted(Gate.Type.Switch);
            List<Gate> outputGates = GetSorted(Gate.Type.Light);

            bool[] stateList = gates.Select(c => c.state).ToArray();

            int p = 1 << inputGates.Count;
            bool[] table = new bool[p * outputGates.Count];

            for (int i = 0; i < outputGates.Count; i++)
                for (int j = 0; j < p; j++)
                {
                    ResetGates();
                    for (int k = 0; k < inputGates.Count; k++)
                        inputGates[k].state = (j >> k & 1) != 0;
                    table[i * p + j] = outputGates[i].input[0].Level;
                }

            for (int i = 0; i < stateList.Length; i++)
                gates[i].state = stateList[i];

            return table;
        }

        bool SavePromt()
        {
            if (!saved)
                switch (MsgBox(
                    "Would you like to save your changes?",
                    "Save?",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                ))
                {
                case DialogResult.Yes:
                    Save(); return saved;
                case DialogResult.No:
                    return true;
                case DialogResult.Cancel:
                    return false;
                }
            return true;
        }
        void SaveAs()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
                Save();
            }
        }
        void Save()
        {
            if (filePath == null)
            {
                SaveAs(); return;
            }
            using (Stream stream = File.Open(filePath, FileMode.Create))
            using (StreamWriter writer = new StreamWriter(stream))
                foreach (var b in GenerateState())
                    writer.Write((char) b);
            saved = true; UpdateTitle();
        }
        List<byte> Read(string filePath)
        {
            List<byte> buffer = new List<byte>();
            using (Stream stream = File.Open(filePath, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
                while (!reader.EndOfStream)
                    buffer.Add((byte) reader.Read());
            return buffer;
        }
        bool OpenFile(string filePath)
        {
            if (LoadState(Read(filePath)))
            {
                this.filePath = filePath;
                saved = true;
                UpdateTitle();
                UpdateButtonVisibility();
                return true;
            }
            return false;
        }
        void Open()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                saved = true;

                LoadState(Read(openFileDialog.FileName));
                ClearSelection();
            }
            UpdateTitle();
        }
        Gate Import()
        {
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = Filter;
            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return ImportState(Read(openFileDialog.FileName));
            return null;
        }
        #endregion

        #region Util
        void TakeSnapshot(string why, List<byte> state = null)
        {
            saved = false; UpdateTitle();
            undoBuffer[Mod(undoIndex++, MaxUndoBufferSize)] = state ?? GenerateState();
            redoIndex++;
            if (undoIndex > redoIndex)
            {
                undoBufferUsed -= undoIndex - redoIndex;
                undoIndex = redoIndex;
            }
            if (undoBufferUsed < MaxUndoBufferSize - 1)
            {
                undoBufferUsed++;
            }
        }

        bool AllowUndo()
        {
            return redoIndex > undoIndex - undoBufferUsed;
        }
        bool AllowRedo()
        {
            return redoIndex < undoIndex;
        }

        void UpdateTitle()
        {
            var fileName = filePath?.Split('\\').Last();
            Text = Title + " - " + (fileName != null && fileName.Length > 0 ? fileName : "Untitled") + (saved ? "" : "*");
        }
        void UpdateButtonVisibility(Gate clicked = null)
        {
            bool gateClicked = clicked != null;
            bool connClicked = clickedConnection != null;

            contextMenu_Add.Visible = !AnySelected;
            contextMenu_Type.Visible = AnySelected;
            contextMenu_AddInput.Visible = gateClicked && (clicked.amtInputs < clicked.maxInputs || clicked.maxInputs < 0);
            contextMenu_Invert.Visible = connClicked;
            contextMenu_RemoveConn.Visible = CanRemoveConnection;
            contextMenu_Rename.Visible = gateClicked;
            contextMenu_Resize.Visible = gateClicked;
            contextMenu_Trim.Visible = gateClicked;
            contextMenu_Toggle.Visible = ButtonSelected;
            contextMenu_Copy.Visible = AnySelected;
            contextMenu_Paste.Visible = AnyCopied;
            contextMenu_Delete.Visible = AnySelected || connClicked;
            contextMenu_Delete.Text = gateClicked ? "Delete" : "Clear";
            contextMenu_Merge.Visible = AnySelected;

            toolStrip_Copy.Enabled = AnySelected;
            toolStrip_Delete.Enabled = AnySelected || connClicked;
            toolStrip_Paste.Enabled = AnyCopied;
            toolStrip_Undo.Enabled = AllowUndo();
            toolStrip_Redo.Enabled = AllowRedo();
            toolStrip.Refresh();
        }

        static DialogResult MsgBox(
            string text, string caption = "",
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1
        )
        {
            msgboxActive = true;
            DialogResult result = MessageBox.Show(text, caption, buttons, icon, defaultButton);
            msgboxActive = false;
            return result;
        }

        static int Mod(int a, int b)
        {
            return a - a / b * b;
        }

        static void WriteName(List<byte> list, string name)
        {
            if (name == null)
                list.Add(0);
            else
            {
                list.Add((byte) name.Length);
                foreach (char letter in name)
                    list.Add((byte) letter);
            }
        }
        #endregion

        #region Gate Util
        void LoadGateList()
        {
            var gateList = new[]
            {
                new { type = Gate.Type.Custom,           str = "Custom Gate"                },
                new { type = Gate.Type.Switch,           str = "Input Switch"               },
                new { type = Gate.Type.Light,            str = "Output Light"               },
                new { type = Gate.Type.RedGreenLight,    str = "Red, Green Light"           },
                new { type = Gate.Type.SegmentDisplay,   str = "7 Segment Display"          },
                null,
                new { type = Gate.Type.And,              str = "And Gate"                   },
                new { type = Gate.Type.Or,               str = "Or Gate"                    },
                new { type = Gate.Type.Xor,              str = "Xor Gate"                   },
                new { type = Gate.Type.Nand,             str = "Nand Gate"                  },
                new { type = Gate.Type.Nor,              str = "Nor Gate"                   },
                new { type = Gate.Type.Xnor,             str = "Xnor Gate"                  },
                new { type = Gate.Type.Not,              str = "Not Gate"                   },
                null,
                new { type = Gate.Type.Clock,            str = "1/2 Hz Clock"               },
                new { type = Gate.Type.RisingEdgePulse,  str = "Rising Edge Pulse"          },
                new { type = Gate.Type.FallingEdgePulse, str = "Falling Edge Pulse"         },
                new { type = Gate.Type.EdgePulse,        str = "Rising+Falling Edge Pulse"  },
                null,
                new { type = Gate.Type.RSFlipFlop,       str = "RS Flip Flop"               },
                new { type = Gate.Type.ClockRSFlipFlop,  str = "RS Flip Flop (Clock Input)" },
                new { type = Gate.Type.JKFlipFlop,       str = "JK Flip Flop"               },
                new { type = Gate.Type.DFlipFlop,        str = "D Flip Flop"                },
                new { type = Gate.Type.TFlipFlop,        str = "T Flip Flop"                },
            };

            foreach (var a in gateList)
            {
                if (a == null)
                {
                    var seperator = new ToolStripSeparator();
                    seperator.Height = 100;
                    //seperator.ForeColor = Color.Black;
                    contextMenu_Add.DropDownItems.Add(seperator);
                    contextMenu_Type.DropDownItems.Add(new ToolStripSeparator());
                    continue;
                }
                contextMenu_Add.DropDownItems.Add(a.str, null, NewGateEvent(a.type));
                contextMenu_Type.DropDownItems.Add(a.str, null, NewGateEvent(a.type));
            }
        }
        public EventHandler NewGateEvent(Gate.Type type)
        {
            return (sender, e) =>
            {
                if (clickedConnection != null && selected.Contains(clickedConnection.self))
                    clickedConnection = null;
                TakeSnapshot("New Gate");
                if (selected.Count == 0)
                {
                    var newGate = new Gate(lastClick.X, lastClick.Y, type);
                    AddGate(newGate);
                    Select(newGate);
                }
                foreach (var gate in selected)
                {
                    gate.ChangeTo(type, type == Gate.Type.Custom ? Import() : null);
                }
            };
        }

        void ResetAll()
        {
            selecting = false;
            objectMoved = false;

            saved = true;
            filePath = null;
            clickedConnection = null;
            undoIndex = redoIndex = undoBufferUsed = 0;

            ClearGates();
            ResetView();
            UpdateTitle();
            UpdateButtonVisibility();
        }
        void ClearGates()
        {
            ClearClipboard();
            ClearConnection();
            ClearSelection();
            clipboard.Clear();
            gates.Clear();
        }
        void ResetView()
        {
            offsetX = offsetY = 0f;
            scale = GridSize;
        }
        void ResetGates()
        {
            gates.ForEach(gate => gate.eval = false);
            ResetCablePoints();
        }
        void ResetCablePoints()
        {
            foreach (var gate in gates)
                foreach (var conn in gate.input)
                    if (!conn.empty) conn.Point.Reset();
        }

        void Zoom(bool delta, bool centered = false)
        {
            float x = lastPos.X, y = lastPos.Y;
            if (centered)
            {
                PointF center = Translated(pictureBox.Width / 2, pictureBox.Height / 2);
                x = center.X; y = center.Y;
            }
            float zoom = delta ? 1f / ScaleFactor : ScaleFactor;
            offsetX += scale * -x * (zoom - 1f);
            offsetY += scale * -y * (zoom - 1f);
            scale *= zoom;
        }
        void ZoomIntoView(List<Gate> gates)
        {
            if (gates.Count > 0)
            {
                RectangleF bounds = BoundsOf(gates);
                offsetX = (pictureBox.Width - bounds.Width) / 2f - bounds.X;
                offsetY = (pictureBox.Height - bounds.Height) / 2f - bounds.Y;
            }
        }
        RectangleF BoundsOf(List<Gate> gates)
        {
            float minX = gates.Min(gate => gate.x);
            float minY = gates.Min(gate => gate.y);
            float maxX = gates.Max(gate => gate.x + gate.w);
            float maxY = gates.Max(gate => gate.y + gate.h);
            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }

        void AddGate(Gate gate)
        {
            gates.Add(gate);
        }
        void Select(Gate gate)
        {
            if (!gate.isSelected)
            {
                gate.isSelected = true;
                selected.Add(gate);
                UpdateButtonVisibility();
            }
        }
        void Select(List<Gate> gates)
        {
            foreach (Gate gate in gates)
                Select(gate);
        }
        void ClearSelection()
        {
            foreach (Gate gate in selected)
                gate.isSelected = false;
            selected.Clear();

            UpdateButtonVisibility();
        }
        List<Gate> GatesIn(float x0, float y0, float x1, float y1)
        {
            return gates.Where(gate => gate.IncludedIn(x0, y0, x1, y1)).ToList();
        }

        void ToggleButtons()
        {
            bool any = false;
            foreach (Gate gate in selected)
                if (gate.type == Gate.Type.Switch)
                    gate.state = !gate.state;
        }

        int AmtInputs()
        {
            return gates.Count(g => g.type == Gate.Type.Switch);
        }
        int AmtOutputs()
        {
            return gates.Count(g => g.type == Gate.Type.Light);
        }
        List<Gate> GetSorted(Gate.Type type)
        {
            return gates.Where(g => g.type == type).OrderBy(g => g.y).ToList();
        }

        void ClearConnection()
        {
            clickedConnection?.Clear();
            clickedConnection = null;
        }
        void DeleteSelected()
        {
            bool any = AnySelected;
            foreach (var clicked in selected)
            {
                if (clickedConnection != null && clickedConnection.self == clicked)
                    clickedConnection = null;
                gates.Remove(clicked);
                clicked.ResetAllConnections();
            }
            ClearSelection();
        }
        void ClearClipboard()
        {
            clipboard.Clear();
            UpdateButtonVisibility();
        }
        void AddSelectedToClipboard()
        {
            foreach (var gate in selected)
                clipboard.Add(gate.Copy());
            UpdateButtonVisibility();
        }
        void PasteFromClipboard()
        {
            var bounds = BoundsOf(clipboard);
            ClearSelection();
            foreach (var gate in clipboard)
            {
                var newGate = gate.Copy();
                newGate.Move(lastPos.X - bounds.X, lastPos.Y - bounds.Y);
                newGate.SnapToGrid();
                Select(newGate);
                AddGate(newGate);
            }
        }

        PointF Translated(PointF point)
        {
            return Translated(point.X, point.Y);
        }
        PointF Translated(float x, float y)
        {
            return new PointF(
                (x - offsetX) / scale,
                (y - offsetY) / scale
            );
        }
        #endregion

        #region Graphics
        void UpdateScreen()
        {
            if (!msgboxActive) pictureBox.Invalidate();
        }
        void DrawScreen(Graphics graphics)
        {
            ResetGates();
            DrawGrid(graphics);
            graphics.Transform = new Matrix(scale, 0, 0, scale, offsetX, offsetY);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            foreach (CablePoint point in Points)
                point.Draw(false, graphics);
            foreach (Gate gate in gates)
                gate.Draw(graphics);

            foreach (var gate in selected)
                DrawAsSelected(gate, graphics);

            if (selecting)
                DrawSelection(lastClick.X, lastClick.Y, lastPos.X, lastPos.Y, graphics);

            if (clickedConnection != null)
            {
                DrawConnection(clickedConnection, graphics);
                if (!clickedConnection.empty)
                    if (clickedConnection.isOutput)
                    {
                        foreach (Connection c in clickedConnection.OtherConnections)
                        {
                            DrawConnection(c, graphics, true);
                            c.Point.Draw(true, graphics);
                        }
                    }
                    else
                    {
                        clickedConnection.Point.Draw(true, graphics);
                        DrawConnection(clickedConnection.OtherConnection, graphics, true);
                    }
            }
        }

        void DrawGrid(Graphics graphics)
        {
            for (float y = offsetY % scale; y < pictureBox.Height; y += scale)
            {
                graphics.DrawLine(new Pen(Theme.GridDark, PenWidth), 0, y - 0.5f, pictureBox.Width, y - 0.5f);
                graphics.DrawLine(new Pen(Theme.GridLight, PenWidth), 0, y + 0.5f, pictureBox.Width, y + 0.5f);
            }
            for (float x = offsetX % scale; x < pictureBox.Width; x += scale)
            {
                graphics.DrawLine(new Pen(Theme.GridDark, PenWidth), x - 0.5f, 0, x - 0.5f, pictureBox.Height);
                graphics.DrawLine(new Pen(Theme.GridLight, PenWidth), x + 0.5f, 0, x + 0.5f, pictureBox.Height);
            }
        }
        void DrawSelection(float x0, float y0, float x1, float y1, Graphics graphics)
        {
            graphics.FillRectangle(
                new SolidBrush(Theme.SelectionLight),
                Math.Min(x0, x1), Math.Min(y0, y1),
                Math.Abs(x1 - x0), Math.Abs(y1 - y0)
            );
            graphics.DrawRectangle(
                new Pen(Theme.SelectionColorFill, 2f / scale),
                Math.Min(x0, x1), Math.Min(y0, y1),
                Math.Abs(x1 - x0), Math.Abs(y1 - y0)
            );
        }
        void DrawConnection(Connection conn, Graphics graphics, bool preview = false)
        {
            int dir = conn.isOutput ? 1 : -1;
            float w = 0.7f, h = 0.5f;
            GraphicsPath path = new GraphicsPath();
            path.AddLine(
                conn.Position.x, conn.Position.y,
                conn.Position.x + dir * w, conn.Position.y + 0.5f
            );
            path.AddLine(
                conn.Position.x + dir * w, conn.Position.y + h,
                conn.Position.x + dir * w, conn.Position.y - h
            );
            path.CloseFigure();

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (!preview)
                graphics.FillPath(new SolidBrush(Theme.SelectionLight), path);
            graphics.DrawPath(new Pen(preview ? Theme.SelectionDim : Theme.ConnectionColor, 2 * PenWidth), path);
            if (preview)
                graphics.DrawPath(new Pen(Theme.SelectionDim, PenWidth), path);
            graphics.SmoothingMode = SmoothingMode.None;
        }
        void DrawAsSelected(Gate gate, Graphics graphics)
        {
            Pen pen = new Pen(Theme.GridLight, 2 * PenWidth);
            pen.DashOffset = DateTime.Now.Millisecond / 1000f * GridSize;
            pen.DashPattern = new[] { GridSize / 2f, GridSize / 2f };

            GraphicsPath path = new GraphicsPath();
            path.AddLine(gate.x + gate.w / 2, gate.y, gate.x + gate.w, gate.y);
            path.AddLine(gate.x + gate.w, gate.y, gate.x + gate.w, gate.y + gate.h);
            path.AddLine(gate.x + gate.w, gate.y + gate.h, gate.x, gate.y + gate.h);
            path.AddLine(gate.x, gate.y + gate.h, gate.x, gate.y);
            path.AddLine(gate.x, gate.y, gate.x + gate.w / 2, gate.y);
            graphics.DrawPath(pen, path);
        }
        #endregion

        #region Events
        void timer_Tick(object sender, EventArgs e)
        {
            UpdateScreen();
        }
        void FocusPictureBox(object sender, EventArgs e)
        {
            pictureBox.Focus();
        }
        void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SavePromt()) e.Cancel = true;
        }

        void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            Zoom(e.Delta > 0);
        }
        void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
            pictureBox.Focus();
            lastPos = lastClick = Translated(e.Location);
            lastLocation = e.Location;
            var pointClicked = Gate.NearestPoint(Points, ConnectionClickRange, lastPos.X, lastPos.Y);
            var connection = Gate.NearestConnection(gates, ConnectionClickRange, lastPos.X, lastPos.Y);
            var clicked = gates.Find(gate => gate.IncludesPos(lastClick.X, lastClick.Y));
            if (pointClicked != null && connection != null)
                if (pointClicked.Dist(lastClick.X, lastClick.Y) < 0.1f * connection.Position.Dist(lastClick.X, lastClick.Y))
                    connection = null;
                else
                    pointClicked = null;

            clickState = GenerateState();

            if (MouseButtons != MouseButtons.Middle && !(e.Button == MouseButtons.Left && ModifierKeys == Keys.Alt))
            {
                if (ModifierKeys != Keys.Control && (clicked == null || !clicked.isSelected))
                {
                    if (AnySelected) TakeSnapshot("Selected Cleared 1 (MouseDown)");
                    ClearSelection();
                }
                if (ModifierKeys == Keys.Shift)
                {
                    selecting = true;
                }
                else if (clicked != null && ModifierKeys != Keys.Shift)
                {
                    if (!clicked.isSelected)
                    {
                        TakeSnapshot("Gate Selected (MouseDown)");
                        Select(clicked);
                    }
                    else if (ModifierKeys == Keys.Control)
                    {
                        TakeSnapshot("Gate Unselected (MouseDown)");
                        clicked.isSelected = false;
                        selected.Remove(clicked);
                    }
                }
                else if (pointClicked != null)
                {
                    if (AnySelected) TakeSnapshot("Selection cleared (MouseDown)");
                    this.pointClicked = pointClicked;
                    ClearSelection();
                }
                else if (connection != null && MouseButtons == MouseButtons.Left)
                {
                    if (Gate.ValidConnection(connection, clickedConnection))
                    {
                        Gate.AddConnection(connection, clickedConnection);
                        if (ModifierKeys != Keys.Control) clickedConnection = null;
                        TakeSnapshot("Connection Created");
                    }
                    else if (Gate.ValidConnection(clickedConnection, connection))
                    {
                        Gate.AddConnection(clickedConnection, connection);
                        if (ModifierKeys != Keys.Control) clickedConnection = null;
                        TakeSnapshot("Connection Created");
                    }
                    else if (clickedConnection == connection)
                    {
                        clickedConnection = null;
                        TakeSnapshot("Clicked Connection swapped");
                    }
                    else
                    {
                        clickedConnection = connection;
                    }
                }
                else if (connection != null)
                {
                    clickedConnection = connection;
                }
                else
                {
                    clickedConnection = null;
                    selecting = true;
                }
                UpdateButtonVisibility(clicked);
            }
        }
        void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PointF pos = Translated(e.Location);
            float dx = pos.X - lastPos.X;
            float dy = pos.Y - lastPos.Y;

            if (e.Button == MouseButtons.Left && (selecting || AnySelected || pointClicked != null))
            {
                offsetX -= Math.Sign(Math.Floor(lastLocation.X / (pictureBox.Width - 4f))) * 5f;
                offsetY -= Math.Sign(Math.Floor(lastLocation.Y / (pictureBox.Height - 4f))) * 5f;
            }

            if (!selecting && e.Button == MouseButtons.Middle
                || e.Button == MouseButtons.Left && ModifierKeys == Keys.Alt)
            {
                offsetX += e.Location.X - lastLocation.X;
                offsetY += e.Location.Y - lastLocation.Y;
            }
            else if (!selecting && e.Button == MouseButtons.Left && mousedown)
            {
                if (AnySelected)
                {
                    ResetCablePoints();
                    foreach (var gate in selected)
                    {
                        gate.Move(dx, dy); objectMoved = true;
                    }
                }
                else if (pointClicked != null)
                {
                    pointClicked.Move(dx, dy); objectMoved = true;
                }
            }
            lastPos = pos;
            lastLocation = e.Location;
        }
        void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
            if (selecting)
            {
                selecting = false;
                var gates = GatesIn(lastClick.X, lastClick.Y, lastPos.X, lastPos.Y);
                if (gates.Count > 0)
                    TakeSnapshot("Gates Selected (MouseUp)");
                foreach (var gate in gates)
                    Select(gate);
            }
            if (pointClicked != null || AnySelected)
            {
                if (objectMoved)
                {
                    TakeSnapshot("Gate Moved (MouseUp)", clickState); objectMoved = false;
                }
                if (pointClicked != null)
                {
                    pointClicked.SnapToGrid();
                    pointClicked = null;
                }
                if (AnySelected)
                    foreach (var gate in selected)
                        gate.SnapToGrid();
            }
        }
        void pictureBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (AnySelected) TakeSnapshot("Selection Cleared");
                ClearSelection();
                clickedConnection = null;
            }
            else if (e.KeyCode == Keys.Space)
            {
                ToggleButtons();
                if (ButtonSelected) TakeSnapshot("Buttons Toggled");
            }
        }
        void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (AnySelected) TakeSnapshot("Cable Reset");
            foreach (var gate in selected)
            {
                gate.ResetCable();
                gate.TrimAllConnections();
            }
            ToggleButtons();
        }

        void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                DrawScreen(e.Graphics);
            }
            catch (Exception ex)
            {
                MsgBox(
                    ex.GetType().Name + "\r\n" + ex.StackTrace,
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                Environment.Exit(0);
            }
        }
        void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ?
                e.AllowedEffect : DragDropEffects.None;
        }
        void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string filePath in (string[]) e.Data.GetData(DataFormats.FileDrop))
                if (SavePromt())
                    OpenFile(filePath);
        }

        void menuStrip_File_New_Click(object sender, EventArgs e)
        {
            if (SavePromt())
                ResetAll();
        }
        void menuStrip_File_Open_Click(object sender, EventArgs e)
        {
            if (SavePromt()) Open();
        }
        void menuStrip_File_Import_Click(object sender, EventArgs e)
        {
            var imported = Import();
            if (imported != null)
            {
                var gate = new Gate(0f, 0f, Gate.Type.Custom, imported);
                AddGate(gate);
            }
        }
        void menuStrip_File_Save_Click(object sender, EventArgs e)
        {
            Save();
        }
        void menuStrip_File_SaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        void menuStrip_Edit_Undo_Click(object sender, EventArgs e)
        {
            if (AllowUndo())
            {
                if (undoIndex == redoIndex)
                    undoBuffer[Mod(redoIndex, MaxUndoBufferSize)] = GenerateState();
                LoadState(undoBuffer[Mod(--redoIndex, MaxUndoBufferSize)]);
            }
            UpdateButtonVisibility();
        }
        void menuStrip_Edit_Redo_Click(object sender, EventArgs e)
        {
            if (AllowRedo())
            {
                LoadState(undoBuffer[Mod(++redoIndex, MaxUndoBufferSize)]);
            }
            UpdateButtonVisibility();
        }
        void menuStrip_Edit_Copy_Click(object sender, EventArgs e)
        {
            if (AnySelected)
            {
                ClearClipboard();
                AddSelectedToClipboard();
            }
        }
        void menuStrip_Edit_Delete_Click(object sender, EventArgs e)
        {
            if (AnySelected)
            {
                if (AnySelected) TakeSnapshot("Deleted Gates");
                DeleteSelected();
            }
            else if (clickedConnection != null)
            {
                TakeSnapshot("Selected Cleared");
                ClearConnection();
            }
        }
        void menuStrip_Edit_Paste_Click(object sender, EventArgs e)
        {
            if (AnyCopied)
            {
                TakeSnapshot("Pasted from Clipboard");
                PasteFromClipboard();
            }
        }
        void menuStrip_Edit_SelectAll_Click(object sender, EventArgs e)
        {
            if (gates.Any(gate => !gate.isSelected)) TakeSnapshot("Selected All");
            foreach (var gate in gates)
                if (!gate.isSelected)
                    Select(gate);
        }

        void menuStrip_View_Reset_Click(object sender, EventArgs e)
        {
            ResetView();
        }
        void menuStrip_View_ZoomIn_Click(object sender, EventArgs e)
        {
            Zoom(true, true);
        }
        void menuStrip_View_ZoomOut_Click(object sender, EventArgs e)
        {
            Zoom(false, true);
        }

        void menuStrip_Tools_Clear_Click(object sender, EventArgs e)
        {
            if (gates.Count > 0) TakeSnapshot("Gates Cleared");
            gates.Clear();
        }
        void menuStrip_Tools_Reload_Click(object sender, EventArgs e)
        {
            LoadState(GenerateState());
        }
        void menuStrip_Tools_AddInput_Click(object sender, EventArgs e)
        {
            TakeSnapshot("Empty Input");
            foreach (Gate gate in selected)
                if (gate.input.Count < gate.maxInputs || gate.maxInputs == -1)
                    gate.AddEmptyInput();
        }
        void menuStrip_Tools_RemoveConn_Click(object sender, EventArgs e)
        {
            if (clickedConnection != null)
            {
                var gate = clickedConnection.self;
                if (CanRemoveConnection)
                {
                    TakeSnapshot("Connection Removed");
                    clickedConnection.Clear();
                    gate.input.Remove(clickedConnection);
                    clickedConnection = null;
                    gate.amtInputs--;
                    for (int i = 0; i < gate.amtInputs; i++)
                        gate.input[i].index = i;
                }
            }
        }
        void menuStrip_Tools_Invert_Click(object sender, EventArgs e)
        {
            TakeSnapshot("Inverted Connection");
            if (clickedConnection != null)
            {
                clickedConnection.inverted = !clickedConnection.inverted;
            }
        }
        void menuStrip_Tools_Rename_Click(object sender, EventArgs e)
        {
            if (AnySelected)
            {
                TakeSnapshot("Renamed");
                Popup rename = new Popup();
                rename.StartPosition = FormStartPosition.Manual;
                rename.Left = Cursor.Position.X;
                rename.Top = Cursor.Position.Y;
                rename.Show();
                rename.Set(selected, Popup.Mode.Name);
            }
        }
        void menuStrip_Tools_Resize_Click(object sender, EventArgs e)
        {
            if (AnySelected)
            {
                TakeSnapshot("Resized");
                Popup rename = new Popup();
                rename.StartPosition = FormStartPosition.Manual;
                rename.Left = Cursor.Position.X;
                rename.Top = Cursor.Position.Y;
                rename.Show();
                rename.Set(selected, Popup.Mode.Size);
            }
        }
        void menuStrip_Tools_Toggle_Click(object sender, EventArgs e)
        {
            if (ButtonSelected) TakeSnapshot("Buttons Toggled");
            ToggleButtons();
        }
        void menuStrip_Tools_TrimInput_Click(object sender, EventArgs e)
        {
            if (AnySelected)
                TakeSnapshot("Input Trimmed");
            foreach (var gate in selected)
                gate.TrimAllConnections();
        }
        void menuStrip_Tools_Merge_Click(object sender, EventArgs e)
        {
            TakeSnapshot("Outputs Merged");
            foreach (var gate in selected)
                foreach (var conns in gate.output)
                    foreach (var conn in conns.OtherConnections)
                    {
                        var point = gate.output[0].OtherConnection.Point;
                        if (point != conn.Point)
                        {
                            conn.Point.x = point.x;
                            conn.Point.y = point.y;
                        }
                    }
        }
        #endregion
    }

    public static class Extensions
    {
        public static void AddInt(this List<byte> list, int value)
        {
            list.AddByte(value >> 8);
            list.AddByte(value);
        }
        public static void AddByte(this List<byte> list, int value)
        {
            list.Add((byte) (value & 0xFF));
        }
        public static void AddBool(this List<byte> list, bool value)
        {
            list.AddByte(value ? 1 : 0);
        }
        public static void AddFloat(this List<byte> list, float value)
        {
            list.AddRange(BitConverter.GetBytes(value));
        }
        public static void AddString(this List<byte> list, string value)
        {
            if (value == null)
                list.AddInt(0);
            else
            {
                list.AddInt(value.Length);
                foreach (char letter in value)
                    list.AddByte(letter);
            }
        }

        public static int ReadInt(this List<byte> list, ref int index)
        {
            return list.ReadByte(ref index) << 8 | list.ReadByte(ref index);
        }
        public static int ReadByte(this List<byte> list, ref int index)
        {
            return list[index++];
        }
        public static bool ReadBool(this List<byte> list, ref int index)
        {
            return list.ReadByte(ref index) != 0;
        }
        public static float ReadFloat(this List<byte> list, ref int index)
        {
            return BitConverter.ToSingle(new[] { list[index++], list[index++], list[index++], list[index++] }, 0);
        }
        public static string ReadString(this List<byte> list, ref int index)
        {
            string str = "";
            int len = list.ReadInt(ref index);
            for (int i = 0; i < len; i++)
                str += (char) list.ReadByte(ref index);
            return str;
        }
    }

    public class CablePoint
    {
        public float x, y;
        public bool movedLeft, movedRight;
        const float size = 1/3f;

        public CablePoint(float x, float y)
        {
            this.x = x; this.y = y;
        }

        public float Dist(float x, float y)
        {
            return (this.x - x) * (this.x - x) + (this.y - y) * (this.y - y);
        }
        public void Reset()
        {
            movedLeft = movedRight = false;
        }
        public void Draw(bool highlighted, Graphics graphics)
        {
            Pen pen = new Pen(
                highlighted ? Theme.ActiveCable : Theme.GridDark,
                (highlighted ? 2 : 1) * Window.PenWidth
            );
            graphics.DrawLine(pen, x - size, y - size, x + size, y + size);
            graphics.DrawLine(pen, x + size, y - size, x - size, y + size);
        }
        public void Move(float x, float y)
        {
            this.x += x; this.y += y;
        }
        public void SnapToGrid()
        {
            x = (float) Math.Round(x / 0.5f) * 0.5f;
            y = (float) Math.Round(y / 0.5f) * 0.5f;
        }
    }

    public class Theme {
        public static Color Transparent        { get { return Color.FromArgb(  0, 0x00, 0x00, 0x00); } }
        public static Color MainColor          { get { return Color.FromArgb(255, 0x00, 0x00, 0x00); } }
        public static Color BackColor          { get { return Color.FromArgb(255, 0xF0, 0xF0, 0xF0); } }
        public static Color LightBackColor     { get { return Color.FromArgb( 45, 0xF0, 0xF0, 0xF0); } }
        public static Color Red                { get { return Color.FromArgb(128, 0xE1, 0x00, 0x3A); } }
        public static Color Glow               { get { return Color.FromArgb(192, 0xE1, 0x1A, 0x3A); } }
        public static Color Green              { get { return Color.FromArgb(128, 0x4E, 0xFF, 0x62); } }
        public static Color GridDark           { get { return Color.FromArgb(255, 0xC1, 0xC1, 0xC8); } }
        public static Color GridLight          { get { return Color.FromArgb(255, 0xB4, 0xB4, 0xB8); } }
        public static Color SelectionDim       { get { return Color.FromArgb( 32, 0x00, 0x00, 0x0F); } }
        public static Color SelectionLight     { get { return Color.FromArgb( 64, 0x33, 0x99, 0xAA); } }
        public static Color SelectionColorFill { get { return Color.FromArgb(128, 0x33, 0x99, 0xFF); } }
        public static Color SelectionColorDark { get { return Color.FromArgb(192, 0x22, 0x55, 0xFF); } }
        public static Color ActiveCable        { get { return Color.FromArgb(255, 0xE1, 0x1A, 0x3A); } }
        public static Color ConnectionColor    { get { return Color.FromArgb(160, 0x0D, 0x1C, 0xC4); } }
    }
}
