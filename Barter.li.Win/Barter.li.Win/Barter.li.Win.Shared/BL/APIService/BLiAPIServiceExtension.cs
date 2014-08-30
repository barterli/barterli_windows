using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Barter.li.Win.BL.APIServices
{
    public enum ResponseAction
    {
        SUCCESS,
        RETRY,
        STOP,
        BAD_REQUEST,
    }

    public partial class BliAPIService
    {
        /* Under dev */

        private ResponseAction GetStatus(HttpStatusCode statusCode, bool isRetrying)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                return ResponseAction.SUCCESS;
            }
            else if (statusCode == HttpStatusCode.NoContent)
            {
                if (isRetrying)
                    return ResponseAction.RETRY;
            }
            else if (statusCode == HttpStatusCode.NotFound)
            {
                if (isRetrying)
                    return ResponseAction.RETRY;
            }
            return ResponseAction.STOP;
        }
    }
}
