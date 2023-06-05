using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Azure;
using Azure.Communication.Email;
using MongoDB.Bson;

namespace app
{
    public partial class Form1 : Form
    {
        readonly Database Database = new Database();

        public Form1()
        {
            InitializeComponent();
        }

        public async void SendMail(string Fullname, string Email, string Password)
        {
            string subject = "Adisyon Uygulaması Şifre Hatırlatıcı";
            string htmlContent = $"<html><body><h2>Adisyon Uygulaması Şifreniz</h2><p>Merhaba sayın {Fullname}.<br />İsteğiniz üzerine uygulamamızdaki hesabınızın şifresini size göndermiş bulunuyoruz.<br />Şifreniz tırnakların arasındadır: \"{Password}\"</p></body></html>";
            string sender = "noreply@emirerenkara.me";
            string recipient = Email;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://authorize.emirerenkara.me/");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                AzureKeyCredential key = new AzureKeyCredential(new StreamReader(response.GetResponseStream()).ReadToEnd());
                Uri endpoint = new Uri("https://restaurant-otomasyonu.communication.azure.com/");
                EmailClient EmailClient = new EmailClient(endpoint, key);
                EmailSendOperation emailSendOperation = await EmailClient.SendAsync(
                    Azure.WaitUntil.Completed,
                    sender,
                    recipient,
                    subject,
                    htmlContent);
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Hata kodu: {ex.ErrorCode}, mesaj: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Database.ConnectToApp())
            {
                MessageBox.Show("Veritabanına bağlanılamadı\nLütfen internet bağlantınızı kontrol ediniz.\nİnternet bağlantınızda bir problem yoksa:\nİletişim: emirerenkara@outlook.com");
            }
            else
            {

                if (!Database.CheckStaff())
                {
                    MessageBox.Show("Masalar üzerinde işlem yapmak için personel hesabı oluşturmalısınız.", "Bilgi.");
                    Form6 AddStaff = new Form6
                    {
                        FirstAttempt = true
                    };
                    AddStaff.ShowDialog();
                }
            }
            CenterToScreen();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string Username = textBox1.Text;
            string Password = textBox2.Text;
            BsonDocument StaffInfo = Database.AuthStaff(Username, Password);
            if (StaffInfo != null)
            {
                MessageBox.Show("Başarıyla giriş yaptınız.", "Başarılı");
                Form3 Uygulama = new Form3
                {
                    WaiterName = StaffInfo["Fullname"].ToString(),
                    AuthLevel = StaffInfo["AuthLevel"].ToString()
                };
                Uygulama.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Girmiş olduğunuz kullanıcı adı ve/veya şifre hatalı.", "Başarısız");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Username = textBox1.Text;
            List<BsonDocument> Staffs = Database.GetStaffs();
            foreach (BsonDocument Staff in Staffs)
            {
                if (Staff["Username"] == Username)
                {
                    SendMail(Staff["Fullname"].AsString, Staff["Email"].AsString, Staff["Password"].AsString);
                    MessageBox.Show($"Sayın {Staff["Fullname"].AsString}, güncel şifreniz sistemimizde kayıtlı \"{Staff["Email"]}\" mail adresinize başarıyla gönderildi.", "Başarılı");
                }
                else
                {
                    MessageBox.Show("Üzgünüz, böyle bir kullanıcı bulunamadı.", "Başarısız");
                }
            }
        }
    }
}
