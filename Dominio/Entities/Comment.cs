
using System;

namespace Dominio.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Alumn { get; set; }
        public int Score { get; set; }
        public string TextComment { get; set; }
        public Guid CourseId { get; set; }
        public DateTime? CreationDate { get; set; }
        public Course Course { get; set; }
    }
}