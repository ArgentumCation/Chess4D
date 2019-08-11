using System.Collections.Generic;

namespace Chess4D
{
    public interface IFrontend
    { 
        bool SelectMove(Piece p, IEnumerable<sbyte> moves);
        bool GetPiece(ref sbyte coords, Teams currentPlayer);
        void PrintBoard();
    }
    
}