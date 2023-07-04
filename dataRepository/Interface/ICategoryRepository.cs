using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;

namespace dataRepository.Interface
{
    public interface ICategoryRepository
    {
        public int addcategory(CategoryVmApi model);
        public List<CategoryInfo> GetCategoriesList();
        public CategoryEditVm GetCategoryById(int id);
        public int EditCategory(CategoryEditVm model);
        public int deleteCategoryById(int id);
        int EditCategory(CategoryEditVmApi model);
        //int addcategory(CategoryVmApi model);
    }
}
