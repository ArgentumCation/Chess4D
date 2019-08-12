using SFML.Window;

namespace Chess4D
{
    public class Player
    {
        public readonly Teams Team;
        public sbyte? SelectedPiece;
        public sbyte? SeletedMove;
        public Player(Teams team)
        {
            Team = team;
        }
    }
}