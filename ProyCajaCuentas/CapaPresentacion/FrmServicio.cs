using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogicaNegocios;

namespace CapaPresentacion
{
    public partial class FrmServicio : Form
    {
        public FrmServicio()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
        }

        //--------------------Methods
        private string InsertarServicioH(string nombre, string descripcion)
        {

            ClsServicio clsServicio = new ClsServicio();
            clsServicio.Nombre = nombre;
            clsServicio.Descripcion = descripcion;

            return (clsServicio.InsertarServicio());
        }

        private DataTable BuscarServicioXId(int idServicioBuscado)
        {
            ClsServicio clsServicio = new ClsServicio();
            clsServicio.Id = idServicioBuscado;
            DataTable respuesta = clsServicio.BuscarServicioXId();
            return (respuesta);
        }


        private void BorrarTxtBoxesFormCapturaServicio()
        {
            textBox1.Clear();
            textBox2.Clear();
        }





        //--------------Eventos
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta;
                respuesta = InsertarServicioH(textBox1.Text, textBox2.Text);
                MessageBox.Show(respuesta);
                BorrarTxtBoxesFormCapturaServicio();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int aux;
                aux = Int32.Parse(textBox3.Text);

                dataGridView1.DataSource = BuscarServicioXId(aux);

            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
