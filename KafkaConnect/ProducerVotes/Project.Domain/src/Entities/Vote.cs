using Project.Domain.src.Enum;

namespace Project.Domain.src.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public Participants Participants { get; set; }
        public int Qtd { get; init; } = 1;
    }
}
