using Project.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Web;
using Project.Core.Interfaces;
using Project.Web.Controllers;
using Project.Core.Exceptions;

namespace Project.Web.Filters
{
    public class AuthorizationFilterAttribute : ActionFilterAttribute
    {
        private string _permission;

        public AuthorizationFilterAttribute(string permission)
        {
            _permission = permission;
        }

        public AuthorizationFilterAttribute()
        {
           
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            var idToken = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToLower() == "idtoken");
            var displayname = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToLower() == "displayname");
            if (idToken.Key.IsNullOrEmpty() )
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        //    var accountInfo = _userService.GetAccountInfo((idToken.Value).Single(),2);
        //    if (accountInfo != null)
        //    {
        //        if (!(accountInfo.Permissions.Any(x => x == _permission) || _permission.IsNullOrEmpty()))
        //        {
        //            context.Result = new UnauthorizedResult();
        //            return;
        //        }

        //        ((IAuthInfo)context.HttpContext.RequestServices.GetService(typeof(IAuthInfo))).Permissions = accountInfo.Permissions;
        //        ((IAuthInfo)context.HttpContext.RequestServices.GetService(typeof(IAuthInfo))).DisplayName = accountInfo.DisplayName;
        //        ((IAuthInfo)context.HttpContext.RequestServices.GetService(typeof(IAuthInfo))).UserId = accountInfo.UserId;
        //    }
        //    else
        //    {
        //        throw new AccessTokenExpiredException();
        //    }
        }
    }
}
