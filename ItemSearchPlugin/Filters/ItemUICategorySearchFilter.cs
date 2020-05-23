﻿using CheapLoc;
using Dalamud.Data;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;
using System.Collections.Generic;
using System.Linq;
using Item = Dalamud.Data.TransientSheet.Item;

namespace ItemSearchPlugin.Filters {
	class ItemUICategorySearchFilter : ISearchFilter {
		public string Name => "Category";
		public string NameLocalizationKey => "DalamudItemSelectCategory";

		public bool IsSet => selectedCategory != 0;

		public bool ShowFilter => true;

		public bool HasChanged {
			get {
				if (lastCategory != selectedCategory){
					lastCategory = selectedCategory;
					return true;
				}
				return false;
			}
		}

		private List<string> uiCategories;
		private string[] uiCategoriesArray;

		private int selectedCategory = 0;
		private int lastCategory = 0;

		public ItemUICategorySearchFilter(DataManager data) {

			uiCategories = new List<string> { Loc.Localize("DalamudItemSelectAll", "All") };

			uiCategories.AddRange(data.GetExcelSheet<ItemUICategory>().GetRows().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => x.Name.Replace("\u0002\u001F\u0001\u0003", "-")));
			uiCategoriesArray = uiCategories.ToArray();
		}


		public bool CheckFilter(Item item) {
			return item.ItemUICategory == selectedCategory;
		}

		public void DrawEditor() {
            ImGui.PushItemWidth(-1);
            ImGui.Combo("##ItemUiCategorySearchFilterBox", ref this.selectedCategory, uiCategoriesArray, uiCategories.Count);
            ImGui.PopItemWidth();
		}

		public void Dispose() {
			
		}
	}
}