using CrdWindowsFormsAdoNet.DataAccess;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrdWindowsFormsAdoNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvPeopleData.DataSource = LoadData();
        }

        private List<People> LoadData()
        {
            txtName.Focus();
            PeopleDbMethods methods = new PeopleDbMethods();
            return methods.GetAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvPeopleData.DataSource = LoadData();
        }

        private void cleanInputs()
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtId.Text = "0";
            txtName.Focus();
        }

        private int CurrentRoId()
        {
            return int.Parse(dgvPeopleData.Rows[dgvPeopleData.CurrentRow.Index].Cells[0].Value.ToString());
        }

        private bool Validations(int action)
        {
            var validationsFlag = false;

            if (action == 1)//Add
            {
                if (string.IsNullOrEmpty(txtName.Text) ||
                    string.IsNullOrEmpty(txtAge.Text) ||
                        int.Parse(txtAge.Text) == 0)
                {
                    MessageBox.Show("Nombre y Edad no deben estar vacios o Edad no puede ser 0: ", "Notificacion!",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    validationsFlag = true;
                }
            }

            if (action == 2)//Update
            {
                if (int.Parse(txtId.Text) == 0)
                {
                    MessageBox.Show("El Id tiene valor 0 o incorrecto: ", "Notificacion!",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else if (string.IsNullOrEmpty(txtName.Text) ||
                    string.IsNullOrEmpty(txtAge.Text) ||
                    int.Parse(txtAge.Text) == 0)
                {
                    MessageBox.Show("Nombre y Edad no deben estar vacios o Edad no puede ser 0: ", "Notificacion!",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    validationsFlag = true;
                }
            }

            if (action == 3)//Delete
            {
                if (int.Parse(txtId.Text) == 0)
                {
                    MessageBox.Show("El Id tiene valor 0 o incorrecto: ", "Notificacion!",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    validationsFlag = true;
                }
            }

            return validationsFlag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            People people = new People();
            PeopleDbMethods dbMethods = new PeopleDbMethods();

            try
            {
                if (Validations(1))
                {
                    people.Name = txtName.Text;
                    people.Age = int.Parse(txtAge.Text);
                    dbMethods.AddPeople(people);
                    cleanInputs();
                    dgvPeopleData.DataSource = LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error del sistema: " + ex.Message, "Notificacion!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            People people = new People();
            PeopleDbMethods dbMethods = new PeopleDbMethods();

            try
            {
                if (Validations(2))
                {
                    people.Id = int.Parse(txtId.Text);
                    people.Name = txtName.Text;
                    people.Age = int.Parse(txtAge.Text);
                    dbMethods.EditPeople(people);
                    cleanInputs();
                    dgvPeopleData.DataSource = LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error del sistema: " + ex.Message, "Notificacion!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPeopleData_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            People people = new People();
            PeopleDbMethods dbMethods = new PeopleDbMethods();

            try
            {
                if (Validations(3))
                {
                    people.Id = int.Parse(txtId.Text);
                    dbMethods.DeletePeople(people);
                    cleanInputs();
                    dgvPeopleData.DataSource = LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error del sistema: " + ex.Message, "Notificacion!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPeopleData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PeopleDbMethods dbMethods = new PeopleDbMethods();
            var rowClicked = CurrentRoId();
            var peopleGet = dbMethods.Get(rowClicked);
            txtId.Text = peopleGet.Id.ToString();
            txtName.Text = peopleGet.Name;
            txtAge.Text = peopleGet.Age.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cleanInputs();
        }
    }
}
