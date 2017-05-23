using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRobastAlg.Filters
{
    class Median : IFilter
    {
        public Median()
        {
            SetParam = false;
        }

        private int _size;
        private Bitmap _srcImage;

        public override void SetFilterParam(Bitmap img, params object[] param)
        {
            _size = (int)param[0];
            _srcImage = img;
            SetParam = true;
        }
        public override Bitmap ApplyFilter()
        {
            if (!SetParam)
                return null;
            var tempBitmap = _srcImage;
            var newBitmap = new Bitmap(tempBitmap.Width, tempBitmap.Height);
            var newGraphics = Graphics.FromImage(newBitmap);
            newGraphics.DrawImage(tempBitmap,
            new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
            new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
            GraphicsUnit.Pixel);
            newGraphics.Dispose();
            var apetureMin = -(_size / 2);
            var apetureMax = _size / 2;

            for (var x = 0; x < newBitmap.Width; ++x)
            {
                for (var y = 0; y < newBitmap.Height; ++y)
                {
                    var rValues = new List<int>();
                    var gValues = new List<int>();
                    var bValues = new List<int>();
                    for (var x2 = apetureMin; x2 < apetureMax; ++x2)
                    {
                        var tempX = x + x2;
                        if (tempX < 0 || tempX >= newBitmap.Width)
                            continue;
                        for (var y2 = apetureMin; y2 < apetureMax; ++y2)
                        {
                            var tempY = y + y2;
                            if (tempY < 0 || tempY >= newBitmap.Height)
                                continue;
                            var tempColor = tempBitmap.GetPixel(tempX, tempY);
                            rValues.Add(tempColor.R);
                            gValues.Add(tempColor.G);
                            bValues.Add(tempColor.B);
                        }
                    }
                    rValues.Sort();
                    gValues.Sort();
                    bValues.Sort();
                    var medianPixel = Color.FromArgb(rValues[rValues.Count / 2],
                    gValues[gValues.Count / 2],
                    bValues[bValues.Count / 2]);
                    newBitmap.SetPixel(x, y, medianPixel);
                }
            }
            return newBitmap;
        }
    }
}
