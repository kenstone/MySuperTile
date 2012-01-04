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
using Microsoft.Phone.Shell;

namespace MySuperTile
{
    public partial class DynamicTile : UserControl
    {
        /// <summary>
        /// Identifies <see cref="TextProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(DynamicTile), null);

        /// <summary>
        /// Identifies <see cref="TitleProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DynamicTile), null);

        /// <summary>
        /// Gets or sets the text displayed in the tile.
        /// This is a dependency property.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title of the tile.
        /// This is a dependency property.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public DynamicTile()
        {
            InitializeComponent();

            DataContext = this;
        }

        /// <summary>
        /// Used to render the contents to a tile
        /// </summary>
        /// <returns>a <see cref="StandardTileData"/> with the contents of this control</returns>
        public StandardTileData ToTile()
        {
            // Need to call these, otherwise the contents aren't rendered correctly.
            this.Measure(new Size(173, 173));
            this.Arrange(new Rect(0, 0, 173, 173));

            // The png encoder is the work of the ImageTools API. http://imagetools.codeplex.com/
            ExtendedImage tileImaged = this.ToImage();

            Encoders.AddEncoder<PngEncoder>();

            var p = new PngEncoder();

            const string tempFileName = "/Shared/ShellContent/tileImage.png";

            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (myIsolatedStorage.FileExists(tempFileName))
                {
                    myIsolatedStorage.DeleteFile(tempFileName);
                }

                IsolatedStorageFileStream fileStream = myIsolatedStorage.CreateFile(tempFileName);
                p.Encode(tileImaged, fileStream);
                fileStream.Close();
            }

            var newTile = new StandardTileData
            {
                Title = Title,
                BackgroundImage =
                    new Uri("isostore:" + tempFileName, UriKind.RelativeOrAbsolute)
            };

            return newTile;
        }
    }
}
