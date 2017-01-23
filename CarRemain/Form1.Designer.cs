namespace CarRemain
{
    partial class Form1
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.lblCar = new System.Windows.Forms.Label();
            this.lblMotor = new System.Windows.Forms.Label();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.lblCarResult = new System.Windows.Forms.Label();
            this.lblMotorResult = new System.Windows.Forms.Label();
            this.lblLastUpdateResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(181, 42);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(193, 38);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = " 剩餘車位數";
            // 
            // lblCar
            // 
            this.lblCar.AutoSize = true;
            this.lblCar.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCar.Location = new System.Drawing.Point(12, 139);
            this.lblCar.Name = "lblCar";
            this.lblCar.Size = new System.Drawing.Size(116, 38);
            this.lblCar.TabIndex = 1;
            this.lblCar.Text = "私家車";
            // 
            // lblMotor
            // 
            this.lblMotor.AutoSize = true;
            this.lblMotor.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotor.Location = new System.Drawing.Point(12, 216);
            this.lblMotor.Name = "lblMotor";
            this.lblMotor.Size = new System.Drawing.Size(116, 38);
            this.lblMotor.TabIndex = 2;
            this.lblMotor.Text = "電單車";
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.AutoSize = true;
            this.lblLastUpdate.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastUpdate.Location = new System.Drawing.Point(12, 409);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(215, 38);
            this.lblLastUpdate.TabIndex = 3;
            this.lblLastUpdate.Text = "上次更新時間";
            // 
            // lblCarResult
            // 
            this.lblCarResult.AutoSize = true;
            this.lblCarResult.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarResult.Location = new System.Drawing.Point(181, 139);
            this.lblCarResult.Name = "lblCarResult";
            this.lblCarResult.Size = new System.Drawing.Size(116, 38);
            this.lblCarResult.TabIndex = 4;
            this.lblCarResult.Text = "私家車";
            // 
            // lblMotorResult
            // 
            this.lblMotorResult.AutoSize = true;
            this.lblMotorResult.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotorResult.Location = new System.Drawing.Point(181, 216);
            this.lblMotorResult.Name = "lblMotorResult";
            this.lblMotorResult.Size = new System.Drawing.Size(116, 38);
            this.lblMotorResult.TabIndex = 5;
            this.lblMotorResult.Text = "私家車";
            // 
            // lblLastUpdateResult
            // 
            this.lblLastUpdateResult.AutoSize = true;
            this.lblLastUpdateResult.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastUpdateResult.Location = new System.Drawing.Point(306, 409);
            this.lblLastUpdateResult.Name = "lblLastUpdateResult";
            this.lblLastUpdateResult.Size = new System.Drawing.Size(116, 38);
            this.lblLastUpdateResult.TabIndex = 6;
            this.lblLastUpdateResult.Text = "私家車";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.lblLastUpdateResult);
            this.Controls.Add(this.lblMotorResult);
            this.Controls.Add(this.lblCarResult);
            this.Controls.Add(this.lblLastUpdate);
            this.Controls.Add(this.lblMotor);
            this.Controls.Add(this.lblCar);
            this.Controls.Add(this.lbTitle);
            this.Name = "Form1";
            this.Text = " 剩餘車位數";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblCar;
        private System.Windows.Forms.Label lblMotor;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Label lblCarResult;
        private System.Windows.Forms.Label lblMotorResult;
        private System.Windows.Forms.Label lblLastUpdateResult;
    }
}

