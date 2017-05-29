using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRobastAlg
{
    public static class Noisiness
    {
        public static Bitmap SimpleNoise(Bitmap img, int frequency)
        {
            if (frequency > 100)
                frequency = 100;
            if (frequency < 0)
                frequency = 0;
            var noiseImg = img;
            var rnd = new Random();

            for (var x = 0; x < img.Width; x++)
            {
                for (var y = 0; y < img.Height; y++)
                {
                    if (rnd.Next(1, 100) > frequency)
                        continue;
                    var num = rnd.Next(0, 256);
                    noiseImg.SetPixel(x, y, Color.FromArgb(255, num, num, num));
                }
            }
            return noiseImg;
        }
    }
}
