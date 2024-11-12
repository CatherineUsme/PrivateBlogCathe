using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrivateBlogCathe.web.Data;

namespace PrivateBlogCathe.web.Helpers
{
    public interface ICombosHelper
    {
        public Task<IEnumerable<SelectListItem>> GetComboSections();
    }

    public class CombosHelper : ICombosHelper

    {
        private readonly DataContext _context;
        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboSections()
        {
            List<SelectListItem> list = await _context.Sections.Select(s => new SelectListItem
            {
                Text=s.Name,
                Value=s.Id.ToString(),


            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text="[Selecctione una seccion]",
                Value="0"
            });
            return list;
        }
    }
}
