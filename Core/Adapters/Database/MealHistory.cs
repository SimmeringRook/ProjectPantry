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
    
    internal partial class MealHistory
    {
        public string MealName { get; set; }
        public int RecipeID { get; set; }
        public System.DateTime DateEaten { get; set; }
    
        internal virtual MealTime MealTime { get; set; }
        internal virtual Recipe Recipe { get; set; }
    }
}
