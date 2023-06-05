using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace app
{
    public partial class Form3 : Form
    {
        readonly Database Database = new Database();
        public string WaiterName
        {
            get; set;
        }
        public string AuthLevel
        {
            get; set;
        }

        List<BsonDocument> Dishes;

        int TableCount;

        string StoreName;
        public Form3()
        {
            InitializeComponent();
        }

        public Form4 CreateTable(string MasaNumarasi)
        {

            Form4 Masa = new Form4
            {
                MasaNumarasi = MasaNumarasi,
                WaiterName = WaiterName
            };
            return Masa;

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            if (!Database.ConnectToApp())
            {
                MessageBox.Show("Veritabanına bağlanılamadı\nLütfen internet bağlantınızı kontrol ediniz.\nİnternet bağlantınızda bir problem yoksa:\nİletişim: emirerenkara@outlook.com");
            }
            else
            {
                BsonDocument StoreInformation = Database.GetStoreInformation();
                TableCount = StoreInformation["StoreTableCount"].AsInt32;
                StoreName = StoreInformation["StoreName"].AsString;
                label10.Text = StoreName;
                Dishes = Database.GetDishes();
                foreach (BsonDocument Dish in Dishes)
                {
                    listBox1.Items.Add($"{Dish["Name"]} => {Dish["Price"]}₺");
                }
                if (label3.Text != null)
                {
                    label3.Text = WaiterName;
                }
                else
                {
                    label3.Text = "HATA";
                }
                Takvim.Text = new ReadyDateAndTime().GetDate();
                switch (AuthLevel)
                {
                    case "0":
                        label5.Text = "Garson";
                        break;
                    case "1":
                        label5.Text = "Şef";
                        break;
                    case "2":
                        label5.Text = "Yönetici";
                        break;
                    default:
                        label5.Text = "HATA";
                        break;
                }
                int basex = flowLayoutPanel1.Location.X;
                int basey = flowLayoutPanel1.Location.Y;
                int width = flowLayoutPanel1.Width;
                int height = flowLayoutPanel1.Height;
                int btnsHeight = height / 2;
                int btnsWidth = width / (TableCount / 2 + 1);
                for (int i = 0; i < TableCount; i++) // first row buttons
                {
                    int WidthToAdd = 0;
                    int HeightToAdd = 0;
                    switch (TableCount)
                    {
                        case 4:
                            WidthToAdd = 200;
                            HeightToAdd = -10;
                            break;
                        case 5:
                            WidthToAdd = 200;
                            HeightToAdd = -50;
                            break;
                        case 6:
                            WidthToAdd = 300;
                            HeightToAdd = -50;
                            break;
                        case 7:
                            WidthToAdd = 95;
                            HeightToAdd = -50;
                            break;
                        case 8:
                        case 9:
                            WidthToAdd = 155;
                            HeightToAdd = -50;
                            break;
                        case 10:
                        case 11:
                            WidthToAdd = 95;
                            HeightToAdd = -50;
                            break;
                        case 12:
                            WidthToAdd = 125;
                            HeightToAdd = -50;
                            break;
                    }
                    Button b = new Button();
                    b.Left = basex;
                    b.Top = basey;
                    basey += 50;
                    b.Width = btnsWidth + WidthToAdd;
                    b.Height = btnsHeight + HeightToAdd;
                    b.Name = String.Format("Masa{0}", i + 1);
                    b.Text = String.Format("{0}", i + 1);
                    b.Click += new EventHandler(b_Click);
                    b.Font = new Font("Segoe UI Semibold", 16);
                    flowLayoutPanel1.Controls.Add(b);

                }
                timer1.Start();
            }
        }

        public void b_Click(object sender, EventArgs e)
        {
            //Button btn = (Button)sender;
            CreateTable((sender as Button).Text).ShowDialog();
            //conn.msgErr(btn.Name.ToString());
        }

        private void Form3_handleClose(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                foreach (BsonDocument Dish in Dishes)
                {
                    if (Dish["Name"] == listBox1.SelectedItem.ToString())
                    {
                        MessageBox.Show("Ürünün Ücreti: " + Dish["Price"] + "₺.", "Bilgilendirme");
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(AuthLevel) < 2)
            {
                MessageBox.Show("Bu işlem için yetkiniz bulunmuyor!", "Hata");
            }
            else
            {
                Hide();
                Form11 AdminMenu = new Form11
                {
                    WaiterName = WaiterName,
                    AuthLevel = AuthLevel
                };
                AdminMenu.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
