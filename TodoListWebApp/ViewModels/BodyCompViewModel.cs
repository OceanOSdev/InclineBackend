using System.Collections.Generic;
using TodoListWebApp.Models;

namespace TodoListWebApp.ViewModels
{
    public class BodyCompViewModel
    {
        /// <summary>
        /// The purpose of the properties with model at the end of them is
        /// so that I can safely pass string representations of the model's properties
        /// between view model and view.
        /// </summary>
        public HeightModel HeightModel { get; set; } = new HeightModel();
        public WeightModel WeightModel { get; set; } = new WeightModel();
        public PercentBodyFatModel PercentBodyFatModel { get; set; } = new PercentBodyFatModel();
        public IEnumerable<HeightModel> Height { get; set; }
        public IEnumerable<WeightModel> Weight { get; set; }
        public IEnumerable<PercentBodyFatModel> BodyFat { get; set; }
    }
}
