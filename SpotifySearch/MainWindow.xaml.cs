using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpotifySearch.Helpers;
using SpotifySearch.Models;

namespace SpotifySearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Task.Run(async () => await SearchHelper.GetTokenAsync());
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            var result = SearchHelper.SearchArtistOrSong(txtSearch.Text);

            if (result == null)
            {
                return;
            }

            var listArtist = new List<SpotifyArtist>();

            foreach (var item in result.artists.items)
            {
                listArtist.Add(new SpotifyArtist()
                {
                    ID = item.id,
                    Image = item.images.Any() ? item.images[0].url : "https://www.pikpng.com/pngl/b/75-756814_login-user-imagen-user-png-clipart.png",
                    Name = item.name,
                    Popularity = $"{item.popularity}% popularidad",
                    Followers = $"{item.followers.total.ToString("N")} seguidores"
                });
            }

            ListArtist.ItemsSource = listArtist;
        }
    }
}
