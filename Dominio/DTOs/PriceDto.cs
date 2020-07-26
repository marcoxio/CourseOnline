using System;

namespace Dominio.DTOs
{
    public class PriceDto
    {
        public Guid PriceId { get; set; }

        public decimal ActualPrice { get; set; }
        public decimal Promotion { get; set; }
        public Guid CourseId { get; set; }
    }
}