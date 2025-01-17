using static Chilaqueria_API.Models.Globals;
using System.Diagnostics;
using System.Collections;

namespace Chilaqueria_API.Handlers
{
    public class ExceptionHandler
    {
        public GlobalResponse<object> MakeExceptionResponse(dynamic _globalData, Exception exception, Stopwatch _stopwatch, int _responseCode)
        {
            GlobalResponse<object> oResponse = new GlobalResponse<object>();

            if (_globalData is IEnumerable enumerable)
            {
                int totalItems = enumerable.Cast<object>().Count();
                oResponse.ResultQnty = totalItems;
            }
            else
            {
                oResponse.ResultQnty = 0;
            }


            Stopwatch stopwatch = _stopwatch;
            stopwatch.Stop();

            oResponse.UserMessage = GetExceptionMessage(exception);
            oResponse.ResponseCode = _responseCode;
            oResponse.GlobalData = _globalData;
            oResponse.ElapsedTime = stopwatch.Elapsed.TotalMilliseconds;

            return oResponse;

        }

        public string GetExceptionMessage(Exception ex)
        {
            string msgEx = string.Empty;

            msgEx = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;

            return msgEx;
        }
    }
}
