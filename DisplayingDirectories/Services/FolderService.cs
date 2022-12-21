using DisplayingDirectories.Models;
using Microsoft.EntityFrameworkCore;

namespace DisplayingDirectories.Services
{
    public class FolderService : IFolderService
    {
        private readonly DirectoryDBContext _context;
        public FolderService(DirectoryDBContext context)
        {
            _context = context;
        }

        public async Task<List<Folder>> GetFolders(string fullRoute)
        {
            var folders = _context.Folders.Where(x => x.ParentId == null);
            if (fullRoute == null)
            {
                return await folders.ToListAsync();
            }

            string[] filesNameArray = fullRoute.Split('/');
            foreach(string fileName in filesNameArray)
            {
                var folder = await folders.Include(f => f.Parent).FirstOrDefaultAsync(x => x.Name == fileName);
                if (folder == null)
                {
                    return null;
                }
                var parentId = folder.Id;
                folders = _context.Folders.Where(x => x.ParentId == parentId);
            }

            return await folders.ToListAsync();
        }

        public string GetFolderName(string fullRoute)
        {
            if (fullRoute == null)
            {
                return "Main";
            }

            string[] filesNameArray = fullRoute.Split('/');

            var folderName = filesNameArray[filesNameArray.Length - 1];
            if (folderName == "")
            {
                folderName = filesNameArray[filesNameArray.Length - 2];
            }
            return folderName;
        }
    }
}
