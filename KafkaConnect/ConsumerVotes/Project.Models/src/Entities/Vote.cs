using Project.Models.src.Enum;

namespace Project.Models.src.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public Participants participants { get; set; }
        public int Qtd { get; init; } = 1;
    }
}
