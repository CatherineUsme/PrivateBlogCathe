﻿using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using PrivateBlogCathe.web.Core;
using PrivateBlogCathe.web.Data;
using PrivateBlogCathe.web.Data.Entities;
using PrivateBlogCathe.web.DTOs;
using PrivateBlogCathe.web.Helpers;

namespace PrivateBlogCathe.web.Services
{
    public interface IBlogsService
    {

        public Task<Response<Blog>> CreateAsync(BlogDTO dto);
        public Task<Response<Blog>> EditAsync(BlogDTO dto);
        public Task<Response<List<Blog>>> GetListAsync();
        public Task<Response<Blog>> GetOneAsync(int id);




    }

    public class BlogsService : IBlogsService
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public BlogsService(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        public async Task<Response<Blog>> CreateAsync(BlogDTO dto)
        {
            try
            {
                Blog blog= _converterHelper.ToBlog(dto);

                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();

                return ResponseHelper<Blog>.MakeResponseSuccess(blog, "Blog creado con exito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Blog>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Blog>> EditAsync(BlogDTO dto)
        {
            try
            {
                Blog? blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == dto.Id);

                if (blog is null)
                {
                    return ResponseHelper<Blog>.MakeResponseFail($"No existe Blog con id '{dto.Id}");
                }

                blog.Title = dto.Title;
                blog.Content = dto.Content;
                blog.SectionId = dto.SectionId;

                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();

                return ResponseHelper<Blog>.MakeResponseSuccess(blog, "Blog actualizado con exito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Blog>.MakeResponseFail(ex);
            }
        }
        public async Task<Response<List<Blog>>> GetListAsync()
        {
            try
            {
                List<Blog> list = await _context.Blogs.Include(b =>b.Section).ToListAsync();

                return ResponseHelper<List<Blog>>.MakeResponseSuccess(list);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<Blog>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Blog>> GetOneAsync(int id)
        {
            try
            {
                Blog? blog = await _context.Blogs.FirstOrDefaultAsync(s => s.Id == id);

                if (blog is null)
                {
                    return ResponseHelper<Blog>.MakeResponseFail("El blog con el Id indicado no existe");
                }

                return ResponseHelper<Blog>.MakeResponseSuccess(blog);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Blog>.MakeResponseFail(ex);
            }
        }
    }
}

