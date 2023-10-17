using BookStore.Model;
using BookStore.Model.ViewModels;
using BookStoreWebApp.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            List<Product> objProduct = _productRepo.GetAll().ToList();
          
            return View(objProduct);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categorylist = _categoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });

            ProductViewModel productVM = new ProductViewModel()
            {
                CategoryList = categorylist,
                Product = new Product()

            };
            //ViewBag.CategoryList = categorylist;
            //ViewData["CategoryList"] = categorylist;
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _productRepo.Add(productViewModel.Product);
                _productRepo.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<SelectListItem> categorylist = _categoryRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                productViewModel.CategoryList = categorylist;
                return View(productViewModel);
            }
            

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb = _productRepo.Get(u => u.Id == id);
            //Category categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _productRepo.Update(obj);
                _productRepo.Save();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _productRepo.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product product = _productRepo.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _productRepo.Remove(product);
            _productRepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }



    }
}
