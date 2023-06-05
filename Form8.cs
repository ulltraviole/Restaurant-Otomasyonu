using MongoDB.Driver.Core.Events;
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
    public partial class Form8 : Form
    {

        Database Database = new Database();

        public string WaiterName
        {
            get; set;
        }

        public string AuthLevel
        {
            get; set;
        }

        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            Database.ConnectToApp();
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;
            string Price = textBox2.Text;
            if (Database.AddDish(Name, Price))
            {
                MessageBox.Show("Ürün başarıyla eklendi.", "Başarılı");
            }
            else
            {
                MessageBox.Show("Ürün eklenemedi.", "Başarısız");
            }
        }
    }
}
