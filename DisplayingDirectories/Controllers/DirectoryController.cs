using DisplayingDirectories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisplayingDirectories.Services;

namespace DisplayingDirectories.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly DirectoryDBContext _context;
        private readonly IFolderService _folderService;

        public DirectoryController(DirectoryDBContext context, IFolderService folderService)
        {
            _context = context;
            _folderService = folderService;
        }

        [Route("/{*filesName}")]
        public async Task<IActionResult> Index(string filesName)
        {
            var folderName = _folderService.GetFolderName(filesName);
            ViewBag.Filename = folderName;
            var folders = await _folderService.GetFolders(filesName);
            return View(folders);
        }
    }
}
