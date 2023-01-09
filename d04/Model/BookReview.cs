
namespace d04.Model
{
    class BookReview : ISearchable
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Rank { get; set; }
        public string ListName { get; set; }
        public string SummaryShort { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return $"{Title.ToUpper()} by {Author} [{Rank} on NYT’s {ListName}]\n{SummaryShort}\n{Url}";
        }
    }
}
