namespace AutopartsBackend.API.Models
{
    public class RecognitionResultDto
    {
        public string PartName { get; set; } = string.Empty;
        public float Confidence { get; set; }
        public string PartNumber { get; set; } = string.Empty;
    }
}