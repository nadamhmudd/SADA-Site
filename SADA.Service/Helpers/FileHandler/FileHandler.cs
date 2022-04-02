using SADA.Core.Interfaces;
using SADA.Services.Helpers.FilesHnadler;

namespace SADA.Services.Helpers.FilesHnadler
{
    public class FileHandler : IFileHandler
    {

        public FileHandler()
        {
            //Initialize 
            Image = new ImageHandler();
        }
        public IBaseHandler Image { get; private set; }
    }
}
