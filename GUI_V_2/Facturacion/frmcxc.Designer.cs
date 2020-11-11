namespace GUI_V_2.Facturacion
{
    partial class frmcxc
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
            this.grvData = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaVence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaFacturacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grvData
            // 
            this.grvData.AllowUserToAddRows = false;
            this.grvData.AllowUserToDeleteRows = false;
            this.grvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.cliente,
            this.Factura,
            this.FechaVence,
            this.FechaFacturacion});
            this.grvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvData.Location = new System.Drawing.Point(0, 0);
            this.grvData.Name = "grvData";
            this.grvData.ReadOnly = true;
            this.grvData.RowHeadersWidth = 51;
            this.grvData.RowTemplate.Height = 24;
            this.grvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvData.Size = new System.Drawing.Size(800, 450);
            this.grvData.TabIndex = 0;
            this.grvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvData_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grvData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 450);
            this.panel2.TabIndex = 0;
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "id";
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.MinimumWidth = 6;
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 125;
            // 
            // cliente
            // 
            this.cliente.DataPropertyName = "Cliente";
            this.cliente.HeaderText = "Cliente";
            this.cliente.MinimumWidth = 6;
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            this.cliente.Width = 125;
            // 
            // Factura
            // 
            this.Factura.DataPropertyName = "Tipo";
            this.Factura.HeaderText = "Factura";
            this.Factura.MinimumWidth = 6;
            this.Factura.Name = "Factura";
            this.Factura.ReadOnly = true;
            this.Factura.Width = 125;
            // 
            // FechaVence
            // 
            this.FechaVence.DataPropertyName = "fvencimiento";
            this.FechaVence.HeaderText = "Fecha de Vecimiento";
            this.FechaVence.MinimumWidth = 6;
            this.FechaVence.Name = "FechaVence";
            this.FechaVence.ReadOnly = true;
            this.FechaVence.Width = 125;
            // 
            // FechaFacturacion
            // 
            this.FechaFacturacion.DataPropertyName = "fcreada";
            this.FechaFacturacion.HeaderText = "Fecha Facturación";
            this.FechaFacturacion.MinimumWidth = 6;
            this.FechaFacturacion.Name = "FechaFacturacion";
            this.FechaFacturacion.ReadOnly = true;
            this.FechaFacturacion.Width = 125;
            // 
            // frmcxc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Name = "frmcxc";
            this.Text = "frmcxc";
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grvData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaVence;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaFacturacion;
    }
}