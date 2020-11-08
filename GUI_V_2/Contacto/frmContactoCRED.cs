using GUI_V_2.Models;
using GUI_V_2.POSDataSetTableAdapters;

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
    public partial class frmContactoCRED : Form
    {
        public int? id;
        public int? idModo;
        public frmContactoCRED(int? _idModo = null, int? _id = null)
        {
            InitializeComponent();
            this.id = _id;
            this.idModo = _idModo;
            StartFrm();
        }
        public void GetData()
        {
            POSEntities pOSEntities = new POSEntities();
            var FounDatao = pOSEntities.Contactoes.Find(id);
            nombreTextBox.Text= FounDatao.Nombre   ;
            apellidoTextBox.Text  =FounDatao.Apellido;
            cedulaTextBox.Text= FounDatao.Cedula ;
            direccionTextBox.Text= FounDatao.Direccion ;
            emailTextBox.Text = FounDatao.Email ;
            empresaTextBox.Text = FounDatao.Empresa ;
            faxTextBox.Text=FounDatao.Fax ;
            rNCTextBox.Text = FounDatao.RNC ;
            telefonosTextBox.Text =FounDatao.Telefonos ;
            celularTextBox.Text =FounDatao.Celular;

        }
            public void StartFrm()
        {

            if (idModo == 1)
            {
                this.Text = "Clientes";

            }
            else
            {
                this.Text = "Proveedores";
            }


            if (id != null)
            {
                GetData();

            }

        }

        private void contactoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
        

        }

        private void frmContactoCRED_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSDataSet.Contacto' table. You can move, or remove it, as needed.
        

        }

        private void button1_Click(object sender, EventArgs e)
        {
            POSEntities pOSEntities = new POSEntities();
            if (id != null)
            {
                Models.Contacto forinsert = pOSEntities.Contactoes.Find(id);
                forinsert.Nombre = nombreTextBox.Text;
                forinsert.Apellido = apellidoTextBox.Text;
                forinsert.Cedula = cedulaTextBox.Text;
                forinsert.Direccion = direccionTextBox.Text;
                forinsert.Email = emailTextBox.Text;
                forinsert.Empresa = empresaTextBox.Text;
                forinsert.Fax = faxTextBox.Text;
                forinsert.Proveedor = false;
                forinsert.RNC = rNCTextBox.Text;
                forinsert.Telefonos = telefonosTextBox.Text;
                forinsert.Celular = celularTextBox.Text;
                if (idModo == 2)
                {
                    forinsert.Proveedor = true;
                }

            }
            else
            {
                Models.Contacto forinsert = new Models.Contacto();
                forinsert.Nombre = nombreTextBox.Text;
                forinsert.Apellido = apellidoTextBox.Text;
                forinsert.Cedula = cedulaTextBox.Text;
                forinsert.Direccion = direccionTextBox.Text;
                forinsert.Email = emailTextBox.Text;
                forinsert.Empresa = empresaTextBox.Text;
                forinsert.Fax = faxTextBox.Text;
                forinsert.Proveedor = true;
                forinsert.RNC = rNCTextBox.Text;
                forinsert.Telefonos = telefonosTextBox.Text;
                forinsert.Celular = celularTextBox.Text;
                if (idModo == 2)
                {
                    forinsert.Proveedor = true;
                }
                pOSEntities.Contactoes.Add(forinsert);
            }
          

            pOSEntities.SaveChanges();
            MessageBox.Show("Exitosa");
            this.Close();
        }
    }
}
