using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace Projektni
{
    public sealed partial class MainPage : Page
    {
        bool GreenChecky = false;
        private MediaPlayer mediaPlayer;
        public MainPage()
        {
            this.InitializeComponent();
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/krava.mp3"));
            mediaPlayer.Play();
        }
        private Border FindBorder(DependencyObject element)
        {
            int childCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);
                if (child is Border border)
                {
                    return border;
                }
                else
                {
                    var result = FindBorder(child);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //nova igra
            if(KutijaIme.Text != "" && GreenChecky == true)
            {
                var border = FindBorder(KutijaIme);

                if (border != null)
                {
                    border.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x12, 0x98, 0x07));
                }
                string ImeIgraca = KutijaIme.Text;
                this.Frame.Navigate(typeof(BlankPage1));
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["ImeIgraca"] = ImeIgraca;
                mediaPlayer.Dispose();
            }
            else if(KutijaIme.Text != "")
            {
                string ImeIgraca = KutijaIme.Text;
                this.Frame.Navigate(typeof(BlankPage1));
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["ImeIgraca"] = ImeIgraca;
                mediaPlayer.Dispose();
            }
            else
            {
                var border = FindBorder(KutijaIme);

                if (border != null)
                {
                    border.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(0x66, 0xFF, 0x00, 0x00));
                }
                GreenChecky = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //liderbord
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //izađi iz igre
            CoreApplication.Exit();
        }
    }
}
