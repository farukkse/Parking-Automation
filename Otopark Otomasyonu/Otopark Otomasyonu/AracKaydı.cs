using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Otopark_Otomasyonu
{
    public partial class AracKaydı : Form
    {
        public AracKaydı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-1VUEV0B\SQLEXPRESS;Initial Catalog=otopark;Integrated Security=True");

       

        private void gruparac_Enter(object sender, EventArgs e)
        {

        }

        private void bosarac()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araçdurumu where durumu='BOŞ'", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboParkyeri.Items.Add(oku["parkyeri"].ToString());
            }
            baglanti.Close();
        }
        private void AracKaydı_Load(object sender, EventArgs e)
        {
            bosarac();
            Marka();
           
        }

        private void Marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markabilgileri ", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboMarka.Items.Add(oku["marka"].ToString());
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            
           SqlCommand komut = new SqlCommand("insert into araç_otopark(tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih) values(@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", baglanti);
            komut.Parameters.AddWithValue("@tc",txtTc.Text);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut.Parameters.AddWithValue("@marka", comboMarka.Text);
            komut.Parameters.AddWithValue("@seri", comboSeri.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@parkyeri", comboParkyeri.Text);
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            komut.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu='DOLU' where parkyeri='"+comboParkyeri.SelectedItem+"'" ,baglanti);
            komut2.ExecuteNonQuery();
           
            baglanti.Close();
            MessageBox.Show("Araç Kaydı Oluşturuldu.","Kayıt");
            comboParkyeri.Items.Clear();
            bosarac();
            comboMarka.Items.Clear();
            Marka();
            comboSeri.Items.Clear();
            foreach (Control item in grupkisi.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = " ";
                }
            }
            foreach (Control item in gruparac.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = " ";
                }
            }
            foreach (Control item in gruparac.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = " ";
                }
            }

        }

        private void btnMarka_Click(object sender, EventArgs e)
        {
            Markas marka = new Markas();
            marka.ShowDialog();

        }

        private void btnSeri_Click(object sender, EventArgs e)
        {
            seri seri = new seri();
            seri.ShowDialog(); 
        }

        private void comboMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSeri.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from seribilgileri where marka='"+comboMarka.SelectedItem+"' ", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboSeri.Items.Add(oku["seri"].ToString());
            }
            baglanti.Close();

        }
    }
}
