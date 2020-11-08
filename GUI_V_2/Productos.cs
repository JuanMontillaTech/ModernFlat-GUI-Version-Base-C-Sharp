using GUI_V_2.Inventario;
using GUI_V_2.Inventario.Almacen;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_V_2
{
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void AbrirFormEnPanel(object Formhijo)
        {
       
            Form fh = Formhijo as Form;
          
            fh.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmCategoria());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new frmAlamacen());
        }
    }
}
