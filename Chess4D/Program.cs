using System;
using System.Collections.Generic;
using System.Linq;

//Todo: Add 4D Capability

// ReSharper disable once CommentTypo
//Todo check for piece movement and update piecemoved
//Todo add castling
namespace Chess4D
{
    internal static class Program
    {
        private static Teams _currentPlayer;


        private static IEnumerable<sbyte> GetMoves(sbyte coords)
        {
            IEnumerable<sbyte> possibleMoves = new HashSet<sbyte>();
            possibleMoves = Board._board[Piece.GetX(coords), Piece.GetY(coords)]
                .Move(coords);
            return possibleMoves as HashSet<sbyte>;
        }
        
        public static void Main(string[] args)
        {
            IFrontend frontend = new ConsoleApp();
            _currentPlayer = Teams.White;
            //Set Up Board
            Board.SetUpBoard();
            frontend.PrintBoard();
            //Prompt Player for move\
            var moveValid = false;
            while (!moveValid)
            {
                sbyte coords = 0x00;
                // Select a piece
                moveValid = frontend.GetPiece(ref coords, _currentPlayer);
                if (moveValid)
                {
                    //find moves
                    var moves = GetMoves(coords);
                    moveValid =
                        frontend.SelectMove(
                            Board._board[Piece.GetX(coords), Piece.GetY(coords)],
                            moves);
                }
            }
        }
    }
}