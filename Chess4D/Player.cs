using SFML.Window;

namespace Chess4D
{
    public class Player
    {
        public readonly Teams Team;
        public sbyte? SelectedPiece;
        public sbyte? SelectedMove;
        public Player(Teams team)
        {
            Team = team;
        }
    }
}