using PrivateBlogCathe.web.Data.Entities;
using PrivateBlogCathe.web.Core;
using PrivateBlogCathe.web.Data;
using PrivateBlogCathe.web.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PrivateBlogCathe.web.Services
{
    public interface ISectionsService
    {
        public Task<Response<Section>> CreateAsync(Section model);
        public Task<Response<List<Section>>> GetListAsync();
        

    }

    public class SectionsService : ISectionsService
    {
        public readonly DataContext _context;
        public SectionsService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Section>> CreateAsync(Section model)
        {
            try
            {
                Section section = new Section
                {
                    Name = model.Name,
                };
                await _context.Sections.AddAsync(section);
                await _context.SaveChangesAsync();
                return ResponseHelper<Section>.MakeResponseSuccess(section, "Sección creada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Section>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<Section>>> GetListAsync()
        {
            try
            {
                List<Section> sections = await _context.Sections.ToListAsync();

                return ResponseHelper<List<Section>>.MakeResponseSuccess(sections);
            }
            catch (Exception ex)
            {
                 return ResponseHelper<List<Section>>.MakeResponseFail(ex);
            }
        }
    }

}
