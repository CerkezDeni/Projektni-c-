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
        bool endgame = false;
        List<int> randomNumbers;
        int numbah;
        public BlankPage1()
        {
            this.InitializeComponent();
            CreateBackgroundThread();
            SetImageForAllButtons();
            randomNumbers = GenerateRandomNumbers();
            Spawnajmine();
            SweeperStatus();
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
                    else
                    {
                        gumb.Content = "C";
                        gumb.Tag = "clear";
                    }
                }
            }

        }

        private void timer_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void SweeperStatus()
        {
            string stringy;
            int giganigga = 0;
            for (int i = 1; i <= 256; i++)
            {
                string buttonName3 = "Gumb" + i;
                Button gumb66 = FindName(buttonName3) as Button;
                if (gumb66.Tag == "flag" || gumb66.Tag == "flagandbombara")
                {
                    giganigga++;
                }
            }
            numbah = 40 - giganigga;
            flagnumberdisplay.Text = numbah.ToString();
            switch (giganigga)
            {
                case 0:
                    string imagePath = "ms-appx:///Assets/smiley.png";
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                    Status.Background = imageBrush;
                    break;
                case 1:
                    string imagePath1 = "ms-appx:///Assets/smiley1.png";
                    ImageBrush imageBrush1 = new ImageBrush();
                    imageBrush1.ImageSource = new BitmapImage(new Uri(imagePath1));
                    Status.Background = imageBrush1;
                    break;
                case 2:
                    string imagePath2 = "ms-appx:///Assets/smiley2.png";
                    ImageBrush imageBrush2 = new ImageBrush();
                    imageBrush2.ImageSource = new BitmapImage(new Uri(imagePath2));
                    Status.Background = imageBrush2;
                    break;
                case 3:
                    string imagePath3 = "ms-appx:///Assets/smiley3.png";
                    ImageBrush imageBrush3 = new ImageBrush();
                    imageBrush3.ImageSource = new BitmapImage(new Uri(imagePath3));
                    Status.Background = imageBrush3;
                    break;
                case 4:
                    string imagePath4 = "ms-appx:///Assets/smiley4.png";
                    ImageBrush imageBrush4 = new ImageBrush();
                    imageBrush4.ImageSource = new BitmapImage(new Uri(imagePath4));
                    Status.Background = imageBrush4;
                    break;
                case 5:
                    string imagePath5 = "ms-appx:///Assets/smiley5.png";
                    ImageBrush imageBrush5 = new ImageBrush();
                    imageBrush5.ImageSource = new BitmapImage(new Uri(imagePath5));
                    Status.Background = imageBrush5;
                    break;
                case 6:
                    string imagePath6 = "ms-appx:///Assets/smiley6.png";
                    ImageBrush imageBrush6 = new ImageBrush();
                    imageBrush6.ImageSource = new BitmapImage(new Uri(imagePath6));
                    Status.Background = imageBrush6;
                    break;
                case 7:
                    string imagePath7 = "ms-appx:///Assets/smiley7.png";
                    ImageBrush imageBrush7 = new ImageBrush();
                    imageBrush7.ImageSource = new BitmapImage(new Uri(imagePath7));
                    Status.Background = imageBrush7;
                    break;
                case 8:
                    string imagePath8 = "ms-appx:///Assets/smiley8.png";
                    ImageBrush imageBrush8 = new ImageBrush();
                    imageBrush8.ImageSource = new BitmapImage(new Uri(imagePath8));
                    Status.Background = imageBrush8;
                    break;
                case 9:
                    string imagePath9 = "ms-appx:///Assets/smiley9.png";
                    ImageBrush imageBrush9 = new ImageBrush();
                    imageBrush9.ImageSource = new BitmapImage(new Uri(imagePath9));
                    Status.Background = imageBrush9;
                    break;
               default:
                    string imagePath10 = "ms-appx:///Assets/smiley9.png";
                    ImageBrush imageBrush10 = new ImageBrush();
                    imageBrush10.ImageSource = new BitmapImage(new Uri(imagePath10));
                    Status.Background = imageBrush10;
                    break;
            }


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
                    if (endgame == false)
                    {
                        if (pressedButton.Tag != "clicked")
                        {
                            
                            if (pressedButton.Tag != "flag" && pressedButton.Tag != "flagandbombara")
                            {
                                if(numbah > 0)
                                {
                                    if (pressedButton.Tag == "bombara")
                                    {
                                        pressedButton.Tag = "flagandbombara";

                                        string imagePath = "ms-appx:///Assets/flagico.png";
                                        ImageBrush imageBrush = new ImageBrush();
                                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                        pressedButton.Background = imageBrush;
                                        SweeperStatus();
                                    }
                                    else if (pressedButton.Tag == "clear")
                                    {
                                        string imagePath = "ms-appx:///Assets/flagico.png";
                                        ImageBrush imageBrush = new ImageBrush();
                                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                        pressedButton.Background = imageBrush;

                                        pressedButton.Tag = "flag";
                                        SweeperStatus();
                                    }
                                }
                            }
                            else
                            {
                                if (pressedButton.Tag != "flagandbombara")
                                {
                                    pressedButton.Tag = "bombara";

                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;
                                    SweeperStatus();
                                }
                                else
                                {
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;

                                    pressedButton.Tag = "clear";
                                    SweeperStatus();
                                }
                            }
                        }
                    }
                   
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Left click
            Button pressedButton = sender as Button;
            if(endgame == false)
            {
                if (pressedButton.Tag != "clicked" && pressedButton.Tag != "flag" && pressedButton.Tag != "flagandbombara")
                {
                    if (pressedButton.Tag == "bombara")
                    {
                        for (int i = 1; i <= 256; i++)
                        {
                            string buttonName3 = "Gumb" + i;
                            Button gumb69 = FindName(buttonName3) as Button;
                            if (gumb69 != null)
                            {
                                if (gumb69.Tag == "bombara" || gumb69.Tag == "flagandbombara")
                                {
                                    string imagePath = "ms-appx:///Assets/tilebomb.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    gumb69.Background = imageBrush;
                                }
                            }
                        }
                        string imagePathnig = "ms-appx:///Assets/tilebombexploded.png";
                        ImageBrush imageBrushnig = new ImageBrush();
                        imageBrushnig.ImageSource = new BitmapImage(new Uri(imagePathnig));
                        pressedButton.Background = imageBrushnig;
                        endgame = true;
                        //dovrsi kasnije
                    }
                    else
                    {
                        pressedButton.Tag = "clicked";
                        string imagePath = "ms-appx:///Assets/tileclicked.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        pressedButton.Background = imageBrush;
                    }
                }
            }
        }
    }
}

