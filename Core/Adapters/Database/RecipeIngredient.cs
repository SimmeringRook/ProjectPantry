//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Adapters.Database
{
    using System;
    using System.Collections.Generic;
    
    internal partial class RecipeIngredient
    {
        public int RecipeID { get; set; }
        public int InventoryID { get; set; }
        public double Amount { get; set; }
        public string Measurement { get; set; }
    
        internal virtual Inventory Inventory { get; set; }
        internal virtual Metric Metric { get; set; }
        internal virtual Recipe Recipe { get; set; }
    }
}
