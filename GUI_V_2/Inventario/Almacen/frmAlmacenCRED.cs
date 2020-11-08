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
    public partial class frmAlmacenCRED : Form
    {
        public int? Id;
        public frmAlmacenCRED(int? id = null)
        {
            InitializeComponent();
            this.Id = id;

            POSEntities pOSEntities = new POSEntities();
            if (Id != null)
            {
                Models.Almacen categoria = pOSEntities.Almacens.Find(Id);
                txtCodigo.Text = id.ToString();
                txtCategoria.Text = categoria.Almacen1;
                button1.Text = "Editar";            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            POSEntities pOSEntities = new POSEntities();
            if (Id != null)
            {
                Models.Almacen categoria = pOSEntities.Almacens.Find(Id);
                categoria.Almacen1 = txtCategoria.Text.ToUpper().Trim();
            }
            else
            {
                Models.Almacen categoria = new Models.Almacen();
                categoria.Almacen1 = txtCategoria.Text.ToUpper().Trim();
                pOSEntities.Almacens.Add(categoria);
            }


            pOSEntities.SaveChanges();
            MessageBox.Show("almacen Creada");
            this.Close();
        }
    }
}
