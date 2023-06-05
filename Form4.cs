using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace app
{
    public partial class Form4 : Form
    {
        readonly Database Database = new Database();

        List<BsonDocument> Dishes = new List<BsonDocument>();

        public bool TableState = false;

        public string MasaNumarasi
        {
            get; set;
        }

        public string WaiterName
        {
            get; set;
        }

        public int sum = 0;

        public Form4()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (!Database.ConnectToApp())
            {
                MessageBox.Show("Veritabanına bağlanılamadı\nLütfen internet bağlantınızı kontrol ediniz.\nİnternet bağlantınızda bir problem yoksa:\nİletişim: emirerenkara@outlook.com");
            }
            BsonDocument Session = Database.FindSession(MasaNumarasi);
            if (Session != null)
            {
                BsonArray BoughtProducts = Session["BoughtProducts"].AsBsonArray;
                foreach (BsonDocument BoughtProduct in BoughtProducts)
                {
                    listBox2.Items.Add(BoughtProduct["Name"].ToString());
                }
                TableState = true;
                sum += Session["Price"].ToInt32();
                label2.Text = "GÜNCEL TUTAR: " + sum + "₺";
            }
            CenterToScreen();
            Dishes = Database.GetDishes();

            foreach (BsonDocument Dish in Dishes)
            {
                listBox1.Items.Add(Dish["Name"]);
            }

            Text += MasaNumarasi + " Numaralı masada işlem yapıyorsunuz.";
            label1.Text += MasaNumarasi;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (TableState)
            {
                if (listBox1.SelectedItem != null)
                {
                    DialogResult result = MessageBox.Show("Ürünü masaya eklemek istiyor musunuz?", "Soru", MessageBoxButtons.YesNo);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            foreach (BsonDocument Dish in Dishes)
                            {
                                string SelectedDish = listBox1.SelectedItem.ToString();
                                if (Dish["Name"] == SelectedDish)
                                {
                                    listBox2.Items.Add(SelectedDish);
                                    sum += Dish["Price"].ToInt32();
                                    Database.UpdateTable(MasaNumarasi, sum.ToString(), new BsonDocument { { "Name", SelectedDish }, { "Price", Dish["Price"] } });
                                    label2.Text = "GÜNCEL TUTAR: " + sum + "₺";
                                    MessageBox.Show("Ürün başarıyla masaya eklendi.", "Başarılı");
                                }
                            }
                            break;
                        default:
                            MessageBox.Show("İşlem isteğiniz üzerine iptal edildi.", "Bilgi");
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Masa ürün ekleyebilmek için önce masayı açmalısınız.", "Bilgi");
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (TableState)
            {
                if (listBox2.SelectedItem != null)
                {
                    DialogResult result = MessageBox.Show("Ürünü masadan çıkarmak istiyor musunuz?", "Soru", MessageBoxButtons.YesNo);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            string SelectedDish = listBox2.SelectedItem.ToString();
                            BsonDocument BoughtProduct = Database.DeleteProduct(MasaNumarasi, SelectedDish);
                            if (BoughtProduct != null)
                            {
                                sum -= Int32.Parse(BoughtProduct["Price"].AsString);
                                label2.Text = "GÜNCEL TUTAR: " + sum + "₺";
                                listBox2.Items.Remove(SelectedDish);
                                MessageBox.Show("Ürün masadan başarıyla kaldırıldı.", "Başarılı");
                            }
                            break;
                        default:
                            MessageBox.Show("İşlem isteğiniz üzerine iptal edildi.", "Bilgi");
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Masadan ürün çıkarmak için önce masayı açmalısınız.", "Bilgi");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BsonDocument Session = Database.FindSession(MasaNumarasi);
            if (Session == null && TableState == false)
            {
                label2.Text = "GÜNCEL TUTAR: 0₺";

                string date = new ReadyDateAndTime().GetDate();
                string time = new ReadyDateAndTime().GetTime();

                if (Database.StartSession(MasaNumarasi, date, time, WaiterName))
                {
                    TableState = true;
                    MessageBox.Show("Masa başarıyla açıldı..", "Başarılı");
                }
            }
            else
            {
                MessageBox.Show("Masa halihazırda açık görünüyor, lütfen önce adisyon alınız.", "Bilgi");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string time = new ReadyDateAndTime().GetTime();

            if (Database.EndSession(MasaNumarasi, time))
            {
                MessageBox.Show("Masadan alınacak ücret: " + sum.ToString() + "₺.", "BİLGİ");
                TableState = false;
                sum = 0;

            }
            else
            {
                MessageBox.Show("Masa açık değil. Adisyon alınamaz.", "Bilgi");
            }
        }
    }
}
