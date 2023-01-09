
namespace d04.Model
{
    class MovieReview : ISearchable
    {
        public string Title { get; set; }
        public bool IsCriticsPick { get; set; }
        public string SummaryShort { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return $"{Title.ToUpper()} {(IsCriticsPick ? "[NYT critic’s pick]" : "")}\n{SummaryShort}\n{Url}";
        }
    }
}