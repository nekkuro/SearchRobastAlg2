using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRobastAlg
{
    public enum FilterList
    {
        Gauss,
        Median,
        HodgesLeman,
        Tikhonov,
        Wiener
    }

    class Colors
    {
        private double _blue;
        private double _green;
        private double _red;

        public Colors(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public Colors()
        {
            Red = 0;
            Green = 0;
            Blue = 0;
        }

        public double Red
        {
            get => _red;
            set
            {
                if (value > 255)
                    _red = 255;
                else if (value < 0)
                    _red = 0;
                else
                    _red = value;
            }
        }

        public double Green
        {
            get => _green;
            set
            {
                if (value > 255)
                    _green = 255;
                else if (value < 0)
                    _green = 0;
                else
                    _green = value;
            }
        }

        public double Blue
        {
            get => _blue;
            set
            {
                if (value > 255)
                    _blue = 255;
                else if (value < 0)
                    _blue = 0;
                else
                    _blue = value;
            }
        }
    }
}
