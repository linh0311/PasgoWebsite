//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PasgoUser
    {
        public int PasgoID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public bool Gender_ { get; set; }
        public bool PhoneConfrimed { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Salt { get; set; }
        public string PasswordHashed { get; set; }
        public int Level { get; set; }
        public Nullable<System.DateTime> Locked { get; set; }
    }
}
