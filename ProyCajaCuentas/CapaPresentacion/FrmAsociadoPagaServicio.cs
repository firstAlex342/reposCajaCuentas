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
    public partial class FrmAsociadoPagaServicio : Form
    {
        //-----------Contructor
        public FrmAsociadoPagaServicio()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;

            //Es el dataGridView donde capturo cantidades
            dataGridView1.Columns.Add("Id_Servicio", "Id_Servicio");
            dataGridView1.Columns.Add("Nombre_Servicio", "Nombre_Servicio");
            dataGridView1.Columns.Add("Cantidad_a_pagar", "Cantidad_a_pagar");

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = false;

        }

        //----------------Methods
        private DataTable BuscarAsociadoConSusServiciosXId(int id)
        {
            ClsAsociado clsAsociado = new ClsAsociado();
            clsAsociado.Id = id;

            DataTable respuesta = clsAsociado.BuscarAsociadoConSusServiciosXId();

            return (respuesta);
        }

        private DataTable BuscarTodosServiciosMostrar_Id_Nombre()
        {
            ClsServicio clsServicio = new ClsServicio();
            DataTable miTabla = clsServicio.BuscarTodosServiciosMostrar_Id_Nombre();

            return (miTabla);
        }

        private void InsertarActualizarEnAsociadoPagaServicio(int idAsociado, int idServicio, decimal cantidad)
        {
            ClsAsociadoPagaServicio clsAsociadoPagaServicio = new ClsAsociadoPagaServicio();
            clsAsociadoPagaServicio.IdAsociado = idAsociado;
            clsAsociadoPagaServicio.IdServicio = idServicio;
            clsAsociadoPagaServicio.Cantidad = cantidad;

            clsAsociadoPagaServicio.InsertarActualizarEnAsociadoPagaServicio();
        }
   



        //----------------Utils
        private void MostrarDataTableAsociadoYServiciosEnView(DataTable tabla)
        {
          

            DataRow fila= tabla.Rows[0];
            textBox2.Text = fila[1].ToString(); // el nombre
            textBox3.Text = fila[2].ToString(); //APaterno
            textBox4.Text = fila[3].ToString();  //AMaterno
            textBox5.Text = fila[4].ToString();//Edad
            textBox6.Text = fila[5].ToString();//Tel 1
            textBox7.Text = fila[6].ToString();//Tel 2
            textBox8.Text = fila[7].ToString();//correo
            textBox9.Text = fila[8].ToString();//Domicilio

          
            //Examinar la tabla para ver si tiene Servicios el asociado
            //item[9] es la columna AsociadoPagaServicio.IdAsociado
            //item[10] es la columna AsociadoPagaServicio.IdServicio
            //item[11] es la columna AsociadoPagaServicio.Cantidad
            var res = (from item in tabla.AsEnumerable()
                       where (item[9] == DBNull.Value)  &&
                       (item[10] == DBNull.Value) && 
                       (item[11]==DBNull.Value)
                       select item).ToList();

            if(res.Count>=1)
            {
                //Este asociado no tiene servicios
                dataGridView2.DataSource = null;
            }
           else
            {
                //Este asociado si tiene servicios
                DataTable resumenTabla = new DataTable();
                DataColumn idServicioColumn = new DataColumn("IdServicio", System.Type.GetType("System.Int32"));
                DataColumn nombreServicioColumn = new DataColumn("Nombre servicio", System.Type.GetType("System.String"));
                DataColumn idCantidadColumn = new DataColumn("Cantidad", System.Type.GetType("System.Decimal"));
                
                resumenTabla.Columns.Add(idServicioColumn);
                resumenTabla.Columns.Add(nombreServicioColumn);
                resumenTabla.Columns.Add(idCantidadColumn);


                foreach(DataRow row in tabla.Rows)
                {                 
                    DataRow nuevaFila = resumenTabla.NewRow();
                    nuevaFila["IdServicio"] = row[10].ToString();
                    nuevaFila["Nombre Servicio"] = row[13].ToString();
                    nuevaFila["Cantidad"] = row[11].ToString() ;
                    resumenTabla.Rows.Add(nuevaFila);
                }

                dataGridView2.DataSource = resumenTabla;
            }                     
        }


        private void LimpiarTextBoxesPanelDetallesSocio()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox9.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }

        private void MostrarTableServiciosEnComBoBox(DataTable miTabla)
        {
            //Mostrar solo los idS de los servicios y el nombre
            foreach (DataRow fila in miTabla.Rows)
                comboBox1.Items.Add(fila[0].ToString() + " - " + fila[1].ToString());
                                               
        }

        private void AddFilaADataGridView_ServiciosAPagar(string idServicioElegidoYSuNombre)
        {
            string[] cadenaDividida = idServicioElegidoYSuNombre.Split('-');
            int n = dataGridView1.Rows.Add();
            dataGridView1.Rows[n].Cells[0].Value = cadenaDividida[0];
            dataGridView1.Rows[n].Cells[1].Value = cadenaDividida[1];
        }

        //private void Mostrar



        //-------------------Eventos
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable respuesta = BuscarAsociadoConSusServiciosXId(Int32.Parse(textBox1.Text));
                bool seEncontro = respuesta.Rows.Count > 0 ? true: false;

                if(seEncontro)
                {   
                    MostrarDataTableAsociadoYServiciosEnView(respuesta);
                }

                else
                {
                    
                    dataGridView2.DataSource = null;
                    LimpiarTextBoxesPanelDetallesSocio();
                    MessageBox.Show("Asociado no encontrado");
                }
                
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmAsociadoPagaServicio_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable miTabla = BuscarTodosServiciosMostrar_Id_Nombre();
                MostrarTableServiciosEnComBoBox(miTabla);
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex >=0)
            {
                AddFilaADataGridView_ServiciosAPagar( comboBox1.SelectedItem.ToString());
            }

            else
            {
                //No selecciono algo
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if(e.ColumnIndex == 2)
            {
                if(String.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Columna cantidad no debe estar vacia";
                    e.Cancel = true;
                }

                
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Limpiar la fila de error en  que el usuario presione esc
            dataGridView1.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               
                DataGridViewRowCollection coleccion = dataGridView1.Rows;
                foreach(DataGridViewRow row in coleccion)
                {
                    int idAsociado = Int32.Parse(textBox1.Text);
                    int idServicio = Int32.Parse(row.Cells[0].EditedFormattedValue.ToString());
                    decimal cantidad = Decimal.Parse(row.Cells[2].EditedFormattedValue.ToString());

                    InsertarActualizarEnAsociadoPagaServicio(idAsociado,idServicio,cantidad);
                }

                LimpiarTextBoxesPanelDetallesSocio();
                dataGridView2.DataSource = null;
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();              
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LimpiarTextBoxesPanelDetallesSocio();
            dataGridView2.DataSource = null;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
        }
    }
}
