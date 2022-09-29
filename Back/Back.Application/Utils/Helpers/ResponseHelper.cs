using CaseBack.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseBack.Application.Utils
{
    public class ResponseHelper : ControllerBase
    {

        public IActionResult CreateResponse(ResponseModel response) 
        {
            return response.StatusCode switch
            {
                200 => Ok(response),
                500 => StatusCode(500, response),
                422 => UnprocessableEntity(response),
                409 => Conflict(response),
                401 => Unauthorized(response),
                404 => NotFound(response),
                _ => StatusCode(500, response),
            };
        }
    }
}