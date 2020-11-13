using GUI_V_2.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_V_2.Facturacion
{
    public partial class frmRecibo : Form
    {
        public int IdDocumento { get; set; }
        public Factura factura { get; set; }
        public Recibo recibo { get; set; }
        public frmRecibo(  Factura _facturacion = null)
        {
            InitializeComponent();
            this.factura = _facturacion;
            StartForm(factura);

        }
        public decimal GetRecibo()
        {
            return decimal.Parse(txtEfectivo.Text);
        }
        public void StartForm(Factura _facturacion)
        {
            using (var db = new POSEntities() )
            {
                var RMetodo = db.MetodoPagoes.ToList();
                cbmMetodo.DisplayMember = "MetodoPago1";
                cbmMetodo.ValueMember = "id";
                cbmMetodo.DataSource = RMetodo;
                lblDocumento.Text = _facturacion.Tipo;
                lblPago.Text = (string.IsNullOrEmpty(_facturacion.Total_Descuento.ToString() )? 0 :(_facturacion.Total_Facturado - _facturacion.Total_Descuento)).ToString();
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEfectivo.Text))
            {
                if (DecimalValido(txtEfectivo.Text))
                {
                    if (decimal.Parse(txtEfectivo.Text) >= 1)
                    {
                        using (var db = new POSEntities())
                        {
                              recibo = new Recibo();
                            recibo.Fecha = DateTime.Now;
                            recibo.IdMetodoPago = (int)cbmMetodo.SelectedValue;
                            recibo.Pagado = decimal.Parse(txtEfectivo.Text);
                            recibo.IDFactura = factura.Id;
                            var fa = db.Facturas.Find(recibo.IDFactura);
                            decimal? OldTotal = 0;
                            if (fa.Total_Pagado != null)
                            {
                                OldTotal = fa.Total_Pagado;
                            }
                            decimal? Calf = (OldTotal + recibo.Pagado);
                            fa.Total_Pagado = Calf;
                            if (fa.Total_Pagado == fa.Total_Facturado)
                            {
                                fa.Estado = 9;
                            }
                            db.Reciboes.Add(recibo);
                            db.SaveChanges();
                            this.Close();

                        }
                    }
                    else
                    {
                        MessageBox.Show("El efectivo entregado es menor al facturado");
                    }
                }

            }
        }
        private bool DecimalValido(string Valor)
        {
            if (!string.IsNullOrEmpty(Valor))
            {
                TextBox textBox = new TextBox();
                textBox.Text = Valor;
                if (Regex.IsMatch(textBox.Text, @"^(?:\d+\.?\d*)?$"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
