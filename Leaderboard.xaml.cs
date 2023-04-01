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
using DataAccessLibrary;
using Windows.ApplicationModel.UserDataTasks;
using Windows.Storage;
using Microsoft.Data.Sqlite;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Projektni
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Leaderboard : Page
    {
        public Leaderboard()
        {
            this.InitializeComponent();
            DataAccess.InitalizeDatabase();

            List<User> users = DataAccess.GetUsers();

            foreach ( User user in users)
            {
                ListBoxItem item = new ListBoxItem();
                item.DataContext = user;
                item.ContentTemplate = (DataTemplate)Resources["UserListItem"];
                UserList.Items.Add(item);

            }
        }

        private void Naslov_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}