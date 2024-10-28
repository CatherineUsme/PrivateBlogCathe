using Microsoft.AspNetCore.Mvc;
using PrivateBlogCathe.web.Core;
using PrivateBlogCathe.web.Data.Entities;
using PrivateBlogCathe.web.Services;

namespace PrivateBlogCathe.web.Controllers
{
    public class SectionsController : Controller
    { 
        private readonly ISectionsService _sectionsService;

        public SectionsController(ISectionsService sectionsService)
        {
            _sectionsService = sectionsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<Section>> response = await _sectionsService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Section section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(section);
                }

                Response<Section> response = await _sectionsService.CreateAsync(section);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                //TODO: Mostrar mensaje de error
                return View(response);
            }
            catch (Exception ex)
            {
                return View(section);
            }

        }
    }
}
