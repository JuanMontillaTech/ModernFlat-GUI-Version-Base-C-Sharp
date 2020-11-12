using GUI_V_2.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_V_2
{
    public partial class InicioResumen : Form
    {
        public InicioResumen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblhora.Text = DateTime.Now.ToString("hh:mm:ss ");
            lblFecha.Text = DateTime.Now.ToLongDateString().ToString(System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR"));
            using (POSEntities pOSDataSet = new POSEntities())
            {
                lblCliente.Text = pOSDataSet.Contactoes.Where(x => x.Proveedor == false).ToList().Count().ToString();
                lblProveedores.Text = pOSDataSet.Contactoes.Where(x => x.Proveedor == true).ToList().Count().ToString();

                lblProducto.Text = pOSDataSet.Productos.ToList().Count().ToString();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new POSEntities())
            {
                foreach (Materiale item in  db.Materiales.Where(x=> x.UND !="1").ToList())
                {
                    var catP = db.Materiales.Find(item.Padre);
                    var categoria = db.Categorias.Where(x => x.Categoria1 == catP.DESCRIPCION ).FirstOrDefault();
                    if (categoria!=null)
                    {
                        var unidad = db.UnidadMedidas.Where(x => x.Name == item.UND).FirstOrDefault();
                        if (unidad !=null)
                        {
                            try
                            {
                                var Producto = new Producto();
                                Producto.IDAlmacen = 1;

                                Producto.IDCategoria = categoria.Id;
                                Producto.IDUnidad = unidad.Id;
                                Producto.Precio_Compra = 0;
                                Producto.Precio_Venta = decimal.Parse(item.PU_ITBIS.Replace("RD$", ""));
                                Producto.Precio_SinITBIS = decimal.Parse(item.PU_SIN_ITBIS.Replace("RD$", ""));
                                Producto.Stock = 100;
                                Producto.Producto1 = item.DESCRIPCION.Trim();
                                Producto.CodigoBarra = "";
                                db.Productos.Add(Producto);
                                db.SaveChanges();
                            }
                            catch (DbEntityValidationException ex)
                            {
                                foreach (var eve in ex.EntityValidationErrors)
                                {
                                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                    foreach (var ve in eve.ValidationErrors)
                                    {
                                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                            ve.PropertyName, ve.ErrorMessage);
                                    }
                                }
                                throw;
                            }

                        }
                    }
                     
                }
            }
        }
    }
}
