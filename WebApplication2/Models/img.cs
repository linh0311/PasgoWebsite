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
    
    public partial class img
    {
        public int imgID { get; set; }
        public string imgLink { get; set; }
        public string imgMoTa { get; set; }
        public Nullable<int> imgType { get; set; }
        public int nhahangID { get; set; }
    
        public virtual PasgoRestaurant PasgoRestaurant { get; set; }
    }
}
