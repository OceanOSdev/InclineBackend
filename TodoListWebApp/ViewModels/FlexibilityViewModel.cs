using System.Collections.Generic;
using TodoListWebApp.Models;

namespace TodoListWebApp.ViewModels
{
    public class FlexibilityViewModel
    {
        public SitAndReachModel SitAndReachModel { get; set; } = new SitAndReachModel();
        public ArmAndShoulderModel ArmAndShoulderModel { get; set; } = new ArmAndShoulderModel();
        public TrunkLiftModel TrunkLiftModel { get; set; } = new TrunkLiftModel();

        public IEnumerable<SitAndReachModel> SitAndReach { get; set; }
        public IEnumerable<ArmAndShoulderModel> ArmAndShoulder { get; set; }
        public IEnumerable<TrunkLiftModel> TrunkLift { get; set; }
    }
}