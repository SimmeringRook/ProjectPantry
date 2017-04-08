﻿using Core.Utilities.Database.Queries.Tables;
using Core.Utilities.UnitConversions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Adapters.Factories
{
    internal static class RecipeIngredientFactory
    {
        /// <summary>
        /// Creates a Recipe Ingredient Object for Client_Desktop use from a RecipeIngredient (database) record.
        /// This will return an empty Recipe Ingredient object (0 for numeric fields, "Error" for strings) if
        /// the record could not be found.
        /// </summary>
        internal static Objects.RecipeIngredient Create_Client_From_Database(Database.RecipeIngredient databaseRecipeIngredient)
        {
            

            if (databaseRecipeIngredient != null)
            {
                Objects.RecipeIngredient clientRecipeIngredient = new Objects.RecipeIngredient(
                    databaseRecipeIngredient.RecipeID,
                    InventoryFactory.Create_Client_From_Database(databaseRecipeIngredient.InventoryID),
                    databaseRecipeIngredient.Amount,
                    databaseRecipeIngredient.Measurement
                    );
                //Populate the Client_Desktop version of the Recipe Ingredient Object

                return clientRecipeIngredient;
            }

            //If the Recipe Ingredient record could not be found,
            //Return an empty object
            return new Objects.RecipeIngredient();
        }


        /// <summary>
        /// This is a helper method to simplify the creation of a Client Recipe object.
        /// It will attempt to get all Recipe Ingredient records from the database for the associated recipe,
        /// and then return a list of Client Recipe Ingredients.
        /// </summary>
        internal static List<Objects.RecipeIngredient> GetIngredients_For_ClientRecipe(Objects.Recipe clientRecipe)
        {
            List<Objects.RecipeIngredient> ingredients = new List<Objects.RecipeIngredient>();

            using (HarvestTableUtility harvestTables = new HarvestTableUtility(new RecipeIngredientQuery()))
            {
                List<Database.RecipeIngredient> databaseRecipeIngredients = harvestTables.Get(clientRecipe.ID) as List<Database.RecipeIngredient>;
                databaseRecipeIngredients.ForEach(dbItem => 
                {
                    ingredients.Add(Create_Client_From_Database(dbItem));
                });
            }

            return ingredients;
        }

        internal static Database.RecipeIngredient Create_Database_From_Client(Objects.RecipeIngredient clientRecipeIngredient)
        {
            Database.RecipeIngredient databaseRecipeIngredient = new Database.RecipeIngredient();

            databaseRecipeIngredient.RecipeID = clientRecipeIngredient.RecipeID;
            databaseRecipeIngredient.InventoryID = clientRecipeIngredient.Inventory.ID;
            databaseRecipeIngredient.Amount = clientRecipeIngredient.Amount;
            databaseRecipeIngredient.Measurement = clientRecipeIngredient.Measurement.ToString();

            return databaseRecipeIngredient;
        }

    }
}