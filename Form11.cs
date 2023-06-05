using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace app
{
    public partial class Form11 : Form
    {

        readonly Database Database = new Database();

        BsonDocument StoreInformation;

        public string WaiterName
        {
            get; set;
        }
        public string AuthLevel
        {
            get; set;
        }

        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            if (!Database.ConnectToApp())
            {
                MessageBox.Show("Veritabanına bağlanılamadı\nLütfen internet bağlantınızı kontrol ediniz.\nİnternet bağlantınızda bir problem yoksa:\nİletişim: emirerenkara@outlook.com");
            }
            else
            {
                StoreInformation = Database.GetStoreInformation();
                label3.Text = StoreInformation["StoreName"].AsString;
                label4.Text = (StoreInformation["StoreTableCount"].AsInt32).ToString();
                textBox1.Text = StoreInformation["StoreName"].AsString;
                textBox2.Text = (StoreInformation["StoreTableCount"].AsInt32).ToString();

            }

            CenterToScreen();

        }

        private void Form11_FormClosing(object sender, FormClosingEventArgs args)
        {
            Form3 Adisyon = new Form3
            {
                WaiterName = WaiterName,
                AuthLevel = AuthLevel,
            };
            Adisyon.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int StoreTableCount = 0;
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    StoreTableCount = Int32.Parse(textBox2.Text);
                    if (StoreTableCount <= 12 && StoreTableCount >= 4)
                    {
                        try
                        {
                            Database.UpdateStoreInformation(textBox1.Text, StoreTableCount);
                            MessageBox.Show("Bilgiler başarıyla kayıt edildi.", "Başarılı");
                        }
                        catch
                        {
                            MessageBox.Show("Kayıt esnasında bir sorun oluştu.\nGeliştirici ile iletişime geçiniz.", "Başarısız");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Masa sayınız 12'den büyük ve 4'ten küçük olmamalı.", "Bilgi.");
                    }
                }
                catch
                {
                    MessageBox.Show("Masa sayısı bölümüne yalnızca sayı girmelisiniz.", "Bilgi.");
                }
            }
            else
            {
                MessageBox.Show("Size ayrılan tüm bölümleri doldurunuz.", "Bilgi.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form6 CalisanEkle = new Form6();
            CalisanEkle.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form10 CalisanKaldir = new Form10
            {
                WaiterName = WaiterName,
                AuthLevel = AuthLevel,
            };
            CalisanKaldir.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form8 UrunEkle = new Form8
            {
                WaiterName = WaiterName,
                AuthLevel = AuthLevel,
            };
            UrunEkle.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

    }
}
