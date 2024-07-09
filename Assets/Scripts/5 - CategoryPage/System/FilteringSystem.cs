using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RequestsUtil;

namespace FilteringSystem{
    public class CategoryFilter
    {
        public string Id { get; private set; }
        public string CategoryName { get; private set; }
        public string SubcategoryName { get; private set; }
        public bool IsSelected;

        public CategoryFilter(string id, string categoryName, string subcategoryName, bool isSelected)
        {
            Id = id;
            CategoryName = categoryName;
            SubcategoryName = subcategoryName;
            IsSelected = isSelected;
        }
    }

    public class CategoryFilterManager
    {
        public List<CategoryFilter> CategoryFilters { get; private set; }
        public HashSet<string> Categories { get; private set; }

        public CategoryFilterManager(FilterIdData categoryData)
        {
            CategoryFilters = GetCategoryFilter(categoryData);
            Categories = GetCategories(categoryData);
        }

        public List<CategoryFilter> GetCategoryFilter(FilterIdData CategoryData)
        {
            return CategoryData.data.Select(x => new CategoryFilter(x[0], x[1], x[2], false)).OrderBy(x => x.SubcategoryName).ToList();
        }

        HashSet<string> GetCategories(FilterIdData categoryData)
        {
            return categoryData.data.Select(x => x[1]).Distinct().OrderBy(x => x).ToHashSet();
        }

        public string[] GetFilters(List<CategoryFilter> subCategories)
        {
            List<string> Filters = new List<string>();

            List<string> GetFiltersByCategory(string name)
            {
                var selectedSubCats = subCategories.Where(x => x.CategoryName.Equals(name) && x.IsSelected).Select(x => x.Id).ToList();
                if (selectedSubCats.Count == subCategories.Count(x => x.CategoryName.Equals(name)))
                {
                    return new List<string>{name};
                }
                return selectedSubCats;
            }

            foreach (var item in Categories)
            {
                Filters.AddRange(GetFiltersByCategory(item));
            }

            return Filters.ToArray();
        }
    }
}
