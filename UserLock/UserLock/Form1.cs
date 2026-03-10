namespace UserLock
{
    public partial class Form1 : Form
    {
        int invalidLogin = 0;
        int lockoutTime = 10; // Lockout time in seconds
        System.Windows.Forms.Timer loginTimer;
        System.Windows.Forms.Timer countdownTimer;
        DateTime competitionDate = new DateTime(2026, 3, 12, 0, 0, 0); // Set your competition date here

        public Form1()
        {
            InitializeComponent();
            loginTimer = new System.Windows.Forms.Timer();
            countdownTimer = new System.Windows.Forms.Timer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "pass")
            {
                MessageBox.Show("Login successful!");
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
                invalidLogin++;
                if (invalidLogin >= 3)
                {
                    MessageBox.Show("Too many invalid login attempts. Try in 10 seconds again", "Error");
                    button1.Enabled = false;
                    loginTimer.Start();
                    label1.Text = $"Locked out for {lockoutTime} seconds";
                }
            }
        }

        private void loginTimerFunction(object? sender, EventArgs e)
        {
            lockoutTime--;
            label1.Text = $"Locked out for {lockoutTime} seconds";
            if (lockoutTime <= 0)
            {
                loginTimer.Stop();
                button1.Enabled = true;
                invalidLogin = 0;
                label1.Text = "";
                lockoutTime = 10; // Reset lockout time for the next round of invalid attempts
            }
        }

        private void countdownTimerFunction(object? sender, EventArgs e)
        {
            TimeSpan timeSpan = competitionDate - DateTime.Now;
            label2.Text = $"Competition starts in {timeSpan.Days} Days, {timeSpan.Hours} Hours, {timeSpan.Minutes} minutes and {timeSpan.Seconds} Seconds";
            if (timeSpan.TotalSeconds <= 0)
            {
                countdownTimer.Stop();
                label2.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Login lockout timer setup
            loginTimer.Interval = 1000;
            loginTimer.Tick += loginTimerFunction;

            // Competition countdown timer setup
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += countdownTimerFunction;
            countdownTimer.Start();

            // Show immediately on load without waiting 1 second
            countdownTimerFunction(null, EventArgs.Empty);
        }
    }
}