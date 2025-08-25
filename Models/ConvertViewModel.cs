namespace NumberToWordsApp.Models
{
    public class ConvertViewModel
    {
        public string? NumberInput { get; set; }
        public string? Result { get; set; }
        public bool HasError { get; set; }
        public string? ErrorMessage { get; set; }
        public string? RequestId { get; set; }
    }
}

