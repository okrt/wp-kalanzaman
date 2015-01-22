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

namespace KalanZaman
{
    public partial class info : PhoneApplicationPage
    {
        public info()
        {
            InitializeComponent();
            textBlock1.Text="KalanZaman\nSürüm: 1.1 \n(Windows® Phone Sürümü)\n\n©2012 Oğuz Kırat\nTüm Hakları Saklıdır.\n\nWeb Adresi:\nhttp://www.oguzkirat.com\n\nWindows, Microsoft Corporation'un ABD ve diğer ülkelerde tescilli ve/veya ticari markasıdır.";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            NavigationService.GoBack();
        }
    }
}