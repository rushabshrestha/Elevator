#nullable disable
namespace ElevatorApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.btnReconnect = new System.Windows.Forms.Button();
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.lblHallCaption = new System.Windows.Forms.Label();
            this.btnHall1 = new System.Windows.Forms.Button();
            this.btnHall0 = new System.Windows.Forms.Button();
            this.shaftA = new ShaftView();
            this.interiorA = new InteriorView();
            this.panelA = new System.Windows.Forms.Panel();
            this.lblPanelTitleA = new System.Windows.Forms.Label();
            this.lblDisplayA = new System.Windows.Forms.Label();
            this.btnFloor0A = new System.Windows.Forms.Button();
            this.btnFloor1A = new System.Windows.Forms.Button();
            this.btnOpenA = new System.Windows.Forms.Button();
            this.btnCloseA = new System.Windows.Forms.Button();
            this.btnSosA = new System.Windows.Forms.Button();
            this.lblDoorTimerA = new System.Windows.Forms.Label();
            this.lblStateA = new System.Windows.Forms.Label();
            this.shaftB = new ShaftView();
            this.interiorB = new InteriorView();
            this.panelB = new System.Windows.Forms.Panel();
            this.lblPanelTitleB = new System.Windows.Forms.Label();
            this.lblDisplayB = new System.Windows.Forms.Label();
            this.btnFloor0B = new System.Windows.Forms.Button();
            this.btnFloor1B = new System.Windows.Forms.Button();
            this.btnOpenB = new System.Windows.Forms.Button();
            this.btnCloseB = new System.Windows.Forms.Button();
            this.btnSosB = new System.Windows.Forms.Button();
            this.lblDoorTimerB = new System.Windows.Forms.Label();
            this.lblStateB = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.panelA.SuspendLayout();
            this.panelB.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblClock);
            this.pnlHeader.Controls.Add(this.btnReconnect);
            this.pnlHeader.Controls.Add(this.btnOpenLog);
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(28, 32, 46);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(1070, 60);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 6);
            this.lblTitle.Text = "ELEVATOR CONTROL SYSTEM";
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 1;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(160, 170, 195);
            this.lblSubtitle.Location = new System.Drawing.Point(21, 38);
            this.lblSubtitle.Text = "Two-floor office building";
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.TabIndex = 2;
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = false;
            this.lblClock.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClock.ForeColor = System.Drawing.Color.FromArgb(120, 255, 150);
            this.lblClock.Location = new System.Drawing.Point(380, 17);
            this.lblClock.Size = new System.Drawing.Size(330, 28);
            this.lblClock.Text = "";
            this.lblClock.Name = "lblClock";
            this.lblClock.TabIndex = 3;
            // 
            // btnReconnect
            // 
            this.btnReconnect.BackColor = System.Drawing.Color.FromArgb(52, 90, 160);
            this.btnReconnect.ForeColor = System.Drawing.Color.White;
            this.btnReconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReconnect.Location = new System.Drawing.Point(885, 14);
            this.btnReconnect.Size = new System.Drawing.Size(170, 32);
            this.btnReconnect.Text = "Reconnect database";
            this.btnReconnect.UseVisualStyleBackColor = false;
            this.btnReconnect.FlatAppearance.BorderSize = 0;
            this.btnReconnect.Name = "btnReconnect";
            this.btnReconnect.TabIndex = 4;
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.BackColor = System.Drawing.Color.FromArgb(40, 120, 100);
            this.btnOpenLog.ForeColor = System.Drawing.Color.White;
            this.btnOpenLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenLog.Location = new System.Drawing.Point(725, 14);
            this.btnOpenLog.Size = new System.Drawing.Size(150, 32);
            this.btnOpenLog.Text = "View event log";
            this.btnOpenLog.UseVisualStyleBackColor = false;
            this.btnOpenLog.FlatAppearance.BorderSize = 0;
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.TabIndex = 60;
            // 
            // lblHallCaption
            // 
            this.lblHallCaption.AutoSize = true;
            this.lblHallCaption.ForeColor = System.Drawing.Color.DimGray;
            this.lblHallCaption.Location = new System.Drawing.Point(15, 72);
            this.lblHallCaption.Text = "Request buttons";
            this.lblHallCaption.Name = "lblHallCaption";
            this.lblHallCaption.TabIndex = 5;
            // 
            // btnHall1
            // 
            this.btnHall1.BackColor = System.Drawing.Color.Gainsboro;
            this.btnHall1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHall1.Location = new System.Drawing.Point(15, 110);
            this.btnHall1.Size = new System.Drawing.Size(125, 50);
            this.btnHall1.Text = "Request\r\nFloor 1";
            this.btnHall1.UseVisualStyleBackColor = false;
            this.btnHall1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnHall1.Name = "btnHall1";
            this.btnHall1.TabIndex = 6;
            // 
            // btnHall0
            // 
            this.btnHall0.BackColor = System.Drawing.Color.Gainsboro;
            this.btnHall0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHall0.Location = new System.Drawing.Point(15, 300);
            this.btnHall0.Size = new System.Drawing.Size(125, 50);
            this.btnHall0.Text = "Request\r\nFloor 0";
            this.btnHall0.UseVisualStyleBackColor = false;
            this.btnHall0.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnHall0.Name = "btnHall0";
            this.btnHall0.TabIndex = 7;
            // 
            // shaftA
            // 
            this.shaftA.Location = new System.Drawing.Point(155, 75);
            this.shaftA.Size = new System.Drawing.Size(155, 340);
            this.shaftA.Title = "Elevator A";
            this.shaftA.Name = "shaftA";
            this.shaftA.TabIndex = 8;
            // 
            // interiorA
            // 
            this.interiorA.Location = new System.Drawing.Point(315, 75);
            this.interiorA.Size = new System.Drawing.Size(255, 340);
            this.interiorA.Title = "Elevator A - inside";
            this.interiorA.Name = "interiorA";
            this.interiorA.TabIndex = 9;
            // 
            // panelA
            // 
            this.panelA.Controls.Add(this.lblPanelTitleA);
            this.panelA.Controls.Add(this.lblDisplayA);
            this.panelA.Controls.Add(this.btnFloor0A);
            this.panelA.Controls.Add(this.btnFloor1A);
            this.panelA.Controls.Add(this.btnOpenA);
            this.panelA.Controls.Add(this.btnCloseA);
            this.panelA.Controls.Add(this.btnSosA);
            this.panelA.Controls.Add(this.lblDoorTimerA);
            this.panelA.Controls.Add(this.lblStateA);
            this.panelA.BackColor = System.Drawing.Color.White;
            this.panelA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelA.Location = new System.Drawing.Point(155, 425);
            this.panelA.Size = new System.Drawing.Size(415, 150);
            this.panelA.Name = "panelA";
            this.panelA.TabIndex = 10;
            // 
            // lblPanelTitleA
            // 
            this.lblPanelTitleA.AutoSize = true;
            this.lblPanelTitleA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPanelTitleA.Location = new System.Drawing.Point(10, 6);
            this.lblPanelTitleA.Text = "Elevator A - control panel";
            this.lblPanelTitleA.Name = "lblPanelTitleA";
            this.lblPanelTitleA.TabIndex = 11;
            // 
            // lblDisplayA
            // 
            this.lblDisplayA.AutoSize = false;
            this.lblDisplayA.BackColor = System.Drawing.Color.Black;
            this.lblDisplayA.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayA.ForeColor = System.Drawing.Color.FromArgb(110, 255, 130);
            this.lblDisplayA.Location = new System.Drawing.Point(10, 30);
            this.lblDisplayA.Size = new System.Drawing.Size(125, 40);
            this.lblDisplayA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDisplayA.Text = "Floor 0";
            this.lblDisplayA.Name = "lblDisplayA";
            this.lblDisplayA.TabIndex = 12;
            // 
            // btnFloor0A
            // 
            this.btnFloor0A.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFloor0A.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFloor0A.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFloor0A.Location = new System.Drawing.Point(145, 30);
            this.btnFloor0A.Size = new System.Drawing.Size(52, 40);
            this.btnFloor0A.Text = "0";
            this.btnFloor0A.UseVisualStyleBackColor = false;
            this.btnFloor0A.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFloor0A.Name = "btnFloor0A";
            this.btnFloor0A.TabIndex = 13;
            // 
            // btnFloor1A
            // 
            this.btnFloor1A.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFloor1A.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFloor1A.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFloor1A.Location = new System.Drawing.Point(203, 30);
            this.btnFloor1A.Size = new System.Drawing.Size(52, 40);
            this.btnFloor1A.Text = "1";
            this.btnFloor1A.UseVisualStyleBackColor = false;
            this.btnFloor1A.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFloor1A.Name = "btnFloor1A";
            this.btnFloor1A.TabIndex = 14;
            // 
            // btnOpenA
            // 
            this.btnOpenA.BackColor = System.Drawing.Color.Gainsboro;
            this.btnOpenA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenA.Location = new System.Drawing.Point(10, 78);
            this.btnOpenA.Size = new System.Drawing.Size(120, 32);
            this.btnOpenA.Text = "Open door";
            this.btnOpenA.UseVisualStyleBackColor = false;
            this.btnOpenA.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOpenA.Name = "btnOpenA";
            this.btnOpenA.TabIndex = 15;
            // 
            // btnCloseA
            // 
            this.btnCloseA.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCloseA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseA.Location = new System.Drawing.Point(138, 78);
            this.btnCloseA.Size = new System.Drawing.Size(120, 32);
            this.btnCloseA.Text = "Close door";
            this.btnCloseA.UseVisualStyleBackColor = false;
            this.btnCloseA.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCloseA.Name = "btnCloseA";
            this.btnCloseA.TabIndex = 16;
            // 
            // btnSosA
            // 
            this.btnSosA.BackColor = System.Drawing.Color.FromArgb(200, 30, 30);
            this.btnSosA.ForeColor = System.Drawing.Color.White;
            this.btnSosA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSosA.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSosA.Location = new System.Drawing.Point(300, 30);
            this.btnSosA.Size = new System.Drawing.Size(105, 80);
            this.btnSosA.Text = "SOS";
            this.btnSosA.UseVisualStyleBackColor = false;
            this.btnSosA.FlatAppearance.BorderSize = 0;
            this.btnSosA.Name = "btnSosA";
            this.btnSosA.TabIndex = 17;
            // 
            // lblDoorTimerA
            // 
            this.lblDoorTimerA.AutoSize = false;
            this.lblDoorTimerA.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoorTimerA.Location = new System.Drawing.Point(10, 120);
            this.lblDoorTimerA.Size = new System.Drawing.Size(190, 20);
            this.lblDoorTimerA.Text = "Door timer: --";
            this.lblDoorTimerA.Name = "lblDoorTimerA";
            this.lblDoorTimerA.TabIndex = 18;
            // 
            // lblStateA
            // 
            this.lblStateA.AutoSize = false;
            this.lblStateA.ForeColor = System.Drawing.Color.DimGray;
            this.lblStateA.Location = new System.Drawing.Point(205, 120);
            this.lblStateA.Size = new System.Drawing.Size(200, 20);
            this.lblStateA.Text = "Idle";
            this.lblStateA.Name = "lblStateA";
            this.lblStateA.TabIndex = 19;
            // 
            // shaftB
            // 
            this.shaftB.Location = new System.Drawing.Point(635, 75);
            this.shaftB.Size = new System.Drawing.Size(155, 340);
            this.shaftB.Title = "Elevator B";
            this.shaftB.Name = "shaftB";
            this.shaftB.TabIndex = 20;
            // 
            // interiorB
            // 
            this.interiorB.Location = new System.Drawing.Point(795, 75);
            this.interiorB.Size = new System.Drawing.Size(255, 340);
            this.interiorB.Title = "Elevator B - inside";
            this.interiorB.Name = "interiorB";
            this.interiorB.TabIndex = 21;
            // 
            // panelB
            // 
            this.panelB.Controls.Add(this.lblPanelTitleB);
            this.panelB.Controls.Add(this.lblDisplayB);
            this.panelB.Controls.Add(this.btnFloor0B);
            this.panelB.Controls.Add(this.btnFloor1B);
            this.panelB.Controls.Add(this.btnOpenB);
            this.panelB.Controls.Add(this.btnCloseB);
            this.panelB.Controls.Add(this.btnSosB);
            this.panelB.Controls.Add(this.lblDoorTimerB);
            this.panelB.Controls.Add(this.lblStateB);
            this.panelB.BackColor = System.Drawing.Color.White;
            this.panelB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelB.Location = new System.Drawing.Point(635, 425);
            this.panelB.Size = new System.Drawing.Size(415, 150);
            this.panelB.Name = "panelB";
            this.panelB.TabIndex = 22;
            // 
            // lblPanelTitleB
            // 
            this.lblPanelTitleB.AutoSize = true;
            this.lblPanelTitleB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPanelTitleB.Location = new System.Drawing.Point(10, 6);
            this.lblPanelTitleB.Text = "Elevator B - control panel";
            this.lblPanelTitleB.Name = "lblPanelTitleB";
            this.lblPanelTitleB.TabIndex = 23;
            // 
            // lblDisplayB
            // 
            this.lblDisplayB.AutoSize = false;
            this.lblDisplayB.BackColor = System.Drawing.Color.Black;
            this.lblDisplayB.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayB.ForeColor = System.Drawing.Color.FromArgb(110, 255, 130);
            this.lblDisplayB.Location = new System.Drawing.Point(10, 30);
            this.lblDisplayB.Size = new System.Drawing.Size(125, 40);
            this.lblDisplayB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDisplayB.Text = "Floor 0";
            this.lblDisplayB.Name = "lblDisplayB";
            this.lblDisplayB.TabIndex = 24;
            // 
            // btnFloor0B
            // 
            this.btnFloor0B.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFloor0B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFloor0B.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFloor0B.Location = new System.Drawing.Point(145, 30);
            this.btnFloor0B.Size = new System.Drawing.Size(52, 40);
            this.btnFloor0B.Text = "0";
            this.btnFloor0B.UseVisualStyleBackColor = false;
            this.btnFloor0B.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFloor0B.Name = "btnFloor0B";
            this.btnFloor0B.TabIndex = 25;
            // 
            // btnFloor1B
            // 
            this.btnFloor1B.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFloor1B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFloor1B.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFloor1B.Location = new System.Drawing.Point(203, 30);
            this.btnFloor1B.Size = new System.Drawing.Size(52, 40);
            this.btnFloor1B.Text = "1";
            this.btnFloor1B.UseVisualStyleBackColor = false;
            this.btnFloor1B.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFloor1B.Name = "btnFloor1B";
            this.btnFloor1B.TabIndex = 26;
            // 
            // btnOpenB
            // 
            this.btnOpenB.BackColor = System.Drawing.Color.Gainsboro;
            this.btnOpenB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenB.Location = new System.Drawing.Point(10, 78);
            this.btnOpenB.Size = new System.Drawing.Size(120, 32);
            this.btnOpenB.Text = "Open door";
            this.btnOpenB.UseVisualStyleBackColor = false;
            this.btnOpenB.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnOpenB.Name = "btnOpenB";
            this.btnOpenB.TabIndex = 27;
            // 
            // btnCloseB
            // 
            this.btnCloseB.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCloseB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseB.Location = new System.Drawing.Point(138, 78);
            this.btnCloseB.Size = new System.Drawing.Size(120, 32);
            this.btnCloseB.Text = "Close door";
            this.btnCloseB.UseVisualStyleBackColor = false;
            this.btnCloseB.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCloseB.Name = "btnCloseB";
            this.btnCloseB.TabIndex = 28;
            // 
            // btnSosB
            // 
            this.btnSosB.BackColor = System.Drawing.Color.FromArgb(200, 30, 30);
            this.btnSosB.ForeColor = System.Drawing.Color.White;
            this.btnSosB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSosB.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSosB.Location = new System.Drawing.Point(300, 30);
            this.btnSosB.Size = new System.Drawing.Size(105, 80);
            this.btnSosB.Text = "SOS";
            this.btnSosB.UseVisualStyleBackColor = false;
            this.btnSosB.FlatAppearance.BorderSize = 0;
            this.btnSosB.Name = "btnSosB";
            this.btnSosB.TabIndex = 29;
            // 
            // lblDoorTimerB
            // 
            this.lblDoorTimerB.AutoSize = false;
            this.lblDoorTimerB.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoorTimerB.Location = new System.Drawing.Point(10, 120);
            this.lblDoorTimerB.Size = new System.Drawing.Size(190, 20);
            this.lblDoorTimerB.Text = "Door timer: --";
            this.lblDoorTimerB.Name = "lblDoorTimerB";
            this.lblDoorTimerB.TabIndex = 30;
            // 
            // lblStateB
            // 
            this.lblStateB.AutoSize = false;
            this.lblStateB.ForeColor = System.Drawing.Color.DimGray;
            this.lblStateB.Location = new System.Drawing.Point(205, 120);
            this.lblStateB.Size = new System.Drawing.Size(200, 20);
            this.lblStateB.Text = "Idle";
            this.lblStateB.Name = "lblStateB";
            this.lblStateB.TabIndex = 31;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 590);
            this.lblStatus.Size = new System.Drawing.Size(1040, 22);
            this.lblStatus.Text = "";
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 34;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(238, 240, 246);
            this.ClientSize = new System.Drawing.Size(1070, 620);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblHallCaption);
            this.Controls.Add(this.btnHall1);
            this.Controls.Add(this.btnHall0);
            this.Controls.Add(this.shaftA);
            this.Controls.Add(this.interiorA);
            this.Controls.Add(this.panelA);
            this.Controls.Add(this.shaftB);
            this.Controls.Add(this.interiorB);
            this.Controls.Add(this.panelB);
            this.Controls.Add(this.lblStatus);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elevator Control System";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.panelA.ResumeLayout(false);
            this.panelA.PerformLayout();
            this.panelB.ResumeLayout(false);
            this.panelB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Button btnReconnect;
        private System.Windows.Forms.Button btnOpenLog;
        private System.Windows.Forms.Label lblHallCaption;
        private System.Windows.Forms.Button btnHall1;
        private System.Windows.Forms.Button btnHall0;
        private ShaftView shaftA;
        private InteriorView interiorA;
        private System.Windows.Forms.Panel panelA;
        private System.Windows.Forms.Label lblPanelTitleA;
        private System.Windows.Forms.Label lblDisplayA;
        private System.Windows.Forms.Button btnFloor0A;
        private System.Windows.Forms.Button btnFloor1A;
        private System.Windows.Forms.Button btnOpenA;
        private System.Windows.Forms.Button btnCloseA;
        private System.Windows.Forms.Button btnSosA;
        private System.Windows.Forms.Label lblDoorTimerA;
        private System.Windows.Forms.Label lblStateA;
        private ShaftView shaftB;
        private InteriorView interiorB;
        private System.Windows.Forms.Panel panelB;
        private System.Windows.Forms.Label lblPanelTitleB;
        private System.Windows.Forms.Label lblDisplayB;
        private System.Windows.Forms.Button btnFloor0B;
        private System.Windows.Forms.Button btnFloor1B;
        private System.Windows.Forms.Button btnOpenB;
        private System.Windows.Forms.Button btnCloseB;
        private System.Windows.Forms.Button btnSosB;
        private System.Windows.Forms.Label lblDoorTimerB;
        private System.Windows.Forms.Label lblStateB;
        private System.Windows.Forms.Label lblStatus;
    }
}
