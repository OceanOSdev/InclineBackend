using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TodoListWebApp.Models
{
    public class ReactionTime
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        [DisplayName("Tennis Ball Drop (ft)")]
        public int TennisBallDrop { get; set; }
        public DateTime Logged { get; set; }
    }
}