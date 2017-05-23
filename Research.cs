using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using SearchRobastAlg.Filters;
using System.Windows;

namespace SearchRobastAlg
{
    public enum FilterList
    {
        Gauss,
        Median
    }

    public static class Research
    {
        public static IFilter SetFilter(FilterList filter)
        {
            try
            {
                switch (filter)
                {
                    case FilterList.Gauss:
                        return new Gauss();
                    case FilterList.Median:
                        return new Median();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return null;
        }

        public static void ApplyFilters(string filePath)
        {
            var original = new Bitmap(filePath);
            var noiseImg = Noise(original);
            noiseImg.Save(@"C:\1\noise.jpg");

            var gauss = Research.SetFilter(FilterList.Gauss);
            gauss.SetFilterParam(noiseImg, 3, 2);
            gauss.ApplyFilter().Save(@"C:\1\gauss.jpg");


            var median = Research.SetFilter(FilterList.Median);
            median.SetFilterParam(noiseImg, 2);
            median.ApplyFilter().Save(@"C:\1\median.jpg");
        }

        private static Bitmap Noise(Bitmap img)
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
