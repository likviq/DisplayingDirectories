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

        public void ImportFolders(string fileLocation)
        {
            string[] folders = Directory.GetDirectories(fileLocation, "*", SearchOption.AllDirectories);
            var firstFile = folders[0].Split(@"\");
            var parentDirectoryName = firstFile[firstFile.Length - 2];

            FolderType directoryType = FolderType.Directory;
            Folder parentFolder = new Folder { Name = parentDirectoryName, FolderType = directoryType, ParentId = null };
            _context.Folders.Add(parentFolder);
            _context.SaveChanges();

            foreach(var folder in folders)
            {
                var route = folder.Split(@"\");
                var parentName = route[route.Length - 2];
                parentFolder = _context.Folders.FirstOrDefault(x => x.Name == parentName);

                var folderModel = new Folder { Name = route[route.Length - 1], FolderType = directoryType, ParentId = null };

                _context.Folders.Add(folderModel);
                _context.SaveChanges();
            }
        }
    }
}
