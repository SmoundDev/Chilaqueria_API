using System.Diagnostics;
using System.Collections;
using static Chilaqueria_API.Models.Globals;

namespace Chilaqueria_API.Handlers
{

    public class ResponseHandler
    {
        public GlobalResponse<object> MakeGlobalResponse( dynamic? _globalData, string _userMsg, Stopwatch _stopwatch, int _responseCode) {
            GlobalResponse<object> oResponse = new GlobalResponse<object>();

            if(_globalData is IEnumerable enumerable)
            {
                int totalItems = enumerable.Cast<object>().Count();
                oResponse.ResultQnty = totalItems;
            }
            else
            {
                oResponse.ResultQnty = 1;
            }


            Stopwatch stopwatch = _stopwatch;
            stopwatch.Stop();

            oResponse.UserMessage = _userMsg;
            oResponse.ResponseCode = _responseCode;
            oResponse.GlobalData = _globalData;
            oResponse.ElapsedTime = stopwatch.Elapsed.TotalMilliseconds;

            return oResponse;

        }

    }
}
