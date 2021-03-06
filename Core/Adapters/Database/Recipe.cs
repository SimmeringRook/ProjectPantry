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
    
    internal partial class Recipe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Recipe()
        {
            this.MealHistory = new HashSet<MealHistory>();
            this.PlannedMeals = new HashSet<PlannedMeals>();
            this.RecipeIngredient = new HashSet<RecipeIngredient>();
        }
    
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public double Servings { get; set; }
        public string RCategory { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<MealHistory> MealHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<PlannedMeals> PlannedMeals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }
        internal virtual RecipeClass RecipeClass { get; set; }
    }
}
