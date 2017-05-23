using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SearchRobastAlg.Filters
{
    public class Gauss : IFilter
    {
        public Gauss()
        {
            SetParam = false;
        }
        private double[,] _kernel;
        private Bitmap _srcImage;

        public override void SetFilterParam(Bitmap img, params object[] param)
        {
            _kernel = GaussianBlur((int)param[0], Convert.ToDouble(param[1]));
            _srcImage = img;
            SetParam = true;
        }

        public override Bitmap ApplyFilter()
        {
            if (!SetParam)
                return null;
            var width = _srcImage.Width;
            var height = _srcImage.Height;
            var srcData = _srcImage.LockBits(new Rectangle(0, 0, width, height),
            ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bytes = srcData.Stride * srcData.Height;
            var buffer = new byte[bytes];
            var result = new byte[bytes];
            Marshal.Copy(srcData.Scan0, buffer, 0, bytes);
            _srcImage.UnlockBits(srcData);

            var lenght = (_kernel.GetLength(0) - 1) / 2;
            for (var y = lenght; y < height - lenght; y++)
                for (var x = lenght; x < width - lenght; x++)
                {
                    var color = new Colors();
                    var kcenter = y * srcData.Stride + x * 4;
                    for (var i = -lenght; i <= lenght; i++)
                        for (var j = -lenght; j <= lenght; j++)
                        {
                            var kpixel = kcenter + i * srcData.Stride + j * 4;
                            color.Red += buffer[kpixel + 0] * _kernel[i + lenght, j + lenght];
                            color.Green += buffer[kpixel + 1] * _kernel[i + lenght, j + lenght];
                            color.Blue += buffer[kpixel + 2] * _kernel[i + lenght, j + lenght];
                        }

                    result[kcenter + 0] = (byte)color.Red;
                    result[kcenter + 1] = (byte)color.Green;
                    result[kcenter + 2] = (byte)color.Blue;
                    result[kcenter + 3] = 255;
                }
            var resultImage = new Bitmap(width, height);
            var resultData = resultImage.LockBits(new Rectangle(0, 0, width, height),
            ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, resultData.Scan0, bytes);
            resultImage.UnlockBits(resultData);
            return resultImage;
        }

        public static double[,] GaussianBlur(int size, double weight)
        {
            var kernel = new double[size, size];
            var kernelSum = 0.0;
            //Левая часть расчетной формулы
            var factor = 1 / (2 * Math.PI * weight * weight);
            //Так как нам нужна нумерация элементов массива от -N до N, где N = (size-1)/2, то вводим переменную
            var lenght = (size - 1) / 2;
            for (var i = -lenght; i <= lenght; i++)
                for (var j = -lenght; j <= lenght; j++)
                {
                    var distance = (i * i + j * j) / (2 * weight * weight);
                    kernel[i + lenght, j + lenght] = factor * Math.Exp(-distance);
                    kernelSum += kernel[i + lenght, j + lenght];
                }
            //Нормализация ядра
            for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                    kernel[i, j] = kernel[i, j] * 1 / kernelSum;
            return kernel;
        }
    }
}
