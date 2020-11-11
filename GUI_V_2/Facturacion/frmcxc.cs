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

namespace GUI_V_2.Facturacion
{
    public partial class frmcxc : Form
    {
        public frmcxc()
        {
            InitializeComponent();

            using (var db = new POSEntities())
            {
                var query = from p in db.Facturas.ToList()
                            select new
                            {
                                id = p.Id,
                                fvencimiento = p.Fecha_Vencimiento,
                                tipo = p.Tipo,
                                fcreada = p.Fecha_Facturacion,
                                cliente = p.Contacto.Nombre  + " " + p.Contacto.Apellido 
                            };
                var result = query.ToList();
                grvData.DataSource = result;
            }
        }

        private void grvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
