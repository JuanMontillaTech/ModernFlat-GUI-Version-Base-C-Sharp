﻿using GUI_V_2.Models;

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
    public partial class InicioResumen : Form
    {
        public InicioResumen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblhora.Text = DateTime.Now.ToString("hh:mm:ss ");
            lblFecha.Text = DateTime.Now.ToLongDateString();
            using (POSEntities pOSDataSet = new POSEntities())
            {
                lblCliente.Text = pOSDataSet.Contactoes.Where(x => x.Proveedor == false).ToList().Count().ToString();
                lblProveedores.Text = pOSDataSet.Contactoes.Where(x => x.Proveedor == true).ToList().Count().ToString();

                lblProducto.Text = pOSDataSet.Productos.ToList().Count().ToString();

            }

        }

    }
}
