using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Chess4D
{
    public class ConsoleApp : IFrontend
    {
        // Get a piece given it's coordinates
        // Returns true if piece is valid, false otherwise


        // Print the board to console
        // TODO: make this handle vanilla chess first
        public void PrintBoard()
        {
            Console.OutputEncoding = Encoding.UTF8;
            //Board.SaveBoard("dai-shogi.json");
            //Print Column Labels
            Console.Write("   ||");
            for (var i = 0; i < Board.BoardSize; i++)
                Console.Write("　" + (char) (i + 65) + " |");
            //Print line break
            Console.WriteLine();

            for (var i = 0; i < 5 + 5 * Board.BoardSize; i++) Console.Write("-");
            Console.WriteLine();
            for (var y = Board.BoardSize; y > 0; y--)
            {
                Console.Write(y);
                Console.Write(y >= 10 ? " ||" : "  ||");
                for (sbyte x = 0; x < Board.BoardSize; x++)
                {
                    var team = Board._board[x, y - 1].Team;
                    var repr = team == Teams.Black
                        ? Board._board[x, y - 1].ToString().ToUpper()
                        : Board._board[x, y - 1].ToString().ToLower();
                    Console.Write("" + repr + "|");
                }

                Console.WriteLine();
            }
        }

        public sbyte? SelectMove(IEnumerable<sbyte> moves)
        {
            sbyte? move = -1;
            do
            {
                Debug.Assert(moves != null, nameof(moves) + " != null");
                foreach (var v in moves) Console.Write(Piece.CoordToString(v));

                Console.WriteLine();
                Console.WriteLine("Select a square to move to");
                var input = Console.ReadLine();
                move = StringToCoords(input);
                if (move.Equals("cancel"))
                {
                    return null;
                }
            } while (!ValidateMoveSelection(move, moves));

            return move ?? throw new InvalidDataException();
        }


        public sbyte SelectPiece(Teams currentPlayer)
        {
            sbyte piece = -1;
            do
            {
                //Prompt user to pick a piece
                Console.WriteLine(currentPlayer + "'s Move? (c,r)\n");
                var input = Console.ReadLine()?.Split(",");

                //convert to sbyte
                piece = Piece.GetCoords(
                    (sbyte) (Convert.ToChar(input?[0].ToUpper()) - 65),
                    (sbyte) (Convert.ToInt32(input?[1]) - 1));
                //validate
            } while (!ValidatePieceSelection(piece, currentPlayer));

            return piece;
        }

        private static sbyte? StringToCoords(string input)
        {
            try
            {
                var s = input.Split(",");
                return Piece.GetCoords(
                    (sbyte) (Convert.ToChar(s?[0].ToUpper()) - 65),
                    (sbyte) (Convert.ToInt32(s?[1]) - 1));
            }
            catch
            {
                return null;
            }
        }

        private static bool ValidateMoveSelection(sbyte? move, IEnumerable<sbyte> moves)
        {
            if (move == null || moves == null) return false;

            return moves.Contains((sbyte) move);
        }

        private static bool ValidatePieceSelection(sbyte piece, Teams currentPlayer)
        {
            var x = Piece.GetX(piece);
            var y = Piece.GetY(piece);
            //check bounds
            if (x < 0 || x >= Board.BoardSize || y < 0 || y >= Board.BoardSize)
                return false;
            //check team
            return Board._board[x, y].Team == currentPlayer;
        }
    }
}