namespace Data.DTOs
{
    public class FeedDTO
    {
        public int Id { get; set;  }
        public string? Source { get; set;  }
        public string? Name { get; set; }
        public int ItemParserID { get; set; }
        public int ParserID { get; set; }
        public List<string> Categories { get; set; } = new();
        public List<ReviewDTO> Reviews { get; set; } = new();
    }
}