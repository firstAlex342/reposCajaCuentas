using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using System.Data;

namespace CapaLogicaNegocios
{
    public class ClsAsociado
    {

        public int Id { set; get; }
        public string Nombre { set; get; }
        public string APaterno { set; get; }
        public string AMaterno { set; get; }
        public int Edad { set; get; }
        public string Telefono1 { set; get; }
        public string Telefono2 { set; get; }
        public string Correo { set; get; }
        public string Domicilio { set; get; }

        private ClsManejador CLSManejador { set; get; }

        //----------------------Constructor
        public ClsAsociado()
        {
            this.Id = 0;
            this.Nombre = String.Empty;
            this.APaterno = String.Empty;
            this.AMaterno = String.Empty;
            this.Edad = 0;
            this.Telefono1 = String.Empty;
            this.Telefono2 = String.Empty;
            this.Correo = String.Empty;
            this.Domicilio = String.Empty;

            this.CLSManejador = new ClsManejador();
        }

        //-----------------Methods
        public string InsertarAsociado()
        {
            string mensaje = "";
            List<ClsParametros> lst = new List<ClsParametros>();

            //Parametros de entrada
            lst.Add(new ClsParametros("@Nombre",this.Nombre));
            lst.Add(new ClsParametros("@APaterno", this.APaterno));
            lst.Add(new ClsParametros("@AMaterno", this.AMaterno));
            lst.Add(new ClsParametros("@Edad", this.Edad));
            lst.Add(new ClsParametros("@Telefono1", this.Telefono1));
            lst.Add(new ClsParametros("@Telefono2", this.Telefono2));
            lst.Add(new ClsParametros("@Correo", this.Correo));
            lst.Add(new ClsParametros("@Domicilio", this.Domicilio));

            //Parametro de salida
            lst.Add(new ClsParametros("@mensaje", SqlDbType.VarChar, 30));
            CLSManejador.Ejecutar_sp("pa_InsertarAsociado", lst);

            //Regresar el valor almacenado en el parametro de salida
            mensaje = lst[8].Valor.ToString();

            return (mensaje);
        }

        public DataTable BuscarAsociadoConSusServiciosXId()
        {
            List<ClsParametros> lst = new List<ClsParametros>();
            lst.Add(new ClsParametros("@idAsociado", this.Id));

            
            return CLSManejador.Listado("pa_BuscarAsociadoConSusServiciosXId", lst);
        }

    }
}
