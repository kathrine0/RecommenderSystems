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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ContentBasedAlghoritmChoice = new System.Windows.Forms.Panel();
            this.ContentBasedAlgorithmCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CollaborativeAlgorithmChoice = new System.Windows.Forms.Panel();
            this.CollaborativeAlgorithmCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
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
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ResultContainer = new System.Windows.Forms.GroupBox();
            this.ResultBox = new System.Windows.Forms.RichTextBox();
            this.Options = new System.Windows.Forms.GroupBox();
            this.dataSetOptionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.ContentBasedAlghoritmChoice.SuspendLayout();
            this.CollaborativeAlgorithmChoice.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ResultContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetOptionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ContentBasedAlghoritmChoice);
            this.groupBox1.Controls.Add(this.CollaborativeAlgorithmChoice);
            this.groupBox1.Controls.Add(this.SaveButton);
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
            this.ContentBasedAlghoritmChoice.Enabled = false;
            this.ContentBasedAlghoritmChoice.Location = new System.Drawing.Point(0, 114);
            this.ContentBasedAlghoritmChoice.Name = "ContentBasedAlghoritmChoice";
            this.ContentBasedAlghoritmChoice.Size = new System.Drawing.Size(280, 22);
            this.ContentBasedAlghoritmChoice.TabIndex = 12;
            // 
            // ContentBasedAlgorithmCombo
            // 
            this.ContentBasedAlgorithmCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ContentBasedAlgorithmCombo.FormattingEnabled = true;
            this.ContentBasedAlgorithmCombo.Location = new System.Drawing.Point(130, 1);
            this.ContentBasedAlgorithmCombo.Name = "ContentBasedAlgorithmCombo";
            this.ContentBasedAlgorithmCombo.Size = new System.Drawing.Size(150, 21);
            this.ContentBasedAlgorithmCombo.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Content-based algorithm";
            // 
            // CollaborativeAlgorithmChoice
            // 
            this.CollaborativeAlgorithmChoice.Controls.Add(this.CollaborativeAlgorithmCombo);
            this.CollaborativeAlgorithmChoice.Controls.Add(this.label3);
            this.CollaborativeAlgorithmChoice.Location = new System.Drawing.Point(0, 84);
            this.CollaborativeAlgorithmChoice.Name = "CollaborativeAlgorithmChoice";
            this.CollaborativeAlgorithmChoice.Size = new System.Drawing.Size(280, 28);
            this.CollaborativeAlgorithmChoice.TabIndex = 11;
            // 
            // CollaborativeAlgorithmCombo
            // 
            this.CollaborativeAlgorithmCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CollaborativeAlgorithmCombo.FormattingEnabled = true;
            this.CollaborativeAlgorithmCombo.Location = new System.Drawing.Point(130, 3);
            this.CollaborativeAlgorithmCombo.Name = "CollaborativeAlgorithmCombo";
            this.CollaborativeAlgorithmCombo.Size = new System.Drawing.Size(150, 21);
            this.CollaborativeAlgorithmCombo.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Collaborative algorithm";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(125, 225);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(93, 23);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "Save results";
            this.SaveButton.UseVisualStyleBackColor = true;
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
            this.RunButton.Size = new System.Drawing.Size(113, 23);
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
            this.menuStrip1.Size = new System.Drawing.Size(535, 24);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 433);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(535, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(26, 17);
            this.StatusLabel.Text = "Idle";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // ResultContainer
            // 
            this.ResultContainer.Controls.Add(this.ResultBox);
            this.ResultContainer.Location = new System.Drawing.Point(314, 38);
            this.ResultContainer.Name = "ResultContainer";
            this.ResultContainer.Size = new System.Drawing.Size(215, 254);
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
            this.ResultBox.Size = new System.Drawing.Size(209, 235);
            this.ResultBox.TabIndex = 0;
            this.ResultBox.Text = "";
            // 
            // Options
            // 
            this.Options.Location = new System.Drawing.Point(12, 298);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(516, 127);
            this.Options.TabIndex = 5;
            this.Options.TabStop = false;
            this.Options.Text = "Options";
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
            this.ClientSize = new System.Drawing.Size(535, 455);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.ResultContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
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
        private System.Windows.Forms.GroupBox Options;
        private System.Windows.Forms.ToolStripMenuItem opcjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zakończToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcja1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcja1aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcja1bToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcja2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.BindingSource mainFormBindingSource;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel CollaborativeAlgorithmChoice;
        private System.Windows.Forms.ComboBox CollaborativeAlgorithmCombo;
        private System.Windows.Forms.ComboBox ContentBasedAlgorithmCombo;
        private System.Windows.Forms.Panel ContentBasedAlghoritmChoice;
        private System.Windows.Forms.BindingSource dataSetOptionBindingSource;
    }
}

