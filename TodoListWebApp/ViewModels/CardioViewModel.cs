using System.Collections.Generic;
using TodoListWebApp.Models;

namespace TodoListWebApp.ViewModels
{
    public class CardioViewModel
    {
        public HalfMileTimeModel HalfMileTimeModel { get; set; } = new HalfMileTimeModel();
        public MileTimeModel MileTimeModel { get; set; } = new MileTimeModel();
        public PacerModel PacerModel { get; set; } = new PacerModel();
        public HeartRateModel HeartRateModel { get; set; } = new HeartRateModel();
        public StepTestModel StepTestModel { get; set; } = new StepTestModel();

        public IEnumerable<HalfMileTimeModel> HalfMile { get; set; }
        public IEnumerable<MileTimeModel> Mile { get; set; }
        public IEnumerable<PacerModel> Pacer { get; set; }
        public IEnumerable<HeartRateModel> HeartRate { get; set; }
        public IEnumerable<StepTestModel> StepTest { get; set; }

    }
}