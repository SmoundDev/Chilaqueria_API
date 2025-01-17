using Chilaqueria_API.Datos;
using Chilaqueria_API.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using static Chilaqueria_API.Models.Globals;

namespace Chilaqueria_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {


        private readonly ChilaqueriaDBContext _dbContext;
        private ResponseHandler _rh = new ResponseHandler();
        private ExceptionHandler _exh = new ExceptionHandler();
        private bool _disposed = false;
        int _codeRes = 000;
        dynamic _dataRes = null;
        private bool success = false;
        private string msg = string.Empty;

        public ProductsController(ChilaqueriaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: ProductsController/Details/5
        [HttpPost("/ProductDetails")]
        public async Task<GlobalResponse<object>> ProductDetails([FromBody] int Product_id)
        {
            GlobalResponse<object> _oResponse = new GlobalResponse<object>();
            try
            {

            }
            catch
            {
            
            }

            return _oResponse;
        }

        private class _Product
        {
            public decimal Cost { get; set; }
            public string Product_name { get; set; }
            public string Description { get; set; }

        }

        // POST: ProductsController/Create
        [HttpPost("/CreateProduct")]
        //[ValidateAntiForgeryToken]
        public async Task<GlobalResponse<object>> CreateProduct(IFormCollection collection)
        {
            GlobalResponse<object> _oResponse = new GlobalResponse<object>();
            try
            {
             
            }
            catch
            {
            }

            return _oResponse;
        }


        // POST: ProductsController/Edit/5
        [HttpPost("/EditProduct")]
        [ValidateAntiForgeryToken]
        public async Task<GlobalResponse<object>>  EditProduct(int id, IFormCollection collection)
        {
            GlobalResponse<object> _oResponse = new GlobalResponse<object>();

            try
            {
            }
            catch
            {
            }

            return _oResponse;
        }


        // POST: ProductsController/Delete/5
        [HttpPost("/DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
