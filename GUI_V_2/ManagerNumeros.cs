using GUI_V_2.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace GUI_V_2
{
    public class ManagerNumeros
    {

        public string GetNumeroTipoFactura (int Id) {
            string NumeroFomart ="";
            using (POSEntities db = new POSEntities())
            {
                var TFactura = db.TipoFacturas.Find(Id); 
                NumeroFomart = TFactura.Codigo.ToUpper() + ((int)(TFactura.Secuencia + 1)).ToString();

            }

            return NumeroFomart;
        }

        public void SetNumeroTipoFactura(int Id)
        {           
            using (POSEntities db = new POSEntities())
            {
                var TFactura = db.TipoFacturas.Find(Id);
                TFactura.Secuencia = ((int)(TFactura.Secuencia + 1));
                db.SaveChanges();

            } 
        }
    }
}
