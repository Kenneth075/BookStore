using BookStore.Model;
using BookStoreWebApp.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStoreWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productlist = _productRepository.GetAll(includeProperties:"Category");
            return View(productlist);
        } 
        
        public IActionResult Details(int productId)
        {
            Product productDetail = _productRepository.Get(u=>u.Id==productId,includeProperties:"Category");
            return View(productDetail);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}