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
    public partial class OtoparkYerleri : Form
    {
        public OtoparkYerleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti =new SqlConnection(@"Data Source=DESKTOP-1VUEV0B\SQLEXPRESS;Initial Catalog=otopark;Integrated Security=True");
        private void OtoparkYerleri_Load(object sender, EventArgs e)
        {
            bosparkyerleri();
            DoluParkYerleri();

            bosparkyerleri();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == oku["parkyeri"].ToString())
                        {
                            item.Text = oku["plaka"].ToString();
                        }
                    }
                }
            }
            baglanti.Close();

        }

        private void DoluParkYerleri()
        {
            bosparkyerleri();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araçdurumu", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == oku["parkyeri"].ToString() && oku["durumu"].ToString() == "DOLU")
                        {
                            item.BackColor = Color.Red;
                        }
                    }
                }
            }
            baglanti.Close();
        }

        private void bosparkyerleri()
            {
                int say = 1;
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        item.Text = "P-" + say;
                        item.Name = "P-" + say;
                        say++;

                    }
                }
            }
        
    }
}
