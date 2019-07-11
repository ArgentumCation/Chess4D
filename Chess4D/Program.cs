using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess4D
{
    internal class Program
    {
        private static Piece.Teams currentPlayer;
        // Checks if a piece is attacking it's allies
        // team is the team of the piece making the attack
        // x and y are target coords
        private static bool AttackingAllies(byte x, byte y, Piece.Teams team)
        {
            
            var attackedPiece = Board._board[x, y];
            if (attackedPiece.team != Piece.Teams.Undefined)
            {
                if (attackedPiece.team == team) return true;

            }
            return false;
        }

        //Find out if a movement would move through a piece
        // x,y are the target
        // currentX, currentY are coords of current piece
        private static bool MoveThroughPieces(byte x, byte y, byte currentX, byte currentY)
        {
            //Get direction of movement
            var directionX = x - currentX;
            if (directionX > 0)
                directionX = 1;
            else if (directionX < 0) directionX = -1;

            var directionY = y - currentY;
            if (directionY > 0)
                directionY = 1;
            else if (directionY < 0) directionY = -1;

            //Check if every square is empty
            int i = currentX;
            int j = currentY;
            //move once to avoid checking self
            i += directionX;
            j += directionY;
            while (i != x || j != y)
            {
                if (Board._board[x, y].team != Piece.Teams.Undefined) return true;

                i += directionX;
                j += directionY;
            }

            return false;
        }

        
        // Get a piece given it's coordinates
        // Returns true if piece is valid, false otherwise
        private static bool GetPiece(ref byte coords)
        {
            if (coords > Board.BOARD_SIZE)
            {
                throw new ArgumentOutOfRangeException(nameof(coords));
            }
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
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].team;
            //horizontal moves
            for (byte i = 0; i < Board.BOARD_SIZE; i++)
            {
                var x = i;
                var y = currentY;
                if (x != currentX)
                    if (!AttackingAllies(x, y, team))
                        if (!MoveThroughPieces(x, y, currentX, currentY))
                            moves.Append(Piece.GetCoords(x, y));
            }

            //horizontal moves
            for (byte i = 0; i < Board.BOARD_SIZE; i++)
            {
                var x = i;
                var y = currentY;
                if (x != currentX)
                    if (!AttackingAllies(x, y, team))
                        if (!MoveThroughPieces(x, y, currentX, currentY))
                            moves.Append(Piece.GetCoords(x, y));
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
            // ReSharper disable once InconsistentNaming
            // ReSharper disable once IdentifierTypo
            CRRBFD,
            // ReSharper disable once InconsistentNaming
            // ReSharper disable once IdentifierTypo
            CRRBWFA,
            // ReSharper disable once InconsistentNaming
            // ReSharper disable once IdentifierTypo
            CRRBRose,
            // ReSharper disable once InconsistentNaming
            // ReSharper disable once IdentifierTypo
            CRRBArchBishop,
            // ReSharper disable once InconsistentNaming
            // ReSharper disable once IdentifierTypo
            CRRBChancellor,
            // ReSharper disable once InconsistentNaming
            // ReSharper disable once IdentifierTypo
            CRRBSuperKnight,

            // ReSharper disable once CommentTypo
            //Todo: Dai Shogi Pieces
            //Todo: Deal with full names later
            ShogiAngryBoar,
            ShogiBishop,
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
            ShogiWhiteHorse,
            ShogiQueen
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
        // TODO: make dynamic so that board size can be variable
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
            
            //Chess on a Really Big Board Setup
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

            
            //Dai Shogi
            Piece.PieceTypes[] DaiShogiArray1 =
            {
                Piece.PieceTypes.ShogiLance,
                Piece.PieceTypes.ShogiKnight,
                Piece.PieceTypes.ShogiStone,
                Piece.PieceTypes.ShogiIron,
                Piece.PieceTypes.ShogiCopper,
                Piece.PieceTypes.ShogiSilver,
                Piece.PieceTypes.ShogiGold,
                Piece.PieceTypes.ShogiKing,
                Piece.PieceTypes.ShogiGold,
                Piece.PieceTypes.ShogiSilver,
                Piece.PieceTypes.ShogiCopper,
                Piece.PieceTypes.ShogiIron,
                Piece.PieceTypes.ShogiStone,
                Piece.PieceTypes.ShogiKnight,
                Piece.PieceTypes.ShogiLance
            };
            Piece.PieceTypes[] DaiShogiArray2 =
            {
                Piece.PieceTypes.ShogiChariot,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiNekomata,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiLeopard,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiTiger,
                Piece.PieceTypes.ShogiElephant,
                Piece.PieceTypes.ShogiTiger,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiLeopard,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiNekomata,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiChariot

            };
            Piece.PieceTypes[] DaiShogiArray3 =
            {
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiViolentOx,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiAngryBoar,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiWolf,
                Piece.PieceTypes.ShogiPhoenix,
                Piece.PieceTypes.ShogiLion,
                Piece.PieceTypes.ShogiKirin,
                Piece.PieceTypes.ShogiWolf,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiAngryBoar,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiViolentOx,
                Piece.PieceTypes.Undefined
            };
            Piece.PieceTypes[] DaiShogiArray4 =
            {
                Piece.PieceTypes.ShogiRook,
                Piece.PieceTypes.ShogiDragon,
                Piece.PieceTypes.ShogiSideMover,
                Piece.PieceTypes.ShogiVerticalMover,
                Piece.PieceTypes.ShogiBishop,
                Piece.PieceTypes.ShogiDragonHorse,
                Piece.PieceTypes.ShogiDragonKing,
                Piece.PieceTypes.ShogiQueen,
                Piece.PieceTypes.ShogiDragonKing,
                Piece.PieceTypes.ShogiDragonHorse,
                Piece.PieceTypes.ShogiBishop,
                Piece.PieceTypes.ShogiVerticalMover,
                Piece.PieceTypes.ShogiSideMover,
                Piece.PieceTypes.ShogiDragon,
                Piece.PieceTypes.ShogiRook
            };
            //Row 5 is just pawns
            Piece.PieceTypes[] DaiShogiArray6 =
            {
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiGoBetween,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiGoBetween,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,

            };

            Piece.PieceTypes[] ShogiArray1 =
            {
                Piece.PieceTypes.ShogiLance,
                Piece.PieceTypes.ShogiKnight,
                Piece.PieceTypes.ShogiSilver,
                Piece.PieceTypes.ShogiGold,
                Piece.PieceTypes.ShogiKing,
                Piece.PieceTypes.ShogiGold,
                Piece.PieceTypes.ShogiSilver,
                Piece.PieceTypes.ShogiKnight,
                Piece.PieceTypes.ShogiLance
            };
            Piece.PieceTypes[] ShogiArray2 =
            {
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.ShogiBishop,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Undefined,
                Piece.PieceTypes.Rook,
                Piece.PieceTypes.Undefined
            };
            //Array 3 is all pawns
            
            //TODO: Chu Shogi

        }
    }
}