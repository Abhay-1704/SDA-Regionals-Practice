using OfficeDb.Models;

namespace OfficeDb
{
    public partial class Form1 : Form
    {
        OfficeDbContext db;
        int selecteditemid = 0;
        public Form1()
        {
            InitializeComponent();
            db = new OfficeDbContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<OfficeSupply> supplies = db.OfficeSupplies.ToList();
            dataGridView1.DataSource = supplies;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.Name = "Select";
            chk.HeaderText = "Select";
            dataGridView1.Columns.Insert(0, chk);

            button3.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Select")
            {
                button3.Enabled = true;
            }
        }

        // function to select and sjow data of that row
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Select")
            {
                return;
            }

            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ItemId"].Value);

            OfficeSupply supplies = db.OfficeSupplies.FirstOrDefault(x => x.ItemId == id);

            if (supplies != null)
            {
                textBox1.Text = supplies.ItemName;
                textBox3.Text = supplies.Department;
                numericUpDown1.Value = Convert.ToInt32(supplies.Quantity);

                selecteditemid = supplies.ItemId;
            }

            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OfficeSupply addsupply = new OfficeSupply();
            addsupply.ItemName = textBox1.Text;
            addsupply.Department = textBox3.Text;
            addsupply.Quantity = Convert.ToInt32(numericUpDown1.Value);
            db.OfficeSupplies.Add(addsupply);
            db.SaveChanges();
            dataGridView1.DataSource = db.OfficeSupplies.ToList();
            textBox1.Clear();
            textBox3.Clear();
            numericUpDown1.Value = 0;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (selecteditemid == null)
            {
                return;
            }

            OfficeSupply supply = db.OfficeSupplies.FirstOrDefault(x => x.ItemId == selecteditemid);

            if (supply != null)
            {
                supply.ItemName = textBox1.Text;
                supply.Department = textBox3.Text;
                supply.Quantity = Convert.ToInt32(numericUpDown1.Value);

                db.SaveChanges();

                dataGridView1.DataSource = db.OfficeSupplies.ToList();

                textBox1.Clear();
                textBox3.Clear();
                numericUpDown1.Value = 0;
                selecteditemid = 0;

                button2.Enabled = false;
                button1.Enabled = true;
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            List<int> selectedids = new List<int>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["Select"].Value);

                if (isChecked)
                {
                    int id = Convert.ToInt32(row.Cells["ItemId"].Value);
                    selectedids.Add(id);
                }
            }

            if (!selectedids.Any())
            {
                MessageBox.Show("No Items are Selected");
            }

            foreach (int id in selectedids)
            {
                OfficeSupply supplies = db.OfficeSupplies.FirstOrDefault(x => x.ItemId == id);

                if (supplies != null)
                {
                    db.OfficeSupplies.Remove(supplies);
                }
            }

            db.SaveChanges();
            dataGridView1.DataSource = db.OfficeSupplies.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
