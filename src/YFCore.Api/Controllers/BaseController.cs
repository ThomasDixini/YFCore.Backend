using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YFCore.Api.Controllers
{
    [ApiController]
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult<ApiResponse<T>> OkResponse<T>(T data, string? message = null)
            => Ok(new ApiResponse<T>(true, data, message));

        protected ActionResult<ApiResponse<T>> CreatedResponse<T>(string actionName, object? routeValues, T data, string? message = null)
        {
            var routeValuesDictionary = new Microsoft.AspNetCore.Routing.RouteValueDictionary(routeValues);
            return CreatedAtAction(actionName, routeValuesDictionary, new ApiResponse<T>(true, data, message));
        }
        protected ActionResult<ApiResponse<object?>> BadRequestResponse(string? message = null, IEnumerable<string>? errors = null)
            => BadRequest(new ApiResponse<object?>(false, null, message, errors));

        protected ActionResult<ApiResponse<object?>> NotFoundResponse(string? message = null)
            => NotFound(new ApiResponse<object?>(false, null, message));

        protected ActionResult<ApiResponse<T>> ErrorResponse<T>(int statusCode, string message, IEnumerable<string>? errors = null)
            => StatusCode(statusCode, new ApiResponse<T>(false, default, message, errors));
    }

    public sealed class ApiResponse<T>
    {
        public ApiResponse(bool success, T? data = default, string? message = null, IEnumerable<string>? errors = null)
        {
            Success = success;
            Data = data;
            Message = message;
            Errors = errors;
        }

        public bool Success { get; }
        public T? Data { get; }
        public string? Message { get; }
        public IEnumerable<string>? Errors { get; }
    }
}
