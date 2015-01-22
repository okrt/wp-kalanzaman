using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Scheduler;


namespace KalanZaman
{
    public partial class EkleSil : PhoneApplicationPage
    {
        bool Starih = true;
        List<string> names = new List<string>();
        List<DateTime> dates = new List<DateTime>();
        IsolatedStorageSettings ISSetting = IsolatedStorageSettings.ApplicationSettings;
        private void save(string ad, DateTime tarih) {
          
            if (!ISSetting.Contains("names"))
            {

                names.Add(ad);
                dates.Add(tarih);
                ISSetting.Add("names", names);
                ISSetting.Add("dates", dates);
            }
            else
            {

                IsolatedStorageSettings.ApplicationSettings.TryGetValue<List<string>>("names", out names);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue<List<DateTime>>("dates", out dates);
                if (names.Count >= 5)
                {
                    MessageBox.Show("Üzgünüz, maksimum geri sayım sayacı sayısına ulaşıldı.");
                }
                else
                {
                    names.Add(ad);
                    dates.Add(tarih);
                    ISSetting["names"] = names;
                    ISSetting["dates"] = dates;
                }
            }
            ISSetting.Save();
            if (alarmBox.IsChecked == true)
            {
                Random random = new Random();
                Alarm alarm = new Alarm("KalanZaman Alarm".ToString() + random.Next(1000));
                alarm.Content = ad;
                alarm.BeginTime = tarih;
                alarm.ExpirationTime = tarih;
                alarm.RecurrenceType = RecurrenceInterval.None;
                ScheduledActionService.Add(alarm);
            }
            MessageBox.Show("Geri sayım sayacı başarıyla eklendi!");
            //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            NavigationService.GoBack();
        }
        private int checkInt(string a) {
            int j;
            bool result = Int32.TryParse(a, out j);
            if (true == result) {
                return j;
            }
            else{
                return -1;
            }
        }
        private void ekle_Click(object sender, RoutedEventArgs e)
        {
            int aGun = checkInt(gun.Text);
            int aAy = checkInt(ay.Text);
            int aYil = checkInt(yil.Text);
            int aSaat = checkInt(saat.Text);
            int aDakika = checkInt(dakika.Text);

            if ((aGun > -1) && (aAy > -1) && (aYil > -1) && (aSaat > -1) && (aDakika > -1) && (textBox1.Text.ToString().Length>0))
            {
                
                

                DateTime hedef;
                if (Starih == true)
                {
                    try
                    {
                        hedef = new DateTime(aYil, aAy, aGun, aSaat, aDakika, 0);
                        DateTime Bugun = DateTime.Now;
                        TimeSpan sifir = new TimeSpan(0, 0, 0, 0);
                        TimeSpan sonuc = hedef - Bugun;
                        if (sonuc > sifir)
                        {
                            save(textBox1.Text, hedef);
                        }
                        else {
                            MessageBox.Show("Geçmişe yönelik geri sayamıyoruz.");
                        }
                    }
                    catch (Exception) {
                        MessageBox.Show("Girdiğiniz tarih gerçekte yok.");

                    }
                    }
                else
                {
                    hedef = DateTime.Now + new TimeSpan(aGun * 24 + aAy * 24 * 30 + aYil * 24 * 365, aDakika, 0);
                    save(textBox1.Text, hedef);
                }

                
            }
            else {
                MessageBox.Show("Geçersiz veri girişi yaptınız.");
            }
        }

        private void tarih_Checked(object sender, RoutedEventArgs e)
        {
            Starih = true;
            try
            {
                gun.Text = "01";
                ay.Text = "01";
                yil.Text = "2015";
                saat.Text = "00";
                dakika.Text = "00";
                aciklama.Text = "Lütfen geri sayım yapılacak tarihi giriniz.";
            }
            catch (Exception)
            { 
            
            }
            
        }

        private void sure_Checked(object sender, RoutedEventArgs e)
        {
            Starih = false;
            gun.Text = "0";
            ay.Text = "0";
            yil.Text = "0";
            saat.Text = "0";
            dakika.Text = "10";
            aciklama.Text = "Lütfen geri sayım yapılacak süreyi giriniz.";
        }

        public EkleSil()
        {
            InitializeComponent();
            silBtn.IsEnabled = false;
            if (ISSetting.Contains("names")) {
             IsolatedStorageSettings.ApplicationSettings.TryGetValue<List<string>>("names", out names);
             IsolatedStorageSettings.ApplicationSettings.TryGetValue<List<DateTime>>("dates", out dates);
             if (names.Count > 0)
             {
                 sayacListesi.ItemsSource = names;
                 silBtn.IsEnabled = true;
             }
             if (names.Count >= 5) {
                 ekle.IsEnabled = false;
                 aciklama.Text = "En fazla geri sayım sayacı sayısına ulaşıldı.";
             }
            }

        }



        private void silBtn_Click(object sender, RoutedEventArgs e)
        {
          
                int secim = sayacListesi.SelectedIndex;
                if (secim >= 0)
                {
                    names.RemoveAt(secim);
                    dates.RemoveAt(secim);
                    ISSetting["names"] = names;
                    ISSetting["dates"] = dates;
                    ISSetting.Save();

                    MessageBox.Show("Başarıyla silindi.");
                    //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    NavigationService.GoBack();
                }
                else {
                    MessageBox.Show("Öncelikle listeden bir seçim yapınız.");
                }
        }

    }
}