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

namespace GUI_V_2.Contacto
{
    public partial class frmContacto : Form
    {
        public int? id;
        public frmContacto( int? _id = null)
        {
            InitializeComponent();
            this.id = _id;
            StartFrm();
        }
       
        public void StartFrm () {

            if (id == 1)
            {
                this.Text = "Clientes";

            }
            else
            {
                
                this.Text = "Proveedores";
            }
        }

 

        private void frmContacto_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSDataSet.Contacto' table. You can move, or remove it, as needed.
            //this.contactoTableAdapter.Fill(this.pOSDataSet.Contacto);
            GetGRV();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmContactoCRED   contactoCRED = new frmContactoCRED(id);
            contactoCRED.ShowDialog();

            GetGRV();
        }

        public void GetGRV()
        {
            using (POSEntities pOSDataSet = new POSEntities())
            {
                bool Proveedor = false; 
                if (id == 2)
                {
                    Proveedor = true;
                }
                var query = from p in pOSDataSet.Contactoes.Where(x => x.Proveedor == Proveedor).ToList()
                            select new
                            {
                                id = p.Id,
                                nombre = p.Nombre + " " +p.Apellido,
                                cedula = p.Cedula


                            };
                var resul = query.ToList();
                dgvcontacto.DataSource = resul;
            } 
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            int? Id = GetIdRow();
            if (Id != null)
            {
                frmContactoCRED  frmContactoCRED = new frmContactoCRED(id,Id);
                frmContactoCRED.ShowDialog();
                GetGRV();
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
                            var forDelete = db.Contactoes.Find(Id);
                            db.Contactoes.Remove(forDelete);
                            MessageBox.Show("Registro Eliminado");
                            db.SaveChanges();
                            GetGRV();
                        }


                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Registro no pudo ser eliminado verifique no esta asignado a otros registros");

                }

            }
        }

        public int? GetIdRow()
        {
            try
            {
                return int.Parse(dgvcontacto.Rows[dgvcontacto.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch (Exception)
            {

                return null;
            }

        }
        public void GetGRV(string Filter)
        {
            using (POSEntities pOSDataSet = new POSEntities())
            {
                bool Proveedor = false;
                if (id == 2)
                {
                    Proveedor = true;
                }
                var query = from p in pOSDataSet.Contactoes.Where(x => x.Proveedor == Proveedor).ToList()
                            select new
                            {
                                id = p.Id,
                                nombre = p.Nombre + " " + p.Apellido,
                                cedula = p.Cedula


                            };
                var resul = query.Where(x=> x.nombre.ToLower().Contains(Filter.ToLower())).ToList();
                dgvcontacto.DataSource = resul;

             }
        }

        private void txtFiltroCliente_KeyPress(object sender, KeyPressEventArgs e)
        {

            GetGRV(((TextBox)sender).Text);
        }
    }
}
