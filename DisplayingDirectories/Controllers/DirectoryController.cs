using DisplayingDirectories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisplayingDirectories.Services;
using System.Web;

namespace DisplayingDirectories.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly IFolderService _folderService;

        public DirectoryController(DirectoryDBContext context, IFolderService folderService)
        {
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

        [Route("/Directory/Import")]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [Route("/Directory/Import")]
        public void Import(string route)
        {
            _folderService.ImportFolders(route);
        }

        [Route("/Directory/Export")]
        public IActionResult Export()
        {
            return View();
        }

        [HttpPost]
        [Route("/Directory/Export")]
        public void Export(string route)
        {
            _folderService.ExportFolders(route);
        }
    }
}
