﻿using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Share.Wrappers
{
    public class PageList<T>: List<T>
    {   
        public PageList(List<T> data,int count=0, int pageNumer=1, int pageSize=6)
        {
            this.PageSize = pageSize;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.CurrentPage = pageNumer;
       
            AddRange(data);
        }
        public PageList()
        {

        }
   
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public List<string> Messages { get; set; }
    
        public bool HasNextPage => CurrentPage < TotalPages;
  
       
    }
    public static class PageListHelper
    {
      
        public static async Task<PageList<T>> ToPageListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();
            var items = await source.Skip(pageSize*(pageNumber - 1)).Take(pageSize).ToListAsync();
            return new PageList<T>(items, count, pageNumber, pageSize);

        }
    }

}
