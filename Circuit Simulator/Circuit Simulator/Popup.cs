using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Circuit_Simulator
{
    public partial class Popup : Form
    {
        public List<Gate> gates;
        Mode mode;

        public enum Mode
        {
            Name, Size
        }

        public Popup()
        {
            InitializeComponent();
        }

        private void Rename_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        public void Set(List<Gate> gates, Mode mode)
        {
            this.gates = gates;
            this.mode = mode;
            switch (mode)
            {
            case Mode.Name:
                textBox.Text = gates.First().name;
                break;
            case Mode.Size:
                textBox.Text = $"{gates.First().w}, {gates.First().h}";
                break;
            }
            textBox.SelectAll(); textBox.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                switch (mode)
                {
                case Mode.Name:
                    gates.ForEach(gate => gate.name = textBox.Text);
                    break;
                case Mode.Size:
                    int index = 0;
                    foreach (Match match in new Regex(@"\d+\.*\d*").Matches(textBox.Text))
                    {
                        float f = Math.Min(Math.Max(1f, (float) Math.Round(Convert.ToSingle(match.ToString()))), 0xFF);
                        if (index == 0)
                            gates.ForEach(gate => gate.w = f);
                        else if (index == 1)
                            gates.ForEach(gate => gate.h = f);
                        else
                            break;
                        index++;
                    }
                    break;
                }
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode != Keys.A)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
