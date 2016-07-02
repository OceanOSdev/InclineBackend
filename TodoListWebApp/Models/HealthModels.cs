using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

// This file is literally a collection of properties
namespace TodoListWebApp.Models
{
    #region Current Models
    /// <summary>
    /// The Model that represents a user's sit and reach.
    /// </summary>
    public class SitAndReachModel : BaseModel
    {
        /// <summary>The length in centimeters of the user's sit and reach. </summary>
        [DisplayName("Sit & Reach (cm)")]
        public int SitAndReach { get; set; }
    }

    /// <summary>
    /// The Model that represents a user's Arm and Shoulder Measurement.
    /// </summary>
    public class ArmAndShoulderModel : BaseModel
    {
        /// <summary>The height in inches of the arm and shoulder thing. </summary>
        [DisplayName("Arm & Shoulder (in)")]
        public int ArmAndShoulder { get; set; }
    }

    /// <summary>
    /// The Model that represents a user's trunk lift.
    /// </summary>
    public class TrunkLiftModel : BaseModel
    {
        /// <summary>The height in inches of the trunk lift. </summary>
        [DisplayName("Trunk Lift (in)")]
        public int TrunkLift { get; set; }
    }

    /// <summary>
    /// The Model that represents a user's height.
    /// </summary>
    public class HeightModel : BaseModel
    {
        /// <summary>The Height in inches. </summary>
        [DisplayName("Height (in)")]
        public int Height { get; set; }
    }

    /// <summary>
    /// The Model that represents a user's weight.
    /// </summary>
    public class WeightModel : BaseModel
    {
        /// <summary>The Weight in pounds. </summary>
        [DisplayName("Weight (lbs)")]
        public int Weight { get; set; }
    }

    /// <summary>
    /// The Model that represents a user's percent body fat.
    /// </summary>
    public class PercentBodyFatModel : BaseModel
    {
        /// <summary>The percent body fat. </summary>
        [DisplayName("Percent Body Fat")]
        public decimal BodyFat { get; set; }
    }

    /// <summary>
    /// The Model that represents a user's half mile time.
    /// </summary>
    public class HalfMileTimeModel : BaseModel
    {
        /// <summary>The time it took to run the half mile. </summary>
        [DisplayName("1/2 Mile Time")]
        public TimeSpan HalfMileTime { get; set; }
    }

    /// <summary>
    /// The model that represents a user's pacer score.
    /// </summary>
    public class PacerModel : BaseModel
    {
        /// <summary>The laps completed during the pacer test. </summary>
        [DisplayName("Pacer (Laps)")]
        public int Pacer { get; set; }
    }

    /// <summary>
    /// The model that represents a user's mile time.
    /// </summary>
    public class MileTimeModel : BaseModel
    {
        /// <summary>The time it took to run the mile. </summary>
        [DisplayName("Mile Run Time")]
        public TimeSpan MileTime { get; set; }
    }

    /// <summary>
    /// The model that represents a user's step test scores.
    /// </summary>
    public class StepTestModel : BaseModel
    {
        /// <summary>The amount of steps taken during the 3 minute step test. </summary>
        [DisplayName("3 Minute Step Test (Steps)")]
        public int StepTestSteps { get; set; }
    }

    /// <summary>
    /// THe model that represents a user's step test heart rate.
    /// </summary>
    public class StepTestHeartRateModel : BaseModel
    {
        /// <summary>The user's heart rate after the 3 minute step test. </summary>
        [DisplayName("Step Test (Heart Rate)")]
        public int StepTestHeartRate { get; set; }
    }

    /// <summary>
    /// The model that represents a user's curl ups.
    /// </summary>
    public class CurlUpModel : BaseModel
    {
        /// <summary>The amount of curl ups completed during the 1 minute curl up test. </summary>
        [DisplayName("1 Minute Curl Ups")]
        public int CurlUps { get; set; }
    }

    /// <summary>
    /// The model that represents a user's push up data.
    /// </summary>
    public class RightAnglePushUpModel : BaseModel
    {
        /// <summary>The amount of right angle push ups completed during the right angle push up test. </summary>
        [DisplayName("Right Angle Push Ups")]
        public int RightAnglePushUps { get; set; }
    }

    /// <summary>
    /// The model that represents a user's max bench.
    /// </summary>
    public class MaxBenchModel : BaseModel
    {
        /// <summary>The user's max bench in pounds. </summary>
        [DisplayName("Max Bench (lbs)")]
        public int MaxBench { get; set; }
    }

    /// <summary>
    /// The model that represents a user's max leg press.
    /// </summary>
    public class MaxLegPressModel : BaseModel
    {
        /// <summary>The user's max leg press in pounds. </summary>
        [DisplayName("Max Leg Press (lbs)")]
        public int MaxLegPress { get; set; }
    }

