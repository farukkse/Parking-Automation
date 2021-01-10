using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otopark_Otomasyonu
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AracKaydı kayit = new AracKaydı();
            kayit.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OtoparkYerleri yerler = new OtoparkYerleri();
            yerler.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AracCikis cık = new AracCikis();
            cık.ShowDialog(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Satis satis = new Satis();
            satis.ShowDialog();
        }
    }
}
