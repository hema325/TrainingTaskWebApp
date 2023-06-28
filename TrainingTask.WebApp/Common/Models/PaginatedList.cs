﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TrainingTask.WebApp.Common.Mapping;

namespace TrainingTask.WebApp.Common.Models
{
    public class PaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get; }

        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public PaginatedList(IReadOnlyCollection<T> items,int totalCount,int pageNumber,int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int TotalPages => (int) Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> query,int pageNumber,int pageSize)
        {
            int totalCount = await query.CountAsync();
            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(data, totalCount, pageNumber, pageSize);
        }

        public static async Task<PaginatedList<TDestinition>> CreateAsync<TDestinition>(IQueryable<T> query, IMapper mapper, int pageNumber, int pageSize)
        {
            int totalCount = await query.CountAsync();
            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<TDestinition>(mapper.ConfigurationProvider).ToListAsync();
            return new PaginatedList<TDestinition>(data, totalCount, pageNumber, pageSize);
        }
    }
}
