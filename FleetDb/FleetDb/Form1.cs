using FleetDb.Models;

namespace FleetDb
{
    public partial class Form1 : Form
    {
        // Step - 1 Intialize the database context (Globally)
        FleetDbContext db;
        public Form1()
        {
            InitializeComponent();
            //Step - 2 Create an instance of the database context
            db = new FleetDbContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Step - 3 Retrieve data from the database and bind it to the DataGridView
            List<Vehicle> vehicles = db.Vehicles.ToList();
            dataGridView1.DataSource = vehicles;
            //Step - 4 Renaming the ColuMns Putting Space in between them
            // This is to fit datagridview to length
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns["VehicleID"].Visible = false;
            dataGridView1.Columns["LicensePlate"].HeaderText = "License Plate";
            dataGridView1.Columns["ModelName"].HeaderText = "Model Name";
            dataGridView1.Columns["LastServiceDate"].HeaderText = "Last Service Date";
            dataGridView1.Columns["RequiresMaintenance"].HeaderText = "Requires Maintenance";

            //Step - 5 Adding Edit Button
            DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
            // These defines the button field
            edit.Name = "Edit";
            edit.HeaderText = "Edit";
            edit.Text = "Edit";
            // This is to use Text value for column name
            edit.UseColumnTextForButtonValue = true;
            // This is to add column at th end
            dataGridView1.Columns.Add(edit);

            //Step - 6 Adding CheckBox to delete bulk data (Create the Column same as edit button)
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.Name = "Select";
            chk.HeaderText = "Select";
            chk.HeaderText = "Select";
            // Insert at zeroth position
            dataGridView1.Columns.Insert(0, chk);

            ApplyColors();

            button3.Enabled = false;
        }

        private void ApplyColors()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int mileage = Convert.ToInt32(row.Cells["Mileage"].Value);
                bool service = Convert.ToBoolean(row.Cells["RequiresMaintenance"].Value);
                DateTime lastServiceDate = Convert.ToDateTime(row.Cells["LastServiceDate"].Value);

                if (service)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else if (lastServiceDate >= DateTime.Now.AddDays(-30))
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (mileage > 100000)
                {
                    row.DefaultCellStyle.BackColor = Color.Orange;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
            {
                // Tells which row was clicked
                int vehicleId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["VehicleID"].Value);
                // MessageBox.Show(vehicleId.ToString());

                //  Go to Edit/Add Form
                // we passing vehicle id as parameter here
                Edit_Add form = new Edit_Add(vehicleId);
                form.Show();
                this.Hide();
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Select")
            {
                button3.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Edit_Add form = new Edit_Add(0);
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Vehicles.ToList();
            ApplyColors();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<int> selectedIds = new List<int>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["Select"].Value);

                if (isChecked)
                {
                    int id = Convert.ToInt32(row.Cells["VehicleID"].Value);
                    selectedIds.Add(id);
                }
            }

            foreach (int id in selectedIds)
            {
                Vehicle vehicle = db.Vehicles.FirstOrDefault(x => x.VehicleId == id);

                if (vehicle != null)
                {
                    db.Vehicles.Remove(vehicle);
                }
            }

            db.SaveChanges();
            dataGridView1.DataSource = db.Vehicles.ToList();
            ApplyColors();
            button3.Enabled = false;
        }
    }
}