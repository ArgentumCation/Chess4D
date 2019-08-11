using System.Collections.Generic;

namespace Chess4D
{
    public class SFMLApp : IFrontend
    {
        public bool SelectMove(Piece p, IEnumerable<sbyte> moves)
        {
            throw new System.NotImplementedException();
        }

        public bool GetPiece(ref sbyte coords, Teams currentPlayer)
        {
            throw new System.NotImplementedException();
        }

        public void PrintBoard()
        {
            throw new System.NotImplementedException();
        }
    }
}