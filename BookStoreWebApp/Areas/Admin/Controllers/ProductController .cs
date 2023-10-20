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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo, IWebHostEnvironment webHostEnvironment)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProduct = _productRepo.GetAll(includeProperties:"Category").ToList();
          
            return View(objProduct);
        }

        public IActionResult Upsert(int?id)
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
            if(id == null || id == 0)
            {
                //Create
                return View(productVM);
            }
            else
            {
                //Edit
                productVM.Product = _productRepo.Get(u => u.Id == id);
                return View(productVM);
            }


            //ViewBag.CategoryList = categorylist;
            //ViewData["CategoryList"] = categorylist;
            
        }


        [HttpPost]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                    {
                        //Delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productViewModel.Product.ImageUrl = @"\images\product\" + fileName;
                }


                if (productViewModel.Product.Id == 0)
                {
                    _productRepo.Add(productViewModel.Product);
                }
                else
                {
                    _productRepo.Update(productViewModel.Product);
                }

                
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


        //public IActionResult Create()
        //{
        //    IEnumerable<SelectListItem> categorylist = _categoryRepo.GetAll().Select(u => new SelectListItem
        //    {
        //        Text = u.Name,
        //        Value = u.Id.ToString(),
        //    });

        //    ProductViewModel productVM = new ProductViewModel()
        //    {
        //        CategoryList = categorylist,
        //        Product = new Product()

        //    };
        //    //ViewBag.CategoryList = categorylist;
        //    //ViewData["CategoryList"] = categorylist;
        //    return View(productVM);
        //}

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product productFromDb = _productRepo.Get(u => u.Id == id);
        //    //Category categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}


        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _productRepo.Update(obj);
        //        _productRepo.Save();
        //        TempData["success"] = "Category Edited Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View("Index");
        //}


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

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProduct = _productRepo.GetAll(includeProperties: "Category").ToList();
            return Json(new { date = objProduct });
        }
        #endregion

    }
}
