using FleetDb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetDb
{
    public partial class Edit_Add : Form
    {
        FleetDbContext db;
        int vehicleId = 0;
        public Edit_Add(int id)
        {
            InitializeComponent();
            db = new FleetDbContext();
            vehicleId = id;
        }

        private void Edit_Add_Load(object sender, EventArgs e)
        {
            if (vehicleId != 0)
            {
                Vehicle vehicle = db.Vehicles.Where(x => x.VehicleId == vehicleId).FirstOrDefault();
                if (vehicle != null)
                {
                    textBox1.Text = vehicle.LicensePlate;
                    textBox2.Text = vehicle.ModelName;
                    dateTimePicker1.Value = vehicle.LastServiceDate;
                    numericUpDown1.Value = vehicle.Mileage;
                    checkBox1.Checked = vehicle.RequiresMaintenance;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (vehicleId == 0)
            {
                Vehicle vehicle = new Vehicle();

                vehicle.LicensePlate = textBox1.Text;
                vehicle.ModelName = textBox2.Text;
                vehicle.LastServiceDate = dateTimePicker1.Value;
                vehicle.Mileage = Convert.ToInt32(numericUpDown1.Value);
                vehicle.RequiresMaintenance = checkBox1.Checked;

                db.Vehicles.Add(vehicle);
            }
            else
            {
                Vehicle vehicle = db.Vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);

                if (vehicle != null)
                {
                    vehicle.LicensePlate = textBox1.Text;
                    vehicle.ModelName = textBox2.Text;
                    vehicle.LastServiceDate = dateTimePicker1.Value;
                    vehicle.Mileage = Convert.ToInt32(numericUpDown1.Value);
                    vehicle.RequiresMaintenance = checkBox1.Checked;
                }
            }

            db.SaveChanges();
            MessageBox.Show("Changes Saved !!");

            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }
    }
}
