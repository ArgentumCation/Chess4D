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
        private static sbyte selectedPiece;


        //Need to check for checkmate)
        private static IEnumerable<sbyte> GetMoves(sbyte? piece)
        {
            if (piece == null) throw new ArgumentNullException();
            var coords = piece ?? -1;
            IEnumerable<sbyte> possibleMoves = new HashSet<sbyte>();
            possibleMoves = Board._board[Piece.GetX(coords), Piece.GetY(coords)]
                .Move(coords);
            return possibleMoves as HashSet<sbyte>;
        }

        public static void GetInput(Player currentPlayer, IFrontend frontend)
        {
            while (true)
            {
                //Select Piece
                CommandProcessor.ExecuteCommand(
                    new SelectPieceCommand(currentPlayer, frontend));

                //get possible moves
                var moves = GetMoves(currentPlayer.SelectedPiece);

                //select move
                CommandProcessor.ExecuteCommand(
                    new SelectMoveCommand(moves, currentPlayer, frontend));


                //if selected piece is null we need to select a piece
                if (currentPlayer.SelectedPiece == null)
                {
                    //Removes move selection from the queue
                    CommandProcessor.Undo();
                    //Removes piece selection from queue
                    CommandProcessor.Undo();
                    //Allows user to select a new piece
                    continue;
                }
                //Todo: Implement
                //actually make the move
                CommandProcessor.ExecuteCommand(
                    new MoveCommand(currentPlayer.SeletedMove));


                break;
            }
        }

        public static void Main(string[] args)
        {
            IFrontend frontend = new ConsoleApp();
            _currentPlayer = Teams.White;
            //Set Up Board
            Board.SetUpBoard();


            //Update Loop
            var turn = 0;
            Player[] PlayerList = {new Player(Teams.White), new Player(Teams.Black)};
            while (gameNotOver())
            {
                frontend.PrintBoard();
                var currentPlayer = PlayerList[turn % PlayerList.Length];
                GetInput(currentPlayer, frontend);
                turn++;
            }
        }

        //todo finish
        private static bool gameNotOver()
        {
            return true;
        }
    }
}