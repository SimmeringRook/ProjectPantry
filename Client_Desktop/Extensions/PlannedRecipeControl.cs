﻿using Core.Adapters;
using Core.Adapters.Objects;
using Core.Utilities.UnitConversions;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Client_Desktop.Extensions
{
    public class PlannedRecipeControl : FlowLayoutPanel
    {
        public PlannedMeal PlannedMeal;
        //Controls
        public RecipeButton RecipeButton;
        private Button _deleteButton;
        private Button _ateButton;

        public PlannedRecipeControl(PlannedMeal plannedRecipe)
        {
            this.MouseDown += PlannedRecipeControl_MouseDown;

            this.PlannedMeal = plannedRecipe;

            this.Margin = new Padding(5, 2, 0, 2);
            this.AutoSize = true;
            this.BorderStyle = BorderStyle.FixedSingle;
            
            RecipeButton = new RecipeButton(PlannedMeal.PlannedRecipe);
            this.Controls.Add(RecipeButton);

            _deleteButton = CreateButtonTemplate("X", deleteButton_Click);
            this.Controls.Add(_deleteButton);

            _ateButton = CreateButtonTemplate("A", ateButton_Click);
            this.Controls.Add(_ateButton);

            if (plannedRecipe.HasBeenEaten)
                SetControlsForHasBeenEaten();
        }

        #region Drag Drop
        private void PlannedRecipeControl_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(sender, DragDropEffects.Move);
        }

        public void UpdatePlan(DateTime dateTime, string mealTime)
        {
            RemovePlanFromWeek(this.PlannedMeal);

            this.PlannedMeal = null;
            this.PlannedMeal = new PlannedMeal() { Date = dateTime, HasBeenEaten = false, MealTime = mealTime, PlannedRecipe = this.RecipeButton.Recipe };
            HarvestAdapter.PlannedMeals.Add(this.PlannedMeal);
        }
        #endregion

        private void RemovePlanFromWeek(PlannedMeal planToRemove)
        {
            foreach (var plannedDay in HarvestAdapter.CurrentWeek.DaysOfWeek)
                if (plannedDay.Day.Equals(PlannedMeal.Date))
                    HarvestAdapter.PlannedMeals.Remove(planToRemove);
        }

        private void SetControlsForHasBeenEaten()
        {
            RecipeButton.Enabled = false;
            _ateButton.Visible = false;
        }

        private Button CreateButtonTemplate(string text, EventHandler eventHandler)
        {
            Button template = new Button();
            template.Text = text;
            template.Size = new System.Drawing.Size(20, RecipeButton.Height);
            template.Click += new EventHandler(eventHandler);
            return template;
        }

        #region Button Click Events
        private void deleteButton_Click(object sender, EventArgs e)
        {
            RemovePlanFromWeek(this.PlannedMeal);

            foreach (Control control in this.Controls)
                control.Dispose();

            this.Controls.Clear();
            this.Parent.Controls.Remove(this);
        }

        private void ateButton_Click(object sender, EventArgs e)
        {
            PlannedMeal.HasBeenEaten = true;
            try
            {
                foreach (RecipeIngredient ingredient in RecipeButton.Recipe.AssociatedIngredients)
                    using (HarvestConverter conversion = new HarvestConverter(new VolumeUnitConversion()))
                    {
                        if (conversion.IsCorrectMeasurementType(ingredient.Measurement) == false)
                            conversion.ConversionType = new WeightUnitConversion();
                        HarvestAdapter.InventoryItems.Single(item => item.Equals(ingredient.Inventory)).Amount -= conversion.Convert(new ConvertedIngredient(ingredient), ingredient.Inventory.Measurement).Amount;
                    }
            }
            catch (InvalidConversionException ex)
            {
                MessageBox.Show(ex.Message);
            }

            SetControlsForHasBeenEaten();
        }
        #endregion
    }
}
