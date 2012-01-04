using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ImageTools;
using ImageTools.IO;
using ImageTools.IO.Png;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MySuperTile
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handler for the Generate Tile Button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
          

            var mySweetDynamicTile = new DynamicTile
                                {
                                    Text = textCount.Text,
                                    Title = "MySuperTile",
                                    Background = new SolidColorBrush(Colors.Purple)
                                 
                                };

            // Retrieve the contents of the tile as a StandardTileData
            var newTile = mySweetDynamicTile.ToTile();

            // Use the new tile as the primary tile for this app.
            ShellTile primaryTile = ShellTile.ActiveTiles.First();

            if (primaryTile != null)
            {
                primaryTile.Update(newTile);
            }

            
        }
    }
}