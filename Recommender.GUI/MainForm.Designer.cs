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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ResultContainer = new System.Windows.Forms.GroupBox();
            this.ResultBox = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ContentBasedOptions = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.ContentBased_AmountOfUsers = new System.Windows.Forms.NumericUpDown();
            this.UserLabel = new System.Windows.Forms.Label();
            this.CollaborativeOptions = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TrainingSetSize = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TestingSetSize = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ContentBased_MinimumItemsRated = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.ContentBased_ActivationFunction = new System.Windows.Forms.ComboBox();
            this.dataSetOptionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.ContentBasedAlghoritmChoice.SuspendLayout();
            this.CollaborativeAlgorithmChoice.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ResultContainer.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.ContentBasedOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_AmountOfUsers)).BeginInit();
            this.CollaborativeOptions.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrainingSetSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestingSetSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_MinimumItemsRated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetOptionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ContentBasedAlghoritmChoice);
            this.groupBox1.Controls.Add(this.CollaborativeAlgorithmChoice);
            this.groupBox1.Controls.Add(this.CancelButton);
            this.groupBox1.Controls.Add(this.recommenderCombo);
            this.groupBox1.Controls.Add(this.datasetCombo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.RunButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
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
            this.menuStrip1.Size = new System.Drawing.Size(673, 24);
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
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBar,
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 555);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(673, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
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
            this.ResultContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultContainer.AutoSize = true;
            this.ResultContainer.Controls.Add(this.ResultBox);
            this.ResultContainer.Location = new System.Drawing.Point(314, 38);
            this.ResultContainer.Name = "ResultContainer";
            this.ResultContainer.Size = new System.Drawing.Size(349, 254);
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
            this.ResultBox.Size = new System.Drawing.Size(343, 235);
            this.ResultBox.TabIndex = 0;
            this.ResultBox.Text = "";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ContentBasedOptions);
            this.flowLayoutPanel1.Controls.Add(this.CollaborativeOptions);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 298);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(673, 254);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // ContentBasedOptions
            // 
            this.ContentBasedOptions.Controls.Add(this.ContentBased_ActivationFunction);
            this.ContentBasedOptions.Controls.Add(this.label13);
            this.ContentBasedOptions.Controls.Add(this.ContentBased_MinimumItemsRated);
            this.ContentBasedOptions.Controls.Add(this.label12);
            this.ContentBasedOptions.Controls.Add(this.label4);
            this.ContentBasedOptions.Controls.Add(this.ContentBased_AmountOfUsers);
            this.ContentBasedOptions.Controls.Add(this.UserLabel);
            this.ContentBasedOptions.Location = new System.Drawing.Point(3, 3);
            this.ContentBasedOptions.Name = "ContentBasedOptions";
            this.ContentBasedOptions.Size = new System.Drawing.Size(308, 130);
            this.ContentBasedOptions.TabIndex = 0;
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
            this.CollaborativeOptions.Location = new System.Drawing.Point(317, 3);
            this.CollaborativeOptions.Name = "CollaborativeOptions";
            this.CollaborativeOptions.Size = new System.Drawing.Size(346, 130);
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
            this.panel1.Location = new System.Drawing.Point(3, 139);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 100);
            this.panel1.TabIndex = 2;
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Training set size";
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(279, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "%";
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(279, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "%";
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Activation function";
            // 
            // ContentBased_ActivationFunction
            // 
            this.ContentBased_ActivationFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ContentBased_ActivationFunction.FormattingEnabled = true;
            this.ContentBased_ActivationFunction.Location = new System.Drawing.Point(139, 75);
            this.ContentBased_ActivationFunction.Name = "ContentBased_ActivationFunction";
            this.ContentBased_ActivationFunction.Size = new System.Drawing.Size(150, 21);
            this.ContentBased_ActivationFunction.TabIndex = 11;
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
            this.ClientSize = new System.Drawing.Size(673, 577);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.ResultContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResultContainer.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ContentBasedOptions.ResumeLayout(false);
            this.ContentBasedOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_AmountOfUsers)).EndInit();
            this.CollaborativeOptions.ResumeLayout(false);
            this.CollaborativeOptions.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrainingSetSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestingSetSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContentBased_MinimumItemsRated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetOptionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
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
        private System.Windows.Forms.ComboBox ContentBased_ActivationFunction;
        private System.Windows.Forms.Label label13;
    }
}

