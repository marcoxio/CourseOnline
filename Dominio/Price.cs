namespace Dominio
{
    public class Price
    {
        public int PrecioId { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal Promotion { get; set; }
        public decimal CourseId { get; set; }
        public Course Course { get; set; }
    }
}