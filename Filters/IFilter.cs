using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRobastAlg
{
    public abstract class IFilter
    {
        private bool _setParam;
        public bool SetParam { get => _setParam; set { _setParam = value; } }
        
        public abstract Bitmap ApplyFilter();
        public abstract void SetFilterParam(Bitmap img, params object [] param);
    }
}
