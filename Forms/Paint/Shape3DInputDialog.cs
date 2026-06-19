using Project_CG_Paint.Controllers.Paint;
using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Project_CG_Paint.Forms.Paint
{
    public class Shape3DInputDialog : Form
    {
        private readonly DrawType _drawType;
        private readonly Dictionary<string, TextBox> _paramInputs = new Dictionary<string, TextBox>();
        private TextBox _txtPosX, _txtPosY, _txtPosZ;

        public Dictionary<string, double> Parameters { get; private set; }
        public Point3D Position { get; private set; }

        public Shape3DInputDialog(DrawType drawType)
        {
            _drawType = drawType;
            InitializeDialog();
        }

        private void InitializeDialog()
        {
            Text = $"Nhập tham số — {_drawType}";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Font = new Font("Segoe UI", 10);
            AutoScaleMode = AutoScaleMode.Font;
            Padding = new Padding(12);

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                AutoSize = true,
                Padding = new Padding(8)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // ---- Shape Parameters Group ----
            GroupBox grpParams = new GroupBox
            {
                Text = "Tham số hình học",
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(8, 12, 8, 8)
            };

            TableLayoutPanel paramTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                AutoSize = true,
                Padding = new Padding(4)
            };
            paramTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            paramTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));

            var paramDefs = GetParameterDefinitions(_drawType);
            int row = 0;
            foreach (var param in paramDefs)
            {
                paramTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                Label lbl = new Label
                {
                    Text = param.Key + ":",
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Margin = new Padding(4, 8, 4, 4)
                };
                paramTable.Controls.Add(lbl, 0, row);

                TextBox txt = new TextBox
                {
                    Text = param.Value.ToString(CultureInfo.InvariantCulture),
                    Width = 120,
                    Margin = new Padding(4, 6, 4, 4)
                };
                paramTable.Controls.Add(txt, 1, row);
                _paramInputs[param.Key] = txt;
                row++;
            }
            paramTable.RowCount = row;

            grpParams.Controls.Add(paramTable);
            mainLayout.Controls.Add(grpParams, 0, 0);

            // ---- Position I Group ----
            GroupBox grpPosition = new GroupBox
            {
                Text = "Vị trí I (Position — tương đối gốc O)",
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(8, 12, 8, 8)
            };

            TableLayoutPanel posTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                RowCount = 1,
                AutoSize = true,
                Padding = new Padding(4)
            };
            posTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            posTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            posTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            posTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            posTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            posTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            posTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            posTable.Controls.Add(new Label { Text = "X:", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(4, 8, 2, 4) }, 0, 0);
            _txtPosX = new TextBox { Text = "0", Width = 60, Margin = new Padding(2, 6, 8, 4) };
            posTable.Controls.Add(_txtPosX, 1, 0);

            posTable.Controls.Add(new Label { Text = "Y:", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(4, 8, 2, 4) }, 2, 0);
            _txtPosY = new TextBox { Text = "0", Width = 60, Margin = new Padding(2, 6, 8, 4) };
            posTable.Controls.Add(_txtPosY, 3, 0);

            posTable.Controls.Add(new Label { Text = "Z:", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(4, 8, 2, 4) }, 4, 0);
            _txtPosZ = new TextBox { Text = "0", Width = 60, Margin = new Padding(2, 6, 8, 4) };
            posTable.Controls.Add(_txtPosZ, 5, 0);

            grpPosition.Controls.Add(posTable);
            mainLayout.Controls.Add(grpPosition, 0, 1);

            // ---- Buttons ----
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                Padding = new Padding(0, 8, 0, 0)
            };

            Button btnCancel = new Button
            {
                Text = "Hủy",
                DialogResult = DialogResult.Cancel,
                Width = 90,
                Height = 36
            };

            Button btnOk = new Button
            {
                Text = "Tạo hình",
                Width = 100,
                Height = 36
            };
            btnOk.Click += BtnOk_Click;

            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnOk);
            mainLayout.Controls.Add(buttonPanel, 0, 2);

            Controls.Add(mainLayout);
            AcceptButton = btnOk;
            CancelButton = btnCancel;
            ClientSize = new Size(380, mainLayout.PreferredSize.Height + 40);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Parameters = new Dictionary<string, double>();
                foreach (var kvp in _paramInputs)
                {
                    if (!double.TryParse(kvp.Value.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
                    {
                        MessageBox.Show($"Giá trị không hợp lệ cho '{kvp.Key}': {kvp.Value.Text}",
                            "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        kvp.Value.Focus();
                        return;
                    }
                    Parameters[kvp.Key] = val;
                }

                if (!double.TryParse(_txtPosX.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double px))
                {
                    MessageBox.Show("Giá trị X không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtPosX.Focus();
                    return;
                }
                if (!double.TryParse(_txtPosY.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double py))
                {
                    MessageBox.Show("Giá trị Y không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtPosY.Focus();
                    return;
                }
                if (!double.TryParse(_txtPosZ.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double pz))
                {
                    MessageBox.Show("Giá trị Z không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtPosZ.Focus();
                    return;
                }

                Position = new Point3D(px, py, pz);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Dictionary<string, double> GetParameterDefinitions(DrawType drawType)
        {
            var defs = new Dictionary<string, double>();

            switch (drawType)
            {
                case DrawType.Cube:
                    defs["Size"] = 10;
                    break;
                case DrawType.Sphere:
                    defs["Radius"] = 8;
                    defs["Stacks"] = 8;
                    defs["Slices"] = 12;
                    break;
                case DrawType.Pyramid:
                    defs["BaseWidth"] = 10;
                    defs["BaseDepth"] = 10;
                    defs["Height"] = 12;
                    break;
                case DrawType.Prism:
                    defs["Radius"] = 6;
                    defs["Sides"] = 6;
                    defs["Height"] = 12;
                    break;
                case DrawType.Cylinder:
                    defs["Radius"] = 6;
                    defs["Height"] = 12;
                    defs["Segments"] = 16;
                    break;
            }

            return defs;
        }
    }
}
