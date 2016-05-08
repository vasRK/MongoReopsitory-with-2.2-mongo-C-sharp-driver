using MongoDB.Driver;

namespace Repository
{
    public class DocumentFilter<T> where T: IDocument
    {
       public FilterDefinition<T> Filter {get;set;}
        public SortDefinition<T> Sort{get;set;}
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
