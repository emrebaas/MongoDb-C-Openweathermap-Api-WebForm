using MongoDB.Bson.IO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeriCekForm.Entities;
using Newtonsoft.Json;
using HtmlAgilityPack;

namespace VeriCekForm
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
              

            
        }

        private MongoClient client;
        private IMongoCollection<HavaModel> colllection;
        private IMongoCollection<besgunModel> colllection2;
        private IMongoCollection<TahminGunlukModel> colllection3;
        void İslem()
        {

            //Şehirler Dizisi
            string[] sehirler = { "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin", "Aydın", "Balikesir", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Isparta", "Mersin", "İstanbul", "İzmir", "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir", "Kocaeli", "Konya", "Kütahya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir", "Niğde", "Ordu", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van", "Yozgat", "Zonguldak", "Aksaray", "Bayburt", "Kirikkale", "Batman", "Şırnak", "Bartın", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce" };
            string sehir;
            int plaka;
            for (int i = 0; i < sehirler.Length; i++)
            {
                sehir = sehirler[i];
                plaka = i + 1;
                 string api = "772cdb6ec917447a98c6c85e8b3ceab5";
                 string baglanti = "http://api.openweathermap.org/data/2.5/weather?q="+sehir+"&units=metric&APPID=" + api;
                 WebClient web = new WebClient();
                var json = web.DownloadString(baglanti);

                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Hava.root>(json);
                Hava.root cikis = result;

                client = new MongoClient("mongodb://127.0.0.1:27017");
                var database = client.GetDatabase("HDTest");
                colllection = database.GetCollection<HavaModel>("Data");

                HavaModel ekle = new HavaModel();
                ekle.plaka = plaka;
                ekle.sehir = sehir;
                ekle.ulke = cikis.sys.country;
                ekle.temp = cikis.main.temp;
                ekle.temp_min = cikis.main.temp_min;
                ekle.temp_max = cikis.main.temp_max;
                ekle.ruzgarhiz = cikis.wind.speed;
                ekle.nem = cikis.main.humidity;
                ekle.basınc = cikis.main.pressure;

                colllection.InsertOne(ekle);
                result = null;
                json = null;
                ekle=null;
            }

            MessageBox.Show("İşlem tamam");

        }
        public void getForcast()
        {
            //Şehirler Dizisi
            string[] sehirler = { "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin", "Aydın", "Balikesir", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Isparta", "Mersin", "İstanbul", "İzmir", "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir", "Kocaeli", "Konya", "Kütahya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir", "Niğde", "Ordu", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van", "Yozgat", "Zonguldak", "Aksaray", "Bayburt", "Kirikkale", "Batman", "Şırnak", "Bartın", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce" };

            int day = 5;
            string sehir;
            int plaka;
            for (int i = 0; i < sehirler.Length; i++)
            {
                sehir = sehirler[i];
                plaka = i + 1;


                string api = "772cdb6ec917447a98c6c85e8b3ceab5";
                 string baglanti="http://api.openweathermap.org/data/2.5/forecast/daily?q="+sehir+"&units=metric&cnt=6&APPID=542ffd081e67f4512b705f89d2a611b2";



                WebClient web = new WebClient();
                var json = web.DownloadString(baglanti);

               
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<weatherForcast>(json);
                weatherForcast havaforcast = result;

                client = new MongoClient("mongodb://127.0.0.1:27017");
                var database = client.GetDatabase("HDTest");
                colllection2 = database.GetCollection<besgunModel>("SevenDayData");

                besgunModel ekle = new besgunModel();

                ekle.plaka = plaka;
                ekle.sehir = sehir;
                ekle.g1d=havaforcast.list[1].weather[0].main;
                ekle.g1des=havaforcast.list[1].weather[0].description;
                ekle.g1temp = havaforcast.list[1].temp.day;
                ekle.g1r = havaforcast.list[1].speed;

                ekle.g2d = havaforcast.list[2].weather[0].main;
                ekle.g2des = havaforcast.list[2].weather[0].description;
                ekle.g2temp = havaforcast.list[2].temp.day;
                ekle.g2r = havaforcast.list[2].speed;


                ekle.g3d = havaforcast.list[3].weather[0].main;
                ekle.g3des = havaforcast.list[3].weather[0].description;
                ekle.g3temp = havaforcast.list[3].temp.day;
                ekle.g3r = havaforcast.list[3].speed;

                ekle.g4d = havaforcast.list[4].weather[0].main;
                ekle.g4des = havaforcast.list[4].weather[0].description;
                ekle.g4temp = havaforcast.list[4].temp.day;
                ekle.g4r = havaforcast.list[4].speed;

                ekle.g5d = havaforcast.list[5].weather[0].main;
                ekle.g5des = havaforcast.list[5].weather[0].description;
                ekle.g5temp = havaforcast.list[5].temp.day;
                ekle.g5r = havaforcast.list[5].speed;




                colllection2.InsertOne(ekle);
                
            }

            MessageBox.Show("Mevcut İllerin Hava durumu Veritabanına Eklendi");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            client = new MongoClient("mongodb://127.0.0.1:27017");
            var database = client.GetDatabase("HDTest");
            colllection3 = database.GetCollection<TahminGunlukModel>("Forcast");

            TahminGunlukModel ekle = new TahminGunlukModel();

            for (int i = 0; i < listBox5.Items.Count; i++)
            {
                ekle.GenelDurum = listBox5.Items[i].ToString();
                colllection3.InsertOne(ekle);
            }
            
            MessageBox.Show("Günlük Tahmin Veritabanına Eklendi");

        }
        public String html, html1, html2, html3;
        public Uri uRl, uRl1, uRl2, uRl3;
        private void button3_Click(object sender, EventArgs e)
        {
             İslem();
             getForcast();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 4; i < 7; i++)
            {
                verial("https://www.mgm.gov.tr/tahmin/gunluk-tahmin.aspx", "/html/body/div[3]/div/p[" + i + "]", listBox1);
            }


            uyarılar("https://www.mgm.gov.tr/tahmin/gunluk-tahmin.aspx", "/html/body/div[3]/div/p[7]", listBox2);

            int s = 8, m = 1, l = 9;
            for (int i = 1; i < 9; i++)
            {
                bolgehava("https://www.mgm.gov.tr/tahmin/gunluk-tahmin.aspx", "/html/body/div[3]/div/h3[" + i + "]", listBox3);
                bolgehava("https://www.mgm.gov.tr/tahmin/gunluk-tahmin.aspx", "/html/body/div[3]/div/p[" + s + "]", listBox3);
                s += 5;

                for (int k = 1; k < 5; k++)
                {
                    bolgehava("https://www.mgm.gov.tr/tahmin/gunluk-tahmin.aspx", "/html/body/div[3]/div/h4[" + m + "]", listBox3);
                    m += 1;
                    bolgehava("https://www.mgm.gov.tr/tahmin/gunluk-tahmin.aspx", "/html/body/div[3]/div/p[" + l + "]", listBox3);
                    l += 1;
                    if (k == 4)
                    {
                        listBox3.Items.Add("");
                    }
                }
                l += 1;
            }


            for (int i = 0; i < 5; i++)
            {
                denizhava("https://www.mgm.gov.tr/tahmin/gunluk-tahmin.aspx", "/html/body/div[3]/div/h3[" + (i + 9) + "]", listBox4);
                denizhava("https://www.mgm.gov.tr/tahmin/gunluk-tahmin.aspx", "/html/body/div[3]/div/p[" + (i + 49) + "]", listBox4);
            }

            string A, B, C, D,bosluk="";
            listBox5.Items.Add("GÜNLÜK DURUM TAHMİN");
            listBox5.Items.Add(bosluk);
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                A = listBox1.Items[i].ToString();
                listBox5.Items.Add(A);
            }
            listBox5.Items.Add(bosluk);
            listBox5.Items.Add(bosluk);
            listBox5.Items.Add("UYARILAR");
            listBox5.Items.Add(bosluk);

            for (int j = 0; j < listBox2.Items.Count; j++)
                {
                    B = listBox2.Items[j].ToString();
                    listBox5.Items.Add(B);
                  }
            listBox5.Items.Add(bosluk);
            listBox5.Items.Add(bosluk);
            listBox5.Items.Add("BÖLGESEL HAVA DURUMU");
            listBox5.Items.Add(bosluk);

            for (int j = 0; j < listBox3.Items.Count; j++)
            {
                C= listBox3.Items[j].ToString();
                listBox5.Items.Add(C);
            }
            listBox5.Items.Add(bosluk);
            listBox5.Items.Add(bosluk);
            listBox5.Items.Add("DENİZLERDE HAVA DURUMU");
            listBox5.Items.Add(bosluk);
            for (int j = 0; j < listBox4.Items.Count; j++)
            {
                D = listBox4.Items[j].ToString();
                listBox5.Items.Add(D);
            }

        }
        public void verial(string url, string path, ListBox list)
        {
            uRl = new Uri(url);
            WebClient baglan = new WebClient();
            baglan.Encoding = System.Text.Encoding.UTF8;
            html = baglan.DownloadString(uRl);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            listBox1.Items.Add(doc.DocumentNode.SelectSingleNode(path).InnerText);
        }
        public void uyarılar(string url, string path, ListBox list)
        {
            uRl = new Uri(url);
            WebClient baglan = new WebClient();
            baglan.Encoding = System.Text.Encoding.UTF8;
            html = baglan.DownloadString(uRl);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            listBox2.Items.Add(doc.DocumentNode.SelectSingleNode(path).InnerText);
        }
        public void bolgehava(string url, string path, ListBox list)
        {
            uRl = new Uri(url);
            WebClient baglan = new WebClient();
            baglan.Encoding = System.Text.Encoding.UTF8;
            html = baglan.DownloadString(uRl);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            listBox3.Items.Add(doc.DocumentNode.SelectSingleNode(path).InnerText);
        }
        public void denizhava(string url, string path, ListBox list)
        {
            uRl = new Uri(url);
            WebClient baglan = new WebClient();
            baglan.Encoding = System.Text.Encoding.UTF8;
            html = baglan.DownloadString(uRl);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            listBox4.Items.Add(doc.DocumentNode.SelectSingleNode(path).InnerText);
        }


    }
}
