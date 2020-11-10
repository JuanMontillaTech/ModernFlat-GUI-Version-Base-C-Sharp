using GUI_V_2.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_V_2.Facturacion
{
    public partial class frmVenta : Form
    {
        private ManagerNumeros ManagerNumeros;
        public List<Itemsell> ListITemSell;
        public frmVenta()
        {
            InitializeComponent();
            StartDDL();
        }
        public void StartDDL()
        {
            using (POSEntities db = new POSEntities())
            {
                var Rtipo = db.TipoFacturas.Where(x=> x.Grupo =="B").ToList();
                cbmTipo.DisplayMember = "Codigo";
                cbmTipo.ValueMember = "id";
                cbmTipo.DataSource = Rtipo;
                ManagerNumeros = new ManagerNumeros();
                txtnudoc.Text = ManagerNumeros.GetNumeroTipoFactura((int)cbmTipo.SelectedValue);
            }
        }

        private void txtIDCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
           
      }

        private void txtIDCliente_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtIDCliente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                using (POSEntities db = new POSEntities())
                {
                    int idCliente = int.Parse(txtIDCliente.Text);
                    var Cliente = db.Contactoes.Find(idCliente);
                    if (Cliente != null)
                    {
                        txtNombreCliente.Text = Cliente.Nombre.ToUpper() +" " + Cliente.Apellido.ToUpper();
                    }



                }
            }
            catch (Exception)
            {

               
            }
        }

        private void txtcbarra_Enter(object sender, EventArgs e)
        {

        }

        private void txtcbarra_Leave(object sender, EventArgs e)
        {
            string CodigoBarra = ((TextBox)sender).Text;
            Producto ArticuloF;
            bool Valida = true;
            using (POSEntities db = new POSEntities())
            {

                ArticuloF = db.Productos.Where(x => x.CodigoBarra == CodigoBarra).FirstOrDefault();

                if (ArticuloF != null)
                {

                    ListITemSell = new List<Itemsell>();
                    foreach (DataGridViewRow rowI in dgvData.Rows)
                    {
                        int IDSave = int.Parse(rowI.Cells[0].Value.ToString());
                        decimal cantidad = 1;
                        if (ArticuloF.Id == IDSave)
                        {
                            cantidad = decimal.Parse(rowI.Cells[3].Value.ToString()) + 1;
                            Valida = false;

                        }

                        ListITemSell.Add(new Itemsell()
                        {
                            IdArticulo = IDSave,
                            Articulo = rowI.Cells[1].Value.ToString(),
                            UnidadMedida = rowI.Cells[2].Value.ToString(),
                            Cantidad = cantidad,
                            PrecioUnidad = decimal.Parse(rowI.Cells[4].Value.ToString()),
                            PrecioTotal = cantidad * decimal.Parse(rowI.Cells[5].Value.ToString()),
                            PrecioCompra = decimal.Parse(rowI.Cells[5].Value.ToString())

                        });

                    }
                    if (Valida)
                    {




                        ListITemSell.Add(new Itemsell()
                        {
                            IdArticulo = ArticuloF.Id,
                            Articulo = ArticuloF.Producto1,
                            Cantidad = 1,
                            PrecioUnidad = ArticuloF.Precio_Venta,
                            PrecioTotal = ArticuloF.Precio_Venta * 1,
                            UnidadMedida = ArticuloF.UnidadMedida.Name,
                            PrecioCompra = ArticuloF.Precio_Compra
                        });




                    }
                    dgvData.DataSource = ListITemSell;
                    GrvRecalcula();
                    txtcbarra.Text = string.Empty;
                    txtcbarra.Focus();
                }
                else
                {
                    txtcbarra.Text = string.Empty;
                   
                }
            }
        }

        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            { 
            string headerText =
      dgvData.Columns[e.ColumnIndex].Name;
            switch (headerText)
            {
                case "Cantidad":
                    GrvRecalcula();
                    break;

                default:
                    break;
            }


        }

        private void GrvRecalcula()
        {
            decimal Gtotal = 0;
            ListITemSell = new List<Itemsell>();
            foreach (DataGridViewRow rowI in dgvData.Rows)
            {
                int IDSave = int.Parse(rowI.Cells[0].Value.ToString());
                decimal TotalRow = decimal.Parse(rowI.Cells[3].Value.ToString()) * decimal.Parse(rowI.Cells[4].Value.ToString());
                ListITemSell.Add(new Itemsell()
                {
                    IdArticulo = IDSave,
                    Articulo = rowI.Cells[1].Value.ToString(),
                    UnidadMedida = rowI.Cells[2].Value.ToString(),
                    Cantidad = decimal.Parse(rowI.Cells[3].Value.ToString()),
                    PrecioUnidad = decimal.Parse(rowI.Cells[4].Value.ToString()),
                    PrecioTotal = TotalRow,
                    PrecioCompra = decimal.Parse(rowI.Cells[5].Value.ToString())

                });
                Gtotal = Gtotal + TotalRow;
            }
            if (DecimalValido(txtDecuento.Text))
            {
                CalcularTotales(Gtotal, decimal.Parse(txtDecuento.Text));
            }
            else
            {
                CalcularTotales(Gtotal, 0);
            }

            try
            {
                dgvData.DataSource = ListITemSell;
            }
            catch (Exception)
            {

                
            }
           
        }
        public void CalcularTotales(decimal Subtotal, decimal Descuento ) {
            txtSubtotal.Text = Subtotal.ToString();
            txtTotal.Text = (Subtotal - Descuento).ToString(); 
        }


        private void dgvData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
       dgvData.Columns[e.ColumnIndex].Name;

            // Abort validation if cell is not in the CompanyName column.
            if (!headerText.Equals("Cantidad")) return;
            
            if (!DecimalValido(e.FormattedValue.ToString()))
            {
                dgvData.Rows[e.RowIndex].ErrorText =
                    "Cantidad no es un formato valido";
                e.Cancel = true;
                return;
            }
          
            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dgvData.Rows[e.RowIndex].ErrorText =
                    "Cantidad no puede estar en blanco";
                e.Cancel = true;
            }
            if (int.Parse(e.FormattedValue.ToString()) <= 0)
            {
                dgvData.Rows[e.RowIndex].ErrorText =
                    "Cantidad no puede ser cero";
                e.Cancel = true;
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

        private void txtDecuento_KeyUp(object sender, KeyEventArgs e)
        {
            GrvRecalcula();
        }
    }
    public class Itemsell
    {
        public int IdArticulo { get; set; }
        public string Articulo { get; set; }
        public string UnidadMedida { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? PrecioUnidad { get; set; }
        public decimal? PrecioCompra { get; set; }
        public decimal? PrecioTotal { get; set; }

    }
}