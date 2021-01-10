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
    public partial class Satis : Form
    {
        public Satis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-1VUEV0B\SQLEXPRESS;Initial Catalog=otopark;Integrated Security=True");
        DataSet daset = new DataSet();
        private void Satis_Load(object sender, EventArgs e)
        {
            satislarıGor();
            hesap();
        }

        private void hesap()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(tutar) from satıs", baglanti);
            label1.Text = "Toplam Tutar" + komut.ExecuteScalar() + " TL ";
            baglanti.Close();
        }

        private void satislarıGor()
        {
            baglanti.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select * from satıs", baglanti);
            adap.Fill(daset ,"satıs");
            dataGridView1.DataSource = daset.Tables["satıs"];
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
