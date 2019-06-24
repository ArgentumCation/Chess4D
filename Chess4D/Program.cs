using System;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Chess4D
{
    class Program
    {
        private static Piece.Teams currentPlayer;

        private static void RookMove(int[] coords)
        {

        }
        // Get a piece given it's coordinates
        // Returns true if piece is valid, false otherwise
        private static bool GetPiece(int[] coords)
        {
            //Prompt user for coordinates
            Console.WriteLine(currentPlayer + "'s Move? ");
            string[] input = Console.ReadLine()?.Split(",");
            //Console.WriteLine("[{0}]", string.Join(", ", input));
            //Convert input into coordinates
            coords[0] = Convert.ToChar(input[0].ToUpper()) - 65;
            coords[1] = Convert.ToInt32(input[1]) - 1;
            //Console.WriteLine("[{0}]", string.Join(", ", coords));

            //check if coord is valid

            //check range
            if (coords[0] < 0 || coords[0] >= Board.BOARD_SIZE || coords[1] < 0 ||
                coords[1] >= Board.BOARD_SIZE)
            {
                return false;
            }
            //check team
            if (Board._board[coords[0], coords[1]].team != currentPlayer)
            {
                return false;
            }
            return true;
        }


        // Get possible moves for a piece?
        // TODO: figure out why this is returns bool
        private static bool GetMoves(int[] coords)
        {
            switch (Board._board[coords[0], coords[1]].type)
            {
                case Piece.PieceTypes.Rook:
                    RookMove(coords);
                    break;
                case Piece.PieceTypes.Knight:
                    break;
                case Piece.PieceTypes.FD:
                    break;
                case Piece.PieceTypes.WFA:
                    break;
                case Piece.PieceTypes.Bishop:
                    break;
                case Piece.PieceTypes.Rose:
                    break;
                case Piece.PieceTypes.Archbishop:
                    break;
                case Piece.PieceTypes.Queen:
                    break;
                case Piece.PieceTypes.King:
                    break;
                case Piece.PieceTypes.Chancellor:
                    break;
                case Piece.PieceTypes.Superknight:
                    break;
                case Piece.PieceTypes.Pawn:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }

        public static void Main(string[] args)
        {
            currentPlayer = Piece.Teams.White;
            //Set Up Board
            Board.setUpBoard();
            Board.PrintBoard();
            //Prompt Player for move\
            bool moveValid = false;
            while (!moveValid)
            {
                int[] coords = { -1, -1 };
                // Select a piece
                moveValid = GetPiece(coords);
                if (moveValid)
                {
                    //find moves
                    moveValid = GetMoves(coords);
                }
            }
        }



    }

    class Piece
    {
        public enum PieceTypes
        {
            Undefined,
            Rook,
            Knight,
            // ReSharper disable once InconsistentNaming
            FD,
            // ReSharper disable once InconsistentNaming
            WFA,
            Bishop,
            Rose,
            Archbishop,
            Queen,
            King,
            Chancellor,
            Superknight,
            Pawn,
        }

        public enum Teams
        {
            Undefined,
            Black,
            White
        }
        public PieceTypes type;
        public Teams team;

        public Piece(PieceTypes type, Teams team)
        {
            this.team = team;
            this.type = type;
        }

        public Piece()
        {
            this.team = Teams.Undefined;
            this.type = PieceTypes.Undefined;
        }

        public override string ToString()
        {
            string res = "";
            switch (type)
            {
                case PieceTypes.Undefined:
                    res += " ";
                    break;
                case PieceTypes.Rook:
                    res += "r";
                    break;
                case PieceTypes.Knight:
                    res += "n";
                    break;
                case PieceTypes.FD:
                    res += "d";
                    break;
                case PieceTypes.WFA:
                    res += "w";
                    break;
                case PieceTypes.Bishop:
                    res += "b";
                    break;
                case PieceTypes.Rose:
                    res += "o";
                    break;
                case PieceTypes.Archbishop:
                    res += "A";
                    break;
                case PieceTypes.Queen:
                    res += "q";
                    break;
                case PieceTypes.King:
                    res += "k";
                    break;
                case PieceTypes.Chancellor:
                    res += "c";
                    break;
                case PieceTypes.Superknight:
                    res += "j";
                    break;
                case PieceTypes.Pawn:
                    res += "p";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (team == Teams.Black)
            {
                res = res.ToUpper();
            }
            return res;
        }
    }

    static class Board
    {
        //Board Size, changing this will probably break something
        //But we may need to change this to 24 when we start working on mapping the board to other surfaces
        public const int BOARD_SIZE = 16;

        public static readonly Piece[,] _board;

        static Board()
        {
            _board = new Piece[BOARD_SIZE, BOARD_SIZE];
            for (int x = 0; x < BOARD_SIZE; x++)
            {
                for (int y = 0; y < BOARD_SIZE; y++)
                {
                    _board[x, y] = new Piece();
                }
            }
        }

        // Print the board to console
        public static void PrintBoard()
        {
            Console.WriteLine("   || a | b | c | d | e | f | g | h | i | j | k | l | m | n | o | p |");
            Console.WriteLine("---------------------------------------------------------------------");
            for (int y = BOARD_SIZE; y > 0; y--)
            {
                Console.Write(y);
                Console.Write((y >= 10) ? " ||" : "  ||");
                for (int x = 0; x < BOARD_SIZE; x++)
                {
                    Console.Write(" " + _board[x, y - 1].ToString() + " |");
                }
                Console.WriteLine();

            }
        }

        // Performs initial setup of the board
        public static void setUpBoard()
        {
            Piece.PieceTypes[] pieceArray =
            {
            Piece.PieceTypes.Rook,
            Piece.PieceTypes.Knight,
            Piece.PieceTypes.FD,
            Piece.PieceTypes.WFA,
            Piece.PieceTypes.Bishop,
            Piece.PieceTypes.Rose,
            Piece.PieceTypes.Archbishop,
            Piece.PieceTypes.Queen,
            Piece.PieceTypes.King,
            Piece.PieceTypes.Chancellor,
            Piece.PieceTypes.Superknight,
            Piece.PieceTypes.Bishop,
            Piece.PieceTypes.FD,
            Piece.PieceTypes.WFA,
            Piece.PieceTypes.Knight,
            Piece.PieceTypes.Rook

            };
            for (int x = 0; x < BOARD_SIZE; x++)
            {
                _board[x, 0] = new Piece(pieceArray[x], Piece.Teams.White);
                _board[x, 1] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.White);
                _board[x, 15] = new Piece(pieceArray[x], Piece.Teams.Black);
                _board[x, 14] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.Black);
            }
        }
    }
}
