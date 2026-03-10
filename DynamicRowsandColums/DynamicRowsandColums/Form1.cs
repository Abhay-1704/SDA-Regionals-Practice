using System.ComponentModel;
using System.Security.Cryptography;

namespace DynamicRowsandColums
{
    public partial class Form1 : Form
    {
        BindingList<Traveller> travellers = new BindingList<Traveller>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = travellers;
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "Delete";
            btn.HeaderText = "Action";
            btn.Text = "Delete";
            btn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Traveller t = new Traveller(Guid.NewGuid().ToString(), textBox1.Text, textBox2.Text);
            travellers.Add(t);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return; 
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                DialogResult = MessageBox.Show("Are you sure you want to delete this traveller?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes)
                {
                    travellers.RemoveAt(e.RowIndex);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String names = "";
            foreach (Traveller t in travellers)
            {
                names = names + t.Name + ", ";
            }
            MessageBox.Show("Travellers: " + names);
        }
    }
    public class Traveller
    {
        public String Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Traveller(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
