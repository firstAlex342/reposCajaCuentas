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
        public FrmSociosCRUD()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
        }

        //-------Methods
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

        }
        
        //------------Eventos
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                ClsAsociado clsAsociado = new ClsAsociado();
                clsAsociado.Nombre = textBox1.Text;
                clsAsociado.APaterno = textBox2.Text;
                clsAsociado.AMaterno = textBox3.Text;
                clsAsociado.Edad = Int32.Parse(textBox9.Text);
                clsAsociado.Telefono1 = textBox4.Text;
                clsAsociado.Telefono2 = textBox5.Text;
                clsAsociado.Correo = textBox6.Text;
                clsAsociado.Domicilio = textBox7.Text;

                MessageBox.Show(clsAsociado.InsertarAsociado());
                BorrarTxtBoxDeCapturaAsociado();

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
    }
}
