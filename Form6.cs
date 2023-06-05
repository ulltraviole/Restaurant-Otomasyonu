using System;
using System.Windows.Forms;

namespace app
{
    public partial class Form6 : Form
    {
        readonly Database Database = new Database();

        public bool FirstAttempt { get; set; }

        public Form6()
        {
            InitializeComponent();
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            if (!Database.ConnectToApp())
            {
                MessageBox.Show("Veritabanına bağlanılamadı\nLütfen internet bağlantınızı kontrol ediniz.\nİnternet bağlantınızda bir problem yoksa:\nİletişim: emirerenkara@outlook.com");
            }
            if (FirstAttempt)
            {
                textBox5.Text = "2";
                textBox5.Hide();
            }
            else
            {
                label1.Hide();
            }
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                void addUser()
                {
                    if (Database.AddStaff(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text))
                    {
                        MessageBox.Show("Başarıyla bir garson hesabı oluşturdunuz.", "Başarılı");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt esnasında bir sorun oluştu.\nGeliştirici ile iletişime geçiniz.", "Başarısız");
                    }
                }
                switch (Int32.Parse(textBox5.Text))
                {
                    default:
                        MessageBox.Show("Lütfen 0/1/2 değerlerinden yalnız birini giriniz.", "Bilgi");
                        break;
                    case 0:
                        addUser();
                        break;
                    case 1:
                        addUser();
                        break;
                    case 2:
                        addUser();
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Lütfen 0/1/2 değerlerinden yalnız birini giriniz.", "Bilgi");
            }
        }
    }
}
