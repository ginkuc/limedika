namespace Limedika.Data.Dtos.PostIt
{
    public class PostItResponseDto
    {
        public string Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Message_Code { get; set; }
        public int Total { get; set; }
        public PostItResponseDataDto[] Data { get; set; }
    }
}