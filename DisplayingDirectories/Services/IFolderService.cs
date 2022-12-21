using DisplayingDirectories.Models;

namespace DisplayingDirectories.Services
{
    public interface IFolderService
    {
        Task<List<Folder>> GetFolders(string fullRoute);
        string GetFolderName(string fullRoute);
        void ImportFolders(string fileLocation);
        void ExportFolders(string saveLocation);
    }
}
