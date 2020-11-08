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
            this.Validate();
            this.categoriaBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pOSDataSet);

        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSDataSet.Categoria' table. You can move, or remove it, as needed.
            this.categoriaTableAdapter.Fill(this.pOSDataSet.Categoria);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            frmCategoriaCRED frmCategoriaCRED = new frmCategoriaCRED();
            frmCategoriaCRED.ShowDialog();

            this.categoriaTableAdapter.Fill(this.pOSDataSet.Categoria);

        }

        private int? GetIdRow()
        {
            try
            {
               return  int.Parse(categoriaDataGridView.Rows[categoriaDataGridView.CurrentRow.Index].Cells[0].Value.ToString());
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
                this.categoriaTableAdapter.Fill(this.pOSDataSet.Categoria);
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
                            this.categoriaTableAdapter.Fill(this.pOSDataSet.Categoria);
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
    }
}
