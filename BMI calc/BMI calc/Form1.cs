namespace BMI_calc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Set up the DataGridView columns
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Weight (kg)";
            dataGridView1.Columns[2].Name = "Height (cm)";
            dataGridView1.Columns[3].Name = "BMI";
            dataGridView1.Columns[4].Name = "Category";

            // Not allow users to add rows directly in the DataGridView and set columns to fill the available space
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                double weight = Convert.ToDouble(textBox2.Text);
                double height = Convert.ToDouble(textBox3.Text);

                //Convert height from cm to meters
                double heightinmeters = height / 100;

                //Calculate BMI
                double bmi = weight / (heightinmeters * heightinmeters);

                //Round BMI to 1 decimal places
                bmi = Math.Round(bmi, 1);

                //Determine BMI category
                string category;

                if (bmi < 18.5)
                {
                    category = "Underweight";
                }
                else if (bmi >= 18.5 && bmi < 25)
                {
                    category = "Normal weight";
                }
                else if (bmi >= 25 && bmi < 30)
                {
                    category = "Overweight";
                }
                else
                {
                    category = "Obese";
                }

                int rowIndex = dataGridView1.Rows.Add(name, weight, height, bmi, category);

                if (category == "Normal weight")
                {
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                } else
                {
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Red;
                }

                //Clear input fields after adding the entry
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

            }
            catch
            {
                MessageBox.Show("Please enter valid inputs for name, weight, and height.");
            }
        }
    }
}
