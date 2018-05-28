using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaAccesoDatos;

namespace CapaLogicaNegocios
{
    public class ClsAsociadoPagaServicio
    {
        //--------properties
        public int IdAsociado { set; get; }
        public int IdServicio { set; get; }
        public decimal Cantidad { set; get; }

        private ClsManejador CLSManejador { set; get; }

        //---------Constructor
        public ClsAsociadoPagaServicio()
        {
            this.IdAsociado = 0;
            this.IdServicio = 0;
            this.Cantidad = 0.0M;

            this.CLSManejador = new ClsManejador();
        }

        //-----------Methods
        public string InsertarActualizarEnAsociadoPagaServicio()
        {
            string mensaje = "";
            List<ClsParametros> lst = new List<ClsParametros>();

            //Parametros de entrada
            lst.Add(new ClsParametros("@idAsociadoBuscado", this.IdAsociado));
            lst.Add(new ClsParametros("@idServicioBuscado", this.IdServicio));
            lst.Add(new ClsParametros("@nuevaCantidad", this.Cantidad));


            //Parametro de salida
            lst.Add(new ClsParametros("@mensaje", SqlDbType.VarChar, 30));
            CLSManejador.Ejecutar_sp("pa_InsertarActualizarEnAsociadoPagaServicio", lst);

            //Regresar el valor almacenado en el parametro de salida
            mensaje = lst[3].Valor.ToString();

            return (mensaje);
        }
    }
}
