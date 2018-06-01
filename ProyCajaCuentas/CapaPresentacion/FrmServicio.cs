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

        //--------------------Methods controller
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

        private string ActualizarServicio(int idServicioBuscado, string nombreNew, string descripcionNew)
        {
            ClsServicio clsServicio = new ClsServicio();
            clsServicio.Id = idServicioBuscado;
            clsServicio.Nombre = nombreNew;
            clsServicio.Descripcion = descripcionNew;

            return(clsServicio.ActualizarServicio());
        }


        //----------Utils

        private void BorrarTxtBoxesFormCapturaServicio()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
        }

        private void MostrarEnTxtBoxsServicio(DataTable tabla)
        {
            DataRow fila = tabla.AsEnumerable().ElementAt(0);  //obtener la unica fila
            textBox4.Text = fila[0].ToString();
            textBox1.Text = fila[1].ToString();
            textBox2.Text = fila[2].ToString();
        }




        //--------------Eventos
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = MessageBox.Show("¿Estas usted seguro que desea continuar?", "Guardar cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(res == DialogResult.Yes)
                {
                    if (String.IsNullOrEmpty(textBox4.Text))
                    {
                        //insertar servicio
                        string respuesta;
                        respuesta = InsertarServicioH(textBox1.Text, textBox2.Text);
                        MessageBox.Show(respuesta);
                        BorrarTxtBoxesFormCapturaServicio();
                    }

                    else
                    {
                        //Actualizar Servicio
                        string respuesta;
                        int idServicio = Int32.Parse(textBox4.Text);
                        respuesta = ActualizarServicio(idServicio, textBox1.Text, textBox2.Text);
                        MessageBox.Show(respuesta);
                        BorrarTxtBoxesFormCapturaServicio();
                    }
                }
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex >= 0))
            {

                if (String.IsNullOrEmpty((dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue).ToString()))
                {
                }
                else
                {
                    try
                    {

                        string idEnTexto = (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue).ToString();
                        DataTable miTabla = BuscarServicioXId( Int32.Parse(idEnTexto));
                        MostrarEnTxtBoxsServicio(miTabla);

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BorrarTxtBoxesFormCapturaServicio();
        }
    }
}
