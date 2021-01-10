using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otopark_Otomasyonu
{
    public partial class AracCikis : Form
    {
        public AracCikis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-1VUEV0B\SQLEXPRESS;Initial Catalog=otopark;Integrated Security=True");
        private void AracCikis_Load(object sender, EventArgs e)
        {
            DoluYerler();
            plakalar();
            timer1.Enabled = true;
            

        }

        private void plakalar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboPlaka.Items.Add(oku["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void DoluYerler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araçdurumu where durumu='DOLU'", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboParkYeri.Items.Add(oku["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void comboPlaka_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark where plaka='"+comboPlaka.SelectedItem+"' ", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                txtParkYeri.Text = oku["parkyeri"].ToString();
            }
            baglanti.Close();
        }

        private void comboParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark where parkyeri" +
                "='" + comboParkYeri.SelectedItem + "' ", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                txtParkYeri2.Text = oku["parkyeri"].ToString();
                txtTc.Text = oku["tc"].ToString();
                txtAd.Text = oku["ad"].ToString();
                txtSoyad.Text = oku["soyad"].ToString();
                txtMarka.Text = oku["marka"].ToString();
                txtSeri.Text = oku["seri"].ToString();
                txtPlaka.Text = oku["plaka"].ToString();
                lblGelis.Text = oku["tarih"].ToString();
            }
            baglanti.Close();
            DateTime geliş, çıkış;
            geliş = DateTime.Parse( lblGelis.Text);
            çıkış = DateTime.Parse(lblCıkıs.Text);
            TimeSpan fark;
            fark = çıkış - geliş;
            lblSüre.Text = fark.TotalHours.ToString("0.00");
            

           lblÜcret.Text = (double.Parse(lblSüre.Text) *(0.75)).ToString("0.00");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCıkıs.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from araç_otopark where plaka='"+txtPlaka.Text+"'",baglanti);
            komut.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu='BOŞ' where parkyeri='" + txtParkYeri2.Text + "'", baglanti);
            komut2.ExecuteNonQuery();

            SqlCommand komut3 = new SqlCommand("insert into satıs (parkyeri,plaka,gelis,cikis,süre,tutar) values(@parkyeri,@plaka,@gelis,@cikis,@süre,@tutar)", baglanti);
            komut3.Parameters.AddWithValue("@parkyeri",txtParkYeri2.Text);
            komut3.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut3.Parameters.AddWithValue("@gelis", lblGelis.Text);
            komut3.Parameters.AddWithValue("@cikis", lblCıkıs.Text);
            komut3.Parameters.AddWithValue("@süre",double.Parse(lblSüre.Text));
            komut3.Parameters.AddWithValue("@tutar", double.Parse (lblÜcret.Text));
            komut3.ExecuteNonQuery();


            baglanti.Close();
            MessageBox.Show("Araç Çıkışı Yapıldı");
            foreach(Control item in groupBox2.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                    txtParkYeri.Text = "";
                    comboParkYeri.Text = "";
                    comboPlaka.Text = "";
                }
            }
            comboPlaka.Items.Clear();
            comboParkYeri.Items.Clear();

            DoluYerler();
            plakalar();
            
        }
    }
}
