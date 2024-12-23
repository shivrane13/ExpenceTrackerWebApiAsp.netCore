namespace ExpenceTrackerWebApiAsp.netCore.Models
{
    public class Income
    {
        public int Id { get; set; }
        public string? title {  get; set; }
        public int? amount   { get; set; }
        public string? type { get; set; }
        public DateTime? date { get; set; }
        public string? category { get; set; }
        public string? description { get; set; }
        public DateTime? created_at { get; set; }
        public string? userId { get; set; }
        public ApplicationUser? user { get; set; }
    }
}
