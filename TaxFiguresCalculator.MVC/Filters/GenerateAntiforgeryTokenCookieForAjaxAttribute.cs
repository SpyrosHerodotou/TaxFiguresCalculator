﻿using Microsoft.AspNetCore.Antiforgery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxFiguresCalculator.MVC.Filters
{
    //public class GenerateAntiforgeryTokenCookieForAjaxAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuted(ActionExecutedContext context)
    //    {
    //        var antiforgery = context.HttpContext.RequestServices.GetService<IAntiforgery>();

    //        // We can send the request token as a JavaScript-readable cookie, 
    //        // and Angular will use it by default.
    //        var tokens = antiforgery.GetAndStoreTokens(context.HttpContext);
    //        context.HttpContext.Response.Cookies.Append(
    //            "XSRF-TOKEN",
    //            tokens.RequestToken,
    //            new CookieOptions() { HttpOnly = false });
    //    }
}
