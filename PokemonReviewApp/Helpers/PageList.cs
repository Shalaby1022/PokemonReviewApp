using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helpers
{
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalAmountOfItems { get; private set; }
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevius => CurrentPage > 1 ;

        public PageList(List<T> items , int count , int pagenumber , int pagesize)
        {
            TotalAmountOfItems = count;
            PageSize = pagesize;
            CurrentPage = pagenumber;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            AddRange(items );

        }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source , int pageNumber , int pageSize)
        {
            var count = source.Count();

            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PageList<T>(items , count , pageNumber , pageSize);
        }

    }
}
