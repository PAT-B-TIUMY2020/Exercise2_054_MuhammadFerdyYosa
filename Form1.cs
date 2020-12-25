using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise2_054_MuhammadFerdyYosa
{
    public partial class Form1 : Form
    {
        string baseUrl = "http://localhost:1907/";
        void BuatMahasiswa() //Membuat Data Mahasiswa
        {
            Mahasiswa mhs = new Mahasiswa();
            mhs.nama = textBox2.Text;
            mhs.nim = textBox1.Text;
            mhs.prodi = textBox3.Text;
            mhs.angkatan = textBox4.Text;

            var data = JsonConvert.SerializeObject(mhs);
            var postdata = new WebClient();
            postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string response = postdata.UploadString(baseUrl + "Mahasiswa", data);
            label6.Text = response;
            AmbilData();
        }
        public Form1()
        {
            InitializeComponent();
            AmbilData();
        }

        public void AmbilData() //Untuk mengambil data pada db, kemudian menampilkannya pada dgv
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            dataGridView1.DataSource = data;
        }

        public void CariData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            string nim = textBox1.Text; //membuat variabel penampung nilai nim
            if (nim == null || nim == "")//Jika nim kosong
            {
                dataGridView1.DataSource = data; //Mereload data pada datagridview
            }
            else
            {
                var item = data.Where(x => x.nim == textBox1.Text).ToList(); //Mengambil data berdasarkan NIM, lalu mengonvert ke list
                dataGridView1.DataSource = item; //Memasukkan data yang sudah diambil sebelumnya kedalam dataGridView
            }
        }

        [DataContract]
        public class Mahasiswa
        {
            private string _nama, _nim, _prodi, _angkatan;
            [DataMember]
            public string nama
            {
                get { return _nama; }
                set { _nama = value; }
            }
            [DataMember]
            public string nim
            {
                get { return _nim; }
                set { _nim = value; }
            }
            [DataMember]
            public string prodi
            {
                get { return _prodi; }
                set { _prodi = value; }
            }
            [DataMember]
            public string angkatan
            {
                get { return _angkatan; }
                set { _angkatan = value; }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            BuatMahasiswa();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CariData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) //Mengambil data pada dgv dan memasukkannnya pada teksbox
        {
            textBox1.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBox2.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            textBox3.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            textBox4.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
        }
    }
}