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
using SearchRobastAlg.Filters.Param;

namespace SearchRobastAlg
{
    public static class Research
    {
        public static IFilter<IParam> SetFilter(FilterList filter)
        {
            try
            {
                switch (filter)
                {
                    case FilterList.Gauss:
                        return (IFilter<IParam>)new Gauss();
                    case FilterList.Median:
                        return (IFilter<IParam>)new Median();
                    case FilterList.HodgesLeman:
                        return (IFilter<IParam>)new HodgesLeman();
                    case FilterList.Tikhonov:
                        return (IFilter<IParam>)new Tikhonov();
                    case FilterList.Wiener:
                        return (IFilter<IParam>)new Wiener();
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
            var noiseImg = Noisiness.SimpleNoise(original, 25);
            noiseImg.Save(@"C:\1\noise.jpg");

            var gauss = SetFilter(FilterList.Gauss);
            gauss.ApplyFilter(noiseImg, new GaussParam {Size=3, Weight =2}).Save(@"C:\1\gauss.jpg");


            var median = SetFilter(FilterList.Median);
            median.ApplyFilter(noiseImg, new MedianParam { Size = 2}).Save(@"C:\1\median.jpg");
        }        
    }
}
