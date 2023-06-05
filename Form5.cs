using MongoDB.Bson;
using System;
using System.Windows.Forms;

namespace app
{
    public partial class Form5 : Form
    {
        readonly Database Database = new Database();
        public Form5()
        {
            InitializeComponent();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            if (!Database.ConnectToApp())
            {
                MessageBox.Show("Veritabanına bağlanılamadı\nLütfen internet bağlantınızı kontrol ediniz.\nİnternet bağlantınızda bir problem yoksa:\nİletişim: emirerenkara@outlook.com");
            }
            if (!Database.CheckStaff())
            {
                MessageBox.Show("Masalar üzerinde işlem yapmak için garson hesabı oluşturmalısınız.", "Bilgi.");
                Form6 RegisterWaiter = new Form6();
                RegisterWaiter.ShowDialog();
            }
            CenterToScreen();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string Username = textBox1.Text;
            string Password = textBox2.Text;
            BsonDocument User = Database.AuthStaff(Username, Password);
            if (User != null)
            {
                MessageBox.Show("Başarıyla giriş yaptınız.", "Başarılı");
                Form3 Uygulama = new Form3
                {
                    WaiterName = User["Fullname"].ToString(),
                    AuthLevel = User["AuthLevel"].ToString()
                };
                Uygulama.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Girmiş olduğunuz kullanıcı adı ve/veya şifre hatalı.", "Başarısız");
            }
        }
    }
}