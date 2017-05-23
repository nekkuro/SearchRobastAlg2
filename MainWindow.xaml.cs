using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace SearchRobastAlg
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string _filePath;
        public void Load(object sender, RoutedEventArgs e)
        {
            _filePath = Data.OpenFile();
            if (String.IsNullOrEmpty(_filePath))
                return;
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(_filePath, UriKind.Relative);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();
            image.Source = src;
            image.Stretch = Stretch.Uniform;
        }

        public void Calculate(object sender, RoutedEventArgs e)
        {
            Research.ApplyFilters(_filePath);
        }

        private Bitmap Noise(Bitmap img)
        {
            var noiseImg = img;
            var rnd = new Random();

            for (var x = 0; x < img.Width; x++)
            {
                for (var y = 0; y < img.Height; y++)
                {
                    if (rnd.Next(0, 4) != 3)
                        continue;
                    var num = rnd.Next(0, 256);
                    noiseImg.SetPixel(x, y, System.Drawing.Color.FromArgb(255, num, num, num));
                }
            }
            return noiseImg;
        }
    }
}
