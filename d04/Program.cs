using d04.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace d04
{
    class Program
    {
        private static List<ISearchable> _consumingMediasList;

        static void Main(string[] args)
        {
            Initalize();

            Console.WriteLine("Input search text");
            string inputText = Console.ReadLine();

            if(inputText == "best")
            {
                var bestBook = _consumingMediasList.OfType<BookReview>().OrderBy(x => x.Rank).FirstOrDefault();
                if(bestBook != null)
                {
                    Console.WriteLine("Best in books:");
                    Console.WriteLine($"- {bestBook}");
                }

                Console.WriteLine();

                var bestMovie = _consumingMediasList.OfType<MovieReview>().OrderByDescending(x => x.IsCriticsPick).FirstOrDefault();
                if (bestMovie != null)
                {
                    Console.WriteLine("Best in movie reviews:");
                    Console.WriteLine($"- {bestMovie}");
                }
            }
            else
            {
                var result = _consumingMediasList.Search<ISearchable>(inputText);

                var books = result.OfType<BookReview>().ToList();

                if (books.Count > 0)
                {
                    Console.WriteLine($"Book search result [{books.Count}]:");
                    foreach (var book in books)
                    {
                        Console.WriteLine($"- {book}");
                    }
                }
                Console.WriteLine();
                var movies = result.OfType<MovieReview>().ToList();

                if (movies.Count > 0)
                {
                    Console.WriteLine($"Movie search result [{movies.Count}]:");
                    foreach (var movie in movies)
                    {
                        Console.WriteLine($"- {movie}");
                    }
                }
            }

            Console.ReadLine();
        }

        private static void Initalize()
        {
            _consumingMediasList = new();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string booksPath = configuration["AppConfiguration:ReviewsDirs:Books"];
            string moviesPath = configuration["AppConfiguration:ReviewsDirs:Movies"];

            string booksJson = File.ReadAllText(booksPath);
            Book book = JsonSerializer.Deserialize<Book>(booksJson);
            book.results.ForEach(x => _consumingMediasList.Add(new BookReview
            {
                Title = x.book_details?.FirstOrDefault()?.title,
                Author = x.book_details?.FirstOrDefault()?.author,
                SummaryShort = x.book_details?.FirstOrDefault()?.description,
                ListName = x.list_name,
                Rank = x.rank,
                Url = x.amazon_product_url
            }));

            string moviesJson = File.ReadAllText(moviesPath);
            Movie movie = JsonSerializer.Deserialize<Movie>(moviesJson);
            movie.results.ForEach(x => _consumingMediasList.Add(new MovieReview
            {
                Title = x.title,
                IsCriticsPick = x.critics_pick > 0,
                SummaryShort = x.summary_short,
                Url = x.link?.url
            }));
        }
    }

    static class SearchIEnumerableExtension
    {
        public static T[] Search<T>(this IEnumerable<T> list, string search) where T : ISearchable
        {
            return string.IsNullOrEmpty(search) && string.IsNullOrWhiteSpace(search)
                ?
                list
                .GroupBy(x => x.GetType())
                .SelectMany(x => x.OrderBy(y => y.Title))
                .ToArray()
                :
                list
                .Where(x => x.Title.ToLower().Contains(search.ToLower()))
                .GroupBy(x => x.GetType())
                .SelectMany(x => x.OrderBy(y => y.Title))
                .ToArray();
        }
    }

    #region Book parse classes
    public class BookDetail
    {
        public string title { get; set; }
        public string description { get; set; }
        public string contributor { get; set; }
        public string author { get; set; }
        public string contributor_note { get; set; }
        public string price { get; set; }
        public string age_group { get; set; }
        public string publisher { get; set; }
        public string primary_isbn13 { get; set; }
        public string primary_isbn10 { get; set; }
    }

    public class Isbn
    {
        public string isbn10 { get; set; }
        public string isbn13 { get; set; }
    }

    public class BooksResult
    {
        public string list_name { get; set; }
        public string display_name { get; set; }
        public string bestsellers_date { get; set; }
        public string published_date { get; set; }
        public int rank { get; set; }
        public int rank_last_week { get; set; }
        public int weeks_on_list { get; set; }
        public int asterisk { get; set; }
        public int dagger { get; set; }
        public string amazon_product_url { get; set; }
        public List<Isbn> isbns { get; set; }
        public List<BookDetail> book_details { get; set; }
        public List<Review> reviews { get; set; }
    }

    public class Review
    {
        public string book_review_link { get; set; }
        public string first_chapter_link { get; set; }
        public string sunday_review_link { get; set; }
        public string article_chapter_link { get; set; }
    }

    public class Book
    {
        public string status { get; set; }
        public string copyright { get; set; }
        public int num_results { get; set; }
        public DateTime last_modified { get; set; }
        public List<BooksResult> results { get; set; }
    }
    #endregion

    #region Movie parse classes
    public class Link
    {
        public string type { get; set; }
        public string url { get; set; }
        public string suggested_link_text { get; set; }
    }

    public class Multimedia
    {
        public string type { get; set; }
        public string src { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }

    public class MoviesResult
    {
        public string title { get; set; }
        public string mpaa_rating { get; set; }
        public int critics_pick { get; set; }
        public string byline { get; set; }
        public string headline { get; set; }
        public string summary_short { get; set; }
        public string publication_date { get; set; }
        public string opening_date { get; set; }
        public string date_updated { get; set; }
        public Link link { get; set; }
        public Multimedia multimedia { get; set; }
    }

    public class Movie
    {
        public string status { get; set; }
        public string copyright { get; set; }
        public bool has_more { get; set; }
        public int num_results { get; set; }
        public List<MoviesResult> results { get; set; }
    }
    #endregion
}