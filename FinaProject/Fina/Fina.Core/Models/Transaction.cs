using Fina.Core.Enums;

namespace Fina.Core.Models
{
    public record Transaction()
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public ETransactionType Type { get; set; }
        public double Value { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UserId { get; set; } = string.Empty;
    }

}
