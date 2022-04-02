using Microsoft.AspNetCore.Http;

namespace SADA.Core.Interfaces;
public interface IBaseHandler
{
    string Upload(IFormFile file, string path);
    void Delete(string path);
}
