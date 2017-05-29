using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRobastAlg
{
    class Appraiser
    {
        private double StandardDeviation(Bitmap imgSrc, Bitmap imgCompressed)
        {
            var colorSum = 0;
            for (var x = 0; x < imgSrc.Width; ++x)
            {
                for (var y = 0; y < imgSrc.Height; ++y)
                {
                    var sourcePixelColor = imgSrc.GetPixel(x, y);
                    var sourcePixelSum = sourcePixelColor.R + sourcePixelColor.G + sourcePixelColor.B;

                    var compressedPixelColor = imgCompressed.GetPixel(x, y);
                    var compressedPixelSum = compressedPixelColor.R + compressedPixelColor.G + compressedPixelColor.B;

                    colorSum += (sourcePixelSum- compressedPixelSum) * (sourcePixelSum - compressedPixelSum);
                }
            }
            return colorSum / 3 * (imgSrc.Width * imgSrc.Height);
        }

        private double Average(Bitmap img)
        {
            var colorSum = 0.0;
            for (var x = 0; x < img.Width; ++x)
            {
                for (var y = 0; y < img.Height; ++y)
                {
                    var sourcePixelColor = img.GetPixel(x, y);
                    var sourcePixelSum = sourcePixelColor.R + sourcePixelColor.G + sourcePixelColor.B;

                    colorSum += sourcePixelColor.R + sourcePixelColor.G + sourcePixelColor.B;
                }
            }
            return colorSum / 3 * (img.Width * img.Height);
        }

        private double Variance(Bitmap img, double avg)
        {
            var variance = 0.0;
            for (var x = 0; x < img.Width; ++x)
            {
                for (var y = 0; y < img.Height; ++y)
                {
                    var pixelColor = img.GetPixel(x, y);
                    variance += Math.Abs(pixelColor.R - avg) * Math.Abs(pixelColor.R - avg);
                    variance += Math.Abs(pixelColor.G - avg) * Math.Abs(pixelColor.G - avg);
                    variance += Math.Abs(pixelColor.B - avg) * Math.Abs(pixelColor.B - avg);
                }
            }
            return variance;
        }

        private double Covariance(Bitmap imgSrc, Bitmap imgCompressed, double avgImgSrc, double avgImgCompressed)
        {
            var covariance = 0.0;
            for (var x = 0; x < imgSrc.Width; ++x)
            {
                for (var y = 0; y < imgSrc.Height; ++y)
                {
                    var sourcePixelColor = imgSrc.GetPixel(x, y);
                    var compressedPixelColor = imgCompressed.GetPixel(x, y);

                    covariance += (sourcePixelColor.R - avgImgSrc) * (compressedPixelColor.R - avgImgCompressed) +
                        (sourcePixelColor.G - avgImgSrc) * (compressedPixelColor.G - avgImgCompressed) +
                        (sourcePixelColor.B - avgImgSrc) * (compressedPixelColor.B - avgImgCompressed);
                }
            }
            return covariance;
        }

        public double Pnsr(Bitmap imgSrc, Bitmap imgCompressed)
        {
            var D = 255;
            return (10 * Math.Log10((D * D) / StandardDeviation(imgSrc, imgCompressed)));
        }

        public double Ssim(Bitmap imgSrc, Bitmap imgCompressed)
        {
            var c1 = (0.01 * 255 * 0.01 * 255);
            var c2 = (0.03 * 255 * 0.03 * 255);
            var avgImgSrc = Average(imgSrc);
            var avgImgCompressed = Average(imgCompressed);
            var varianceImgSrc = Variance(imgSrc, avgImgSrc);
            var varianceImgCompressed = Variance(imgCompressed, avgImgCompressed);
            var covariance = Covariance(imgSrc, imgCompressed, avgImgSrc, avgImgCompressed);
            return (2 * avgImgSrc * avgImgCompressed + c1) * (2 * covariance + c2) /
                ((avgImgSrc * avgImgSrc + avgImgCompressed * avgImgCompressed + c1) * (varianceImgSrc * varianceImgSrc + varianceImgCompressed * varianceImgCompressed + c2));
        }
    }
}
