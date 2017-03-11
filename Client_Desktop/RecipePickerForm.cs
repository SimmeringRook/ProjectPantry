﻿using Core;
using Core.DatabaseUtilities;
using Core.DatabaseUtilities.BindingListQueries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Desktop
{
    public partial class RecipePickerForm : Form
    {
        public Recipe SelectedRecipe { get; private set; }
        public RecipePickerForm()
        {
            InitializeComponent();

            using (HarvestBindingListUtility harvestBindingList = new HarvestBindingListUtility(new RecipeBindingListQuery()))
                RecipeGridView.DataSource = harvestBindingList.GetBindingList() as BindingList<Recipe>;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
                SelectedRecipe = (Recipe) RecipeGridView.Rows[selectedIndex].DataBoundItem;
            this.DialogResult = DialogResult.OK;
        }

        #region Filter
        private void updateButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private int selectedIndex = -1;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            selectedIndex = RecipeGridView.CurrentCell.RowIndex;
        }
    }
}
