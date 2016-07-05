namespace Recommender.GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ContentBasedAlghoritmChoice = new System.Windows.Forms.Panel();
            this.ContentBasedAlgorithmCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CollaborativeAlgorithmChoice = new System.Windows.Forms.Panel();
            this.CollaborativeAlgorithmCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.recommenderCombo = new System.Windows.Forms.ComboBox();
            this.datasetCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RunButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opcjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcja1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcja1aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcja1bToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcja2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zakończToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ResultContainer = new System.Windows.Forms.GroupBox();
            this.ResultBox = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.ContentBased_Teacher = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.ContentBased_BackPropPanel = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.ContentBased_LearningRate = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.ContentBased_Momentum = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.ContentBased_GeneticPanel = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.ContentBased_PopulationSize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ContentBased_Iterations = new System.Windows.Forms.NumericUpDown();
            this.ContentBased_SigmoidAlpha = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.ContentBased_HiddenLayerNeurons = new System.Windows.Forms.NumericUpDown();
            this.ContentBasedOptions = new System.Windows.Forms.Panel();
            this.ContentBased_MinFeatures = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.ContentBased_MinimumItemsRated = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ContentBased_AmountOfUsers = new System.Windows.Forms.NumericUpDown();
            this.UserLabel = new System.Windows.Forms.Label();
            this.CollaborativeOptions = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.TestingSetSize = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TrainingSetSize = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataSetOptionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.ContentBasedAlghoritmChoice.SuspendLayout();
            this.CollaborativeAlgorithmChoice.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.ResultContainer.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.ContentBased_BackPropPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_LearningRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_Momentum)).BeginInit();
            this.ContentBased_GeneticPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_PopulationSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_Iterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_SigmoidAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_HiddenLayerNeurons)).BeginInit();
            this.ContentBasedOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_MinFeatures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_MinimumItemsRated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_AmountOfUsers)).BeginInit();
            this.CollaborativeOptions.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TestingSetSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainingSetSize)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetOptionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.ContentBasedAlghoritmChoice);
            this.groupBox1.Controls.Add(this.CollaborativeAlgorithmChoice);
            this.groupBox1.Controls.Add(this.CancelButton);
            this.groupBox1.Controls.Add(this.recommenderCombo);
            this.groupBox1.Controls.Add(this.datasetCombo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.RunButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 254);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // ContentBasedAlghoritmChoice
            // 
            this.ContentBasedAlghoritmChoice.Controls.Add(this.ContentBasedAlgorithmCombo);
            this.ContentBasedAlghoritmChoice.Controls.Add(this.label6);
            this.ContentBasedAlghoritmChoice.Location = new System.Drawing.Point(6, 114);
            this.ContentBasedAlghoritmChoice.Name = "ContentBasedAlghoritmChoice";
            this.ContentBasedAlghoritmChoice.Size = new System.Drawing.Size(274, 22);
            this.ContentBasedAlghoritmChoice.TabIndex = 12;
            // 
            // ContentBasedAlgorithmCombo
            // 
            this.ContentBasedAlgorithmCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ContentBasedAlgorithmCombo.FormattingEnabled = true;
            this.ContentBasedAlgorithmCombo.Location = new System.Drawing.Point(124, 1);
            this.ContentBasedAlgorithmCombo.Name = "ContentBasedAlgorithmCombo";
            this.ContentBasedAlgorithmCombo.Size = new System.Drawing.Size(150, 21);
            this.ContentBasedAlgorithmCombo.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Content-based algorithm";
            // 
            // CollaborativeAlgorithmChoice
            // 
            this.CollaborativeAlgorithmChoice.Controls.Add(this.CollaborativeAlgorithmCombo);
            this.CollaborativeAlgorithmChoice.Controls.Add(this.label3);
            this.CollaborativeAlgorithmChoice.Enabled = false;
            this.CollaborativeAlgorithmChoice.Location = new System.Drawing.Point(6, 84);
            this.CollaborativeAlgorithmChoice.Name = "CollaborativeAlgorithmChoice";
            this.CollaborativeAlgorithmChoice.Size = new System.Drawing.Size(274, 28);
            this.CollaborativeAlgorithmChoice.TabIndex = 11;
            // 
            // CollaborativeAlgorithmCombo
            // 
            this.CollaborativeAlgorithmCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CollaborativeAlgorithmCombo.FormattingEnabled = true;
            this.CollaborativeAlgorithmCombo.Location = new System.Drawing.Point(124, 3);
            this.CollaborativeAlgorithmCombo.Name = "CollaborativeAlgorithmCombo";
            this.CollaborativeAlgorithmCombo.Size = new System.Drawing.Size(150, 21);
            this.CollaborativeAlgorithmCombo.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Collaborative algorithm";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.Enabled = false;
            this.CancelButton.Location = new System.Drawing.Point(92, 225);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(69, 23);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // recommenderCombo
            // 
            this.recommenderCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.recommenderCombo.FormattingEnabled = true;
            this.recommenderCombo.Location = new System.Drawing.Point(130, 58);
            this.recommenderCombo.Name = "recommenderCombo";
            this.recommenderCombo.Size = new System.Drawing.Size(150, 21);
            this.recommenderCombo.TabIndex = 5;
            this.recommenderCombo.SelectedIndexChanged += new System.EventHandler(this.recommenderCombo_SelectedIndexChanged);
            // 
            // datasetCombo
            // 
            this.datasetCombo.DisplayMember = "Value";
            this.datasetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.datasetCombo.FormattingEnabled = true;
            this.datasetCombo.Location = new System.Drawing.Point(130, 31);
            this.datasetCombo.Name = "datasetCombo";
            this.datasetCombo.Size = new System.Drawing.Size(150, 21);
            this.datasetCombo.TabIndex = 4;
            this.datasetCombo.ValueMember = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Recommender type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dataset";
            // 
            // RunButton
            // 
            this.RunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RunButton.Location = new System.Drawing.Point(6, 225);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(80, 23);
            this.RunButton.TabIndex = 0;
            this.RunButton.Text = "Start";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcjeToolStripMenuItem,
            this.zakończToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(714, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // opcjeToolStripMenuItem
            // 
            this.opcjeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcja1ToolStripMenuItem,
            this.opcja2ToolStripMenuItem});
            this.opcjeToolStripMenuItem.Name = "opcjeToolStripMenuItem";
            this.opcjeToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.opcjeToolStripMenuItem.Text = "Options";
            // 
            // opcja1ToolStripMenuItem
            // 
            this.opcja1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcja1aToolStripMenuItem,
            this.opcja1bToolStripMenuItem});
            this.opcja1ToolStripMenuItem.Name = "opcja1ToolStripMenuItem";
            this.opcja1ToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.opcja1ToolStripMenuItem.Text = "Opcja1";
            // 
            // opcja1aToolStripMenuItem
            // 
            this.opcja1aToolStripMenuItem.Name = "opcja1aToolStripMenuItem";
            this.opcja1aToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.opcja1aToolStripMenuItem.Text = "Opcja1a";
            // 
            // opcja1bToolStripMenuItem
            // 
            this.opcja1bToolStripMenuItem.Name = "opcja1bToolStripMenuItem";
            this.opcja1bToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.opcja1bToolStripMenuItem.Text = "Opcja1b";
            // 
            // opcja2ToolStripMenuItem
            // 
            this.opcja2ToolStripMenuItem.Name = "opcja2ToolStripMenuItem";
            this.opcja2ToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.opcja2ToolStripMenuItem.Text = "Opcja2";
            // 
            // zakończToolStripMenuItem
            // 
            this.zakończToolStripMenuItem.Name = "zakończToolStripMenuItem";
            this.zakończToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.zakończToolStripMenuItem.Text = "Close";
            // 
            // statusStrip1
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBar,
            this.StatusLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 590);
            this.StatusStrip.Name = "statusStrip1";
            this.StatusStrip.Size = new System.Drawing.Size(714, 22);
            this.StatusStrip.TabIndex = 2;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(26, 17);
            this.StatusLabel.Text = "Idle";
            // 
            // ResultContainer
            // 
            this.ResultContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultContainer.AutoSize = true;
            this.ResultContainer.Controls.Add(this.ResultBox);
            this.ResultContainer.Location = new System.Drawing.Point(308, 6);
            this.ResultContainer.Name = "ResultContainer";
            this.ResultContainer.Size = new System.Drawing.Size(366, 254);
            this.ResultContainer.TabIndex = 4;
            this.ResultContainer.TabStop = false;
            this.ResultContainer.Text = "Results";
            // 
            // ResultBox
            // 
            this.ResultBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultBox.Location = new System.Drawing.Point(3, 16);
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.ReadOnly = true;
            this.ResultBox.Size = new System.Drawing.Size(360, 235);
            this.ResultBox.TabIndex = 0;
            this.ResultBox.Text = "";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.panel3);
            this.flowLayoutPanel1.Controls.Add(this.ContentBasedOptions);
            this.flowLayoutPanel1.Controls.Add(this.CollaborativeOptions);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 263);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(673, 282);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.ContentBased_Teacher);
            this.panel3.Controls.Add(this.flowLayoutPanel2);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.ContentBased_Iterations);
            this.panel3.Controls.Add(this.ContentBased_SigmoidAlpha);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.ContentBased_HiddenLayerNeurons);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(327, 190);
            this.panel3.TabIndex = 4;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label26.Location = new System.Drawing.Point(6, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(120, 13);
            this.label26.TabIndex = 2;
            this.label26.Text = "Neural Network Options";
            // 
            // ContentBased_Teacher
            // 
            this.ContentBased_Teacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ContentBased_Teacher.FormattingEnabled = true;
            this.ContentBased_Teacher.Location = new System.Drawing.Point(139, 101);
            this.ContentBased_Teacher.Name = "ContentBased_Teacher";
            this.ContentBased_Teacher.Size = new System.Drawing.Size(150, 21);
            this.ContentBased_Teacher.TabIndex = 11;
            this.ContentBased_Teacher.SelectedIndexChanged += new System.EventHandler(this.ContentBased_Teacher_SelectedIndexChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.ContentBased_BackPropPanel);
            this.flowLayoutPanel2.Controls.Add(this.ContentBased_GeneticPanel);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 126);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(321, 57);
            this.flowLayoutPanel2.TabIndex = 29;
            // 
            // ContentBased_BackPropPanel
            // 
            this.ContentBased_BackPropPanel.Controls.Add(this.label21);
            this.ContentBased_BackPropPanel.Controls.Add(this.ContentBased_LearningRate);
            this.ContentBased_BackPropPanel.Controls.Add(this.numericUpDown1);
            this.ContentBased_BackPropPanel.Controls.Add(this.label20);
            this.ContentBased_BackPropPanel.Controls.Add(this.ContentBased_Momentum);
            this.ContentBased_BackPropPanel.Controls.Add(this.label23);
            this.ContentBased_BackPropPanel.Controls.Add(this.label18);
            this.ContentBased_BackPropPanel.Location = new System.Drawing.Point(3, 3);
            this.ContentBased_BackPropPanel.Name = "ContentBased_BackPropPanel";
            this.ContentBased_BackPropPanel.Size = new System.Drawing.Size(303, 52);
            this.ContentBased_BackPropPanel.TabIndex = 27;
            this.ContentBased_BackPropPanel.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label21.Location = new System.Drawing.Point(59, 85);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(44, 9);
            this.label21.TabIndex = 26;
            this.label21.Text = "(0 - infinity)";
            // 
            // ContentBased_LearningRate
            // 
            this.ContentBased_LearningRate.DecimalPlaces = 2;
            this.ContentBased_LearningRate.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.ContentBased_LearningRate.Location = new System.Drawing.Point(133, 2);
            this.ContentBased_LearningRate.Name = "ContentBased_LearningRate";
            this.ContentBased_LearningRate.Size = new System.Drawing.Size(150, 20);
            this.ContentBased_LearningRate.TabIndex = 22;
            this.ContentBased_LearningRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(127, 81);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(150, 20);
            this.numericUpDown1.TabIndex = 25;
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(9, 4);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 13);
            this.label20.TabIndex = 18;
            this.label20.Text = "Learning rate";
            // 
            // ContentBased_Momentum
            // 
            this.ContentBased_Momentum.DecimalPlaces = 2;
            this.ContentBased_Momentum.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.ContentBased_Momentum.Location = new System.Drawing.Point(133, 28);
            this.ContentBased_Momentum.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ContentBased_Momentum.Name = "ContentBased_Momentum";
            this.ContentBased_Momentum.Size = new System.Drawing.Size(150, 20);
            this.ContentBased_Momentum.TabIndex = 20;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 81);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(50, 13);
            this.label23.TabIndex = 24;
            this.label23.Text = "Iterations";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(9, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 13);
            this.label18.TabIndex = 16;
            this.label18.Text = "Momentum";
            // 
            // ContentBased_GeneticPanel
            // 
            this.ContentBased_GeneticPanel.Controls.Add(this.label22);
            this.ContentBased_GeneticPanel.Controls.Add(this.ContentBased_PopulationSize);
            this.ContentBased_GeneticPanel.Controls.Add(this.numericUpDown3);
            this.ContentBased_GeneticPanel.Controls.Add(this.label24);
            this.ContentBased_GeneticPanel.Controls.Add(this.label25);
            this.ContentBased_GeneticPanel.Location = new System.Drawing.Point(3, 61);
            this.ContentBased_GeneticPanel.Name = "ContentBased_GeneticPanel";
            this.ContentBased_GeneticPanel.Size = new System.Drawing.Size(303, 24);
            this.ContentBased_GeneticPanel.TabIndex = 28;
            this.ContentBased_GeneticPanel.Visible = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label22.Location = new System.Drawing.Point(59, 85);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(44, 9);
            this.label22.TabIndex = 26;
            this.label22.Text = "(0 - infinity)";
            // 
            // ContentBased_PopulationSize
            // 
            this.ContentBased_PopulationSize.Location = new System.Drawing.Point(133, 1);
            this.ContentBased_PopulationSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ContentBased_PopulationSize.Name = "ContentBased_PopulationSize";
            this.ContentBased_PopulationSize.Size = new System.Drawing.Size(150, 20);
            this.ContentBased_PopulationSize.TabIndex = 22;
            this.ContentBased_PopulationSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown3.Location = new System.Drawing.Point(127, 81);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(150, 20);
            this.numericUpDown3.TabIndex = 25;
            this.numericUpDown3.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(9, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(78, 13);
            this.label24.TabIndex = 18;
            this.label24.Text = "Population size";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(3, 81);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(50, 13);
            this.label25.TabIndex = 24;
            this.label25.Text = "Iterations";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 104);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 13);
            this.label17.TabIndex = 27;
            this.label17.Text = "Teacher";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(102, 13);
            this.label19.TabIndex = 17;
            this.label19.Text = "Sigmoid alpha value";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label16.Location = new System.Drawing.Point(64, 78);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 9);
            this.label16.TabIndex = 26;
            this.label16.Text = "(0 - infinity)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 53);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Hidden layer neurons";
            // 
            // ContentBased_Iterations
            // 
            this.ContentBased_Iterations.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ContentBased_Iterations.Location = new System.Drawing.Point(139, 75);
            this.ContentBased_Iterations.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.ContentBased_Iterations.Name = "ContentBased_Iterations";
            this.ContentBased_Iterations.Size = new System.Drawing.Size(150, 20);
            this.ContentBased_Iterations.TabIndex = 25;
            this.ContentBased_Iterations.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // ContentBased_SigmoidAlpha
            // 
            this.ContentBased_SigmoidAlpha.DecimalPlaces = 2;
            this.ContentBased_SigmoidAlpha.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.ContentBased_SigmoidAlpha.Location = new System.Drawing.Point(139, 23);
            this.ContentBased_SigmoidAlpha.Name = "ContentBased_SigmoidAlpha";
            this.ContentBased_SigmoidAlpha.Size = new System.Drawing.Size(150, 20);
            this.ContentBased_SigmoidAlpha.TabIndex = 21;
            this.ContentBased_SigmoidAlpha.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Iterations";
            // 
            // ContentBased_HiddenLayerNeurons
            // 
            this.ContentBased_HiddenLayerNeurons.Location = new System.Drawing.Point(139, 49);
            this.ContentBased_HiddenLayerNeurons.Name = "ContentBased_HiddenLayerNeurons";
            this.ContentBased_HiddenLayerNeurons.Size = new System.Drawing.Size(150, 20);
            this.ContentBased_HiddenLayerNeurons.TabIndex = 23;
            this.ContentBased_HiddenLayerNeurons.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // ContentBasedOptions
            // 
            this.ContentBasedOptions.Controls.Add(this.ContentBased_MinFeatures);
            this.ContentBasedOptions.Controls.Add(this.label14);
            this.ContentBasedOptions.Controls.Add(this.ContentBased_MinimumItemsRated);
            this.ContentBasedOptions.Controls.Add(this.label12);
            this.ContentBasedOptions.Controls.Add(this.label4);
            this.ContentBasedOptions.Controls.Add(this.ContentBased_AmountOfUsers);
            this.ContentBasedOptions.Controls.Add(this.UserLabel);
            this.ContentBasedOptions.Location = new System.Drawing.Point(336, 3);
            this.ContentBasedOptions.Name = "ContentBasedOptions";
            this.ContentBasedOptions.Size = new System.Drawing.Size(327, 116);
            this.ContentBasedOptions.TabIndex = 0;
            // 
            // ContentBased_MinFeatures
            // 
            this.ContentBased_MinFeatures.Location = new System.Drawing.Point(139, 75);
            this.ContentBased_MinFeatures.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ContentBased_MinFeatures.Name = "ContentBased_MinFeatures";
            this.ContentBased_MinFeatures.Size = new System.Drawing.Size(150, 20);
            this.ContentBased_MinFeatures.TabIndex = 13;
            this.ContentBased_MinFeatures.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 78);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(112, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "Min repeating features";
            // 
            // ContentBased_MinimumItemsRated
            // 
            this.ContentBased_MinimumItemsRated.Location = new System.Drawing.Point(139, 49);
            this.ContentBased_MinimumItemsRated.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.ContentBased_MinimumItemsRated.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ContentBased_MinimumItemsRated.Name = "ContentBased_MinimumItemsRated";
            this.ContentBased_MinimumItemsRated.Size = new System.Drawing.Size(150, 20);
            this.ContentBased_MinimumItemsRated.TabIndex = 4;
            this.ContentBased_MinimumItemsRated.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Minimum items rated";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Location = new System.Drawing.Point(15, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Content-Based Filtering Options";
            // 
            // ContentBased_AmountOfUsers
            // 
            this.ContentBased_AmountOfUsers.Location = new System.Drawing.Point(139, 23);
            this.ContentBased_AmountOfUsers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ContentBased_AmountOfUsers.Name = "ContentBased_AmountOfUsers";
            this.ContentBased_AmountOfUsers.Size = new System.Drawing.Size(150, 20);
            this.ContentBased_AmountOfUsers.TabIndex = 1;
            this.ContentBased_AmountOfUsers.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(15, 25);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(84, 13);
            this.UserLabel.TabIndex = 0;
            this.UserLabel.Text = "Number of users";
            // 
            // CollaborativeOptions
            // 
            this.CollaborativeOptions.Controls.Add(this.label5);
            this.CollaborativeOptions.Location = new System.Drawing.Point(3, 199);
            this.CollaborativeOptions.Name = "CollaborativeOptions";
            this.CollaborativeOptions.Size = new System.Drawing.Size(327, 51);
            this.CollaborativeOptions.TabIndex = 1;
            this.CollaborativeOptions.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label5.Location = new System.Drawing.Point(18, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Collaborative Filtering Options";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.TestingSetSize);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.TrainingSetSize);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(336, 199);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 73);
            this.panel1.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(279, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "%";
            // 
            // TestingSetSize
            // 
            this.TestingSetSize.Location = new System.Drawing.Point(139, 49);
            this.TestingSetSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.TestingSetSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TestingSetSize.Name = "TestingSetSize";
            this.TestingSetSize.Size = new System.Drawing.Size(134, 20);
            this.TestingSetSize.TabIndex = 6;
            this.TestingSetSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TestingSetSize.ValueChanged += new System.EventHandler(this.TestingSetSize_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Testing set size";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(279, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "%";
            // 
            // TrainingSetSize
            // 
            this.TrainingSetSize.Location = new System.Drawing.Point(139, 22);
            this.TrainingSetSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.TrainingSetSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TrainingSetSize.Name = "TrainingSetSize";
            this.TrainingSetSize.Size = new System.Drawing.Size(134, 20);
            this.TrainingSetSize.TabIndex = 3;
            this.TrainingSetSize.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TrainingSetSize.ValueChanged += new System.EventHandler(this.TrainingSetSize_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Training set size";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(15, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "General options";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new System.Drawing.Size(690, 0);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.ResultContainer);
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(707, 555);
            this.panel2.TabIndex = 8;
            // 
            // dataSetOptionBindingSource
            // 
            this.dataSetOptionBindingSource.DataSource = typeof(Recommender.GUI.Options.DataSetOption);
            // 
            // mainFormBindingSource
            // 
            this.mainFormBindingSource.DataSource = typeof(Recommender.GUI.MainForm);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 612);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "MainForm";
            this.Text = "Recommender Systems Research";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ContentBasedAlghoritmChoice.ResumeLayout(false);
            this.ContentBasedAlghoritmChoice.PerformLayout();
            this.CollaborativeAlgorithmChoice.ResumeLayout(false);
            this.CollaborativeAlgorithmChoice.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResultContainer.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ContentBased_BackPropPanel.ResumeLayout(false);
            this.ContentBased_BackPropPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_LearningRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_Momentum)).EndInit();
            this.ContentBased_GeneticPanel.ResumeLayout(false);
            this.ContentBased_GeneticPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_PopulationSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_Iterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_SigmoidAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_HiddenLayerNeurons)).EndInit();
            this.ContentBasedOptions.ResumeLayout(false);
            this.ContentBasedOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_MinFeatures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_MinimumItemsRated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_AmountOfUsers)).EndInit();
            this.CollaborativeOptions.ResumeLayout(false);
            this.CollaborativeOptions.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TestingSetSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainingSetSize)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetOptionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox recommenderCombo;
        private System.Windows.Forms.ComboBox datasetCombo;
        private System.Windows.Forms.GroupBox ResultContainer;
        private System.Windows.Forms.RichTextBox ResultBox;
        private System.Windows.Forms.ToolStripMenuItem opcjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zakończToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcja1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcja1aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcja1bToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcja2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.BindingSource mainFormBindingSource;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel CollaborativeAlgorithmChoice;
        private System.Windows.Forms.ComboBox CollaborativeAlgorithmCombo;
        private System.Windows.Forms.ComboBox ContentBasedAlgorithmCombo;
        private System.Windows.Forms.Panel ContentBasedAlghoritmChoice;
        private System.Windows.Forms.BindingSource dataSetOptionBindingSource;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel ContentBasedOptions;
        private System.Windows.Forms.Panel CollaborativeOptions;
        private System.Windows.Forms.NumericUpDown ContentBased_AmountOfUsers;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown TrainingSetSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown TestingSetSize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown ContentBased_MinimumItemsRated;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown ContentBased_MinFeatures;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown ContentBased_HiddenLayerNeurons;
        private System.Windows.Forms.NumericUpDown ContentBased_LearningRate;
        private System.Windows.Forms.NumericUpDown ContentBased_SigmoidAlpha;
        private System.Windows.Forms.NumericUpDown ContentBased_Momentum;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown ContentBased_Iterations;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel ContentBased_BackPropPanel;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox ContentBased_Teacher;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.NumericUpDown ContentBased_PopulationSize;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel ContentBased_GeneticPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
    }
}

