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

namespace GUI_V_2.Inventario
{
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        private void categoriaBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
         

        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSDataSet.Categoria' table. You can move, or remove it, as needed.
            GetData();

        }
        public void GetData() {

            using (var db = new POSEntities())
            { 
                var query = from p in db.Categorias.ToList()
                            select new
                            {
                                id = p.Id,
                                categoria = p.Categoria1
                            };

                grvData.DataSource = query.ToList();
                    
                    
                    
                    }

         


        }
        public void GetData(string filtro)
        {

            using (var db = new POSEntities())
            {
                var query = from p in db.Categorias.ToList()
                            select new
                            {
                                id = p.Id,
                                categoria = p.Categoria1
                            };

                var result = query.Where(x => x.categoria.ToUpper().Contains(filtro.ToUpper())).ToList();
                grvData.DataSource = result;



            }




        }

        private void button1_Click(object sender, EventArgs e)
        {

            frmCategoriaCRED frmCategoriaCRED = new frmCategoriaCRED();
            frmCategoriaCRED.ShowDialog();
            GetData();

        }

        private int? GetIdRow()
        {
            try
            {
               return  int.Parse(grvData.Rows[grvData.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch (Exception)
            {

                return null;
            }

        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            int? Id = GetIdRow();
            if (Id !=null)
            {
                frmCategoriaCRED frmCategoriaCRED = new frmCategoriaCRED(Id);
                frmCategoriaCRED.ShowDialog();
                GetData();
            }
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
                            var forDelete = db.Categorias.Find(Id);
                            db.Categorias.Remove(forDelete);
                            MessageBox.Show("Registro Eliminado");
                            db.SaveChanges();
                            GetData();
                        }


                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Registro no pudo ser eliminado verifique no esta asignado a otros registros");

                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            GetData(txtFiltro.Text.ToUpper());
        }
    }
}
