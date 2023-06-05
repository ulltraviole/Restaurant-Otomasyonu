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
    public partial class Form10 : Form
    {
        readonly Database Database = new Database();
        List<BsonDocument> Waiters;
        public string WaiterName
        {
            get; set;
        }

        public string AuthLevel
        {
            get; set;
        }
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            if (!Database.ConnectToApp())
            {
                MessageBox.Show("Veritabanına bağlanılamadı\nLütfen internet bağlantınızı kontrol ediniz.\nİnternet bağlantınızda bir problem yoksa:\nİletişim: emirerenkara@outlook.com");
            }
            if (Int32.Parse(AuthLevel) < 2)
            {
                MessageBox.Show("Bu işlem için yetkiniz bulunmuyor!", "Hata");
                Close();
            }
            else
            {
                Waiters = Database.GetStaffs();
                foreach (BsonDocument Waiter in Waiters)
                {
                    listBox1.Items.Add(Waiter["Fullname"]);
                }
            }

            CenterToScreen();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                foreach (BsonDocument Waiter in Waiters)
                {
                    if (Waiter["Fullname"] == listBox1.SelectedItem.ToString())
                    {
                        DialogResult result = MessageBox.Show("Garsonu veritabanından kaldırmak istediğinize emin misiniz?", "İşlemi Onaylayınız.", MessageBoxButtons.YesNo);
                        switch (result)
                        {
                            case DialogResult.Yes:
                                if (WaiterName != listBox1.SelectedItem.ToString())
                                {
                                    if (Database.RemoveStaff(listBox1.SelectedItem.ToString()))
                                    {
                                        MessageBox.Show("Garson kaldırıldı.", "Başarılı");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Veritabanı Hatası", "Hata");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Aktif kullanıcı hesabı silinemez. Lütfen başka bir kullanıcı hesabı seçiniz.", "Bilgi");
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
    }
}
