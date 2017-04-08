﻿using Core.Adapters.Database;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Core.Utilities.Database.Queries.Tables
{
    internal class RecipeIngredientQuery : IHarvestQuery
    {
        public object Get(object itemID, HarvestDatabaseEntities HarvestDatabase)
        {
            HarvestDatabase.RecipeIngredient.Load();
            if ((int)itemID == -1)
                return HarvestDatabase.RecipeIngredient.ToList();
            return HarvestDatabase.RecipeIngredient.Where(inventory => inventory.RecipeID.Equals((int)itemID)).ToList();
        }

        public void Insert(object itemToAdd, HarvestDatabaseEntities HarvestDatabase)
        {
            HarvestDatabase.RecipeIngredient.Load();
            HarvestDatabase.RecipeIngredient.Add(itemToAdd as RecipeIngredient);
            HarvestDatabase.SaveChanges();
        }

        public void Remove(object itemToRemove, HarvestDatabaseEntities HarvestDatabase)
        {
            HarvestDatabase.RecipeIngredient.Load();
            RecipeIngredient recipeIngredient = itemToRemove as RecipeIngredient;
            RecipeIngredient ingredientToDelete = HarvestDatabase.RecipeIngredient.Single( ri => 
                ri.RecipeID == recipeIngredient.RecipeID &&
                ri.InventoryID == recipeIngredient.InventoryID
                );
            HarvestDatabase.RecipeIngredient.Remove(ingredientToDelete);
            HarvestDatabase.SaveChanges();
        }

        public void Update(object itemToChange, HarvestDatabaseEntities HarvestDatabase)
        {
            HarvestDatabase.RecipeIngredient.Load();
            HarvestDatabase.RecipeIngredient.AddOrUpdate(itemToChange as RecipeIngredient);
            HarvestDatabase.SaveChanges();
        }
    }
}
