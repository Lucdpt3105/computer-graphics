namespace Project_CG_Paint.Forms.Paint
{
    partial class PaintForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaintForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnOpenAnimation = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDraw2D = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDraw3D = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenTransform = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExitApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lbSelection = new System.Windows.Forms.Label();
            this.btnSelection = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lbShape2D = new System.Windows.Forms.Label();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPoint = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnEllipse = new System.Windows.Forms.Button();
            this.btnTriangle = new System.Windows.Forms.Button();
            this.btnDiamond = new System.Windows.Forms.Button();
            this.btnParallelogram = new System.Windows.Forms.Button();
            this.btnPolygon = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lbShape3D = new System.Windows.Forms.Label();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCube = new System.Windows.Forms.Button();
            this.btnPrism = new System.Windows.Forms.Button();
            this.btnCylinder = new System.Windows.Forms.Button();
            this.btnPyramid = new System.Windows.Forms.Button();
            this.btnSphere = new System.Windows.Forms.Button();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lbTool = new System.Windows.Forms.Label();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFill = new System.Windows.Forms.Button();
            this.btnReflectO = new System.Windows.Forms.Button();
            this.btnLineStyle = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnReflectY = new System.Windows.Forms.Button();
            this.btnReflectX = new System.Windows.Forms.Button();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.lbColor = new System.Windows.Forms.Label();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.btnBlack = new System.Windows.Forms.Button();
            this.btnWhite = new System.Windows.Forms.Button();
            this.btnRed = new System.Windows.Forms.Button();
            this.btnOrange = new System.Windows.Forms.Button();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnGreen = new System.Windows.Forms.Button();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnPurple = new System.Windows.Forms.Button();
            this.btnGray = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMousePos = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusFormSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1356, 1450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1348, 383);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenAnimation,
            this.btnDraw2D,
            this.btnDraw3D,
            this.btnOpenTransform,
            this.btnExitApplication});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1348, 114);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnOpenAnimation
            // 
            this.btnOpenAnimation.Name = "btnOpenAnimation";
            this.btnOpenAnimation.Size = new System.Drawing.Size(86, 108);
            this.btnOpenAnimation.Text = "Scene";
            this.btnOpenAnimation.Click += new System.EventHandler(this.btnOpenAnimation_Click);
            // 
            // btnDraw2D
            // 
            this.btnDraw2D.Name = "btnDraw2D";
            this.btnDraw2D.Size = new System.Drawing.Size(101, 108);
            this.btnDraw2D.Text = "2D-Oxy";
            // 
            // btnDraw3D
            // 
            this.btnDraw3D.Name = "btnDraw3D";
            this.btnDraw3D.Size = new System.Drawing.Size(111, 108);
            this.btnDraw3D.Text = "3D-Oxyz";
            // 
            // btnOpenTransform
            // 
            this.btnOpenTransform.Name = "btnOpenTransform";
            this.btnOpenTransform.Size = new System.Drawing.Size(123, 108);
            this.btnOpenTransform.Text = "Transform";
            this.btnOpenTransform.Click += new System.EventHandler(this.btnOpenTransform_Click);
            // 
            // btnExitApplication
            // 
            this.btnExitApplication.Name = "btnExitApplication";
            this.btnExitApplication.Size = new System.Drawing.Size(64, 108);
            this.btnExitApplication.Text = "Exit";
            this.btnExitApplication.Click += new System.EventHandler(this.btnExitApplication_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel7, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel8, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel9, 4, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 118);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1340, 261);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.lbSelection, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.btnSelection, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(59, 253);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // lbSelection
            // 
            this.lbSelection.AutoSize = true;
            this.lbSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSelection.Location = new System.Drawing.Point(4, 222);
            this.lbSelection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSelection.Name = "lbSelection";
            this.lbSelection.Size = new System.Drawing.Size(51, 31);
            this.lbSelection.TabIndex = 0;
            this.lbSelection.Text = "Selection";
            this.lbSelection.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSelection
            // 
            this.btnSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelection.Image = ((System.Drawing.Image)(resources.GetObject("btnSelection.Image")));
            this.btnSelection.Location = new System.Drawing.Point(4, 4);
            this.btnSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelection.Name = "btnSelection";
            this.btnSelection.Size = new System.Drawing.Size(51, 214);
            this.btnSelection.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnSelection, "Select Object");
            this.btnSelection.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.lbShape2D, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel10, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(71, 4);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(260, 253);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // lbShape2D
            // 
            this.lbShape2D.AutoSize = true;
            this.lbShape2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbShape2D.Location = new System.Drawing.Point(4, 222);
            this.lbShape2D.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbShape2D.Name = "lbShape2D";
            this.lbShape2D.Size = new System.Drawing.Size(252, 31);
            this.lbShape2D.TabIndex = 0;
            this.lbShape2D.Text = "Shape2D";
            this.lbShape2D.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 3;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.Controls.Add(this.btnPoint, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.btnLine, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.btnRectangle, 2, 0);
            this.tableLayoutPanel10.Controls.Add(this.btnCircle, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.btnEllipse, 1, 1);
            this.tableLayoutPanel10.Controls.Add(this.btnTriangle, 2, 1);
            this.tableLayoutPanel10.Controls.Add(this.btnDiamond, 0, 2);
            this.tableLayoutPanel10.Controls.Add(this.btnParallelogram, 1, 2);
            this.tableLayoutPanel10.Controls.Add(this.btnPolygon, 2, 2);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(252, 214);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // btnPoint
            // 
            this.btnPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPoint.Image = ((System.Drawing.Image)(resources.GetObject("btnPoint.Image")));
            this.btnPoint.Location = new System.Drawing.Point(4, 4);
            this.btnPoint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(76, 63);
            this.btnPoint.TabIndex = 0;
            this.btnPoint.Tag = "";
            this.toolTip1.SetToolTip(this.btnPoint, "Draw Point");
            this.btnPoint.UseVisualStyleBackColor = true;
            // 
            // btnLine
            // 
            this.btnLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnLine.Image")));
            this.btnLine.Location = new System.Drawing.Point(88, 4);
            this.btnLine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(76, 63);
            this.btnLine.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnLine, "Draw Line");
            this.btnLine.UseVisualStyleBackColor = true;
            // 
            // btnRectangle
            // 
            this.btnRectangle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnRectangle.Image")));
            this.btnRectangle.Location = new System.Drawing.Point(172, 4);
            this.btnRectangle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(76, 63);
            this.btnRectangle.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnRectangle, "Draw Rectangle");
            this.btnRectangle.UseVisualStyleBackColor = true;
            // 
            // btnCircle
            // 
            this.btnCircle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnCircle.Image")));
            this.btnCircle.Location = new System.Drawing.Point(4, 75);
            this.btnCircle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(76, 63);
            this.btnCircle.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnCircle, "Draw Circle");
            this.btnCircle.UseVisualStyleBackColor = true;
            // 
            // btnEllipse
            // 
            this.btnEllipse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEllipse.Image = ((System.Drawing.Image)(resources.GetObject("btnEllipse.Image")));
            this.btnEllipse.Location = new System.Drawing.Point(88, 75);
            this.btnEllipse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(76, 63);
            this.btnEllipse.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnEllipse, "Draw Ellipse");
            this.btnEllipse.UseVisualStyleBackColor = true;
            // 
            // btnTriangle
            // 
            this.btnTriangle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTriangle.Image = ((System.Drawing.Image)(resources.GetObject("btnTriangle.Image")));
            this.btnTriangle.Location = new System.Drawing.Point(172, 75);
            this.btnTriangle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTriangle.Name = "btnTriangle";
            this.btnTriangle.Size = new System.Drawing.Size(76, 63);
            this.btnTriangle.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnTriangle, "Draw Triangle");
            this.btnTriangle.UseVisualStyleBackColor = true;
            // 
            // btnDiamond
            // 
            this.btnDiamond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDiamond.Image = ((System.Drawing.Image)(resources.GetObject("btnDiamond.Image")));
            this.btnDiamond.Location = new System.Drawing.Point(4, 146);
            this.btnDiamond.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDiamond.Name = "btnDiamond";
            this.btnDiamond.Size = new System.Drawing.Size(76, 64);
            this.btnDiamond.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnDiamond, "Draw Diamond");
            this.btnDiamond.UseVisualStyleBackColor = true;
            // 
            // btnParallelogram
            // 
            this.btnParallelogram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnParallelogram.Image = ((System.Drawing.Image)(resources.GetObject("btnParallelogram.Image")));
            this.btnParallelogram.Location = new System.Drawing.Point(88, 146);
            this.btnParallelogram.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnParallelogram.Name = "btnParallelogram";
            this.btnParallelogram.Size = new System.Drawing.Size(76, 64);
            this.btnParallelogram.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnParallelogram, "Draw Parallelogram");
            this.btnParallelogram.UseVisualStyleBackColor = true;
            // 
            // btnPolygon
            // 
            this.btnPolygon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPolygon.Image = ((System.Drawing.Image)(resources.GetObject("btnPolygon.Image")));
            this.btnPolygon.Location = new System.Drawing.Point(172, 146);
            this.btnPolygon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(76, 64);
            this.btnPolygon.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnPolygon, "Draw Polygon");
            this.btnPolygon.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.lbShape3D, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel11, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(339, 4);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(260, 253);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // lbShape3D
            // 
            this.lbShape3D.AutoSize = true;
            this.lbShape3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbShape3D.Location = new System.Drawing.Point(4, 222);
            this.lbShape3D.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbShape3D.Name = "lbShape3D";
            this.lbShape3D.Size = new System.Drawing.Size(252, 31);
            this.lbShape3D.TabIndex = 0;
            this.lbShape3D.Text = "Shape3D";
            this.lbShape3D.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.btnCube, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.btnPrism, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.btnCylinder, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.btnPyramid, 1, 1);
            this.tableLayoutPanel11.Controls.Add(this.btnSphere, 0, 2);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 3;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(252, 214);
            this.tableLayoutPanel11.TabIndex = 1;
            // 
            // btnCube
            // 
            this.btnCube.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCube.Image = ((System.Drawing.Image)(resources.GetObject("btnCube.Image")));
            this.btnCube.Location = new System.Drawing.Point(4, 4);
            this.btnCube.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCube.Name = "btnCube";
            this.btnCube.Size = new System.Drawing.Size(118, 63);
            this.btnCube.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnCube, "Draw Cube");
            this.btnCube.UseVisualStyleBackColor = true;
            // 
            // btnPrism
            // 
            this.btnPrism.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrism.Image = ((System.Drawing.Image)(resources.GetObject("btnPrism.Image")));
            this.btnPrism.Location = new System.Drawing.Point(130, 4);
            this.btnPrism.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrism.Name = "btnPrism";
            this.btnPrism.Size = new System.Drawing.Size(118, 63);
            this.btnPrism.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnPrism, "Draw Prism");
            this.btnPrism.UseVisualStyleBackColor = true;
            // 
            // btnCylinder
            // 
            this.btnCylinder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCylinder.Image = ((System.Drawing.Image)(resources.GetObject("btnCylinder.Image")));
            this.btnCylinder.Location = new System.Drawing.Point(4, 75);
            this.btnCylinder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCylinder.Name = "btnCylinder";
            this.btnCylinder.Size = new System.Drawing.Size(118, 63);
            this.btnCylinder.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnCylinder, "Draw Cylinder");
            this.btnCylinder.UseVisualStyleBackColor = true;
            // 
            // btnPyramid
            // 
            this.btnPyramid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPyramid.Image = ((System.Drawing.Image)(resources.GetObject("btnPyramid.Image")));
            this.btnPyramid.Location = new System.Drawing.Point(130, 75);
            this.btnPyramid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPyramid.Name = "btnPyramid";
            this.btnPyramid.Size = new System.Drawing.Size(118, 63);
            this.btnPyramid.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnPyramid, "Draw Pyramid");
            this.btnPyramid.UseVisualStyleBackColor = true;
            // 
            // btnSphere
            // 
            this.btnSphere.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSphere.Image = ((System.Drawing.Image)(resources.GetObject("btnSphere.Image")));
            this.btnSphere.Location = new System.Drawing.Point(4, 146);
            this.btnSphere.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSphere.Name = "btnSphere";
            this.btnSphere.Size = new System.Drawing.Size(118, 64);
            this.btnSphere.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnSphere, "Draw Sphere");
            this.btnSphere.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.lbTool, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel12, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(607, 4);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(260, 253);
            this.tableLayoutPanel8.TabIndex = 3;
            // 
            // lbTool
            // 
            this.lbTool.AutoSize = true;
            this.lbTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTool.Location = new System.Drawing.Point(4, 222);
            this.lbTool.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTool.Name = "lbTool";
            this.lbTool.Size = new System.Drawing.Size(252, 31);
            this.lbTool.TabIndex = 0;
            this.lbTool.Text = "Tool";
            this.lbTool.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.btnFill, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.btnReflectO, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.btnLineStyle, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.btnClear, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.btnReflectY, 1, 2);
            this.tableLayoutPanel12.Controls.Add(this.btnReflectX, 1, 1);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 3;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(252, 214);
            this.tableLayoutPanel12.TabIndex = 1;
            // 
            // btnFill
            // 
            this.btnFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFill.Image = ((System.Drawing.Image)(resources.GetObject("btnFill.Image")));
            this.btnFill.Location = new System.Drawing.Point(4, 4);
            this.btnFill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(118, 63);
            this.btnFill.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnFill, "Fill Color");
            this.btnFill.UseVisualStyleBackColor = true;
            // 
            // btnReflectO
            // 
            this.btnReflectO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReflectO.Image = ((System.Drawing.Image)(resources.GetObject("btnReflectO.Image")));
            this.btnReflectO.Location = new System.Drawing.Point(130, 4);
            this.btnReflectO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReflectO.Name = "btnReflectO";
            this.btnReflectO.Size = new System.Drawing.Size(118, 63);
            this.btnReflectO.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnReflectO, "Reflect Origin");
            this.btnReflectO.UseVisualStyleBackColor = true;
            // 
            // btnLineStyle
            // 
            this.btnLineStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLineStyle.Image = ((System.Drawing.Image)(resources.GetObject("btnLineStyle.Image")));
            this.btnLineStyle.Location = new System.Drawing.Point(4, 75);
            this.btnLineStyle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLineStyle.Name = "btnLineStyle";
            this.btnLineStyle.Size = new System.Drawing.Size(118, 63);
            this.btnLineStyle.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnLineStyle, "Change Line Style");
            this.btnLineStyle.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(4, 146);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(118, 64);
            this.btnClear.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnClear, "Clear Canvas");
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnReflectY
            // 
            this.btnReflectY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReflectY.Image = ((System.Drawing.Image)(resources.GetObject("btnReflectY.Image")));
            this.btnReflectY.Location = new System.Drawing.Point(130, 146);
            this.btnReflectY.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReflectY.Name = "btnReflectY";
            this.btnReflectY.Size = new System.Drawing.Size(118, 64);
            this.btnReflectY.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnReflectY, "Reflect Y-Axis");
            this.btnReflectY.UseVisualStyleBackColor = true;
            // 
            // btnReflectX
            // 
            this.btnReflectX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReflectX.Image = ((System.Drawing.Image)(resources.GetObject("btnReflectX.Image")));
            this.btnReflectX.Location = new System.Drawing.Point(130, 75);
            this.btnReflectX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReflectX.Name = "btnReflectX";
            this.btnReflectX.Size = new System.Drawing.Size(118, 63);
            this.btnReflectX.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnReflectX, "Reflect X-Axis");
            this.btnReflectX.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.lbColor, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel13, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(875, 4);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(260, 253);
            this.tableLayoutPanel9.TabIndex = 4;
            // 
            // lbColor
            // 
            this.lbColor.AutoSize = true;
            this.lbColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbColor.Location = new System.Drawing.Point(4, 222);
            this.lbColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbColor.Name = "lbColor";
            this.lbColor.Size = new System.Drawing.Size(252, 31);
            this.lbColor.TabIndex = 0;
            this.lbColor.Text = "Color";
            this.lbColor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 3;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel13.Controls.Add(this.btnBlack, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnWhite, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnRed, 2, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnOrange, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.btnYellow, 1, 1);
            this.tableLayoutPanel13.Controls.Add(this.btnGreen, 2, 1);
            this.tableLayoutPanel13.Controls.Add(this.btnBlue, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.btnPurple, 1, 2);
            this.tableLayoutPanel13.Controls.Add(this.btnGray, 2, 2);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel13.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 3;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(252, 214);
            this.tableLayoutPanel13.TabIndex = 1;
            // 
            // btnBlack
            // 
            this.btnBlack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBlack.Location = new System.Drawing.Point(4, 4);
            this.btnBlack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(76, 63);
            this.btnBlack.TabIndex = 0;
            this.btnBlack.Text = "Black";
            this.btnBlack.UseVisualStyleBackColor = true;
            // 
            // btnWhite
            // 
            this.btnWhite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWhite.Location = new System.Drawing.Point(88, 4);
            this.btnWhite.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnWhite.Name = "btnWhite";
            this.btnWhite.Size = new System.Drawing.Size(76, 63);
            this.btnWhite.TabIndex = 0;
            this.btnWhite.Text = "White";
            this.btnWhite.UseVisualStyleBackColor = true;
            // 
            // btnRed
            // 
            this.btnRed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRed.Location = new System.Drawing.Point(172, 4);
            this.btnRed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(76, 63);
            this.btnRed.TabIndex = 0;
            this.btnRed.Text = "Red";
            this.btnRed.UseVisualStyleBackColor = true;
            // 
            // btnOrange
            // 
            this.btnOrange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOrange.Location = new System.Drawing.Point(4, 75);
            this.btnOrange.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOrange.Name = "btnOrange";
            this.btnOrange.Size = new System.Drawing.Size(76, 63);
            this.btnOrange.TabIndex = 0;
            this.btnOrange.Text = "Orange";
            this.btnOrange.UseVisualStyleBackColor = true;
            // 
            // btnYellow
            // 
            this.btnYellow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnYellow.Location = new System.Drawing.Point(88, 75);
            this.btnYellow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(76, 63);
            this.btnYellow.TabIndex = 0;
            this.btnYellow.Text = "Yellow";
            this.btnYellow.UseVisualStyleBackColor = true;
            // 
            // btnGreen
            // 
            this.btnGreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGreen.Location = new System.Drawing.Point(172, 75);
            this.btnGreen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGreen.Name = "btnGreen";
            this.btnGreen.Size = new System.Drawing.Size(76, 63);
            this.btnGreen.TabIndex = 0;
            this.btnGreen.Text = "Green";
            this.btnGreen.UseVisualStyleBackColor = true;
            // 
            // btnBlue
            // 
            this.btnBlue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBlue.Location = new System.Drawing.Point(4, 146);
            this.btnBlue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(76, 64);
            this.btnBlue.TabIndex = 0;
            this.btnBlue.Text = "Blue";
            this.btnBlue.UseVisualStyleBackColor = true;
            // 
            // btnPurple
            // 
            this.btnPurple.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPurple.Location = new System.Drawing.Point(88, 146);
            this.btnPurple.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPurple.Name = "btnPurple";
            this.btnPurple.Size = new System.Drawing.Size(76, 64);
            this.btnPurple.TabIndex = 0;
            this.btnPurple.Text = "Purple";
            this.btnPurple.UseVisualStyleBackColor = true;
            // 
            // btnGray
            // 
            this.btnGray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGray.Location = new System.Drawing.Point(172, 146);
            this.btnGray.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGray.Name = "btnGray";
            this.btnGray.Size = new System.Drawing.Size(76, 64);
            this.btnGray.TabIndex = 0;
            this.btnGray.Text = "Gray";
            this.btnGray.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel3.Controls.Add(this.canvas, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.treeView1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 395);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1348, 1007);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(179, 4);
            this.canvas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(935, 999);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(4, 4);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(167, 999);
            this.treeView1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMousePos,
            this.statusFormSize});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1406);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1356, 44);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusMousePos
            // 
            this.statusMousePos.Name = "statusMousePos";
            this.statusMousePos.Size = new System.Drawing.Size(141, 35);
            this.statusMousePos.Text = "Tọa độ chuột:";
            // 
            // statusFormSize
            // 
            this.statusFormSize.Name = "statusFormSize";
            this.statusFormSize.Size = new System.Drawing.Size(186, 35);
            this.statusFormSize.Text = "Kích thước canvas:";
            // 
            // PaintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 1450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PaintForm";
            this.Text = "PaintForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnOpenAnimation;
        private System.Windows.Forms.ToolStripMenuItem btnDraw2D;
        private System.Windows.Forms.ToolStripMenuItem btnDraw3D;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label lbSelection;
        private System.Windows.Forms.Label lbShape2D;
        private System.Windows.Forms.Label lbShape3D;
        private System.Windows.Forms.Label lbTool;
        private System.Windows.Forms.Label lbColor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.Button btnSelection;
        private System.Windows.Forms.Button btnPoint;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button btnEllipse;
        private System.Windows.Forms.Button btnTriangle;
        private System.Windows.Forms.Button btnDiamond;
        private System.Windows.Forms.Button btnParallelogram;
        private System.Windows.Forms.Button btnPolygon;
        private System.Windows.Forms.Button btnCube;
        private System.Windows.Forms.Button btnPrism;
        private System.Windows.Forms.Button btnCylinder;
        private System.Windows.Forms.Button btnPyramid;
        private System.Windows.Forms.Button btnSphere;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMousePos;
        private System.Windows.Forms.ToolStripStatusLabel statusFormSize;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Button btnReflectO;
        private System.Windows.Forms.Button btnLineStyle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnReflectY;
        private System.Windows.Forms.Button btnReflectX;
        private System.Windows.Forms.ToolStripMenuItem btnOpenTransform;
        private System.Windows.Forms.ToolStripMenuItem btnExitApplication;
        private System.Windows.Forms.Button btnBlack;
        private System.Windows.Forms.Button btnWhite;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnOrange;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.Button btnGreen;
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Button btnPurple;
        private System.Windows.Forms.Button btnGray;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TreeView treeView1;
    }
}

