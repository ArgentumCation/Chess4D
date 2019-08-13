using System.Collections.Generic;

namespace Chess4D
{
    public interface IFrontend
    {
        void PrintBoard();
        sbyte? SelectMove(IEnumerable<sbyte> moves);
        sbyte SelectPiece(Teams currentPlayer);
    }
    
}