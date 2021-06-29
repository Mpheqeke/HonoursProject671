using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Project.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class ValidationErrorCollection
{
	public string Key { get; set; }
	public string ErrorMessage { get; set; }
}

public class ApiExceptionFilter : ExceptionFilterAttribute
{
	public override void OnException(ExceptionContext context)
	{
		ApiError apiError = null;
		if (context.Exception is ValidationException)
		{
			apiError = new ApiError(context.Exception.Message);
			context.HttpContext.Response.StatusCode = 405;
		}
		else if (context.Exception is UnauthorizedAccessException)
		{
			apiError = new ApiError("Unauthorized Access");
			context.HttpContext.Response.StatusCode = 401;

			// handle logging here
		}
        else if (context.Exception is AccessTokenExpiredException)
        {
            apiError = new ApiError("Oops! Session Expired. Please log in again.");
            context.HttpContext.Response.StatusCode = 498;
        }
        else
		{
			// Unhandled errors

			var msg = context.Exception.GetBaseException().Message;
			string stack = context.Exception.StackTrace;


			apiError = new ApiError(msg);
			apiError.detail = stack;

			context.HttpContext.Response.StatusCode = 500;

			// handle logging here
		}

		// always return a JSON result
		context.Result = new JsonResult(apiError);

		base.OnException(context);
	}
}
public class ApiException : Exception
{
	public int StatusCode { get; set; }

	public ValidationErrorCollection Errors { get; set; }

	public ApiException(string message,
		int statusCode = 500,
		ValidationErrorCollection errors = null) :
		base(message)
	{
		StatusCode = statusCode;
		Errors = errors;
	}
	public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
	{
		StatusCode = statusCode;
	}
}

public class ApiError
{
	public string message { get; set; }
	public bool isError { get; set; }
	public string detail { get; set; }
	public ValidationErrorCollection errors { get; set; }

	public ApiError(string message)
	{
		this.message = message;
		isError = true;
	}

	public ApiError(ModelStateDictionary modelState)
	{
		this.isError = true;
		if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
		{
			message = "Please correct the specified errors and try again.";
			//errors = modelState.SelectMany(m => m.Value.Errors.Select( me => new KeyValuePair<string,string>( m.Key,me.ErrorMessage) ));
			//errors = modelState.SelectMany(m => m.Value.Errors.Select(me => new ModelError { FieldName = m.Key, ErrorMessage = me.ErrorMessage }));
		}
	}
}