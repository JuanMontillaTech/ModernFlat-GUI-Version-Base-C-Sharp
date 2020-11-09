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

namespace GUI_V_2.Inventario.Productos
{
    public partial class frmProductoCRED : Form
    {
        public int? Id;
        public frmProductoCRED(int ? _id = null)
        {
            InitializeComponent();
            this.Id = _id;
            StartDDL();
        } 
        public void StartDDL()
        {
            using (POSEntities db = new POSEntities())
            {
                var RCategoria = db.Categorias.ToList();
                cmbCategoria.DisplayMember = "Categoria1";
                cmbCategoria.ValueMember = "id";
                cmbCategoria.DataSource = RCategoria;
                var RAlmacen = db.Almacens.ToList();
                cmbAlmacen.DisplayMember = "Almacen1";
                cmbAlmacen.ValueMember = "id";
                cmbAlmacen.DataSource = RAlmacen;
                var RUnidadM = db.UnidadMedidas.ToList(); 
                cmbUnidad.DisplayMember = "Name";
                cmbUnidad.ValueMember = "id";
                cmbUnidad.DataSource = RUnidadM;
                if (Id != null)
                {
                    var FoundP = db.Productos.Find(Id);
                   
                    cmbUnidad.SelectedIndex = cmbUnidad.FindString(FoundP.UnidadMedida.Name);
                    cmbCategoria.SelectedIndex = cmbCategoria.FindString(FoundP.Categoria.Categoria1);
                    cmbAlmacen.SelectedIndex = cmbAlmacen.FindString(FoundP.Almacen.Almacen1);
                    txtProducto.Text = FoundP.Producto1;
                    txtPrecioCompra.Text = FoundP.Precio_Compra.ToString();
                    txtPrecioVenta.Text = FoundP.Precio_Venta.ToString();
                    txtStock.Text = FoundP.Stock.ToString();
                    cbarra.Text = FoundP.CodigoBarra;
                    txtCodigo.Text = Id.ToString();

                }
            } 
        } 
        private void button5_Click(object sender, EventArgs e)
        {
            bool Exits = false;
            Exits = ValidaForm(txtCodigo);
            Exits = ValidaForm(txtPrecioCompra);
            Exits = ValidaForm(txtPrecioVenta);
            Exits = ValidaForm(txtStock);
            Exits = ValidaForm(txtProducto);
            if (Exits)
            {
                if (Id == null)
                {
                    Producto producto = new Producto();
                    producto.IDAlmacen = (int)cmbAlmacen.SelectedValue;
                    producto.IDCategoria = (int)cmbCategoria.SelectedValue;
                    producto.IDUnidad = (int)cmbUnidad.SelectedValue;
                    producto.Producto1 = txtProducto.Text;
                    producto.Precio_Compra = decimal.Parse(txtPrecioCompra.Text.Trim());
                    producto.Precio_Venta = decimal.Parse(txtPrecioVenta.Text.Trim());
                    producto.Stock = int.Parse(txtStock.Text);
                    producto.CodigoBarra = cbarra.Text;
                    using (POSEntities db = new POSEntities())
                    {
                        db.Productos.Add(producto);
                        db.SaveChanges(); 
                    } 
                }
                else
                {
                    using (POSEntities db = new POSEntities())
                    {
                        Producto producto = db.Productos.Find(Id);
                        producto.IDAlmacen = (int)cmbAlmacen.SelectedValue;
                        producto.IDCategoria = (int)cmbCategoria.SelectedValue;
                        producto.IDUnidad = (int)cmbUnidad.SelectedValue;
                        producto.Producto1 = txtProducto.Text;
                        producto.Precio_Compra = decimal.Parse(txtPrecioCompra.Text.Trim());
                        producto.Precio_Venta = decimal.Parse(txtPrecioVenta.Text.Trim());
                        producto.Stock = int.Parse(txtStock.Text);
                        producto.CodigoBarra = cbarra.Text; 
                        db.SaveChanges();
                    }
                }
                this.Close();
            }           
        }
        private void Text_Validating(object sender, CancelEventArgs e)
        {
            ValidaForm(sender);
        }
        public bool ValidaForm (object Campo)
        {
            var CampoV = (TextBox)Campo;
            if (string.IsNullOrEmpty(CampoV.Text))
            {
                errorProvider1.SetError(CampoV, "Por Favor el campo es requerido");
                return false;
            }
            else
            {
                errorProvider1.SetError(CampoV, null);
                return true;
            }
        }
        private const char SignoDecimal = '.'; // Carácter separador decimal
        private string _prevTextBoxValue; // Variable que almacena el valor anterior del Textbox
 
        private void Decimal_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            // Comprueba si el valor del TextBox se ajusta a un valor válido
            if (Regex.IsMatch(textBox.Text, @"^(?:\d+\.?\d*)?$"))
            {
                // Si es válido se almacena el valor actual en la variable privada
                _prevTextBoxValue = textBox.Text;
            }
            else
            {
                // Si no es válido se recupera el valor de la variable privada con el valor anterior
                // Calcula el nº de caracteres después del cursor para dejar el cursor en la misma posición
                var charsAfterCursor = textBox.TextLength - textBox.SelectionStart - textBox.SelectionLength;
                // Recupera el valor anterior
                textBox.Text = _prevTextBoxValue;
                // Posiciona el cursor en la misma posición
                textBox.SelectionStart = Math.Max(0, textBox.TextLength - charsAfterCursor);
            }
        }

    }
}
