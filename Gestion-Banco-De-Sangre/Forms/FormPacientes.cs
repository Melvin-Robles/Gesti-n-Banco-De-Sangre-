using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Banco_De_Sangre.Forms
{
    public partial class FormPacientes : Form
    {

        public FormPacientes()
        {
            InitializeComponent();
            CargarCmb();
        }

        private void Cargar()
        {
            BaseGestionBDSEntitiesConect contexto = new BaseGestionBDSEntitiesConect();
            DGVPacientes.Columns.Clear();
            DGVPacientes.DataSource = contexto.Paciente.ToList();

            DGVPacientes.Columns.Add("Género", "Género");
            DGVPacientes.Columns.Add("Tipo Sangre", "Tipo Sangre");
            DGVPacientes.Columns.Add("Tipo RH", "Tipo RH");

            for (int i = 0; i < DGVPacientes.RowCount; i++)
            {
                DGVPacientes.Rows[i].Cells[12].Value = contexto.Genero.Find(DGVPacientes.Rows[i].Cells[3].Value).NombreGenero;
                DGVPacientes.Rows[i].Cells[13].Value = contexto.TipoSangre.Find(DGVPacientes.Rows[i].Cells[5].Value).NombreTS;
                DGVPacientes.Rows[i].Cells[14].Value = contexto.TipoRH.Find(DGVPacientes.Rows[i].Cells[6].Value).NombreRH;
            }

            for (int i = 0; i < 7; i++)
            {
                DGVPacientes.Columns.RemoveAt(5);
            }

            DGVPacientes.Columns.RemoveAt(3);
            DGVPacientes.AutoResizeColumns();
            DGVPacientes.AutoResizeRows();
            
            Refrescar();

        }

        private void Refrescar()
        {
            BaseGestionBDSEntitiesConect contexto = new BaseGestionBDSEntitiesConect();
            txtId.Text = Convert.ToString(contexto.Paciente.Count() + 1);
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtEdad.Text = null;
            cmbGenero.SelectedIndex = -1;
            cmbTipoSangre.SelectedIndex = -1;
            cmbTipoRH.SelectedIndex = -1;
            contexto.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Paciente', RESEED, {0})", contexto.Paciente.Count());
        }

        private void CargarCmb()
        {
            BaseGestionBDSEntitiesConect contexto = new BaseGestionBDSEntitiesConect();

            for (int i = 1; i <= contexto.Genero.Count(); i++)
                cmbGenero.Items.Add(contexto.Genero.Find(i).NombreGenero);

            for (int i = 1; i <= contexto.TipoSangre.Count(); i++)
                cmbTipoSangre.Items.Add(contexto.TipoSangre.Find(i).NombreTS);

            for (int i = 1; i <= contexto.TipoRH.Count(); i++)
                cmbTipoRH.Items.Add(contexto.TipoRH.Find(i).NombreRH);
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            Cargar();
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloNumeros(e);
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloLetras(e);
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloLetras(e);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarVacío())
            {
                MessageBox.Show("Debe completar la informacion");

                return;
            }
            
            using (BaseGestionBDSEntitiesConect contexto = new BaseGestionBDSEntitiesConect())
            {
                Paciente c = new Paciente()
                {
                    Id = Convert.ToInt16(txtId.Text),
                    Nombres = txtNombres.Text,
                    Apellidos = txtApellidos.Text,
                    Genero = cmbGenero.SelectedIndex + 1,
                    Edad = Convert.ToInt16(txtEdad.Text),
                    TipoSangre = cmbTipoSangre.SelectedIndex + 1,
                    TipoRH = cmbTipoRH.SelectedIndex + 1
                };

                contexto.Paciente.Add(c);
                contexto.SaveChanges();
                Cargar();
            }
        }

        private void LlenarModificar()
        {
            txtId.Text = DGVPacientes.SelectedRows[0].Cells[0].Value.ToString();
            txtNombres.Text = DGVPacientes.SelectedRows[0].Cells[1].Value.ToString();
            txtApellidos.Text = DGVPacientes.SelectedRows[0].Cells[2].Value.ToString();
            txtEdad.Text = DGVPacientes.SelectedRows[0].Cells[3].Value.ToString();
            cmbGenero.SelectedItem = DGVPacientes.SelectedRows[0].Cells[4].Value.ToString();
            cmbTipoSangre.SelectedItem = DGVPacientes.SelectedRows[0].Cells[5].Value.ToString();
            cmbTipoRH.SelectedItem = DGVPacientes.SelectedRows[0].Cells[6].Value.ToString();
        }

        private bool ValidarVacío()
        {
            bool v1 = false, v2 = false, validar = false;

            if (string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidos.Text) || string.IsNullOrEmpty(txtEdad.Text))
                v1 = false;
            else
                v1 = true;

            if (cmbGenero.SelectedIndex == -1 || cmbTipoSangre.SelectedIndex == -1 || cmbTipoRH.SelectedIndex == -1)
                v2 = false;
            else
                v2 = true;

            if (v1 && v2)
                validar = false;
            else
                validar = true;

            return validar;
        }

        private void DGVPacientes_Click(object sender, EventArgs e)
        {
            LlenarModificar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarVacío())
            {
                MessageBox.Show("Debe completar la informacion");

                return;
            }

            int id = Convert.ToInt16(txtId.Text);

            using(BaseGestionBDSEntitiesConect contexto = new BaseGestionBDSEntitiesConect())
            {
                Paciente c = contexto.Paciente.FirstOrDefault(x => x.Id == id);
                c.Nombres = txtNombres.Text;
                c.Apellidos = txtApellidos.Text;
                c.Genero = cmbGenero.SelectedIndex + 1;
                c.Edad = Convert.ToInt32(txtEdad.Text);
                c.TipoSangre = cmbTipoSangre.SelectedIndex + 1;
                c.TipoRH = cmbTipoRH.SelectedIndex + 1;
                contexto.SaveChanges();
                Cargar();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(txtId.Text) > DGVPacientes.RowCount)
            {
                MessageBox.Show("Debe seleccionar un registro para eliminar");

                return;
            }

            int id = Convert.ToInt16(txtId.Text);

            using (BaseGestionBDSEntitiesConect contexto = new BaseGestionBDSEntitiesConect())
            {
                Paciente c = contexto.Paciente.FirstOrDefault(x => x.Id == id);
                contexto.Paciente.Remove(c);
                contexto.SaveChanges();
                Cargar();
            }
        }
    }
}
