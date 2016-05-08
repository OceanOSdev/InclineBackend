using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

// This file is literally a collection of properties
namespace TodoListWebApp.Models
{
    public class Flexibility
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        [DisplayName("Sit & Reach (cm)")]
        public int SitAndReach { get; set; }
        [DisplayName("Arm & Shoulder (in)")]
        public int ArmAndShoulder { get; set; }
        [DisplayName("Trunk Lift (in)")]
        public int TrunkLift { get; set; }
        public DateTime Logged { get; set; }
    }

    public class BodyComposition
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        [DisplayName("Height (in)")]
        public int Height { get; set; }
        [DisplayName("Weight (lbs)")]
        public int Weight { get; set; }
        [DisplayName("Percent Body Fat")]
        public decimal BodyFat { get; set; }
        public DateTime Logged { get; set; }
    }

    public class CardiovascularFitness
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        [DisplayName("1/2 Mile Time")]
        public TimeSpan HalfMileTime { get; set; }
        [DisplayName("Pacer (Laps)")]
        public int Pacer { get; set; }
        [DisplayName("Mile Run Time")]
        public TimeSpan MileTime { get; set; }
        [DisplayName("3 Minute Step Test (Steps)")]
        public int StepTestSteps { get; set; }
        [DisplayName("Step Test (Heart Rate)")]
        public int StepTestHeartRate { get; set; }
        public DateTime Logged { get; set; }
    }

    public class MuscularStrengthAndEndurance
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        [DisplayName("1 Minute Curl Ups")]
        public int CurlUps { get; set; }
        [DisplayName("Right Angle Push Ups")]
        public int RightAnglePushUps { get; set; }
        [DisplayName("Max Bench (lbs)")]
        public int MaxBench { get; set; }
        [DisplayName("Max Leg Press (lbs)")]
        public int MaxLegPress { get; set; }
        [DisplayName("Pull Ups")]
        public int PullUps { get; set; }
        [DisplayName("Flexed Arm Hang (seconds)")]
        public TimeSpan FlexedArmHang { get; set; }
        public DateTime Logged { get; set; }
    }
}