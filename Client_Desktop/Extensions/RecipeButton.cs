﻿using Core.Adapters.Objects;
using System;
using System.Windows.Forms;

namespace Client_Desktop.Extensions
{
    public class RecipeButton : Button
    {
        public Recipe Recipe { get; private set; }

        public RecipeButton(Recipe recipe)
        {
            this.Recipe = recipe;
            this.Anchor = AnchorStyles.None;
            this.Text = recipe.Name;
            this.Tag = "Recipe";
            this.AutoSize = true;
            this.Click += new System.EventHandler(ShowRecipe_Click);
        }

        public void ShowRecipe_Click(object sender, EventArgs e)
        {
            using (RecipeForm recipe = new RecipeForm((sender as RecipeButton).Recipe))
            {
                if (recipe.ShowDialog() == DialogResult.OK)
                    return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.Recipe = null;
            base.Dispose(disposing);
        }
    }
}
