using Microsoft.AspNetCore.Http;
using SADA.Core.Interfaces;

namespace SADA.Service;
public class URLHelper : IURLHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public URLHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Url(string path)
    {
        return $"https://{_httpContextAccessor.HttpContext.Request.Host.ToString()}/{path}";
    }
}
