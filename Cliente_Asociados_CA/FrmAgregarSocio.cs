using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente_Asociados_CA
{
    public partial class FrmAgregarSocio : Form
    {
        public FrmAgregarSocio()
        {
            InitializeComponent();
            cargarGrid();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(txtId.Text);
            var numeroSocio = txtNoSocio.Text;
            var nombre = txtNombre.Text;
            var apellidoP = txtApellidoPaterno.Text;
            var apellidoM = txtApellidoMaterno.Text;
            var fecha = DateTime.Parse(dateFecha.Text);
            var edad = Convert.ToInt32(txtEdad.Text);
            var cel = txtCelular.Text;
            var telf = txtTelefono.Text;
            var añoJub = cmbAñoJub.SelectedItem.ToString();
            var estadoCivil = cmbEstadoCivil.SelectedItem.ToString();
            var tipoSangre = cmbTipoSangre.SelectedItem.ToString();         
            var noImss = txtNoIMS.Text;
            var curp = txtCurp.Text;
            var direccion = 0;  
            var status = cmbStatus.SelectedItem.ToString();

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            pictureImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var imagen = ms.GetBuffer();
            var nombreCompletoE = txtNombreE.Text;
            var direccionE = txtDireccionE.Text;
            var telefonoE = txtTelefonoE.Text;

            using (Server_Asociados.ServerAsociadosClient socio = new Server_Asociados.ServerAsociadosClient())
            {
                socio.registrarSocio(id, numeroSocio, nombre, apellidoP,apellidoM, fecha, edad, cel,telf, añoJub, estadoCivil, 
                    tipoSangre, noImss, curp, direccion, imagen, nombreCompletoE,direccionE,telefonoE,status);
                MessageBox.Show("Registro Exitoso");

            }
        }

        void cargarGrid()
        {
            //using (Server_Asociados.ServerAsociadosClient socio = new Server_Asociados.ServerAsociadosClient())
            //{
            //    dgvBueno.DataSource = socio.allSocios();                
            //}

            Server_Asociados.ServerAsociadosClient socio = new Server_Asociados.ServerAsociadosClient();
            Server_Asociados.mostrarSocios s = new Server_Asociados.mostrarSocios();
            s = socio.getSocio();
            DataTable dt = new DataTable();
            dt = s.SocioTab;
            dgvSocio.DataSource = dt;
        }

        private void btnCargarImagen_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.openFileDialog1.ShowDialog();
                if (this.openFileDialog1.FileName.Equals("") == false)
                {

                    pictureImage.Load(this.openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido");
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscarPorNum_Click(object sender, EventArgs e)
        {
            var numSocio = txtBuscarNumSocio.Text;
            Server_Asociados.ServerAsociadosClient socio = new Server_Asociados.ServerAsociadosClient();
            Server_Asociados.getSocioByNoSocio s = new Server_Asociados.getSocioByNoSocio();
            s = socio.getBynoSocio(numSocio);
            DataTable dt = new DataTable();
            dt = s.socioTab;
            dgvSocio.DataSource = dt;
        }
    }
}
