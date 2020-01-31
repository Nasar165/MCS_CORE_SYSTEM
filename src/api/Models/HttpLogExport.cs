using System;
using api.Models.Error;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Models
{
    public class HttpLogExport : Attribute, IAuthorizationFilter
    {
        private bool ExportLogHttpEnabled()
            => bool.Parse(AppConfigHelper.
                    Instance.GetValueFromAppConfig("AppSettings","ExportLogHttp"));
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!ExportLogHttpEnabled())
                context.Result = new ErrorRespons("Exporting of log files is disabled", 403);
        }
    }
}