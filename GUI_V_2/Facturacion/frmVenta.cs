using GUI_V_2.Contacto;
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
using System.Drawing.Printing;
using GUI_V_2.Inventario.Productos;

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
            this.txtIDCliente.Focus();
          
        }
        public void StartDDL()
        {
            using (POSEntities db = new POSEntities())
            {

                var Rtipo = db.TipoFacturas.Where(x => x.Grupo == "B").ToList();
                cbmTipo.DisplayMember = "Codigo";
                cbmTipo.ValueMember = "id";
                cbmTipo.DataSource = Rtipo;
                GetnuDoc();
            }
        }
        private void KeyEvent(object sender, KeyEventArgs e) //Keyup Event 
        {
            if (e.KeyCode == Keys.F9)
            {
                MessageBox.Show("Function F9");
            }
            if (e.KeyCode == Keys.F6)
            {
                MessageBox.Show("Function F6");
            }
            else
                MessageBox.Show("No Function");

        }
        private void GetnuDoc()
        {
            ManagerNumeros = new ManagerNumeros();
            txtnudoc.Text = ManagerNumeros.GetNumeroTipoFactura((int)cbmTipo.SelectedValue);
        }
        private void UpdateDoc()
        {
            ManagerNumeros = new ManagerNumeros();
              ManagerNumeros.SetNumeroTipoFactura((int)cbmTipo.SelectedValue);
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
                BuscarClientePorID();
            }
            catch (Exception)
            {

               
            }
        }

        private void BuscarClientePorID()
        {
            using (POSEntities db = new POSEntities())
            {
                int idCliente = int.Parse(txtIDCliente.Text);
                var Cliente = db.Contactoes.Find(idCliente);
                if (Cliente != null)
                {
                    txtNombreCliente.Text = Cliente.Nombre.ToUpper() + " " + Cliente.Apellido.ToUpper();
                }
            }
        }

        private void txtcbarra_Enter(object sender, EventArgs e)
        {

        }

        private void txtcbarra_Leave(object sender, EventArgs e)
        {
            
        }

        private void BuscarCodigoBarra(object sender)
        {
            string CodigoBarra = ((TextBox)sender).Text;
            Producto ArticuloF;
            
            using (POSEntities db = new POSEntities())
            {
                ArticuloF = db.Productos.Where(x => x.CodigoBarra == CodigoBarra).FirstOrDefault();
                var resultado = CargarProducto(ArticuloF);
            }
        }

        private bool CargarProducto(Producto ArticuloF )
        {
            bool Valida = true;
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

            return Valida;
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
            if (decimal.Parse(e.FormattedValue.ToString()) <= -1)
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

        private void btnClientes_Click(object sender, EventArgs e)
        {
            CargarCliente();
        }

        private void CargarCliente()
        {
            frmContacto frmContactos = new frmContacto(1);
            frmContactos.ShowDialog();
            int? id = frmContactos.GetIdRow();
            txtIDCliente.Text = id.ToString();
            txtIDCliente.Enabled = false;
            try
            {
                BuscarClientePorID();
            }
            catch (Exception)
            {


            }
        }

        private void txtNombreCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Do something
                e.Handled = true;
            }
        }

        private void txtcbarra_Click(object sender, EventArgs e)
        {

        }

        private void txtcbarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BuscarCodigoBarra(sender);
            }
             
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += Imprimir;
            printDocument1.Print();
        }
        private void Imprimir(object sender, PrintPageEventArgs e) {

            Font font = new Font("Arial",14, GraphicsUnit.Point);
            int width = 200;
            int y = 20;
            e.Graphics.DrawString("Un ticket Felix", font,Brushes.Black,new RectangleF(0,y+=20,width,20));
            e.Graphics.DrawString("Un ticket Felix 2", font, Brushes.Black, new RectangleF(0, y += 20, width, 20));

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void CargarProductos()
        {
            frmProducto productos = new frmProducto();
            productos.ShowDialog();
            int? id = productos.GetIdRow();

            try
            {

                using (POSEntities db = new POSEntities())
                {
                    var ArticuloF = db.Productos.Find(id);
                    var resultado = CargarProducto(ArticuloF);
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            CrearFactura();

        }

        private void CrearFactura()
        {
            if (!string.IsNullOrEmpty(txtIDCliente.Text))
            {
                if (dgvData.Rows.Count >= 1)
                {
                    var Factura = CrearFactura(1);
                    Settoprint();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("La factura no tiene detalle");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un clinete");
            }
        }
        private void CrearFacturaRecibo()
        {
            if (!string.IsNullOrEmpty(txtIDCliente.Text))
            {
                if (dgvData.Rows.Count >= 1)
                {
                    var Factura = CrearFactura(1);
                    frmRecibo frmRecibo = new frmRecibo(Factura);
                    frmRecibo.ShowDialog();
                    var recb = frmRecibo.recibo;
                    Settoprint();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("La factura no tiene detalle");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un clinete");
            }
        }



        private Factura CrearFactura(int Estado)
        {
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

            }

            Factura factura = new Factura();
            factura.IdCliente = int.Parse(txtIDCliente.Text);
            factura.IdTipo = (int)cbmTipo.SelectedValue;
            factura.Tipo = txtnudoc.Text;
            factura.Total_Facturado = decimal.Parse(txtTotal.Text);
            factura.Total_Descuento = string.IsNullOrEmpty(txtDecuento.Text) ? 0 : decimal.Parse(txtDecuento.Text);
            factura.Fecha_Facturacion = DateTime.Now;
            factura.Fecha_Vencimiento = Estado == 1 ? DateTime.Now.AddDays(30) : DateTime.Now;
            factura.Estado = Estado;
            using (var db = new POSEntities())
            {
                db.Facturas.Add(factura);
                db.SaveChanges();
                foreach (var item in ListITemSell)
                {



                    FacturaDetalle fd = new FacturaDetalle();
                    fd.IdFactura = factura.Id;
                    fd.IdArticulo = item.IdArticulo;
                    fd.Decripcion = item.Articulo;
                    fd.Cantida = item.Cantidad;
                    fd.Precio = item.PrecioUnidad;
                    db.FacturaDetalles.Add(fd);
                    db.SaveChanges();


                }


            }
          
            return factura;
        }

        private void Settoprint( )
        {
            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += ImprimirFact;
            printDocument1.Print();
        }
        private void ImprimirFact(object sender, PrintPageEventArgs e)
        {
            using (var db = new POSEntities())
            {


                Factura fac = db.Facturas.Where(x => x.Tipo == txtnudoc.Text).FirstOrDefault();

                Font font = new Font("Arial", 9, GraphicsUnit.Point);
                int width = 300;
                string line = "";
                for (int i = 0; i < width; i++)
                {
                    line += "-";
                }
                int y = 20;
                e.Graphics.DrawString("FERRETERIA NAPOLES " , font, Brushes.Black, new RectangleF(0, y += 20, width, 20));
                e.Graphics.DrawString("Calle penetración # 17 Recidencial Amalia", font, Brushes.Black, new RectangleF(0, y += 20, width, 20));
                e.Graphics.DrawString("829-921-7825", font, Brushes.Black, new RectangleF(0, y += 20, width, 20));
                e.Graphics.DrawString("FACTURA# : " + fac.Tipo, font, Brushes.Black, new RectangleF(0, y += 20, width, 20));
                e.Graphics.DrawString("FECHA: " + fac.Fecha_Facturacion.ToString(), font, Brushes.Black, new RectangleF(0, y += 20, width, 20));
                e.Graphics.DrawString("VENCIMIENTO: " + fac.Fecha_Facturacion.ToString(), font, Brushes.Black, new RectangleF(0, y += 20, width, 20));
                e.Graphics.DrawString(line, font, Brushes.Black, new RectangleF(0, y += 10, width, 20));
                e.Graphics.DrawString("CANt.  DESCRIPCION                          VALOR   ", font, Brushes.Black, new RectangleF(0, y += 10, width, 20)); 
                e.Graphics.DrawString(line, font, Brushes.Black, new RectangleF(0, y += 5, width, 20));
                foreach (FacturaDetalle item in fac.FacturaDetalles)
                {
                e.Graphics.DrawString(item.Cantida.ToString() + " "+item.Decripcion.Substring(0,22) +" "+ item.Precio.ToString(), font, Brushes.Black, new RectangleF(0, y += 10, width, 20));

                }
                e.Graphics.DrawString(line, font, Brushes.Black, new RectangleF(0, y += 5, width, 20)); 
                e.Graphics.DrawString("DESCUENTO: " + fac.Total_Descuento.ToString(), font, Brushes.Black, new RectangleF(0, y += 20, width, 20));
                
                e.Graphics.DrawString("TOTAL A PAGAR " + fac.Total_Facturado.ToString(), font, Brushes.Black, new RectangleF(0, y += 20, width, 20));
                e.Graphics.DrawString("TOTAL PAGADO " + fac.Total_Facturado.ToString(), font, Brushes.Black, new RectangleF(0, y += 20, width, 20));

            }

        }
        public void ClearForm()
        {
            UpdateDoc();
            ListITemSell = new List<Itemsell>();

            dgvData.DataSource = ListITemSell;
            txtcbarra.Text = ""; 
            txtIDCliente.Text = "";
            txtMoneda.Text = "";
            txtNombreCliente.Text = "";
            txtSubtotal.Text = "";
            txtTotal.Text = "";
            txtIDCliente.Enabled = true;
            GetnuDoc();
             }

        private void frmVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
             
                //MessageBox.Show($"Form.KeyPress: '{e.KeyChar}' pressed.");

            
             
        }

        private void frmVenta_Load(object sender, EventArgs e)
        {
            this.txtIDCliente.Focus();

        }

        private void frmVenta_KeyUp(object sender, KeyEventArgs e)
        {
            
                switch (e.KeyCode)
                {
                    case Keys.F1:
                    CargarCliente();
                        break;
                case Keys.F2:
                    CargarProductos();
                    break;
                default:
                        break;
                }
             
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            CrearFacturaRecibo();
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