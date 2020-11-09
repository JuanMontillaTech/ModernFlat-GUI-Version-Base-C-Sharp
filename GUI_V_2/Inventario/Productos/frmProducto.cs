using GUI_V_2.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_V_2.Inventario.Productos
{
    public partial class frmProducto : Form
    {
        public frmProducto()
        {
            InitializeComponent();
            GetGRVData();
        }
        private int? GetIdRow()
        {
            try
            {
                return int.Parse(dgvData.Rows[dgvData.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch (Exception)
            {

                return null;
            }

        }
        public void GetGRVData()
        {
            using (POSEntities db = new POSEntities())
            {
                var query = from p in db.Productos.ToList()
                            select new
                            {
                                id = p.Id,
                                almacen = p.Almacen.Almacen1,
                                Categoria = p.Categoria.Categoria1,
                                pventa= p.Precio_Venta,
                                pcompra = p.Precio_Compra,
                                stock = p.Stock,
                                producto = p.Producto1,
                                cbarra = p.CodigoBarra,
                                Unidad = p.UnidadMedida.Name

                            };
                var result = query.ToList();
                dgvData.DataSource = result;
            }
           


        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmProductoCRED frmProductoCRED = new frmProductoCRED();
            frmProductoCRED.ShowDialog();
            GetGRVData();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int? Id = GetIdRow();
            if (Id != null)
            {
                frmProductoCRED frmCategoriaCRED = new frmProductoCRED(Id);
                frmCategoriaCRED.ShowDialog();
                GetGRVData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Eliminar();
        }
        private void Eliminar()
        {
            int? Id = GetIdRow();
            if (Id != null)
            {
                try
                {

                    DialogResult result = MessageBox.Show("Quiere Eliminartar el registro " + Id.ToString(), "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        using (POSEntities db = new POSEntities())
                        {
                            var forDelete = db.Productos.Find(Id);
                            db.Productos.Remove(forDelete);
                            MessageBox.Show("Registro Eliminado");
                            db.SaveChanges();
                            GetGRVData();
                        }


                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Registro no pudo ser eliminado verifique no esta asignado a otros registros");

                }

            }
        }
    }
}
