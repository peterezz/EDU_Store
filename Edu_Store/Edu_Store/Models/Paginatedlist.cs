namespace Edu_Store.Models
{
    public class Paginatedlist<T>:List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }

        public Paginatedlist(List<T>items,int count,int pageIndex, int pagesize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)pagesize);
            this.AddRange(items);
        }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPage;
        public static Paginatedlist<T> create(List<T> source,int pageIndex,int pagesize)
        {
            var count=source.Count;
            var items=source.Skip((pageIndex-1)*pagesize).Take(pagesize).ToList();
            return new Paginatedlist<T>(items,count,pageIndex,pagesize);

        }
    }
}
