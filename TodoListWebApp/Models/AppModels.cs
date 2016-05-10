﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoListWebApp.Models
{
    /// <summary>
    /// The base class that contains all generic model
    /// information.
    /// </summary>
    public class BaseModel
    {
        /// <summary>The Identifier.</summary>
        public int ID { get; set; }
        /// <summary>The string that identifies the owner.</summary>
        public string Owner { get; set; }
        /// <summary>The date that the data was logged.</summary>
        public DateTime Logged { get; set; }
    }

    // Entity class for todo entries
    public class Todo
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
    }

    

    // Entity for keeping track of organizations onboarded as customers of the app
    public class Tenant
    {
        public int ID { get; set; }
        public string IssValue { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        [DisplayName("Check this if you are an administrator and you want to enable the app for all your users")]
        public bool AdminConsented { get; set; }
    }

    // Entity for keeping track of individual users onboarded as customers of the app
    public class User
    {
        [Key]
        public string UPN { get; set; }
        public string TenantID { get; set; }
    }
}