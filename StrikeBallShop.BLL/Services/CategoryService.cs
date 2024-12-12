using StrikeBallShop.DAL.Repositories;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeBallShop.BLL.Services
{
    public class CategoryService
    {
        private CategoryRepository categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public Category GetCategoryById(int id)
        {
            return categoryRepository.GetCategoryById(id);
        }

        public void CreateCategory(Category category)
        {
            categoryRepository.CreateCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            categoryRepository.UpdateCategory(category);
        }

        public void DeleteCategory(int id)
        {
            categoryRepository.DeleteCategory(id);
        }
        public List<Category> GetAllCategories()
        {
            return categoryRepository.GetAllCategories();
        }
    }
}
