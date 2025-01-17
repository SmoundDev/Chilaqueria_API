using Chilaqueria_API.Controllers.BussinessLogic;
using Chilaqueria_API.Datos;
using Chilaqueria_API.Handlers;
using Chilaqueria_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Chilaqueria_API.Models.Globals;    

namespace Chilaqueria_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly ChilaqueriaDBContext _dbContext;
        private ResponseHandler _rh = new ResponseHandler();
        private ExceptionHandler _exh = new ExceptionHandler();
        private bool _disposed = false;
        int _codeRes = 000;
        dynamic _dataRes = null;
        private bool success = false;
        private string msg = string.Empty;

        AccountLogic _ac ;

        public AccountController(ChilaqueriaDBContext dbContext, AccountLogic accountLogic)
        {
            _dbContext = dbContext;
            _ac = accountLogic;
        }


        [HttpGet("/GetAllActiveUsers")]
        public async Task<GlobalResponse<object>> GetAllActiveUsers()
        {
            GlobalResponse<object> _oResponse = new GlobalResponse<object>();

            Stopwatch _stopwatch = Stopwatch.StartNew();
            try
            {
                _oResponse = _ac._GetAllActiveUsers(_dbContext);
               
            }
            catch (Exception ex)
            {
                _codeRes = 400;
                _oResponse = _exh.MakeExceptionResponse(null, ex, _stopwatch, _codeRes);
            }

            return _oResponse;
        }

        [HttpPost("/LoginIn")]
        public async Task<GlobalResponse<object>> LoginIn([FromBody]  string username, string pass)
        {
            GlobalResponse<object> _oResponse = new GlobalResponse<object>();
            Stopwatch _stopwatch = Stopwatch.StartNew();

            try
            {
                var data = _dbContext.Prod_Users.Where(x => x.User_active && x.User_username == username).ToList();

                if (!data.Any())
                {
                    msg = "No se encontró el usuario ingresado";
                    _codeRes = 404;
                }
                else
                {
                    string _pass = data[0].User_pass;
                    var passed = ValidatePass(_pass, pass);
                    if (!passed)
                    {
                        msg = "El usuario y/o la contraseña ingresados son incorrectos";
                        _codeRes = 501;
                    }
                    else
                    {
                        msg = "Se ha logueado correctamente";
                        _codeRes = 200;
                        _dataRes = data;
                    }

                }

                _oResponse = _rh.MakeGlobalResponse(_dataRes, msg, _stopwatch, 404);
            }
            catch (Exception ex)
            {
                _oResponse = _exh.MakeExceptionResponse(null, ex, _stopwatch, 400);
            }

            return _oResponse;
        }
    public static bool ValidatePass(string passDb, string pass)
        {
            bool correct = false;

            correct = true;
            return correct;
        }
    }
}
