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
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;

namespace KalanZaman
{


    public partial class MainPage : PhoneApplicationPage
    {

        DispatcherTimer timer = null;
        public void recalc()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("names"))
            {

                List<string> names = new List<string>();
                List<DateTime> dates = new List<DateTime>();
                IsolatedStorageSettings.ApplicationSettings.TryGetValue<List<string>>("names", out names);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue<List<DateTime>>("dates", out dates);
                int sayi = names.Count;
                if (sayi > 0)
                {
                    icerik.Text = "";
                    for (int x = 0; x < sayi; x++)
                    {

                        DateTime Bugun = DateTime.Now;
                        string olayadi = names[x];
                        TimeSpan sonuc = dates[x] - Bugun;
                        TimeSpan sifir = new TimeSpan(0, 0, 0, 0);
                        icerik.Text = icerik.Text.ToString() + olayadi + ":\n";
                        if (sonuc > sifir)
                        {
                            icerik.Text = icerik.Text.ToString() + sonuc.Days.ToString() + " gün " + sonuc.Hours.ToString() + " saat " + sonuc.Minutes.ToString() + " dakika " + sonuc.Seconds.ToString() + " saniye\n\n";
                        }
                        else
                        {
                            icerik.Text = icerik.Text.ToString() + "Süre doldu...\n\n";
                        }
                    }
                }
                else
                {
                    icerik.Text = "Sayaç bulunamadı. Lütfen bir sayaç ekleyin. ";
                }
            }
            else
            {
                icerik.Text = "Sayaç bulunamadı. Lütfen bir sayaç ekleyin. ";
            }
        }
        void counter(object sender, EventArgs e)
        {
            recalc();
        }
        public MainPage()
        {
            
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);


        }
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
       //     try
         //   {
           //     NavigationService.RemoveBackEntry();
            //}
            //catch(Exception){
            
            //}
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1); // one second
            timer.Tick += new EventHandler(counter);
            timer.Start();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/EkleSil.xaml", UriKind.Relative));
            
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/info.xaml", UriKind.Relative));
            
        }
    }
}