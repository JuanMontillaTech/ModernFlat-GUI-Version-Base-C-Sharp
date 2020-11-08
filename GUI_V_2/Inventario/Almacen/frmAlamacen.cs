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

namespace GUI_V_2.Inventario.Almacen
{
    public partial class frmAlamacen : Form
    {
        public frmAlamacen()
        {
            InitializeComponent();
        }

        private void almacenBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.almacenBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pOSDataSet);

        }
        private int? GetIdRow()
        {
            try
            {
                return int.Parse(almacenDataGridView.Rows[almacenDataGridView.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch (Exception)
            {

                return null;
            }

        }
        private void frmAlamacen_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSDataSet.Almacen' table. You can move, or remove it, as needed.
            this.almacenTableAdapter.Fill(this.pOSDataSet.Almacen);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAlmacenCRED frmCategoriaCRED = new frmAlmacenCRED();
            frmCategoriaCRED.ShowDialog();

            this.almacenTableAdapter.Fill(this.pOSDataSet.Almacen);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int? Id = GetIdRow();
            if (Id != null)
            {
                frmAlmacenCRED frmCategoriaCRED = new frmAlmacenCRED(Id);
                frmCategoriaCRED.ShowDialog();
                this.almacenTableAdapter.Fill(this.pOSDataSet.Almacen);
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                            var forDelete = db.Almacens.Find(Id);
                            db.Almacens.Remove(forDelete);
                            MessageBox.Show("Registro Eliminado");
                            db.SaveChanges();
                            this.almacenTableAdapter.Fill(this.pOSDataSet.Almacen);
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
