// Copyright © 2007 Triamec Motion AG

namespace Triamec.Tam.Samples {
	partial class GearUpForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.MenuStrip menuStrip;
			System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem exitMenuItem;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GearUpForm));
			System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem explorerMenuItem;
			System.Windows.Forms.ToolTip toolTip;
			System.Windows.Forms.GroupBox driveGroupBox;
			System.Windows.Forms.GroupBox motionGroupBox;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.GroupBox couplingGroupBox;
			this._moveNegativeButton = new System.Windows.Forms.Button();
			this._enableButton = new System.Windows.Forms.Button();
			this._disableButton = new System.Windows.Forms.Button();
			this._movePositiveButton = new System.Windows.Forms.Button();
			this._velocityTrackBar = new System.Windows.Forms.TrackBar();
			this._coupleButton = new System.Windows.Forms.Button();
			this._decoupleButton = new System.Windows.Forms.Button();
			menuStrip = new System.Windows.Forms.MenuStrip();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			explorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolTip = new System.Windows.Forms.ToolTip(this.components);
			driveGroupBox = new System.Windows.Forms.GroupBox();
			motionGroupBox = new System.Windows.Forms.GroupBox();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			couplingGroupBox = new System.Windows.Forms.GroupBox();
			menuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._velocityTrackBar)).BeginInit();
			driveGroupBox.SuspendLayout();
			motionGroupBox.SuspendLayout();
			couplingGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			fileToolStripMenuItem,
			viewToolStripMenuItem});
			resources.ApplyResources(menuStrip, "menuStrip");
			menuStrip.Name = "menuStrip";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			exitMenuItem});
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
			// 
			// exitMenuItem
			// 
			exitMenuItem.Name = "exitMenuItem";
			resources.ApplyResources(exitMenuItem, "exitMenuItem");
			exitMenuItem.Click += new System.EventHandler(this.Shutdown);
			// 
			// viewToolStripMenuItem
			// 
			viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			explorerMenuItem});
			viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			resources.ApplyResources(viewToolStripMenuItem, "viewToolStripMenuItem");
			// 
			// explorerMenuItem
			// 
			explorerMenuItem.CheckOnClick = true;
			explorerMenuItem.Name = "explorerMenuItem";
			resources.ApplyResources(explorerMenuItem, "explorerMenuItem");
			explorerMenuItem.Click += new System.EventHandler(this.OnExplorerMenuItemClick);
			// 
			// _moveNegativeButton
			// 
			resources.ApplyResources(this._moveNegativeButton, "_moveNegativeButton");
			this._moveNegativeButton.Name = "_moveNegativeButton";
			toolTip.SetToolTip(this._moveNegativeButton, resources.GetString("_moveNegativeButton.ToolTip"));
			this._moveNegativeButton.UseVisualStyleBackColor = true;
			this._moveNegativeButton.Click += new System.EventHandler(this.OnMoveNegativeButtonClick);
			// 
			// _enableButton
			// 
			resources.ApplyResources(this._enableButton, "_enableButton");
			this._enableButton.Name = "_enableButton";
			toolTip.SetToolTip(this._enableButton, resources.GetString("_enableButton.ToolTip"));
			this._enableButton.UseVisualStyleBackColor = true;
			this._enableButton.Click += new System.EventHandler(this.OnEnableButtonClick);
			// 
			// _disableButton
			// 
			resources.ApplyResources(this._disableButton, "_disableButton");
			this._disableButton.Name = "_disableButton";
			toolTip.SetToolTip(this._disableButton, resources.GetString("_disableButton.ToolTip"));
			this._disableButton.UseVisualStyleBackColor = true;
			this._disableButton.Click += new System.EventHandler(this.OnDisableButtonClick);
			// 
			// _movePositiveButton
			// 
			resources.ApplyResources(this._movePositiveButton, "_movePositiveButton");
			this._movePositiveButton.Name = "_movePositiveButton";
			toolTip.SetToolTip(this._movePositiveButton, resources.GetString("_movePositiveButton.ToolTip"));
			this._movePositiveButton.UseVisualStyleBackColor = true;
			this._movePositiveButton.Click += new System.EventHandler(this.OnMovePositiveButtonClick);
			// 
			// _velocityTrackBar
			// 
			resources.ApplyResources(this._velocityTrackBar, "_velocityTrackBar");
			this._velocityTrackBar.LargeChange = 10;
			this._velocityTrackBar.Maximum = 100;
			this._velocityTrackBar.Minimum = 10;
			this._velocityTrackBar.Name = "_velocityTrackBar";
			this._velocityTrackBar.TickFrequency = 5;
			toolTip.SetToolTip(this._velocityTrackBar, resources.GetString("_velocityTrackBar.ToolTip"));
			this._velocityTrackBar.Value = 100;
			// 
			// _coupleButton
			// 
			resources.ApplyResources(this._coupleButton, "_coupleButton");
			this._coupleButton.Name = "_coupleButton";
			toolTip.SetToolTip(this._coupleButton, resources.GetString("_coupleButton.ToolTip"));
			this._coupleButton.UseVisualStyleBackColor = true;
			this._coupleButton.Click += new System.EventHandler(this.OnCoupleButtonClick);
			// 
			// _decoupleButton
			// 
			resources.ApplyResources(this._decoupleButton, "_decoupleButton");
			this._decoupleButton.Name = "_decoupleButton";
			toolTip.SetToolTip(this._decoupleButton, resources.GetString("_decoupleButton.ToolTip"));
			this._decoupleButton.UseVisualStyleBackColor = true;
			this._decoupleButton.Click += new System.EventHandler(this.OnDecoupleButtonClick);
			// 
			// driveGroupBox
			// 
			driveGroupBox.Controls.Add(this._enableButton);
			driveGroupBox.Controls.Add(this._disableButton);
			resources.ApplyResources(driveGroupBox, "driveGroupBox");
			driveGroupBox.Name = "driveGroupBox";
			driveGroupBox.TabStop = false;
			// 
			// motionGroupBox
			// 
			motionGroupBox.Controls.Add(label3);
			motionGroupBox.Controls.Add(label2);
			motionGroupBox.Controls.Add(label1);
			motionGroupBox.Controls.Add(this._velocityTrackBar);
			motionGroupBox.Controls.Add(this._moveNegativeButton);
			motionGroupBox.Controls.Add(this._movePositiveButton);
			resources.ApplyResources(motionGroupBox, "motionGroupBox");
			motionGroupBox.Name = "motionGroupBox";
			motionGroupBox.TabStop = false;
			// 
			// label3
			// 
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			// 
			// label2
			// 
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			// 
			// couplingGroupBox
			// 
			couplingGroupBox.Controls.Add(this._coupleButton);
			couplingGroupBox.Controls.Add(this._decoupleButton);
			resources.ApplyResources(couplingGroupBox, "couplingGroupBox");
			couplingGroupBox.Name = "couplingGroupBox";
			couplingGroupBox.TabStop = false;
			// 
			// GearUpForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(couplingGroupBox);
			this.Controls.Add(motionGroupBox);
			this.Controls.Add(driveGroupBox);
			this.Controls.Add(menuStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = menuStrip;
			this.Name = "GearUpForm";
			this.Load += new System.EventHandler(this.GearUpForm_Load);
			menuStrip.ResumeLayout(false);
			menuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._velocityTrackBar)).EndInit();
			driveGroupBox.ResumeLayout(false);
			motionGroupBox.ResumeLayout(false);
			motionGroupBox.PerformLayout();
			couplingGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _moveNegativeButton;
		private System.Windows.Forms.Button _enableButton;
		private System.Windows.Forms.Button _disableButton;
		private System.Windows.Forms.Button _movePositiveButton;
		private System.Windows.Forms.TrackBar _velocityTrackBar;
		private System.Windows.Forms.Button _coupleButton;
		private System.Windows.Forms.Button _decoupleButton;
	}
}

