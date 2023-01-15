
using System.Text;

namespace d05.Nasa.Apod.Models
{
    public class MediaOfToday
    {
        public string Copyright { get; set; }
        public string Date { get; set; }
        public string Explanation { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();
            if (!string.IsNullOrEmpty(Date))
            {
                sb.AppendLine(Date);
            }

            if (!string.IsNullOrEmpty(Title))
            {
                if (string.IsNullOrEmpty(Copyright))
                {
                    sb.AppendLine($"'{Title}'");
                }
                else
                {
                    sb.AppendLine($"'{Title}' by {Copyright}");
                }
            }

            if (!string.IsNullOrEmpty(Explanation))
            {
                sb.AppendLine(Explanation);
            }

            if (!string.IsNullOrEmpty(Url))
            {
                sb.AppendLine($"{Url}.");
            }

            if(sb.Length > 0)
            {
                return sb.ToString();
            }
            else
            {
                return "Wrong media";
            }
        }
    }
}
