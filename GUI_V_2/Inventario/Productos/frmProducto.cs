using GUI_V_2.Models;

using Spire.Xls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public int? GetIdRow()
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
        public void GetGRVData(string   filter)
        {
            using (POSEntities db = new POSEntities())
            {
                var query = from p in db.Productos.Where(x=> x.Producto1.ToLower().Contains(filter.ToLower())).ToList()
                            select new
                            {
                                id = p.Id,
                                almacen = p.Almacen.Almacen1,
                                Categoria = p.Categoria.Categoria1,
                                pventa = p.Precio_Venta,
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

        private void txtFiltroCliente_TextChanged(object sender, EventArgs e)
        {
            GetGRVData(((TextBox)sender).Text);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

            Spire.License.LicenseProvider.ClearLicense();
            Spire.License.LicenseProvider.SetLicenseKey("Zm24hayRaTaMYqseAQATZ+YLRZJqefSBRkbCg8r9Wm6FhE7spOM+hxyACUnPcSD6sJW//tEF5IKcbb603OYhUnRdn/H7OCLV4Y3WSLSWwJecmNHNFuzdLRCZktrWJ3+QcfDFknv12A4THQr4aq7++azV8tExCvi0qv6CSaciBXuUv5wpw/s9zqijLhaiOJN8Anr9FPjJ53UAj0U6thkfyYBrH+mvH+V2D2xwYUdijc0JoEZ+TTnEq+GmN8HFUl14M54c98RKqlr3BeMUKocaBZj9LT+yFYygHKQ9P+fIHz1M3IrEwuahATV/vnMEVplY5PSFvdpGETy2sm5TKG1as1mdraiNWUzk5kM0DBQhPwh8G49SaZbkOR/vpmAY7WGERU1V9gcB57Ztf+xPRr5Lcux9I8/Yq7VcjQNzeWEtamIOMfbGByePJ2NAHtB8kroH4SIOMyVA7w0aRilzbjIW3R4ZI7iYe9O+5E3Z7si+kQ45Sx27l5+SUtYpcqWguSKR/VXsfA3Wsg1i4Hxy2J1D5Z0VjANLmut+hUuJhUTSR8PaKIJ8MQNlHc+Arhas8ClYMBHrgkUFTSVQBBHMwaphTaIKFzOajzvVSud+QF2+zmS6EaXcQLfjenNQneyrd/uyRjV+dRMe7mvNC44CpaDchV7Y3LEfXsQLVb7c4Q1W9E3+3F0M+lpmz5awuS/D/5eW4lsFAYOzTULRRUz4oAXTxWYJoYuJnwznuWhbp1QUlVV/4N5XwGA3TwHCrzlzPlnNKN5KU383TA5Ylf09YoQilCwewhhhhQnOlwsfDftKA7sPO7THfrRzB0Wy2oAtxAj1SWMqCoEoTM3tcCn3W0PiJNvOuzF0SPae2NDwATqzZfepJ4NYYZqF0cb7WYBKtJr879n/mZkh3hVUtPFcqyjBSkgf1H4QgSmitcJIuqkwuzQ0h9i0Wvk0krgyKavJkniDm+B2+sPH6QKfuRO/Ytws4+j0sg9Jjr3CLq5Nabs3Wr3mb2SXQFE1AAMcJJNaiHAa7XlKF0E4vV8kPBipCdgYKo6mKmEq6tcc5MCB40gz8jnPhMvxPUfzQeqHcPd63IJjMOUYHd29i4/V51CAD6S0K1hW/6gUVUX2ZcFRPrDdlDAvvMX1ivuT5oyozLgMvXWT/QMwLMul8RJbNR2m5Vf4IQPRgXWjmsqcxVdnvUhmhqT+/XG05cf1Nugcm953tNrdUqQXVUFEZ7Fq7aMd/drS2EF0p1+lomNy1jWOxhaAY8Z8CLKWFlpUopK+W7Tfhuup0ODO1JdO5FQZAtjpRPID0V7Ps3vV+wvYQhAPa65ttLdlx1kZBEH3z0XFvaaZFeeEdOvJSFjcY+hctRtm9uzBNCwygXgfhvuWvaBRTL8bKPa4GYMPlf/k7Xa8jk+x5vJ88kqQ/Ob19G4HI2F2euEgrU/AUDd1t6B1b/fYykCkNV2N1U+OFRAw+V9zBzCXZW4nz5qH6RirRqfavmR4KMwnKqS8RiCKh3peo46GxD/qJhT+SXG12ELtt/KmxjpiMBiR4s5vjI8vcZ8=");
        Spire.License.LicenseProvider.LoadLicense();


            DataTable t = new DataTable();
            t.Columns.Add("id", typeof(String));
            t.Columns.Add("almacen", typeof(String));
            t.Columns.Add("pventa", typeof(String));
            t.Columns.Add("pcompra", typeof(String));
            t.Columns.Add("stock", typeof(String));
            t.Columns.Add("producto", typeof(String));
            t.Columns.Add("cbarra", typeof(String));
            t.Columns.Add("Unidad", typeof(String));
            t.Columns.Add("Categoria", typeof(String));

            DataRow row;
           

            using (POSEntities db = new POSEntities())
            {
                var query = from p in db.Productos.ToList()
                            select new
                            {
                                id = p.Id,
                                almacen = p.Almacen.Almacen1,
                                Categoria = p.Categoria.Categoria1,
                                pventa = p.Precio_Venta,
                                pcompra = p.Precio_Compra,
                                stock = p.Stock,
                                producto = p.Producto1,
                                cbarra = p.CodigoBarra,
                                Unidad = p.UnidadMedida.Name

                            };
                var result = query.ToList();
                foreach (var i in result)
                {
                    row = t.NewRow();
                    row["id"] = i.id;
                    row["almacen"] = i.almacen;
                    row["pventa"] = i.pventa;
                    row["pcompra"] = i.pcompra;
                    row["stock"] = i.stock;
                    row["producto"] = i.producto;
                    row["cbarra"] = i.cbarra;
                    row["Unidad"] = i.Unidad;
                    row["Categoria"] = i.Categoria;
                    t.Rows.Add(row);

                }
            }
             





            Workbook book = new Workbook();
            

                Worksheet sheet = book.Worksheets[0]; 

                sheet.InsertDataTable(t, true, 1, 1);
            string productoFile = "Productos" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString()+ ".xls";
                book.SaveToFile(productoFile, ExcelVersion.Version97to2003); 

                System.Diagnostics.Process.Start(productoFile);


        }
    }
}
