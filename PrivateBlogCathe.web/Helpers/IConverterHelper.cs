using PrivateBlogCathe.web.Data.Entities;
using PrivateBlogCathe.web.DTOs;

namespace PrivateBlogCathe.web.Helpers
{

    public interface IConverterHelper
    {
        public Blog ToBlog(BlogDTO dto);
        public Task<BlogDTO> ToBlogDTO(Blog result);
    }
    public class ConverterHelper : IConverterHelper
    {
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(ICombosHelper combosHelper)
        {
            _combosHelper = combosHelper;
        }

        public Blog ToBlog(BlogDTO dto)
        {
            return new Blog
            {
                Id = dto.Id,
                Title = dto.Title,
                Content = dto.Content,
                IsPublished = dto.IsPublished,
                SectionId = dto.SectionId,
            };
        }

        public async Task<BlogDTO> ToBlogDTO(Blog blog)
        {
            return new BlogDTO
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                IsPublished = blog.IsPublished,
                SectionId = blog.SectionId,
                Sections= await _combosHelper.GetComboSections()

            };
        }
    }
}
