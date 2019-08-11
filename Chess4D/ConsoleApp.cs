using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Chess4D
{
    public class ConsoleApp : IFrontend
    {
        public bool SelectMove(Piece p, IEnumerable<sbyte> moves)
        {
            foreach (var v in moves)
            {
                p.FirstMove = false; //Todo: move this after move is selected
                Console.Write(Piece.CoordToString(v));
            }

            throw new NotImplementedException();
        }

        // Get a piece given it's coordinates
        // Returns true if piece is valid, false otherwise
        public bool GetPiece(ref sbyte coords, Teams currentPlayer)
        {
            if (coords > Board.BoardSize)
                throw new ArgumentOutOfRangeException(nameof(coords));
            //Prompt user for coordinates
            Console.WriteLine(currentPlayer + "'s Move? (c,r)\n");
            var input = Console.ReadLine()?.Split(",");

            //Convert input into coordinates

            //First coord is encoded as 4 LSB
            if (input != null)
            {
                coords = (sbyte) (Convert.ToChar(input[0].ToUpper()) - 65);
                //Second coords is encoded as 4 MSB
                coords |= (sbyte) ((Convert.ToInt32(input[1]) - 1) << 4);
            }

            //Console.WriteLine("[{0}]", string.Join(", ", coords));

            //check if coord is valid

            //check range
            if ((coords & 0xF) < 0 || (coords & 0xF) >= Board.BoardSize ||
                coords >> 4 < 0 ||
                coords >> 4 >= Board.BoardSize)
                return false;

            //check team
            return Board._board[coords & 0xF, coords >> 4].Team == currentPlayer;
        }

        // Print the board to console
        // TODO: make this handle vanilla chess first
        public void PrintBoard()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
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
    }
}