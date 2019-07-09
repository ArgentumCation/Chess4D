using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess4D
{
    internal class Program
    {
        private static Piece.Teams currentPlayer;


        // Get a piece given it's coordinates
        // Returns true if piece is valid, false otherwise
        private static bool GetPiece(ref byte coords)
        {
            //Prompt user for coordinates
            Console.WriteLine(currentPlayer + "'s Move? (c,r)");
            var input = Console.ReadLine()?.Split(",");

            //Convert input into coordinates

            //First coord is encoded as 4 LSB
            coords = (byte) (Convert.ToChar(input[0].ToUpper()) - 65);
            //Second coords is encoded as 4 MSB
            coords |= (byte) ((Convert.ToInt32(input[1]) - 1) << 4);
            //Console.WriteLine("[{0}]", string.Join(", ", coords));

            //check if coord is valid

            //check range
            if ((coords & 0xF) < 0 || (coords & 0xF) >= Board.BOARD_SIZE || coords >> 4 < 0 ||
                coords >> 4 >= Board.BOARD_SIZE)
                return false;

            //check team
            if (Board._board[coords & 0xF, coords >> 4].team != currentPlayer) return false;

            return true;
        }


        // Get possible moves for a piece
        //Todo: finish vanilla, CRRB
        //Todo: add shogi
        private static IEnumerable<byte> GetMoves(byte coords)
        {
            IEnumerable<byte> possibleMoves = new List<byte>();
            switch (Board._board[coords & 0xF, coords >> 4].type)
            {
                case Piece.PieceTypes.Rook:
                    possibleMoves = RookMove(coords);
                    break;
                case Piece.PieceTypes.Knight:
                    possibleMoves = KnightMove(coords);
                    break;
                //Todo: decompose into ferz and dabbaba
                case Piece.PieceTypes.CRRBFD:
                    possibleMoves = FDMove(coords);
                    break;
                //Todo: decompose into King/Alfil
                case Piece.PieceTypes.CRRBWFA:
                    possibleMoves = WFAMove(coords);
                    break;
                case Piece.PieceTypes.Bishop:
                    possibleMoves = BishopMove(coords);
                    break;
                case Piece.PieceTypes.CRRBRose:
                    possibleMoves = RoseMove(coords);
                    break;
                case Piece.PieceTypes.CRRBArchBishop:
                    possibleMoves = BishopMove(coords);
                    possibleMoves.Union(KnightMove(coords));
                    break;
                case Piece.PieceTypes.Queen:
                    possibleMoves = RookMove(coords);
                    possibleMoves.Union(BishopMove(coords));
                    break;
                //Todo handle check/checkmate
                case Piece.PieceTypes.King:
                    possibleMoves = KingMove(coords);
                    break;
                case Piece.PieceTypes.CRRBChancellor:
                    possibleMoves = RookMove(coords);
                    possibleMoves.Union(KnightMove(coords));
                    break;
                //Todo: decompose into knight, camel, and zebra
                case Piece.PieceTypes.CRRBSuperKnight:
                    possibleMoves = SuperKnightMoves(coords);
                    break;
                //Todo: en passant / attack move / Promotion
                case Piece.PieceTypes.Pawn:
                    possibleMoves = PawnMoves(coords);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //Todo: remove duplicates
            //Todo: king move
            return possibleMoves;
        }

        //Todo:finish
        private static IEnumerable<byte> PawnMoves(byte coords)
        {
            //Console.WriteLine(""+ Piece.GetX(coords)+" " + Piece.GetY(coords));
            throw new NotImplementedException();
        }

//Todo:finish
        private static IEnumerable<byte> SuperKnightMoves(byte coords)
        {
            throw new NotImplementedException();
        }

//Todo:finish
        private static IEnumerable<byte> KingMove(byte coords)
        {
            throw new NotImplementedException();
        }

//Todo:finish
        private static IEnumerable<byte> RoseMove(byte coords)
        {
            throw new NotImplementedException();
        }

//Todo:finish
        private static IEnumerable<byte> BishopMove(byte coords)
        {
            throw new NotImplementedException();
        }

//Todo:finish
        private static IEnumerable<byte> WFAMove(byte coords)
        {
            throw new NotImplementedException();
        }

//Todo:finish
        private static IEnumerable<byte> FDMove(byte coords)
        {
            throw new NotImplementedException();
        }

//Todo:finish
        private static IEnumerable<byte> KnightMove(byte coords)
        {
            throw new NotImplementedException();
        }

        //Todo: test
        private static IEnumerable<byte> RookMove(byte coords)
        {
            IEnumerable<byte> moves = new List<byte>();
            byte currentX = Piece.GetX(coords);
            byte currentY = Piece.GetY(coords);
               //            //Check Right
               //            for (var x = (byte) (currentX + 1); x < Board.BOARD_SIZE; x++)
               //            {
               //                moves.Append(Piece.GetCoords(x, currentY));
               //                //Stop iteration after find a piece
               //                if (Board._board[x, currentY].type != Piece.PieceTypes.Undefined) break;
               //            }
               //
               //            //Check Left
               //            for (var x = (byte) (currentX - 1); x < Board.BOARD_SIZE; x--)
               //            {
               //                moves.Append(Piece.GetCoords(x, currentY));
               //                //Stop iteration after find a piece
               //                if (Board._board[x, currentY].type != Piece.PieceTypes.Undefined) break;
               //            }
               //
               //            // Check Up
               //            for (var y = (byte) (currentY + 1); y < Board.BOARD_SIZE; y++)
               //            {
               //                moves.Append(Piece.GetCoords(currentX, y));
               //                //Stop iteration after find a piece
               //                if (Board._board[currentX, y].type != Piece.PieceTypes.Undefined) break;
               //            }
               //
               //            //Check Down
               //            for (var y = (byte) (currentY - 1); y < Board.BOARD_SIZE; y--)
               //            {
               //                moves.Append(Piece.GetCoords(currentX, y));
               //                //Stop iteration after find a piece
               //                if (Board._board[currentX, y].type != Piece.PieceTypes.Undefined) break;
               //            }
               
               //horizontal moves
               for (byte i = 0; i < Board.BOARD_SIZE; i++)
               {
                   byte x = i;
                   byte y = currentY;
                   if (x != currentX)
                   {
                       if (!AttackingAllies(x, y))
                       {
                           if (!MoveThroughPieces(x, y))
                           {
                               moves.Append(Piece.GetCoords(x,y))
                           }
                       }
                   }
               }
               //horizontal moves
               for (byte i = 0; i < Board.BOARD_SIZE; i++)
               {
                   byte x = i;
                   byte y = currentY;
                   if (x != currentX)
                   {
                       if (!AttackingAllies(x, y))
                       {
                           if (!MoveThroughPieces(x, y))
                           {
                               moves.Append(Piece.GetCoords(x,y))
                           }
                       }
                   }
               }
            return moves;
        }

        public static void Main(string[] args)
        {
            currentPlayer = Piece.Teams.White;
            //Set Up Board
            Board.SetUpBoard();
            Board.PrintBoard();
            //Prompt Player for move\
            var moveValid = false;
            while (!moveValid)
            {
                byte coords = 0x00;
                // Select a piece
                moveValid = GetPiece(ref coords);
                if (moveValid)
                {
                    //find moves
                    var moves = GetMoves(coords);
                    moveValid = selectMove(moves);
                }
            }
        }

        private static bool selectMove(IEnumerable<byte> moves)
        {
            throw new NotImplementedException();
        }
    }

    internal class Piece
    {
        public enum PieceTypes
        {
            Undefined,

            //Vanilla Chess
            Rook,
            Knight,
            Bishop,
            Queen,
            King,
            Pawn,

            //Chess on a Really Big Board
            CRRBFD,
            CRRBWFA,
            CRRBRose,
            CRRBArchBishop,
            CRRBChancellor,
            CRRBSuperKnight,

            //Todo: Dai Shogi Pieces
            //Todo: Deal with full names later
            ShogiAngryBoar,
            shogiBishop,
            ShogiTiger,
            ShogiNekomata,
            ShogiCopper,
            ShogiDragonHorse,
            ShogiDragonKing,
            ShogiElephant,
            ShogiWolf,
            ShogiLeopard,
            ShogiDragon,
            ShogiFlyingOx,
            ShogiStag,
            ShogiFreeBoar,
            ShogiGoBetween,
            ShogiGold,
            ShogiFalcon,
            ShogiIron,
            ShogiKing,
            ShogiKirin,
            ShogiKnight,
            ShogiLance,
            ShogiLion,
            ShogiPawn,
            ShogiPhoenix,
            ShogPrince,
            ShogiChariot,
            ShogiRook,
            ShogiSideMover,
            ShogiSilver,
            ShogiStone,
            ShogiEagle,
            ShogiVerticalMover,
            ShogiViolentOx,
            ShogiWhale,
            ShogiWhiteHorse
        }

        public enum Teams
        {
            Undefined,
            Black,
            White
        }

        public Teams team;

        public PieceTypes type;

        public Piece(PieceTypes type, Teams team)
        {
            this.team = team;
            this.type = type;
        }

        public Piece()
        {
            team = Teams.Undefined;
            type = PieceTypes.Undefined;
        }

        public static byte GetX(byte b)
        {
            return (byte) (b & 0xF);
        }

        public static byte GetY(byte b)
        {
            return (byte) (b >> 4);
        }

        public static byte GetCoords(byte x, byte y)
        {
            return (byte) ((y << 4) | (x & 0xF));
        }

        public override string ToString()
        {
            var res = "";
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
                case PieceTypes.CRRBFD:
                    res += "d";
                    break;
                case PieceTypes.CRRBWFA:
                    res += "w";
                    break;
                case PieceTypes.Bishop:
                    res += "b";
                    break;
                case PieceTypes.CRRBRose:
                    res += "o";
                    break;
                case PieceTypes.CRRBArchBishop:
                    res += "A";
                    break;
                case PieceTypes.Queen:
                    res += "q";
                    break;
                case PieceTypes.King:
                    res += "k";
                    break;
                case PieceTypes.CRRBChancellor:
                    res += "c";
                    break;
                case PieceTypes.CRRBSuperKnight:
                    res += "j";
                    break;
                case PieceTypes.Pawn:
                    res += "p";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (team == Teams.Black) res = res.ToUpper();

            return res;
        }
    }

    internal static class Board
    {
        //Board Size, changing this will probably break something
        //But we may need to change this to 24 when we start working on mapping the board to other surfaces
        public const byte BOARD_SIZE = 16;

        public static readonly Piece[,] _board;

        static Board()
        {
            _board = new Piece[BOARD_SIZE, BOARD_SIZE];
            for (byte x = 0; x < BOARD_SIZE; x++)
            for (byte y = 0; y < BOARD_SIZE; y++)
                _board[x, y] = new Piece();
        }

        // Print the board to console
        public static void PrintBoard()
        {
            Console.WriteLine("   || a | b | c | d | e | f | g | h | i | j | k | l | m | n | o | p |");
            Console.WriteLine("---------------------------------------------------------------------");
            for (var y = BOARD_SIZE; y > 0; y--)
            {
                Console.Write(y);
                Console.Write(y >= 10 ? " ||" : "  ||");
                for (byte x = 0; x < BOARD_SIZE; x++) Console.Write(" " + _board[x, y - 1] + " |");

                Console.WriteLine();
            }
        }

        // Performs initial setup of the board
        public static void SetUpBoard()
        {
            Piece.PieceTypes[] CRRBpieceArray =
            {
                Piece.PieceTypes.Rook,
                Piece.PieceTypes.Knight,
                Piece.PieceTypes.CRRBFD,
                Piece.PieceTypes.CRRBWFA,
                Piece.PieceTypes.Bishop,
                Piece.PieceTypes.CRRBRose,
                Piece.PieceTypes.CRRBArchBishop,
                Piece.PieceTypes.Queen,
                Piece.PieceTypes.King,
                Piece.PieceTypes.CRRBChancellor,
                Piece.PieceTypes.CRRBSuperKnight,
                Piece.PieceTypes.Bishop,
                Piece.PieceTypes.CRRBFD,
                Piece.PieceTypes.CRRBWFA,
                Piece.PieceTypes.Knight,
                Piece.PieceTypes.Rook
            };

            for (byte x = 0; x < BOARD_SIZE; x++)
            {
                _board[x, 0] = new Piece(CRRBpieceArray[x], Piece.Teams.White);
                _board[x, 1] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.White);
                _board[x, 15] = new Piece(CRRBpieceArray[x], Piece.Teams.Black);
                _board[x, 14] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.Black);
            }
        }
    }
}