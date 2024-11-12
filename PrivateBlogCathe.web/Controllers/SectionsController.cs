﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PrivateBlogCathe.web.Core;
using PrivateBlogCathe.web.Data.Entities;
using PrivateBlogCathe.web.Requests;
using PrivateBlogCathe.web.Services;

namespace PrivateBlogCathe.web.Controllers
{
    public class SectionsController : Controller
    { 
        private readonly ISectionsService _sectionsService;
        private readonly INotyfService _notifyService;

        public SectionsController(ISectionsService sectionsService, INotyfService notifyService)
        {
            _sectionsService = sectionsService;
            _notifyService = notifyService;
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
                    _notifyService.Error("Debe ajustar los errores de validacion");
                    return View(section);
                }

                Response<Section> response = await _sectionsService.CreateAsync(section);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(response);
            }
            catch (Exception ex)
            {
                return View(section);
            }

        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<Section> response = await _sectionsService.GetOneAsync(id);
            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            _notifyService.Error(response.Message);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Section section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validacion");
                    return View(section);
                }

                Response<Section> response = await _sectionsService.EditAsync(section);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(response);
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
                return View(section);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Response<Section> response = await _sectionsService.DeleteAsync(id);
            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
               
            }
            else
            {
                _notifyService.Error(response.Message);
            }
                      
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Toggle( int SectionId, bool Hide)
        {
            ToggleSectionStatusRequest request = new ToggleSectionStatusRequest
            {
                Hide = Hide,
                SectionId = SectionId
            };

            Response<Section> response = await _sectionsService.ToggleAsync(request);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);

            }
            else
            {
                _notifyService.Error(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
