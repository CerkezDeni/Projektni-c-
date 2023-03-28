using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Xaml.Media.Imaging;

namespace Projektni
{
    public sealed partial class BlankPage1 : Page
    {

        List<int> randomNumbers;
        public BlankPage1()
        {
            this.InitializeComponent();
            CreateBackgroundThread();
            SetImageForAllButtons();
            randomNumbers = GenerateRandomNumbers();
            Spawnajmine();
        }

        public List<int> GenerateRandomNumbers()
        {
            Random random = new Random();
            List<int> numbers = new List<int>();

            for (int i = 0; i < 40; i++)
            {
                numbers.Add(random.Next(1, 257));
            }

            return numbers;
        }
        private void SetImageForAllButtons()
        {
            string imagePath = "ms-appx:///Assets/tile.png";
            for (int i = 1; i <= 256; i++)
            {
                string buttonName = "Gumb" + i;
                Button button = FindName(buttonName) as Button;
                if (button != null)
                {
                    button.Content = null;
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                    button.Background = imageBrush;
                }
            }
        }

        private void Spawnajmine()
        {
            foreach (int i in randomNumbers)
            {
                string buttonName = "Gumb" + i;
                Button button = FindName(buttonName) as Button;
                if (button != null)
                {
                    button.Tag = "bombara";
                }
            }
            for (int i = 1; i <= 256; i++)
            {
                string buttonName2 = "Gumb" + i;
                Button gumb = FindName(buttonName2) as Button;
                if (gumb != null)
                {
                    if(gumb.Tag=="bombara")
                    {
                        gumb.Content = "bomba";
                    }
                }
            }

        }

        private void timer_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void CreateBackgroundThread()
        {
            int sec = 0;
            int min = 0;
            string csec = "err";
            string dmin = "err";
            bool e = false;
            bool destroy= false;
            await Task.Run(async () =>
            {
                
                while (destroy==false)
                {
                    if(min==60)
                    {
                        sec--;
                        destroy= true;
                    }
                    if (e == true)
                    {
                        e = false;
                        min++;
                        sec = -1;
                    }
                    sec++;
                    if (sec == 59)
                    {
                        e = true;
                    }
                    csec=sec.ToString();
                    dmin=min.ToString();
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        if(sec<10 && min>9)
                        {
                            timeraaa.Text = dmin + ":" + "0" + csec;
                        }
                        else if (sec > 9 && min < 10)
                        {
                            timeraaa.Text = "0" + dmin + ":" + csec;
                        }
                        else if (sec < 10 && min < 10)
                        {
                            timeraaa.Text = "0" + dmin + ":" + "0" + csec;
                        }
                        else if(sec > 9 && min > 9)
                        {
                            timeraaa.Text = dmin + ":" + csec;
                        }
                    });
                    await Task.Delay(1000);
                }
            });
            timeraaa.FontSize = 100;
            timeraaa.Text = "nigga💀";
        }
        private void Button_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var properties = e.GetCurrentPoint(sender as UIElement).Properties;
                if (properties.IsRightButtonPressed)
                {
                    // Right click
                
                    Button pressedButton = sender as Button;
                    pressedButton.Content = ":DDDD";

                    string imagePath = "ms-appx:///Assets/flagico.png";
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                    pressedButton.Background = imageBrush;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Left click
            Button pressedButton = sender as Button;
            pressedButton.Content = ":DC";
        }
    }
}

