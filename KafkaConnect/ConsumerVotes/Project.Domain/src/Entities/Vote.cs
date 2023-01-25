using Project.Domain.src.Enum;

namespace Project.Domain.src.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public Participants participants { get; set; }
        public int Qtd { get; init; } = 1;
    }
}
