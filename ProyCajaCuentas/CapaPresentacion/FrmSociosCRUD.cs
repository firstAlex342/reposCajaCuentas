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
    public partial class FrmSociosCRUD : Form
    {

        //---------Constructor
        public FrmSociosCRUD()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
        }

        //-----------Methods Controladores
        private string InsertarAsociado(string nombre, string aPaterno, string aMaterno,int edad, string telefono1, string telefono2, string correo, string domicilio)
        {
            ClsAsociado clsAsociado = new ClsAsociado();
            clsAsociado.Nombre = nombre;
            clsAsociado.APaterno = aPaterno;
            clsAsociado.AMaterno = aMaterno;
            clsAsociado.Edad = edad;
            clsAsociado.Telefono1 = telefono1;
            clsAsociado.Telefono2 = telefono2;
            clsAsociado.Correo = correo;
            clsAsociado.Domicilio = domicilio;

            return (clsAsociado.InsertarAsociado());
        }

        private string ActualizarAsociado(int idBuscado, string nombreNew, string APaternoNew, string AMaternoNew, int edadNew, string telefono1New, string telefono2New, string correoNew, string domicilioNew)
        {
            ClsAsociado clsAsociado = new ClsAsociado();
            clsAsociado.Id = idBuscado;
            clsAsociado.Nombre = nombreNew;
            clsAsociado.APaterno = APaternoNew;
            clsAsociado.AMaterno = AMaternoNew;
            clsAsociado.Edad = edadNew;
            clsAsociado.Telefono1 = telefono1New;
            clsAsociado.Telefono2 = telefono2New;
            clsAsociado.Correo = correoNew;
            clsAsociado.Domicilio = domicilioNew;

            return(clsAsociado.ActualizarAsociado());
        }

        private DataTable BuscarAsociadoXId(int idBuscado)
        {
            ClsAsociado clsAsociado = new ClsAsociado();
            clsAsociado.Id = idBuscado;

            return (clsAsociado.BuscarAsociadoXId());
        }

        //-----------Methods utils
        public void BorrarTxtBoxDeCapturaAsociado()
        {
            textBox1.Clear();         
            textBox2.Clear();
            textBox3.Clear();
            textBox9.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox10.Clear(); //textBox de Id
        }

        private void MostrarAsociadoEnTxtBoxes(DataTable tabla)
        {
            DataRow fila = tabla.AsEnumerable().ElementAt(0);  //obtener la primera fila, que es la unica de la tabla
            textBox10.Text = fila[0].ToString();  //id         
            textBox2.Text = fila[2].ToString(); //APaterno
            textBox3.Text = fila[3].ToString(); //AMaterno
            textBox1.Text = fila[1].ToString(); //nombre
            textBox9.Text = fila[4].ToString();  //Edad
            textBox4.Text = fila[5].ToString();  //Telefono1
            textBox5.Text = fila[6].ToString(); //Telefono2
            textBox6.Text = fila[7].ToString(); //Correo
            textBox7.Text = fila[8].ToString(); //Domicilio

        }


        //------------Eventos
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult res = MessageBox.Show("¿Estas usted seguro que desea continuar?", "Guardar cambios",  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(res== DialogResult.Yes)
                {
                    if (String.IsNullOrEmpty(textBox10.Text))
                    {   //Se hace un insercion
                        string respuesta = InsertarAsociado(textBox1.Text, textBox2.Text, textBox3.Text, Int32.Parse(textBox9.Text), textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);

                        MessageBox.Show(respuesta);
                        BorrarTxtBoxDeCapturaAsociado();
                    }

                    else
                    {   //Se hace una actualizacion
                        int id = Int32.Parse(textBox10.Text);
                        int edad = Int32.Parse(textBox9.Text);

                        string respuesta = ActualizarAsociado(id, textBox1.Text, textBox2.Text, textBox3.Text, edad, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
                        MessageBox.Show(respuesta);
                        BorrarTxtBoxDeCapturaAsociado();
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
                ClsAsociado clsAsociado = new ClsAsociado();
                clsAsociado.Id = Int32.Parse(textBox8.Text);

                DataSet miDataSet = new DataSet();
                miDataSet.Tables.Add(clsAsociado.BuscarAsociadoConSusServiciosXId());
                dataGridView1.DataSource = miDataSet.Tables[0];
                

                
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
                        //MessageBox.Show((dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue).ToString());
                        string idEnTexto =(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue).ToString();
                        DataTable miTabla = BuscarAsociadoXId(Int32.Parse(idEnTexto));
                        MostrarAsociadoEnTxtBoxes(miTabla);

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
            BorrarTxtBoxDeCapturaAsociado();
        }
    }
}
