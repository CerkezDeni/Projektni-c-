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

namespace Projektni
{
    public sealed partial class BlankPage1 : Page
    {
        private CancellationTokenSource cancellationTokenSource;

        public BlankPage1()
        {
            this.InitializeComponent();
            cancellationTokenSource = new CancellationTokenSource();
            CreateBackgroundThread();
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
                
                while (!cancellationTokenSource.IsCancellationRequested || destroy==false)
                {
                    if(min==60)
                    {
                        timeraaa.Text = "EXPIRED";
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
            }, cancellationTokenSource.Token);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            cancellationTokenSource.Cancel();
            base.OnNavigatingFrom(e);
        }
    }
}

