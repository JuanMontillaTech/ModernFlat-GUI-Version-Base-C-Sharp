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
    public partial class frmCategoriaCRED : Form
    {

        public int? Id;
        public frmCategoriaCRED(int? id=null)
        {
            InitializeComponent();
            this.Id = id;
         
            POSEntities pOSEntities = new POSEntities();
            if (Id != null)
            {
                Models.Categoria categoria = pOSEntities.Categorias.Find(Id);
                txtCodigo.Text = id.ToString();
                txtCategoria.Text = categoria.Categoria1;
                button1.Text = "Editar";
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            POSEntities pOSEntities = new POSEntities();
            if (Id != null)
            {
                Models.Categoria categoria = pOSEntities.Categorias.Find(Id);
                categoria.Categoria1 = txtCategoria.Text.ToUpper().Trim();
            }
            else
            {
                Models.Categoria categoria = new Models.Categoria();

                categoria.Categoria1 = txtCategoria.Text.ToUpper().Trim();
                pOSEntities.Categorias.Add(categoria);
            }
         

            pOSEntities.SaveChanges();
            MessageBox.Show("Categoria Creada");
            this.Close();
        }
    }
}
