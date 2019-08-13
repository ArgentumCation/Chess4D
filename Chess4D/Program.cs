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

                //todo add castling
                //todo prune moves (puts you in check)
                //PruneMoves(currentPlayer, selectedPiece, moves);
                
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
                    new MoveCommand(currentPlayer.SelectedMove));
                //Todo Check for promotions
                /* if(promotions{
                    CommandProcessor.ExecuteCommand(new PromoteCommand();
                 }*/
                break;
            }
        }

        public static void Main(string[] args)
        {
            IFrontend frontend = new FNAApp();
            _currentPlayer = Teams.White;
            //Set Up Board
            Board.SetUpBoard();


            //Update Loop
            var turn = 0;
            Player[] PlayerList = {new Player(Teams.White), new Player(Teams.Black)};
            while (!gameOver())
            {
                frontend.PrintBoard();
                var currentPlayer = PlayerList[turn % PlayerList.Length];
                GetInput(currentPlayer, frontend);
                turn++;
            }
        }

        //todo finish
        private static bool gameOver()
        {
            /*
             * //Looks for checkmate
             * foreach(var piece in board){
             *    var moves = piece.getMoves();
             *    foreach(Var move in moves
             *    {    
             *    CommandProcessor.ExecuteCommand(new MoveCommand(move));
             *     LookForCheck();
             *     CommandProcessor.Undo();
             *    }
             * }
             * //todo regular shogi impasse
             * //Looks for two kings
             * //Looks for turn count
             * //Check for Board Positions Repeating(1 time in dai shogi 4 in chu/normal shogi
             * //Todo put somewhere else: Stalemated player loses in dai/chu shogi
             * //Todo: shogi pieces can move in and out of check
             * //Todo only normal shogi has drops
             * //todo pawns can not checkmate when dropped
             */
            return false;
        }
    }
}