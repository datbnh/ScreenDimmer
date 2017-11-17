using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Augustine.ScreenDimmer
{
    internal partial class HelpWindow : Form
    {
        private TableLayoutPanel panel;
        public HelpWindow()
        {
            InitializeComponent();
            Text = "Hotkeys";
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panel = new TableLayoutPanel();
            Controls.Add(panel);
            initHotkeyPanel();
        }

        private void initHotkeyPanel()
        {
            panel.AutoSize = true;
            panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panel.Dock = DockStyle.Fill;

            ResetHotkeyPanel();
        }

        private void initHeaderHotkeyPanel()
        {
            panel.RowCount = 1;
            panel.ColumnCount = 3;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panel.Controls.Add(new Label()
            {
                Text = "Description",
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Font = new Font(panel.Font, FontStyle.Bold)
            }, 0, 0);
            panel.Controls.Add(new Label()
            {
                Text = "Hotkey",
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Font = new Font(panel.Font, FontStyle.Bold)
            }, 1, 0);
        }

        public void ResetHotkeyPanel()
        {
            panel.SuspendLayout();

            // avoid memory leak
            foreach (Control item in panel.Controls)
            {    
                item.Dispose();
            }
            panel.Controls.Clear();
            panel.RowStyles.Clear();
            panel.ColumnStyles.Clear();

            initHeaderHotkeyPanel();

            panel.ResumeLayout();
        }

        public void AddHotKey(GlobalHotKey hotkey)
        {
            panel.RowCount = panel.RowCount + 1;
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panel.Controls.Add(new Label()
            {
                Text = hotkey.Description,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            }, 0, panel.RowCount - 1);
            panel.Controls.Add(new Label()
            {
                Text = hotkey.ToString(),
                Anchor = AnchorStyles.Left,
                AutoSize = true
            }, 1, panel.RowCount - 1);
            Label statusLabel = new Label() { Anchor = AnchorStyles.Left, AutoSize = true };
            if (hotkey.IsRegistered)
            {
                statusLabel.Text = "✔";
                statusLabel.ForeColor = Color.DarkGreen;
                toolTip1.SetToolTip(statusLabel, "Successfully Registered");
            }
            else
            {
                statusLabel.Text = "✘";
                statusLabel.ForeColor = Color.DarkRed;
                toolTip2.SetToolTip(statusLabel, "Failed to Register");
            }
            panel.Controls.Add(statusLabel, 2, panel.RowCount - 1);
        }
    }
}
