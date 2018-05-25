using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using System.Data;

namespace CapaLogicaNegocios
{
    public class ClsServicio
    {
        //------properties
        public int Id { set; get; }
        public string Nombre { set; get; }
        public string Descripcion { set; get; }

        private ClsManejador CLSManejador { set; get; }

        //------------Constructor
        public ClsServicio()
        {
            this.Id = 0;
            this.Nombre = String.Empty;
            this.Descripcion = String.Empty;
            this.CLSManejador = new ClsManejador();
        }


        public string InsertarServicio()
        {
            string mensaje = "";
            List<ClsParametros> lst = new List<ClsParametros>();

            //Parametros de entrada
            lst.Add(new ClsParametros("@nombre", this.Nombre));
            lst.Add(new ClsParametros("@descripcion", this.Descripcion));


            //Parametro de salida
            lst.Add(new ClsParametros("@mensaje", SqlDbType.VarChar, 30));
            CLSManejador.Ejecutar_sp("pa_InsertarServicio", lst);

            //Regresar el valor almacenado en el parametro de salida
            mensaje = lst[2].Valor.ToString();

            return (mensaje);
        }

        public DataTable BuscarServicioXId()
        {
            List<ClsParametros> lst = new List<ClsParametros>();
            lst.Add(new ClsParametros("@idBuscado", this.Id));


            return CLSManejador.Listado("pa_BuscarServicioXId", lst);
        }
    }
}