    /// <summary>
    /// The model that represents a user's pull ups.
    /// </summary>
    public class PullUpModel : BaseModel
    {
        /// <summary>The amount of pull ups completed during the pull up test. </summary>
        [DisplayName("Pull Ups")]
        public int PullUps { get; set; }
    }

    /// <summary>
    /// The model that represents a user's flexed arm hang.
    /// </summary>
    public class FlexedArmHangModel : BaseModel
    {
        /// <summary>The duration of time that the user was able to perform the flexed arm hang in seconds. </summary>
        [DisplayName("Flexed Arm Hang (seconds)")]
        public TimeSpan FlexedArmHang { get; set; }
    }
    #endregion

    #region Deprecated Models
    /// <summary>
    /// Models the data relevant to Flexibility.
    /// </summary>
    public class Flexibility
    {
        /// <summary>The ID. </summary>
        public int ID { get; set; }
        /// <summary>The weird string that identifies the user. </summary>
        public string Owner { get; set; }
        /// <summary>The length in centimeters of the user's sit and reach. </summary>
        [DisplayName("Sit & Reach (cm)")]
        public int SitAndReach { get; set; }
        /// <summary>The height in inches of the arm and shoulder thing. </summary>
        [DisplayName("Arm & Shoulder (in)")]
        public int ArmAndShoulder { get; set; }
        /// <summary>The height in inches of the trunk lift. </summary>
        [DisplayName("Trunk Lift (in)")]
        public int TrunkLift { get; set; }
        /// <summary>The time the model was logged. </summary>
        public DateTime Logged { get; set; }
    }

    /// <summary>
    /// Models the data relevant to Body Composition.
    /// </summary>
    public class BodyComposition
    {
        /// <summary>The ID. </summary>
        public int ID { get; set; }
        /// <summary>The string identifier for the user. </summary>
        public string Owner { get; set; }
        /// <summary>The Height in inches. </summary>
        [DisplayName("Height (in)")]
        public int Height { get; set; }
        /// <summary>The Weight in pounds. </summary>
        [DisplayName("Weight (lbs)")]
        public int Weight { get; set; }
        /// <summary>The percent body fat. </summary>
        [DisplayName("Percent Body Fat")]
        public decimal BodyFat { get; set; }
        /// <summary>The time the model was logged. </summary>
        public DateTime Logged { get; set; }
    }

    /// <summary>
    /// Models the data relevant to Cardiovascular Fitness.
    /// </summary>
    public class CardiovascularFitness
    {
        /// <summary>The ID. </summary>
        public int ID { get; set; }
        /// <summary>The string identifier for the user. </summary>
        public string Owner { get; set; }
        /// <summary>The time it took to run the half mile. </summary>
        [DisplayName("1/2 Mile Time")]
        public TimeSpan HalfMileTime { get; set; }
        /// <summary>The laps completed during the pacer test. </summary>
        [DisplayName("Pacer (Laps)")]
        public int Pacer { get; set; }
        /// <summary>The time it took to run the mile. </summary>
        [DisplayName("Mile Run Time")]
        public TimeSpan MileTime { get; set; }
        /// <summary>The amount of steps taken during the 3 minute step test. </summary>
        [DisplayName("3 Minute Step Test (Steps)")]
        public int StepTestSteps { get; set; }
        /// <summary>The user's heart rate after the 3 minute step test. </summary>
        [DisplayName("Step Test (Heart Rate)")]
        public int StepTestHeartRate { get; set; }
        /// <summary>The time the model was logged. </summary>
        public DateTime Logged { get; set; }
    }

    /// <summary>
    /// Models the data relevant to Muscular Strength and Endurance.
    /// </summary>
    public class MuscularStrengthAndEndurance
    {
        /// <summary>The ID. </summary>
        public int ID { get; set; }
        /// <summary>The string identifier. </summary>
        public string Owner { get; set; }
        /// <summary>The amount of curl ups completed during the 1 minute curl up test. </summary>
        [DisplayName("1 Minute Curl Ups")]
        public int CurlUps { get; set; }
        /// <summary>The amount of right angle push ups completed during the right angle push up test. </summary>
        [DisplayName("Right Angle Push Ups")]
        public int RightAnglePushUps { get; set; }
        /// <summary>The user's max bench in pounds. </summary>
        [DisplayName("Max Bench (lbs)")]
        public int MaxBench { get; set; }
        /// <summary>The user's max leg press in pounds. </summary>
        [DisplayName("Max Leg Press (lbs)")]
        public int MaxLegPress { get; set; }
        /// <summary>The amount of pull ups completed during the pull up test. </summary>
        [DisplayName("Pull Ups")]
        public int PullUps { get; set; }
        /// <summary>The duration of time that the user was able to perform the flexed arm hang in seconds. </summary>
        [DisplayName("Flexed Arm Hang (seconds)")]
        public TimeSpan FlexedArmHang { get; set; }
        /// <summary>The time the model was logged. </summary>
        public DateTime Logged { get; set; }
    }
    #endregion
}