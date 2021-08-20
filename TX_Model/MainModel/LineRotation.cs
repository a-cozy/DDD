using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MainModel
{
    public class LineRotation : ILineRotation
    {
        private readonly IScaleAdjuster _ScaleAdjuster;

        public event EventHandler EndCalDeg;

        public double DispWidth { get; private set; }

        public double DispHeight { get; private set; }

        public LineRotation(IScaleAdjuster scale)
        {
            _ScaleAdjuster = scale;
            _ScaleAdjuster.ResetImage += (s, e) => 
            {

            };
        }
    }

    public interface ILineRotation
    {
        event EventHandler EndCalDeg;

    }

}
