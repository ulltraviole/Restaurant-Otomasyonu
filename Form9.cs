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

namespace app
{
    public partial class Form9 : Form
    {
        readonly Database Database = new Database();
        List<BsonDocument> Dishes;
        public string WaiterName
        {
            get; set;
        }

        public string AuthLevel
        {
            get; set;
        }
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            if (!Database.ConnectToApp())
            {
                MessageBox.Show("Veritabanına bağlanılamadı\nLütfen internet bağlantınızı kontrol ediniz.\nİnternet bağlantınızda bir problem yoksa:\nİletişim: emirerenkara@outlook.com");
            }
            Dishes = Database.GetDishes();
            foreach (BsonDocument Dish in Dishes)
            {
                listBox1.Items.Add(Dish["Name"]);
            }
            CenterToScreen();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                foreach (BsonDocument Dish in Dishes)
                {
                    if (Dish["Name"] == listBox1.SelectedItem.ToString())
                    {
                        DialogResult result = MessageBox.Show("Ürünü veritabanından kaldırmak istediğinize emin misiniz?", "İşlemi Onaylayınız.", MessageBoxButtons.YesNo);
                        switch (result)
                        {
                            case DialogResult.Yes:
                                if (Database.RemoveDish(Dish["Name"].AsString))
                                {
                                    MessageBox.Show("Ürün kaldırıldı.", "Başarılı");
                                }
                                else
                                {
                                    MessageBox.Show("Veritabanı Hatası", "Hata");
                                }

                                break;
                            case DialogResult.No:
                                MessageBox.Show("İşlem iptal edildi.", "İptal");
                                break;
                        }
                    }
                }
            }
        }
        private void Form9_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form3 Uygulama = new Form3
            {
                WaiterName = WaiterName,
                AuthLevel = AuthLevel
            };
            Uygulama.Show();
        }
    }
}
