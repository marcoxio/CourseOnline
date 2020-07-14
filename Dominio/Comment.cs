namespace Dominio
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Alumn { get; set; }
        public int Score { get; set; }
        public string TextComment { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}