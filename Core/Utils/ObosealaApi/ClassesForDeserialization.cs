using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.ObosealaApi
{
    public class FacialLandmarks
{
    public float right_eye { get; set; }
    public float left_eye { get; set; }
    public float nose { get; set; }
    public float mouth { get; set; }
    public float undereye_left { get; set; }
    public float undereye_right { get; set; }
    public float face { get; set; }
    public float final_prediction { get; set; }
}

public class FacialPredictionResult
{
    public FacialLandmarks Landmarks { get; set; }
    public string PredictedClass { get; set; }
}
}
