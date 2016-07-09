using System.Collections.Generic;
using TodoListWebApp.Models;

namespace TodoListWebApp.ViewModels
{
    public class StrengthEnduranceViewModel
    {
        public CurlUpModel CurlUpModel { get; set; } = new CurlUpModel();
        public RightAnglePushUpModel RightAnglePushUpModel { get; set; } = new RightAnglePushUpModel();
        public MaxBenchModel MaxBenchModel { get; set; } = new MaxBenchModel();
        public MaxLegPressModel MaxLegPressModel { get; set; } = new MaxLegPressModel();
        public PullUpModel PullUpModel { get; set; } = new PullUpModel();
        public FlexedArmHangModel FlexedArmHangModel { get; set; } = new FlexedArmHangModel();

        public IEnumerable<CurlUpModel> CurlUp { get; set; }
        public IEnumerable<RightAnglePushUpModel> RightAnglePushUp { get; set; }
        public IEnumerable<MaxBenchModel> MaxBench { get; set; }
        public IEnumerable<MaxLegPressModel> MaxLegPress { get; set; }
        public IEnumerable<PullUpModel> PullUp { get; set; }
        public IEnumerable<FlexedArmHangModel> FlexedArmHang { get; set; }
    }
}