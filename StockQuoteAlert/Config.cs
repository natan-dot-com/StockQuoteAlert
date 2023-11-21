namespace StockQuoteAlert.Parsing;

class Config
{
    public class APIConfig
    {
        public string key { get; set; } = string.Empty;
    }

    public class EmailConfig
    {
        public string host { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public List<string> senderList { get; set; } = new List<string>();
    }

    public APIConfig api {  get; set; }
    public EmailConfig email { get; set; }
}
