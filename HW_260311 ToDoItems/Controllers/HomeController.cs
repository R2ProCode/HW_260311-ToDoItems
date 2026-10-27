using HW_260311_ToDoItems.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW_260311_ToDoItems.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=ToDoItems;Integrated Security=true;TrustServerCertificate=true;";
        public IActionResult Index()
        {
            ToDoItemsDbManager manager = new ToDoItemsDbManager(_connectionString);
            var viewModel = new ActiveViewModel
            {
                Items = manager.GetActiveItems()
            };
            return View(viewModel);
        }

        public IActionResult MarkAsCompleted(int itemId)
        {
            ToDoItemsDbManager manager = new ToDoItemsDbManager(_connectionString);
            manager.MarkAsCompleted(itemId);
            return Redirect("/home/index");
        }

        public IActionResult Completed()
        {
            ToDoItemsDbManager manager = new ToDoItemsDbManager(_connectionString);
            var viewModel = new CompletedViewModel
            {
                Items = manager.GetCompletedItems()
            };
            return View(viewModel);
        }

        public IActionResult Categories()
        {
            ToDoItemsDbManager manager = new ToDoItemsDbManager(_connectionString);
            var viewModel = new CategoriesViewModel
            {
                Categories = manager.GetAllCategories()
            };
            return View(viewModel);
        }

        public IActionResult EditCategory(int categoryId)
        {
            var viewModel = new EditCategoryViewModel
            {
                //CategoryId = categoryId,
                Category = new ToDoItemsDbManager(_connectionString).GetCategoryById(categoryId)
            };
            return View(viewModel);
        }

        public IActionResult NewItem()
        {
            ToDoItemsDbManager manager = new ToDoItemsDbManager(_connectionString);
            var viewModel = new NewItemViewModel
            {
                Categories = manager.GetAllCategories()
            };
            return View(viewModel);
        }

        public IActionResult AddItem(Item item)
        {
            ToDoItemsDbManager manager = new ToDoItemsDbManager(_connectionString);
            manager.AddItem(item);
            return Redirect("/home/index");
        }

        public IActionResult NewCategory()
        {
            return View();
        }

        public IActionResult AddCategory(Category category)
        {
            ToDoItemsDbManager manager = new ToDoItemsDbManager(_connectionString);
            manager.AddCategory(category);
            return Redirect("/home/categories");
        }

        public IActionResult UpdateCategory(Category category)
        {
            ToDoItemsDbManager manager = new ToDoItemsDbManager(_connectionString);
            manager.UpdateCategory(category);
            return Redirect("/home/categories");
        }
    }
}
