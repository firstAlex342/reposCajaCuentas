using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        //--------------Methods
        private void AbrirFormulario(object formHijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);

            Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;

            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        }

        //----------Eventos

        private void operacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmSociosCRUD frmSociosCRUD = new FrmSociosCRUD();
            //frmSociosCRUD.ShowDialog(this);
            //frmSociosCRUD.Dispose();
            AbrirFormulario(new FrmSociosCRUD());
            
        }

        private void ingresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCajaIngresar frmCajaIngresar = new FrmCajaIngresar();
            frmCajaIngresar.ShowDialog(this);
            frmCajaIngresar.Dispose();
        }

        private void observacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmObservacion frmObservacion = new FrmObservacion();
            frmObservacion.ShowDialog(this);
            frmObservacion.Dispose();
        }



        private void nuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmServicio());
        }
    }
}
