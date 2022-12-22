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

        public async Task<List<FolderDto>> GetFolders(string fullRoute)
        {
            var folders = _context.Folders.Where(x => x.ParentId == null);
            List<FolderDto> folderDto = new List<FolderDto>();
            
            if (fullRoute == null)
            {
                folderDto = await ConvertToDto(folders);
                return folderDto;
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

            folderDto = await ConvertToDto(folders);
            return folderDto;
        }

        private async Task<List<FolderDto>> ConvertToDto(IQueryable<Folder> folders)
        {
            var folderDto = await folders.Select(x =>
                                   new FolderDto()
                                   {
                                       Name = x.Name
                                   }).ToListAsync();
            return folderDto;
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

                var folderModel = new Folder { Name = route[route.Length - 1], FolderType = directoryType, ParentId = parentFolder.Id };

                _context.Folders.Add(folderModel);
                _context.SaveChanges();
            }
        }

        public void ExportFolders(string saveLocation)
        {
            List<Folder> foldersModel = _context.Folders.ToList();
            
            foreach (Folder folder in foldersModel)
            {
                var route = saveLocation + '\\' + GetRoute(folder);
                Directory.CreateDirectory(route);
            }
        }

        private string GetRoute(Folder folder)
        {
            string route = folder.Name;
            
            while(folder.ParentId != null)
            {
                folder = _context.Folders.FirstOrDefault(x => x.Id == folder.ParentId);
                route =  folder.Name + '\\' + route;
            }

            return route;
        }
    }
}
