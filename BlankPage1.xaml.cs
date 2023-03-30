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
using Windows.Media.Playback;
using DataAccessLibrary;
using System.ServiceModel.Channels;

namespace Projektni
{
    public sealed partial class BlankPage1 : Page
    {
        string ImeIgraca;
        bool destroy = false;
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
            GenerateNumbersGrid();
            ImeIgraca = Windows.Storage.ApplicationData.Current.LocalSettings.Values["ImeIgraca"] as string;
        }
        private async void AddButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            AddMusic _addMusic = new AddMusic();
            await _addMusic.AddMedia(playList, mediaPlayer);
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            playList.Items.Remove(playList.SelectedItem);
        }

        private void MediaTransportControls_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Space)

                if (mediaPlayer.MediaPlayer.PlaybackSession.PlaybackState == MediaPlaybackState.Playing)
                {
                    mediaPlayer.MediaPlayer.Pause();
                }
                else if (mediaPlayer.MediaPlayer.PlaybackSession.PlaybackState == MediaPlaybackState.Paused)
                {
                    mediaPlayer.MediaPlayer.Play();
                }
        }

        private void playList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GenerateNumbersGrid()
        {

            for (int i = 1; i <= 256; i++)
            {

                int zbroj = 0;
                string GumbIme = "Gumb" + i;
                Button Gumbara = FindName(GumbIme) as Button;
                Button Gumb1 = (Button)FindName("Gumb" + (i - 17).ToString());
                Button Gumb2 = (Button)FindName("Gumb" + (i - 16).ToString());
                Button Gumb3 = (Button)FindName("Gumb" + (i - 15).ToString());
                Button Gumb4 = (Button)FindName("Gumb" + (i - 1).ToString());
                Button Gumb5 = (Button)FindName("Gumb" + (i + 1).ToString());
                Button Gumb6 = (Button)FindName("Gumb" + (i + 15).ToString());
                Button Gumb7 = (Button)FindName("Gumb" + (i + 16).ToString());
                Button Gumb8 = (Button)FindName("Gumb" + (i + 17).ToString());
                if(Gumbara.Tag != "bombara")
                {
                    if (Gumb1 != null && Gumb1.Tag == "bombara")
                    {
                        zbroj++;
                    }
                    if (Gumb2 != null && Gumb2.Tag == "bombara")
                    {
                        zbroj++;
                    }
                    if (Gumb3 != null && Gumb3.Tag == "bombara")
                    {
                        zbroj++;
                    }
                    if (Gumb4 != null && Gumb4.Tag == "bombara")
                    {
                        zbroj++;
                    }
                    if (Gumb5 != null && Gumb5.Tag == "bombara")
                    {
                        zbroj++;
                    }
                    if (Gumb6 != null && Gumb6.Tag == "bombara")
                    {
                        zbroj++;
                    }
                    if (Gumb7 != null && Gumb7.Tag == "bombara")
                    {
                        zbroj++;
                    }
                    if (Gumb8 != null && Gumb8.Tag == "bombara")
                    {
                        zbroj++;
                    }


                    switch (zbroj)
                    {
                        case 0:
                            Gumbara.Tag = "clear";
                            break;
                        case 1:
                            Gumbara.Tag = "1";
                            break;
                        case 2:
                            Gumbara.Tag = "2";
                            break;
                        case 3:
                            Gumbara.Tag = "3";
                            break;
                        case 4:
                            Gumbara.Tag = "4";
                            break;
                        case 5:
                            Gumbara.Tag = "5";
                            break;
                        case 6:
                            Gumbara.Tag = "6";
                            break;
                        case 7:
                            Gumbara.Tag = "7";
                            break;
                        case 8:
                            Gumbara.Tag = "8";
                            break;
                    }
                }
            }
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
                    if(gumb.Tag!="bombara")
                    {
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
                            
                            if (pressedButton.Tag != "flag" && pressedButton.Tag != "flagandbombara" && pressedButton.Tag != "flag1" && pressedButton.Tag != "flag2" && pressedButton.Tag != "flag3" && pressedButton.Tag != "flag4" && pressedButton.Tag != "flag5" && pressedButton.Tag != "flag6" && pressedButton.Tag != "flag7" && pressedButton.Tag != "flag8")
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
                                    else if(pressedButton.Tag == "1")
                                    {
                                        string imagePath = "ms-appx:///Assets/flagico.png";
                                        ImageBrush imageBrush = new ImageBrush();
                                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                        pressedButton.Background = imageBrush;

                                        pressedButton.Tag = "flag1";
                                        SweeperStatus();
                                    }
                                    else if (pressedButton.Tag == "2")
                                    {
                                        string imagePath = "ms-appx:///Assets/flagico.png";
                                        ImageBrush imageBrush = new ImageBrush();
                                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                        pressedButton.Background = imageBrush;

                                        pressedButton.Tag = "flag2";
                                        SweeperStatus();
                                    }
                                    else if (pressedButton.Tag == "3")
                                    {
                                        string imagePath = "ms-appx:///Assets/flagico.png";
                                        ImageBrush imageBrush = new ImageBrush();
                                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                        pressedButton.Background = imageBrush;

                                        pressedButton.Tag = "flag3";
                                        SweeperStatus();
                                    }
                                    else if (pressedButton.Tag == "4")
                                    {
                                        string imagePath = "ms-appx:///Assets/flagico.png";
                                        ImageBrush imageBrush = new ImageBrush();
                                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                        pressedButton.Background = imageBrush;

                                        pressedButton.Tag = "flag4";
                                        SweeperStatus();
                                    }
                                    else if (pressedButton.Tag == "5")
                                    {
                                        string imagePath = "ms-appx:///Assets/flagico.png";
                                        ImageBrush imageBrush = new ImageBrush();
                                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                        pressedButton.Background = imageBrush;

                                        pressedButton.Tag = "flag5";
                                        SweeperStatus();
                                    }
                                    else if (pressedButton.Tag == "6")
                                    {
                                        string imagePath = "ms-appx:///Assets/flagico.png";
                                        ImageBrush imageBrush = new ImageBrush();
                                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                        pressedButton.Background = imageBrush;

                                        pressedButton.Tag = "flag6";
                                        SweeperStatus();
                                    }
                                    else if (pressedButton.Tag == "7")
                                    {
                                        string imagePath = "ms-appx:///Assets/flagico.png";
                                        ImageBrush imageBrush = new ImageBrush();
                                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                        pressedButton.Background = imageBrush;

                                        pressedButton.Tag = "flag7";
                                        SweeperStatus();
                                    }
                                    else if (pressedButton.Tag == "8")
                                    {
                                        string imagePath = "ms-appx:///Assets/flagico.png";
                                        ImageBrush imageBrush = new ImageBrush();
                                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                        pressedButton.Background = imageBrush;

                                        pressedButton.Tag = "flag8";
                                        SweeperStatus();
                                    }
                                }
                            }
                            else
                            {
                                if (pressedButton.Tag == "flagandbombara")
                                {
                                    pressedButton.Tag = "bombara";
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;
                                    SweeperStatus();
                                }
                                else if(pressedButton.Tag == "flag")
                                {
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;

                                    pressedButton.Tag = "clear";
                                    SweeperStatus();
                                }
                                else if(pressedButton.Tag == "flag1")
                                {
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;

                                    pressedButton.Tag = "1";
                                    SweeperStatus();
                                }
                                else if (pressedButton.Tag == "flag2")
                                {
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;

                                    pressedButton.Tag = "2";
                                    SweeperStatus();
                                }
                                else if (pressedButton.Tag == "flag3")
                                {
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;

                                    pressedButton.Tag = "3";
                                    SweeperStatus();
                                }
                                else if (pressedButton.Tag == "flag4")
                                {
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;

                                    pressedButton.Tag = "4";
                                    SweeperStatus();
                                }
                                else if (pressedButton.Tag == "flag5")
                                {
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;

                                    pressedButton.Tag = "5";
                                    SweeperStatus();
                                }
                                else if (pressedButton.Tag == "flag6")
                                {
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;

                                    pressedButton.Tag = "6";
                                    SweeperStatus();
                                }
                                else if (pressedButton.Tag == "flag7")
                                {
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;

                                    pressedButton.Tag = "7";
                                    SweeperStatus();
                                }
                                else if (pressedButton.Tag == "flag8")
                                {
                                    string imagePath = "ms-appx:///Assets/tile.png";
                                    ImageBrush imageBrush = new ImageBrush();
                                    imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                                    pressedButton.Background = imageBrush;

                                    pressedButton.Tag = "8";
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
                if (pressedButton.Tag != "clicked" && pressedButton.Tag != "flag" && pressedButton.Tag != "flagandbombara" && pressedButton.Tag != "flag1" && pressedButton.Tag != "flag2" && pressedButton.Tag != "flag3" && pressedButton.Tag != "flag4" && pressedButton.Tag != "flag5" && pressedButton.Tag != "flag6" && pressedButton.Tag != "flag7" && pressedButton.Tag != "flag8")
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
                        destroy = true;
                        //ovdje ti je sve đeljana
                        //ImeIgraca <- ime igraca (objasnjavam ako si retardiran slucajno)
                        //timeraaa.Text <- vrijeme (objasnjavam ako si retardiran slucajno)

                        
                        DataAccess.AddToTable(ImeIgraca, timeraaa.Text);
                      
                    }
                    else
                    {
                        if(pressedButton.Tag == "clear")
                        {
                            pressedButton.Tag = "clicked";
                            string imagePath = "ms-appx:///Assets/tileclicked.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            pressedButton.Background = imageBrush;
                        }
                        else if(pressedButton.Tag == "1")
                        {
                            pressedButton.Tag = "1clicked";
                            string imagePath = "ms-appx:///Assets/1.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            pressedButton.Background = imageBrush;
                        }
                        else if (pressedButton.Tag == "2")
                        {
                            pressedButton.Tag = "2clicked";
                            string imagePath = "ms-appx:///Assets/2.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            pressedButton.Background = imageBrush;
                        }
                        else if (pressedButton.Tag == "3")
                        {
                            pressedButton.Tag = "3clicked";
                            string imagePath = "ms-appx:///Assets/3.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            pressedButton.Background = imageBrush;
                        }
                        else if (pressedButton.Tag == "4")
                        {
                            pressedButton.Tag = "4clicked";
                            string imagePath = "ms-appx:///Assets/4.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            pressedButton.Background = imageBrush;
                        }
                        else if (pressedButton.Tag == "5")
                        {
                            pressedButton.Tag = "5clicked";
                            string imagePath = "ms-appx:///Assets/5.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            pressedButton.Background = imageBrush;
                        }
                        else if (pressedButton.Tag == "6")
                        {
                            pressedButton.Tag = "6clicked";
                            string imagePath = "ms-appx:///Assets/6.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            pressedButton.Background = imageBrush;
                        }
                        else if (pressedButton.Tag == "7")
                        {
                            pressedButton.Tag = "7clicked";
                            string imagePath = "ms-appx:///Assets/7.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            pressedButton.Background = imageBrush;
                        }
                        else if (pressedButton.Tag == "8")
                        {
                            pressedButton.Tag = "8clicked";
                            string imagePath = "ms-appx:///Assets/8.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            pressedButton.Background = imageBrush;
                        }
                    }
                    AdvanceField(pressedButton);
                    SweeperStatus();
                }
            }
        }

        private void AdvanceField(Button Gumb)
        {
            int i = GetButtonNumber(Gumb.Name);
            Button Gumb1 = (Button)FindName("Gumb" + (i - 17).ToString());
            Button Gumb2 = (Button)FindName("Gumb" + (i - 16).ToString());
            Button Gumb3 = (Button)FindName("Gumb" + (i - 15).ToString());
            Button Gumb4 = (Button)FindName("Gumb" + (i - 1).ToString());
            Button Gumb5 = (Button)FindName("Gumb" + (i + 1).ToString());
            Button Gumb6 = (Button)FindName("Gumb" + (i + 15).ToString());
            Button Gumb7 = (Button)FindName("Gumb" + (i + 16).ToString());
            Button Gumb8 = (Button)FindName("Gumb" + (i + 17).ToString());
            if(Gumb.Tag != "1clicked" && Gumb.Tag != "2clicked" && Gumb.Tag != "3clicked" && Gumb.Tag != "4clicked" && Gumb.Tag != "5clicked" && Gumb.Tag != "6clicked" && Gumb.Tag != "7clicked" && Gumb.Tag != "8clicked" && Gumb.Tag != "1" && Gumb.Tag != "2" && Gumb.Tag != "3" && Gumb.Tag != "4" && Gumb.Tag != "5" && Gumb.Tag != "6" && Gumb.Tag != "7" && Gumb.Tag != "8")
            {
                if(i != 1 && i != 17 && i != 33 && i != 49 && i != 65 && i != 81 && i != 97 && i != 113 && i != 129 && i != 145 && i != 161 && i != 177 && i != 193 && i != 209 && i != 225 && i != 241)
                {
                    if (Gumb1 != null)
                    {
                        if (Gumb1.Tag == "flag" || Gumb1.Tag == "clear")
                        {
                            Gumb1.Tag = "clicked";
                            string imagePath = "ms-appx:///Assets/tileclicked.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb1.Background = imageBrush;
                            AdvanceField(Gumb1);
                        }
                        else if (Gumb1.Tag == "1" || Gumb1.Tag == "flag1")
                        {
                            Gumb1.Tag = "1clicked";
                            string imagePath = "ms-appx:///Assets/1.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb1.Background = imageBrush;
                        }
                        else if (Gumb1.Tag == "2" || Gumb1.Tag == "flag2")
                        {
                            Gumb1.Tag = "2clicked";
                            string imagePath = "ms-appx:///Assets/2.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb1.Background = imageBrush;
                        }
                        else if (Gumb1.Tag == "3" || Gumb1.Tag == "flag3")
                        {
                            Gumb1.Tag = "3clicked";
                            string imagePath = "ms-appx:///Assets/3.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb1.Background = imageBrush;
                        }
                        else if (Gumb1.Tag == "4" || Gumb1.Tag == "flag4")
                        {
                            Gumb1.Tag = "4clicked";
                            string imagePath = "ms-appx:///Assets/4.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb1.Background = imageBrush;
                        }
                        else if (Gumb1.Tag == "5" || Gumb1.Tag == "flag5")
                        {
                            Gumb1.Tag = "5clicked";
                            string imagePath = "ms-appx:///Assets/5.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb1.Background = imageBrush;
                        }
                        else if (Gumb1.Tag == "6" || Gumb1.Tag == "flag6")
                        {
                            Gumb1.Tag = "6clicked";
                            string imagePath = "ms-appx:///Assets/6.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb1.Background = imageBrush;
                        }
                        else if (Gumb1.Tag == "7" || Gumb1.Tag == "flag7")
                        {
                            Gumb1.Tag = "7clicked";
                            string imagePath = "ms-appx:///Assets/7.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb1.Background = imageBrush;
                        }
                        else if (Gumb1.Tag == "8" || Gumb1.Tag == "flag8")
                        {
                            Gumb1.Tag = "8clicked";
                            string imagePath = "ms-appx:///Assets/8.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb1.Background = imageBrush;
                        }
                    }
                }
                if (Gumb2 != null)
                {
                    if (Gumb2.Tag == "flag" || Gumb2.Tag == "clear")
                    {
                        Gumb2.Tag = "clicked";
                        string imagePath = "ms-appx:///Assets/tileclicked.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb2.Background = imageBrush;
                        AdvanceField(Gumb2);
                    }
                    else if (Gumb2.Tag == "1" || Gumb2.Tag == "flag1")
                    {
                        Gumb2.Tag = "1clicked";
                        string imagePath = "ms-appx:///Assets/1.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb2.Background = imageBrush;
                    }
                    else if (Gumb2.Tag == "2" || Gumb2.Tag == "flag2")
                    {
                        Gumb2.Tag = "2clicked";
                        string imagePath = "ms-appx:///Assets/2.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb2.Background = imageBrush;
                    }
                    else if (Gumb2.Tag == "3" || Gumb2.Tag == "flag3")
                    {
                        Gumb2.Tag = "3clicked";
                        string imagePath = "ms-appx:///Assets/3.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb2.Background = imageBrush;
                    }
                    else if (Gumb2.Tag == "4" || Gumb2.Tag == "flag4")
                    {
                        Gumb2.Tag = "4clicked";
                        string imagePath = "ms-appx:///Assets/4.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb2.Background = imageBrush;
                    }
                    else if (Gumb2.Tag == "5" || Gumb2.Tag == "flag5")
                    {
                        Gumb2.Tag = "5clicked";
                        string imagePath = "ms-appx:///Assets/5.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb2.Background = imageBrush;
                    }
                    else if (Gumb2.Tag == "6" || Gumb2.Tag == "flag6")
                    {
                        Gumb2.Tag = "6clicked";
                        string imagePath = "ms-appx:///Assets/6.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb2.Background = imageBrush;
                    }
                    else if (Gumb2.Tag == "7" || Gumb2.Tag == "flag7")
                    {
                        Gumb2.Tag = "7clicked";
                        string imagePath = "ms-appx:///Assets/7.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb2.Background = imageBrush;
                    }
                    else if (Gumb2.Tag == "8" || Gumb2.Tag == "flag8")
                    {
                        Gumb2.Tag = "8clicked";
                        string imagePath = "ms-appx:///Assets/8.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb2.Background = imageBrush;
                    }
                }
                if(i != 16 && i != 32 && i != 48 && i != 64 && i != 80 && i != 96 && i != 112 && i != 128 && i != 144 && i != 160 && i != 176 && i != 192 && i != 208 && i != 224 && i != 240 && i != 256)
                {
                    if (Gumb3 != null)
                    {
                        if (Gumb3.Tag == "flag" || Gumb3.Tag == "clear")
                        {
                            Gumb3.Tag = "clicked";
                            string imagePath = "ms-appx:///Assets/tileclicked.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb3.Background = imageBrush;
                            AdvanceField(Gumb3);
                        }
                        else if (Gumb3.Tag == "1" || Gumb3.Tag == "flag1")
                        {
                            Gumb3.Tag = "1clicked";
                            string imagePath = "ms-appx:///Assets/1.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb3.Background = imageBrush;
                        }
                        else if (Gumb3.Tag == "2" || Gumb3.Tag == "flag2")
                        {
                            Gumb3.Tag = "2clicked";
                            string imagePath = "ms-appx:///Assets/2.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb3.Background = imageBrush;
                        }
                        else if (Gumb3.Tag == "3" || Gumb3.Tag == "flag3")
                        {
                            Gumb3.Tag = "3clicked";
                            string imagePath = "ms-appx:///Assets/3.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb3.Background = imageBrush;
                        }
                        else if (Gumb3.Tag == "4" || Gumb3.Tag == "flag4")
                        {
                            Gumb3.Tag = "4clicked";
                            string imagePath = "ms-appx:///Assets/4.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb3.Background = imageBrush;
                        }
                        else if (Gumb3.Tag == "5" || Gumb3.Tag == "flag5")
                        {
                            Gumb3.Tag = "5clicked";
                            string imagePath = "ms-appx:///Assets/5.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb3.Background = imageBrush;
                        }
                        else if (Gumb3.Tag == "6" || Gumb3.Tag == "flag6")
                        {
                            Gumb3.Tag = "6clicked";
                            string imagePath = "ms-appx:///Assets/6.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb3.Background = imageBrush;
                        }
                        else if (Gumb3.Tag == "7" || Gumb3.Tag == "flag7")
                        {
                            Gumb3.Tag = "7clicked";
                            string imagePath = "ms-appx:///Assets/7.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb3.Background = imageBrush;
                        }
                        else if (Gumb3.Tag == "8" || Gumb3.Tag == "flag8")
                        {
                            Gumb3.Tag = "8clicked";
                            string imagePath = "ms-appx:///Assets/8.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb3.Background = imageBrush;
                        }
                    }
                }
                if (i != 1 && i != 17 && i != 33 && i != 49 && i != 65 && i != 81 && i != 97 && i != 113 && i != 129 && i != 145 && i != 161 && i != 177 && i != 193 && i != 209 && i != 225 && i != 241)
                {
                    if (Gumb4 != null)
                    {
                        if (Gumb4.Tag == "flag" || Gumb4.Tag == "clear")
                        {
                            Gumb4.Tag = "clicked";
                            string imagePath = "ms-appx:///Assets/tileclicked.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb4.Background = imageBrush;
                            AdvanceField(Gumb4);
                        }
                        else if (Gumb4.Tag == "1" || Gumb4.Tag == "flag1")
                        {
                            Gumb4.Tag = "1clicked";
                            string imagePath = "ms-appx:///Assets/1.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb4.Background = imageBrush;
                        }
                        else if (Gumb4.Tag == "2" || Gumb4.Tag == "flag2")
                        {
                            Gumb4.Tag = "2clicked";
                            string imagePath = "ms-appx:///Assets/2.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb4.Background = imageBrush;
                        }
                        else if (Gumb4.Tag == "3" || Gumb4.Tag == "flag3")
                        {
                            Gumb4.Tag = "3clicked";
                            string imagePath = "ms-appx:///Assets/3.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb4.Background = imageBrush;
                        }
                        else if (Gumb4.Tag == "4" || Gumb4.Tag == "flag4")
                        {
                            Gumb4.Tag = "4clicked";
                            string imagePath = "ms-appx:///Assets/4.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb4.Background = imageBrush;
                        }
                        else if (Gumb4.Tag == "5" || Gumb4.Tag == "flag5")
                        {
                            Gumb4.Tag = "5clicked";
                            string imagePath = "ms-appx:///Assets/5.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb4.Background = imageBrush;
                        }
                        else if (Gumb4.Tag == "6" || Gumb4.Tag == "flag6")
                        {
                            Gumb4.Tag = "6clicked";
                            string imagePath = "ms-appx:///Assets/6.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb4.Background = imageBrush;
                        }
                        else if (Gumb4.Tag == "7" || Gumb4.Tag == "flag7")
                        {
                            Gumb4.Tag = "7clicked";
                            string imagePath = "ms-appx:///Assets/7.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb4.Background = imageBrush;
                        }
                        else if (Gumb4.Tag == "8" || Gumb4.Tag == "flag8")
                        {
                            Gumb4.Tag = "8clicked";
                            string imagePath = "ms-appx:///Assets/8.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb4.Background = imageBrush;
                        }
                    }
                }
                if (i != 16 && i != 32 && i != 48 && i != 64 && i != 80 && i != 96 && i != 112 && i != 128 && i != 144 && i != 160 && i != 176 && i != 192 && i != 208 && i != 224 && i != 240 && i != 256)
                {
                    if (Gumb5 != null)
                    {
                        if (Gumb5.Tag == "flag" || Gumb5.Tag == "clear")
                        {
                            Gumb5.Tag = "clicked";
                            string imagePath = "ms-appx:///Assets/tileclicked.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb5.Background = imageBrush;
                            AdvanceField(Gumb5);
                        }
                        else if (Gumb5.Tag == "1" || Gumb5.Tag == "flag1")
                        {
                            Gumb5.Tag = "1clicked";
                            string imagePath = "ms-appx:///Assets/1.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb5.Background = imageBrush;
                        }
                        else if (Gumb5.Tag == "2" || Gumb5.Tag == "flag2")
                        {
                            Gumb5.Tag = "2clicked";
                            string imagePath = "ms-appx:///Assets/2.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb5.Background = imageBrush;
                        }
                        else if (Gumb5.Tag == "3" || Gumb5.Tag == "flag3")
                        {
                            Gumb5.Tag = "3clicked";
                            string imagePath = "ms-appx:///Assets/3.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb5.Background = imageBrush;
                        }
                        else if (Gumb5.Tag == "4" || Gumb5.Tag == "flag4")
                        {
                            Gumb5.Tag = "4clicked";
                            string imagePath = "ms-appx:///Assets/4.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb5.Background = imageBrush;
                        }
                        else if (Gumb5.Tag == "5" || Gumb5.Tag == "flag5")
                        {
                            Gumb5.Tag = "5clicked";
                            string imagePath = "ms-appx:///Assets/5.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb5.Background = imageBrush;
                        }
                        else if (Gumb5.Tag == "6" || Gumb5.Tag == "flag6")
                        {
                            Gumb5.Tag = "6clicked";
                            string imagePath = "ms-appx:///Assets/6.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb5.Background = imageBrush;
                        }
                        else if (Gumb5.Tag == "7" || Gumb5.Tag == "flag7")
                        {
                            Gumb5.Tag = "7clicked";
                            string imagePath = "ms-appx:///Assets/7.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb5.Background = imageBrush;
                        }
                        else if (Gumb5.Tag == "8" || Gumb5.Tag == "flag8")
                        {
                            Gumb5.Tag = "8clicked";
                            string imagePath = "ms-appx:///Assets/8.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb5.Background = imageBrush;
                        }
                    }
                }
                if (i != 1 && i != 17 && i != 33 && i != 49 && i != 65 && i != 81 && i != 97 && i != 113 && i != 129 && i != 145 && i != 161 && i != 177 && i != 193 && i != 209 && i != 225 && i != 241)
                {
                    if (Gumb6 != null)
                    {
                        if (Gumb6.Tag == "flag" || Gumb6.Tag == "clear")
                        {
                            Gumb6.Tag = "clicked";
                            string imagePath = "ms-appx:///Assets/tileclicked.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb6.Background = imageBrush;
                            AdvanceField(Gumb6);
                        }
                        else if (Gumb6.Tag == "1" || Gumb6.Tag == "flag1")
                        {
                            Gumb6.Tag = "1clicked";
                            string imagePath = "ms-appx:///Assets/1.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb6.Background = imageBrush;
                        }
                        else if (Gumb6.Tag == "2" || Gumb6.Tag == "flag2")
                        {
                            Gumb6.Tag = "2clicked";
                            string imagePath = "ms-appx:///Assets/2.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb6.Background = imageBrush;
                        }
                        else if (Gumb6.Tag == "3" || Gumb6.Tag == "flag3")
                        {
                            Gumb6.Tag = "3clicked";
                            string imagePath = "ms-appx:///Assets/3.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb6.Background = imageBrush;
                        }
                        else if (Gumb6.Tag == "4" || Gumb6.Tag == "flag4")
                        {
                            Gumb6.Tag = "4clicked";
                            string imagePath = "ms-appx:///Assets/4.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb6.Background = imageBrush;
                        }
                        else if (Gumb6.Tag == "5" || Gumb6.Tag == "flag5")
                        {
                            Gumb6.Tag = "5clicked";
                            string imagePath = "ms-appx:///Assets/5.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb6.Background = imageBrush;
                        }
                        else if (Gumb6.Tag == "6" || Gumb6.Tag == "flag6")
                        {
                            Gumb6.Tag = "6clicked";
                            string imagePath = "ms-appx:///Assets/6.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb6.Background = imageBrush;
                        }
                        else if (Gumb6.Tag == "7" || Gumb6.Tag == "flag7")
                        {
                            Gumb6.Tag = "7clicked";
                            string imagePath = "ms-appx:///Assets/7.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb6.Background = imageBrush;
                        }
                        else if (Gumb6.Tag == "8" || Gumb6.Tag == "flag8")
                        {
                            Gumb6.Tag = "8clicked";
                            string imagePath = "ms-appx:///Assets/8.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb6.Background = imageBrush;
                        }
                    }
                }
                if (Gumb7 != null)
                {
                    if (Gumb7.Tag == "flag" || Gumb7.Tag == "clear")
                    {
                        Gumb7.Tag = "clicked";
                        string imagePath = "ms-appx:///Assets/tileclicked.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb7.Background = imageBrush;
                        AdvanceField(Gumb7);
                    }
                    else if (Gumb7.Tag == "1" || Gumb7.Tag == "flag1")
                    {
                        Gumb7.Tag = "1clicked";
                        string imagePath = "ms-appx:///Assets/1.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb7.Background = imageBrush;
                    }
                    else if (Gumb7.Tag == "2" || Gumb7.Tag == "flag2")
                    {
                        Gumb7.Tag = "2clicked";
                        string imagePath = "ms-appx:///Assets/2.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb7.Background = imageBrush;
                    }
                    else if (Gumb7.Tag == "3" || Gumb7.Tag == "flag3")
                    {
                        Gumb7.Tag = "3clicked";
                        string imagePath = "ms-appx:///Assets/3.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb7.Background = imageBrush;
                    }
                    else if (Gumb7.Tag == "4" || Gumb7.Tag == "flag4")
                    {
                        Gumb7.Tag = "4clicked";
                        string imagePath = "ms-appx:///Assets/4.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb7.Background = imageBrush;
                    }
                    else if (Gumb7.Tag == "5" || Gumb7.Tag == "flag5")
                    {
                        Gumb7.Tag = "5clicked";
                        string imagePath = "ms-appx:///Assets/5.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb7.Background = imageBrush;
                    }
                    else if (Gumb7.Tag == "6" || Gumb7.Tag == "flag6")
                    {
                        Gumb7.Tag = "6clicked";
                        string imagePath = "ms-appx:///Assets/6.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb7.Background = imageBrush;
                    }
                    else if (Gumb7.Tag == "7" || Gumb7.Tag == "flag7")
                    {
                        Gumb7.Tag = "7clicked";
                        string imagePath = "ms-appx:///Assets/7.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb7.Background = imageBrush;
                    }
                    else if (Gumb7.Tag == "8" || Gumb7.Tag == "flag8")
                    {
                        Gumb7.Tag = "8clicked";
                        string imagePath = "ms-appx:///Assets/8.png";
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                        Gumb7.Background = imageBrush;
                    }
                }
                if (i != 16 && i != 32 && i != 48 && i != 64 && i != 80 && i != 96 && i != 112 && i != 128 && i != 144 && i != 160 && i != 176 && i != 192 && i != 208 && i != 224 && i != 240 && i != 256)
                {
                    if (Gumb8 != null)
                    {
                        if (Gumb8.Tag == "flag" || Gumb8.Tag == "clear")
                        {
                            Gumb8.Tag = "clicked";
                            string imagePath = "ms-appx:///Assets/tileclicked.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb8.Background = imageBrush;
                            AdvanceField(Gumb8);
                        }
                        else if (Gumb8.Tag == "1" || Gumb8.Tag == "flag1")
                        {
                            Gumb8.Tag = "1clicked";
                            string imagePath = "ms-appx:///Assets/1.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb8.Background = imageBrush;
                        }
                        else if (Gumb8.Tag == "2" || Gumb8.Tag == "flag2")
                        {
                            Gumb8.Tag = "2clicked";
                            string imagePath = "ms-appx:///Assets/2.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb8.Background = imageBrush;
                        }
                        else if (Gumb8.Tag == "3" || Gumb8.Tag == "flag3")
                        {
                            Gumb8.Tag = "3clicked";
                            string imagePath = "ms-appx:///Assets/3.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb8.Background = imageBrush;
                        }
                        else if (Gumb8.Tag == "4" || Gumb8.Tag == "flag4")
                        {
                            Gumb8.Tag = "4clicked";
                            string imagePath = "ms-appx:///Assets/4.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb8.Background = imageBrush;
                        }
                        else if (Gumb8.Tag == "5" || Gumb8.Tag == "flag5")
                        {
                            Gumb8.Tag = "5clicked";
                            string imagePath = "ms-appx:///Assets/5.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb8.Background = imageBrush;
                        }
                        else if (Gumb8.Tag == "6" || Gumb8.Tag == "flag6")
                        {
                            Gumb8.Tag = "6clicked";
                            string imagePath = "ms-appx:///Assets/6.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb8.Background = imageBrush;
                        }
                        else if (Gumb8.Tag == "7" || Gumb8.Tag == "flag7")
                        {
                            Gumb8.Tag = "7clicked";
                            string imagePath = "ms-appx:///Assets/7.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb8.Background = imageBrush;
                        }
                        else if (Gumb8.Tag == "8" || Gumb8.Tag == "flag8")
                        {
                            Gumb8.Tag = "8clicked";
                            string imagePath = "ms-appx:///Assets/8.png";
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                            Gumb8.Background = imageBrush;
                        }
                    }
                }
            }
        }
        private int GetButtonNumber(string buttonName)
        {
            int buttonNumber = 0;
            string numberString = string.Empty;

            for (int i = buttonName.Length - 1; i >= 0; i--)
            {
                char c = buttonName[i];
                if (char.IsDigit(c))
                {
                    numberString = c + numberString;
                }
                else
                {
                    break;
                }
            }
            if (int.TryParse(numberString, out buttonNumber))
            {
                return buttonNumber;
            }
            return 0;
        }
    }
}

