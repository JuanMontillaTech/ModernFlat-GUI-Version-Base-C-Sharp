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
    public partial class frmRecibo : Form
    {
        public int IdDocumento { get; set; }
        public frmRecibo(int Documento)
        {
            InitializeComponent();
            this.IdDocumento = Documento;
        }




    }
}
