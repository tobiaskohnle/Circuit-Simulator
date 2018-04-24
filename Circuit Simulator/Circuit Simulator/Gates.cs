using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Circuit_Simulator
{
    public class Connection
    {
        public List<Connection> connections = new List<Connection>();
        public CablePoint point;
        public Gate self;
        public bool empty, inverted, isOutput;
        public int amtOtherConnections, index;

        public Connection OtherConnection
        {
            get {
                return connections[0];
            }
            set {
                if (connections.Count == 0)
                    connections.Add(value);
                else
                    connections[0] = value;
            }
        }
        public List<Connection> OtherConnections
        {
            get {
                return connections;
            }
            set {
                connections = value;
            }
        }
        public CablePoint Point
        {
            get {
                return point;
            }
            set {
                point = value;
            }
        }
        public CablePoint Position
        {
            get {
                return new CablePoint(
                    self.x + (isOutput ? self.w : 0),
                    self.y + self.h * (1 + 2 * index)
                    / (2 * (isOutput ? self.amtOutputs : self.amtInputs))
                );
            }
        }

        public Connection(Gate self, int index, bool inverted, bool isOutput)
        {
            empty = true;
            this.self = self;
            this.index = index;
            this.inverted = inverted;
            this.isOutput = isOutput;
        }

        public bool Level
        {
            get {
                return inverted != (!empty && OtherConnection.inverted
                    != OtherConnection.self.Level(OtherConnection.index));
            }
        }

        public void ConnectTo(Connection connection)
        {
            if (connection.empty)
                connection.OtherConnections.Clear();
            connection.OtherConnections.Add(this);
            OtherConnection = connection;
            connection.empty = empty = false;
        }
        public void Clear()
        {
            if (isOutput && !empty)
            {
                foreach (Connection c in OtherConnections.ToList())
                    c.Clear();
            }
            else if (!empty)
            {
                point = null;
                OtherConnection.OtherConnections.Remove(this);
                OtherConnection.empty = OtherConnection.OtherConnections.Count == 0;
            }
            empty = true;
        }

        public void Draw(Graphics graphics)
        {
            bool level = isOutput ?
                inverted != self.Level(index) :
                !empty && OtherConnection.inverted != OtherConnection.self.Level(OtherConnection.index);
            var pos = Position;
            int dir = isOutput ? 1 : -1;
            Pen pen = new Pen(level ? Theme.ActiveCable : Theme.MainColor, 2 * Window.PenWidth);
            graphics.DrawLine(pen, pos.x + (inverted ? dir * (0.5f + Window.PenWidth) : 0), pos.y, pos.x + dir, pos.y);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (inverted)
            {
                pen.Color = !level ? Theme.ActiveCable : Theme.MainColor;
                graphics.DrawEllipse(pen, pos.x + dir * 2 * Window.PenWidth, pos.y - 0.25f, dir * 0.5f, 0.5f);
            }
            graphics.SmoothingMode = SmoothingMode.None;
        }
    }

    public class Gate
    {
        #region Fields and Properties
        public string name;
        public Type type;

        public bool isSelected, state, togglestate, eval;
        public int minInputs, maxInputs, amtInputs, amtOutputs;
        public float x, y, w, h;

        public bool[] truthTable;
        public string[] nameList;

        public List<Connection> input = new List<Connection>();
        public List<Connection> output = new List<Connection>();

        public List<Connection> Connections
        {
            get {
                return input.Concat(output).ToList();
            }
        }
        #endregion

        public Gate()
        {
        }
        public Gate(float x, float y, Type type, Gate custom = null)
        {
            this.x = x; this.y = y;
            SnapToGrid();
            ChangeTo(type, custom);
        }

        public enum Type
        {
            Custom, Switch, Light,
            And, Or, Xor, Nand, Nor, Xnor, Not,
            RedGreenLight, SegmentDisplay,
            RisingEdgePulse, FallingEdgePulse, EdgePulse, Clock,
            RSFlipFlop, ClockRSFlipFlop, JKFlipFlop, DFlipFlop, TFlipFlop,
            JKMasterSlave
        }
        public string Tag
        {
            get {
                switch (type)
                {
                case Type.And: case Type.Nand: return "&";
                case Type.Or: case Type.Nor: return "\u22651";
                case Type.Xor: case Type.Xnor: return "=1";
                case Type.Not: return "1";
                default: return "";
                }
            }
        }
        public bool IsMutable
        {
            get {
                return type == Type.And
                    || type == Type.Or
                    || type == Type.Xor
                    || type == Type.Nand
                    || type == Type.Nor
                    || type == Type.Xnor;
            }
        }

        public static bool ValidConnection(Connection a, Connection b)
        {
            if (a == null) return false;
            if (b == null) return false;
            if (a == b) return false;
            if (a.self == b.self) return false;
            if (a.isOutput == b.isOutput) return false;
            if (a.isOutput) return false;
            if (!a.empty && a.OtherConnection == b) return false;
            if (b.self.Find(a.self)) return false;
            return true;
        }
        public static void AddConnection(Connection incoming, Connection outgoing)
        {
            incoming.Clear();
            incoming.ConnectTo(outgoing);
            incoming.Point = new CablePoint(
                (incoming.Position.x + outgoing.Position.x) / 2f,
                (incoming.Position.y + outgoing.Position.y) / 2f
            );
            incoming.Point.SnapToGrid();
        }
        public static Connection NearestConnection(List<Gate> gates, float maxDist, float x, float y)
        {
            float best = float.PositiveInfinity;
            Connection connection = null;

            foreach (var gate in gates)
                foreach (var conn in gate.Connections)
                {
                    var pos = conn.Position;
                    float dist = (pos.x - x) * (pos.x - x) + (pos.y - y) * (pos.y - y);
                    if (dist < best && dist < maxDist * maxDist)
                    {
                        best = dist; connection = conn;
                    }
                }
            return connection;
        }
        public static CablePoint NearestPoint(List<CablePoint> points, float maxDist, float x, float y)
        {
            float best = float.PositiveInfinity;
            CablePoint bestPoint = null;

            foreach (CablePoint point in points)
            {
                float dist = (point.x - x) * (point.x - x) + (point.y - y) * (point.y - y);
                if (dist < best && dist < maxDist * maxDist)
                {
                    best = dist; bestPoint = point;
                }
            }
            return bestPoint;
        }
        public void AddEmptyInput()
        {
            input.Add(new Connection(this, amtInputs++, false, false));
        }
        public void AddEmptyOutput()
        {
            output.Add(new Connection(this, amtOutputs++, false, true));
        }

        public void DrawCable(Connection conn, Graphics graphics)
        {
            if (conn.empty) return;

            Pen pen = new Pen(conn.inverted != conn.Level ? Theme.ActiveCable : Theme.MainColor, 2 * Window.PenWidth);
            pen.StartCap = pen.EndCap = LineCap.Triangle;

            var a = conn.Position; a.x -= 1f;
            var b = conn.OtherConnection.Position; b.x += 1f;
            var c = conn.Point;

            float x = a.x, y = a.y;
            bool vertical = a.x >= b.x;

            graphics.DrawLine(pen, x, y, vertical ? x = c.x : x, vertical ? y : y = c.y);
            graphics.DrawLine(pen, x, y, vertical ? x : x = b.x, vertical ? y = b.y : y);
            graphics.DrawLine(pen, x, y, b.x, b.y);
        }
        public void Draw(Graphics graphics)
        {
            float x0 = x + 0.5f, x1 = x + w - 0.5f;
            float y0 = y + 0.5f, y1 = y + h - 0.5f;
            float ym = y + h / 2f;
            Pen pen = new Pen(Theme.MainColor, 1.5f * Window.PenWidth);
            pen.StartCap = pen.EndCap = LineCap.Square;

            graphics.FillRectangle(new SolidBrush(Theme.LightBackColor), x, y, w, h);

            switch (type)
            {
            case Type.RSFlipFlop:
                nameList = new[] { "S", "R", "Q", "!Q" };
                goto case Type.Custom;
            case Type.ClockRSFlipFlop:
                nameList = new[] { "1S", "C1", "1R", "Q", "!Q" };
                goto case Type.Custom;
            case Type.JKFlipFlop:
                nameList = new[] { "1J", "C1", "1K", "Q", "!Q" };
                goto case Type.Custom;
            case Type.JKMasterSlave:
                nameList = new[] { "S", "1J", "C1", "1K", "R", "Q", "!Q" };
                goto case Type.Custom;
            case Type.DFlipFlop:
                nameList = new[] { "1D", "C1", "Q", "!Q" };
                goto case Type.Custom;
            case Type.TFlipFlop:
                nameList = new[] { "1T", "C1", "Q", "!Q" };
                goto case Type.Custom;
            case Type.RisingEdgePulse:
            case Type.FallingEdgePulse:
            case Type.EdgePulse:
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                bool up = type == Type.EdgePulse || type == Type.RisingEdgePulse;
                bool dn = type == Type.EdgePulse || type == Type.FallingEdgePulse;
                Pen arrow = new Pen(Theme.MainColor, 1.5f * Window.PenWidth);
                arrow.CustomEndCap = new AdjustableArrowCap(5f, 5f);
                graphics.DrawLine(pen, x, y1, x0, y1);
                graphics.DrawLine(pen, x0, y1, x0, y0);
                if (up) graphics.DrawLine(arrow, x0, y1, x0, ym - 3/20f);
                graphics.DrawLine(pen, x0, y0, x1, y0);
                graphics.DrawLine(pen, x1, y0, x1, y1);
                if (dn) graphics.DrawLine(arrow, x1, y0, x1, ym + 3/20f);
                graphics.DrawLine(pen, x1, y1, x + w, y1);
                graphics.SmoothingMode = SmoothingMode.None;
                break;
            case Type.Clock:
                GraphicsPath clockPath = new GraphicsPath();
                clockPath.AddLines(new[]
                {
                    new PointF(x + 0/6f * w, ym),
                    new PointF(x + 1/6f * w, ym),
                    new PointF(x + 1/6f * w, y0),
                    new PointF(x + 3/6f * w, y0),
                    new PointF(x + 3/6f * w, y1),
                    new PointF(x + 5/6f * w, y1),
                    new PointF(x + 5/6f * w, ym),
                    new PointF(x + 6/6f * w, ym),
                });
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawPath(pen, clockPath);
                graphics.SmoothingMode = SmoothingMode.None;
                break;
            case Type.Custom:
                int ix = 0;
                foreach (var conn in Connections)
                {
                    StringFormat format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Near;
                    if (conn.isOutput)
                        format.Alignment = StringAlignment.Far;
                    graphics.DrawString(
                        nameList[ix++], new Font(Window.NameFont, 0.5f),
                        new SolidBrush(Theme.MainColor), conn.Position.x, conn.Position.y,
                        format
                    );
                }
                break;
            case Type.Switch:
                graphics.FillRectangle(new SolidBrush(state ? Theme.Glow : Theme.Transparent), x, y, w, h);
                break;
            case Type.Light:
                graphics.FillRectangle(new SolidBrush(input[0].Level ? Theme.Glow : Theme.Transparent), x, y, w, h);
                break;
            case Type.SegmentDisplay:
                float ww = 3, vv = 0.5f, vw = 2;
                for (int i = 0; i < 7; i++)
                {
                    GraphicsPath path = new GraphicsPath();
                    float x = (6 >> i & 1) * ww + vv, y = (5796 >> 2 * i & 3) * ww + vv;
                    int dx = 73 >> i & 1, dy = dx - 1;
                    path.AddLine(x, y, x += vv, y -= vv);
                    path.AddLine(x, y, x += dx * vw, y += dy * vw);
                    int d = 2 * dx - 1;
                    path.AddLine(x, y, x += d * vv, y += d * vv);
                    path.AddLine(x, y, x -= vv, y += vv);
                    path.AddLine(x, y, x -= dx * vw, y -= dy * vw);
                    path.Transform(new Matrix(1, 0, 0, 1, this.x + (w - 4) / 2f, this.y + (h - 7) / 2f));
                    path.CloseFigure();
                    graphics.FillPath(new SolidBrush(input[i].Level ? Theme.Glow : Theme.SelectionLight), path);
                    graphics.DrawPath(new Pen(Theme.BackColor, 2 * Window.PenWidth), path);
                }
                break;
            case Type.RedGreenLight:
                Color top = input[0].Level ? Theme.SelectionDim : Theme.Red;
                Color bot = input[0].Level ? Theme.Green : Theme.SelectionDim;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                RectangleF bounds = new RectangleF(x, y, w, h / 2f);
                bounds.Inflate(-1f * Window.PenWidth, -1f * Window.PenWidth);
                graphics.FillEllipse(new SolidBrush(top), bounds);
                bounds.Inflate(-1f * Window.PenWidth, -1f * Window.PenWidth);
                graphics.DrawEllipse(new Pen(top, 2 * Window.PenWidth), bounds);
                bounds.Inflate(2f * Window.PenWidth, 2f * Window.PenWidth); bounds.Y += bounds.Height;
                bounds.Inflate(-1f * Window.PenWidth, -1f * Window.PenWidth);
                graphics.FillEllipse(new SolidBrush(bot), bounds);
                bounds.Inflate(-1f * Window.PenWidth, -1f * Window.PenWidth);
                graphics.DrawEllipse(new Pen(bot, 2 * Window.PenWidth), bounds);
                graphics.SmoothingMode = SmoothingMode.None;
                break;
            }

            foreach (var conn in input)
                DrawCable(conn, graphics);

            foreach (var conn in Connections)
                conn.Draw(graphics);

            StringFormat format1 = new StringFormat();
            format1.LineAlignment = StringAlignment.Center;
            format1.Alignment = StringAlignment.Center;
            graphics.DrawString(
                Tag, new Font(Window.TagFont, 1f),
                new SolidBrush(Theme.MainColor), x, y
            );
            graphics.DrawString(
                name, new Font(Window.NameFont, 0.5f),
                new SolidBrush(Theme.MainColor), x + w / 2f,
                type == Type.Switch || type == Type.Light ? y + h / 2f : y - 0.5f,
                format1
            );
            graphics.DrawRectangle(new Pen(Theme.MainColor, 2 * Window.PenWidth), x, y, w, h);
        }

        public bool IncludesPos(float x0, float y0)
        {
            return x <= x0 && y <= y0 && x + w >= x0 && y + h >= y0;
        }
        public bool IncludedIn(float x0, float y0, float x1, float y1)
        {
            return x >= Math.Min(x0, x1) - w && x <= Math.Max(x0, x1)
                && y >= Math.Min(y0, y1) - h && y <= Math.Max(y0, y1);
        }

        public void TrimConnections(List<Connection> connections, int min, int max, bool isOut)
        {
            connections.RemoveAll(conn => conn.self.IsMutable && conn.empty && !conn.inverted);
            for (int i = 0; i < connections.Count; i++)
                connections[i].index = i;
            while (connections.Count > max && max != -1)
            {
                connections.Last().Clear();
                connections.RemoveAt(connections.Count - 1);
            }
            while (connections.Count < min)
                connections.Add(new Connection(this, connections.Count, false, isOut));
            if (isOut)
                amtOutputs = connections.Count;
            else
                amtInputs = connections.Count;
        }
        public void TrimAllConnections()
        {
            TrimConnections(input, minInputs, maxInputs, false);
            TrimConnections(output, amtOutputs, amtOutputs, true);
        }
        public void ResetAllConnections()
        {
            amtInputs = 0; amtOutputs = 0;
            input.ForEach(c => c.Clear());
            output.ForEach(c => c.Clear());
            input.Clear(); output.Clear();
        }
        public void ResetCable()
        {
            foreach (var conn in input)
                if (!conn.empty)
                {
                    conn.Point.x = (conn.Position.x + conn.OtherConnection.Position.x) / 2f;
                    conn.Point.y = (conn.Position.y + conn.OtherConnection.Position.y) / 2f;
                    conn.Point.SnapToGrid();
                }
        }

        public void Set(float w, float h, int minIn, int maxIn, int amtOut, bool inv)
        {
            this.w = w; this.h = h;
            TrimConnections(input, minInputs = minIn, maxInputs = maxIn, false);
            TrimConnections(output, amtOut, amtOut, true);
            output.ForEach(c => c.inverted = inv);
        }
        public void ChangeTo(Type type, Gate custom = null)
        {
            switch (this.type = type)
            {
            case Type.And:
            case Type.Or:
            case Type.Xor:
                Set(3, 4, 2, -1, 1, false); break;
            case Type.Nand:
            case Type.Nor:
            case Type.Xnor:
                Set(3, 4, 2, -1, 1, true); break;
            case Type.Not:
                Set(3, 4, 1, 1, 1, true); break;
            case Type.Switch:
                Set(2, 2, 0, 0, 1, false); break;
            case Type.Light:
                Set(2, 2, 1, 1, 0, false); break;
            case Type.RedGreenLight:
                Set(2, 4, 1, 1, 0, false); break;
            case Type.SegmentDisplay:
                Set(5, 8, 7, 7, 0, false); break;
            case Type.RSFlipFlop:
            case Type.TFlipFlop:
            case Type.DFlipFlop:
                Set(3, 4, 2, 2, 2, false); output[1].inverted = true; break;
            case Type.ClockRSFlipFlop:
            case Type.JKFlipFlop:
                Set(3, 4, 3, 3, 2, false); output[1].inverted = true; break;
            case Type.JKMasterSlave:
                Set(3, 5, 5, 5, 2, false); output[1].inverted = true; break;
            case Type.RisingEdgePulse:
            case Type.FallingEdgePulse:
            case Type.EdgePulse:
                Set(2, 2, 1, 1, 1, false); break;
            case Type.Clock:
                Set(2, 2, 0, 0, 1, false); break;
            case Type.Custom:
                if (custom == null)
                    break;
                Set(5, 6, custom.amtInputs, custom.amtInputs, custom.amtOutputs, false);
                truthTable = custom.truthTable;
                nameList = custom.nameList;
                break;
            }
        }

        public bool Find(Gate gate)
        {
            foreach (var conn in input)
                if (!conn.empty && (conn.OtherConnection.self == gate || conn.OtherConnection.self.Find(gate)))
                    return true;
            return false;
        }
        public void Move(float x, float y)
        {
            this.x += x; this.y += y;
            foreach (var conns in Connections)
            {
                if (!conns.empty)
                    if (conns.isOutput)
                    {
                        foreach (var conn in conns.OtherConnections)
                            if (!conn.empty)
                            {
                                if (!conn.Point.movedRight)
                                    conn.Point.Move(x / 2f, y / 2f);
                                conn.Point.movedRight = true;
                            }
                    }
                    else
                    {
                        if (!conns.Point.movedLeft)
                            conns.Point.Move(x / 2f, y / 2f);
                        conns.Point.movedLeft = true;
                    }
            }
        }
        public void SnapToGrid()
        {
            float dx = (float) Math.Round(x) - x;
            float dy = (float) Math.Round(y) - y;
            x += dx; y += dy;
            foreach (var conn in Connections)
                if (conn.Point != null)
                {
                    conn.Point.Move(dx, dy);
                    conn.Point.SnapToGrid();
                }
                else if (conn.isOutput && !conn.empty && conn.OtherConnection.Point != null)
                {
                    //conn.connection.point.Move(dx, dy);
                    conn.OtherConnection.Point.SnapToGrid();
                }
        }

        public bool Level(int index)
        {
            if ((eval || type == Type.Switch) && type != Type.Custom)
                return state;
            bool[] levels = input.Select(g => g.Level).ToArray();
            eval = true;

            switch (type)
            {
            case Type.And:
            case Type.Nand:
                foreach (bool level in levels)
                    if (!level) return state = false;
                return state = true;
            case Type.Or:
            case Type.Nor:
                foreach (bool level in levels)
                    if (level) return state = true;
                return state = false;
            case Type.Xor:
            case Type.Xnor:
                state = false;
                foreach (bool level in levels)
                    if (level) state = !state;
                return state;
            case Type.Not:
                return state = levels[0];
            case Type.Custom:
                return state = truthTable[TruthTableIndex(index)];
            case Type.ClockRSFlipFlop:
                if (levels[1])
                {
                    if (levels[2]) state = false;
                    if (levels[0]) state = true;
                }
                return state;
            case Type.RSFlipFlop:
                if (levels[1]) state = false;
                if (levels[0]) state = true;
                return state;
            case Type.DFlipFlop:
                if (levels[1]) state = levels[0];
                return state;
            case Type.TFlipFlop:
                if (levels[0] && levels[1]) state = !state;
                return state;
            case Type.JKFlipFlop:
                if (levels[1])
                {
                    if (levels[0] && levels[2]) state = !state;
                    else if (levels[2]) state = false;
                    else if (levels[0]) state = true;
                }
                return state;
            case Type.JKMasterSlave:
                if (levels[4]) state = false;
                if (levels[0]) state = true;
                if (levels[2])
                {
                    if (levels[1] && levels[3]) state = !state;
                    else if (levels[3]) state = false;
                    else if (levels[1]) state = true;
                }
                return state;
            case Type.RisingEdgePulse:
            case Type.FallingEdgePulse:
                if (type == Type.FallingEdgePulse != levels[0])
                {
                    if (!togglestate) return state = togglestate = true;
                    return state = false;
                }
                return state = togglestate = false;
            case Type.EdgePulse:
                state = togglestate != levels[0];
                togglestate = levels[0];
                return state;
            case Type.Clock:
                return state = DateTime.Now.Second % 2 == 0;
            }
            return state;
        }
        public int TruthTableIndex(int index)
        {
            int ix = 0;
            for (int i = 0; i < amtInputs; i++)
                if (input[i].Level)
                    ix |= 1 << i;
            return index * (1 << amtInputs) + ix;
        }
    }
}
