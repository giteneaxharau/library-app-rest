using System.Net;

namespace library_app_rest.Models;

public class Response
{
    public Response()
    {
        ErrorMessages = new List<string>();
    }

    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; } = true;
    public List<string> ErrorMessages { get; set; }
    public object Result { get; set; }
}