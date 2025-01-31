using Microsoft.EntityFrameworkCore;
using static Chilaqueria_API.Models.Globals;
using System.Diagnostics;
using Chilaqueria_API.Datos;
using Chilaqueria_API.Handlers;
using Chilaqueria_API.Repositories;

namespace Chilaqueria_API.Controllers.BussinessLogic
{
    public class AccountLogic
    {
        private readonly ChilaqueriaDBContext _dbContext;
        private ResponseHandler _rh = new ResponseHandler();
        private ExceptionHandler _exh = new ExceptionHandler();
        private bool _disposed = false;
        int _codeRes = 000;
        dynamic _dataRes = null;
        private bool success = false;
        private string msg = string.Empty;

        private readonly AccountService _accountService;


        public  GlobalResponse<object> _GetAllActiveUsers(ChilaqueriaDBContext _db)
        {
            GlobalResponse<object> _oResponse = new GlobalResponse<object>();

            Stopwatch _stopwatch = Stopwatch.StartNew();
            try
            {
                var data = _db.Prod_Users.Where(x => x.User_active).ToList();
                //var data = _accountService.GetAllUsersAsync();
                msg = "Aquí están los usuarios activos";
                _codeRes = 200;
                _oResponse = _rh.MakeGlobalResponse(data, msg, _stopwatch, _codeRes);
            }
            catch (Exception ex)
            {
                _codeRes = 400;
                _oResponse = _exh.MakeExceptionResponse(null, ex, _stopwatch, _codeRes);
            }

            return _oResponse;
        }

    }
}
