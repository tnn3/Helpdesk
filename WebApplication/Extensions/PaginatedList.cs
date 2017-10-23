﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Extensions
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var enumerable = source as T[] ?? source.ToArray();
            var items = enumerable.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new PaginatedList<T>(items.ToList(), enumerable.Length, pageIndex, pageSize);
        }
    }
}
