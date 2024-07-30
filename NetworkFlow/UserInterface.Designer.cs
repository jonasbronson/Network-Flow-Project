namespace NetworkFlow
{
    partial class UserInterface
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UxSolveButton = new System.Windows.Forms.Button();
            this.UxSourceLabel = new System.Windows.Forms.Label();
            this.UxSourceList = new System.Windows.Forms.ListBox();
            this.UxDestinationLabel = new System.Windows.Forms.Label();
            this.UxDestinationList = new System.Windows.Forms.ListBox();
            this.UxFlowText = new System.Windows.Forms.TextBox();
            this.UxGraphText = new System.Windows.Forms.TextBox();
            this.UxOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.UxLoadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UxSolveButton
            // 
            this.UxSolveButton.Location = new System.Drawing.Point(39, 90);
            this.UxSolveButton.Name = "UxSolveButton";
            this.UxSolveButton.Size = new System.Drawing.Size(75, 23);
            this.UxSolveButton.TabIndex = 1;
            this.UxSolveButton.Text = "Solve";
            this.UxSolveButton.UseVisualStyleBackColor = true;
            this.UxSolveButton.Click += new System.EventHandler(this.UxSolveButtonClick);
            // 
            // UxSourceLabel
            // 
            this.UxSourceLabel.AutoSize = true;
            this.UxSourceLabel.Location = new System.Drawing.Point(159, 71);
            this.UxSourceLabel.Name = "UxSourceLabel";
            this.UxSourceLabel.Size = new System.Drawing.Size(43, 15);
            this.UxSourceLabel.TabIndex = 2;
            this.UxSourceLabel.Text = "Source";
            // 
            // UxSourceList
            // 
            this.UxSourceList.FormattingEnabled = true;
            this.UxSourceList.ItemHeight = 15;
            this.UxSourceList.Location = new System.Drawing.Point(159, 101);
            this.UxSourceList.Name = "UxSourceList";
            this.UxSourceList.Size = new System.Drawing.Size(120, 34);
            this.UxSourceList.TabIndex = 3;
            // 
            // UxDestinationLabel
            // 
            this.UxDestinationLabel.AutoSize = true;
            this.UxDestinationLabel.Location = new System.Drawing.Point(318, 71);
            this.UxDestinationLabel.Name = "UxDestinationLabel";
            this.UxDestinationLabel.Size = new System.Drawing.Size(67, 15);
            this.UxDestinationLabel.TabIndex = 4;
            this.UxDestinationLabel.Text = "Destination";
            // 
            // UxDestinationList
            // 
            this.UxDestinationList.FormattingEnabled = true;
            this.UxDestinationList.ItemHeight = 15;
            this.UxDestinationList.Location = new System.Drawing.Point(318, 101);
            this.UxDestinationList.Name = "UxDestinationList";
            this.UxDestinationList.Size = new System.Drawing.Size(120, 34);
            this.UxDestinationList.TabIndex = 5;
            // 
            // UxFlowText
            // 
            this.UxFlowText.Location = new System.Drawing.Point(476, 103);
            this.UxFlowText.Name = "UxFlowText";
            this.UxFlowText.Size = new System.Drawing.Size(267, 23);
            this.UxFlowText.TabIndex = 6;
            // 
            // UxGraphText
            // 
            this.UxGraphText.Location = new System.Drawing.Point(39, 163);
            this.UxGraphText.Multiline = true;
            this.UxGraphText.Name = "UxGraphText";
            this.UxGraphText.Size = new System.Drawing.Size(704, 275);
            this.UxGraphText.TabIndex = 7;
            // 
            // UxLoadButton
            // 
            this.UxLoadButton.Location = new System.Drawing.Point(39, 52);
            this.UxLoadButton.Name = "UxLoadButton";
            this.UxLoadButton.Size = new System.Drawing.Size(75, 23);
            this.UxLoadButton.TabIndex = 8;
            this.UxLoadButton.Text = "LOAD";
            this.UxLoadButton.UseVisualStyleBackColor = true;
            this.UxLoadButton.Click += new System.EventHandler(this.UxLoadButtonClick);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UxLoadButton);
            this.Controls.Add(this.UxGraphText);
            this.Controls.Add(this.UxFlowText);
            this.Controls.Add(this.UxDestinationList);
            this.Controls.Add(this.UxDestinationLabel);
            this.Controls.Add(this.UxSourceList);
            this.Controls.Add(this.UxSourceLabel);
            this.Controls.Add(this.UxSolveButton);
            this.Name = "UserInterface";
            this.Text = "Flow Network";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button UxSolveButton;
        private Label UxSourceLabel;
        private ListBox UxSourceList;
        private Label UxDestinationLabel;
        private ListBox UxDestinationList;
        private TextBox UxFlowText;
        private TextBox UxGraphText;
        private OpenFileDialog UxOpenFileDialog;
        private Button UxLoadButton;
    }
}