﻿using System;
using System.Collections.Generic;
using System.Linq;


//Todo: Add 4D Capability
namespace Chess4D
{
    internal class Program
    {
        private static Piece.Teams currentPlayer;

        // Checks if a piece is attacking it's allies
        // team is the team of the piece making the attack
        // x and y are target coords
        private static bool AttackingAllies(sbyte x, sbyte y, Piece.Teams team)
        {
            var attackedPiece = Board._board[x, y];
            if (attackedPiece.team != Piece.Teams.Undefined)
                if (attackedPiece.team == team)
                    return true;
            return false;
        }

        //Find out if a movement would move through a piece
        // x,y are the target
        // currentX, currentY are coords of current piece
        private static bool MoveThroughPieces(sbyte x, sbyte y, sbyte currentX, sbyte currentY)
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

        private static bool WithinBounds(sbyte x, sbyte y)
        {
            if (x >= 0 && y >= 0 && x < Board.BoardSize && y < Board.BoardSize)
            {
                return true;
            }

            return false;
        }
        
        // Get a piece given it's coordinates
        // Returns true if piece is valid, false otherwise
        private static bool GetPiece(ref sbyte coords)
        {
            if (coords > Board.BoardSize) throw new ArgumentOutOfRangeException(nameof(coords));
            //Prompt user for coordinates
            Console.WriteLine(currentPlayer + "'s Move? (c,r)");
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
            if ((coords & 0xF) < 0 || (coords & 0xF) >= Board.BoardSize || coords >> 4 < 0 ||
                coords >> 4 >= Board.BoardSize)
                return false;

            //check team
            if (Board._board[coords & 0xF, coords >> 4].team != currentPlayer) return false;

            return true;
        }

        private static IEnumerable<sbyte> GetMoves(sbyte coords)
        {
            IEnumerable<sbyte> possibleMoves;
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
                    possibleMoves = possibleMoves.Union(KnightMove(coords));
                    break;
                case Piece.PieceTypes.Queen:
                    possibleMoves = QueenMove(coords);
                   
                    break;
                //Todo handle check/checkmate
                case Piece.PieceTypes.King:
                    possibleMoves = KingMove(coords);
                    break;
                case Piece.PieceTypes.CRRBChancellor:
                    possibleMoves = RookMove(coords);
                    possibleMoves = possibleMoves.Union(KnightMove(coords));
                    break;
                //Todo: decompose into knight, camel, and zebra
                case Piece.PieceTypes.CRRBSuperKnight:
                    possibleMoves = SuperKnightMoves(coords);
                    break;
                //Todo: en passant / attack move / Promotion
                case Piece.PieceTypes.Pawn:
                    possibleMoves = PawnMove(coords);
                    break;
                case Piece.PieceTypes.Undefined:
                    throw new ArgumentOutOfRangeException();
                    break;
                case Piece.PieceTypes.ShogiAngryBoar:
                    break;
                case Piece.PieceTypes.ShogiBishop:
                    break;
                case Piece.PieceTypes.ShogiTiger:
                    break;
                case Piece.PieceTypes.ShogiNekomata:
                    break;
                case Piece.PieceTypes.ShogiCopper:
                    break;
                case Piece.PieceTypes.ShogiDragonHorse:
                    break;
                case Piece.PieceTypes.ShogiDragonKing:
                    break;
                case Piece.PieceTypes.ShogiElephant:
                    break;
                case Piece.PieceTypes.ShogiWolf:
                    break;
                case Piece.PieceTypes.ShogiLeopard:
                    break;
                case Piece.PieceTypes.ShogiDragon:
                    break;
                case Piece.PieceTypes.ShogiFlyingOx:
                    break;
                case Piece.PieceTypes.ShogiStag:
                    break;
                case Piece.PieceTypes.ShogiFreeBoar:
                    break;
                case Piece.PieceTypes.ShogiGoBetween:
                    break;
                case Piece.PieceTypes.ShogiGold:
                    break;
                case Piece.PieceTypes.ShogiFalcon:
                    break;
                case Piece.PieceTypes.ShogiIron:
                    break;
                case Piece.PieceTypes.ShogiKing:
                    break;
                case Piece.PieceTypes.ShogiKirin:
                    break;
                case Piece.PieceTypes.ShogiKnight:
                    break;
                case Piece.PieceTypes.ShogiLance:
                    break;
                case Piece.PieceTypes.ShogiLion:
                    break;
                case Piece.PieceTypes.ShogiPawn:
                    break;
                case Piece.PieceTypes.ShogiPhoenix:
                    break;
                case Piece.PieceTypes.ShogPrince:
                    break;
                case Piece.PieceTypes.ShogiChariot:
                    break;
                case Piece.PieceTypes.ShogiRook:
                    break;
                case Piece.PieceTypes.ShogiSideMover:
                    break;
                case Piece.PieceTypes.ShogiSilver:
                    break;
                case Piece.PieceTypes.ShogiStone:
                    break;
                case Piece.PieceTypes.ShogiEagle:
                    break;
                case Piece.PieceTypes.ShogiVerticalMover:
                    break;
                case Piece.PieceTypes.ShogiViolentOx:
                    break;
                case Piece.PieceTypes.ShogiWhale:
                    break;
                case Piece.PieceTypes.ShogiWhiteHorse:
                    break;
                case Piece.PieceTypes.ShogiQueen:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //Todo: remove duplicates
            //Todo: king move
            return possibleMoves;
        }

        
        // Get possible moves for a piece
        //Todo: finish vanilla, CRRB
        //Todo: add shogi
        
        //Vanilla Chess
        
        //Todo: test
        //Todo: Check(mate)
        private static IEnumerable<sbyte> KingMove(sbyte coords)
        {
            IEnumerable<sbyte> moves = new List<sbyte>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].team;
            for (var i = -1; i < 2; i++)
            {
                for (var j = -1; j < 2; i++)
                {
                    var x = currentX;
                    var y = currentY;
                    if (WithinBounds(x, y))
                    {
                        if (i != 0 || j != 0)
                        {
                            moves.Append(Piece.GetCoords(x, y));
                        }
                        
                    }
                }
            }
            return moves;
        }
        
        //Todo: test
        private static IEnumerable<sbyte> RookMove(sbyte coords)
        {
            IEnumerable<sbyte> moves = new List<sbyte>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].team;
            //horizontal moves
            
            for (sbyte i = 0; i < Board.BoardSize; i++)
            {
                var x = i;
                var y = currentY;
                if (x != currentX)
                    if (!AttackingAllies(x, y, team))
                        if (!MoveThroughPieces(x, y, currentX, currentY))
                            moves.Append(Piece.GetCoords(x, y));
            }

            //horizontal moves
            for (sbyte i = 0; i < Board.BoardSize; i++)
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
        //Todo: test
        private static IEnumerable<sbyte> BishopMove(sbyte coords)
        {
            IEnumerable<sbyte> moves = new List<sbyte>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].team;
            //Diagonal
            for (sbyte i = 0; i < Board.BoardSize; i++)
            {
                sbyte x = i;
                sbyte y = (sbyte)(currentY - (currentX - i));
                if (x != currentX)
                {
                    if (WithinBounds(x, y))
                    {
                        if (!AttackingAllies(x, y, team))
                        {
                            if (!MoveThroughPieces(x, y, currentX, currentY))
                            {
                                moves.Append(Piece.GetCoords(x, y));
                            }
                        }
                    }
                }
            }

            for (sbyte i = 0; i < Board.BoardSize; i++)
            {
                sbyte x =  (sbyte) (currentX + (currentY - i));
                var y = i;
                if(x != currentX)
                {
                    if (WithinBounds(x, y))
                    {
                        if (!AttackingAllies(x, y, team))
                        {
                            if (!MoveThroughPieces(x, y, currentX, currentY))
                            {
                                moves.Append(Piece.GetCoords(x,y));
                            }
                        }
                    }
                }
            }

            return moves;
        }
        
        //Todo:test
        private static IEnumerable<sbyte> QueenMove(sbyte coords)
        {
            IEnumerable<sbyte> possibleMoves = RookMove(coords);
            possibleMoves = possibleMoves.Union(BishopMove(coords));
            return possibleMoves;
        }
        
        private static IEnumerable<sbyte> KnightMove(sbyte coords)
        {
            IEnumerable<sbyte> moves = new List<sbyte>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].team;

            for (sbyte i = -2; i < 3; i += 4)
            {
                for (sbyte j = -1; j < 2; j += 2)
                {
                    sbyte x = (sbyte)(i + currentX);
                    sbyte y = (sbyte) (j + currentY);
                    if (!AttackingAllies(x, y, team))
                    {
                        if (WithinBounds(x, y))
                        {
                            moves.Append(Piece.GetCoords(x, y));
                        }
                    }
                }
            }
            for (sbyte i = -1; i < 2; i += 2)
            {
                for (sbyte j = -2; j < 3; j += 4)
                {
                    sbyte x = (sbyte)(i + currentX);
                    sbyte y = (sbyte) (j + currentY);
                    if (!AttackingAllies(x, y, team))
                    {
                        if (WithinBounds(x, y))
                        {
                            moves.Append(Piece.GetCoords(x, y));
                        }
                    }
                }
            }

            return moves;
        }
        
        private static IEnumerable<sbyte> PawnMove(sbyte coords)
        {
            IEnumerable<sbyte> moves = new List<sbyte>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].team;
            
            for (sbyte i = -1; i < 2; i += 2)
            {
                var x = (sbyte)(currentX + i);
                var y = (sbyte) ((team == Piece.Teams.White) ? currentY - 1 : currentY + 1);

                var attacking = Board._board[x, y];
                // if there's an piece in killing range that isn't an ally
                if ((attacking.type != Piece.PieceTypes.Undefined) && !AttackingAllies(x, y, team))
                {
                    moves.Append(Piece.GetCoords(x, y));
                }
            }

            var x = currentX;
            var y = (sbyte) ((team == Piece.Teams.White) ? currentY - 1 : currentY + 1);
            //if no piece and within bounds
            if (WithinBounds(x, y) && Board._board[x, y].team != Piece.Teams.Undefined)
            {
                moves.Append(Piece.GetCoords(x, y));
            }

            //todo: finish
            if (Board._board[currentX, currentY].FirstMove)
            {
                y = (sbyte) ((team == Piece.Teams.White) ? currentY - 2 : currentY + 2);
            }
            throw new NotImplementedException();
        }
        
       
        
        private static IEnumerable<sbyte> SuperKnightMoves(sbyte coords)
        {
            throw new NotImplementedException();
        }
        
      
        
        private static IEnumerable<sbyte> RoseMove(sbyte coords)
        {
            throw new NotImplementedException();
        }
        
        
        
        private static IEnumerable<sbyte> WFAMove(sbyte coords)
        {
            throw new NotImplementedException();
        }
        
        private static IEnumerable<sbyte> FDMove(sbyte coords)
        {
            throw new NotImplementedException();
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
                sbyte coords = 0x00;
                // Select a piece
                moveValid = GetPiece(ref coords);
                if (moveValid)
                {
                    //find moves
                    var moves = GetMoves(coords);
                    moveValid = SelectMove(moves);
                }
            }
        }

        private static bool SelectMove(IEnumerable<sbyte> moves)
        {
            throw new NotImplementedException();
        }
    }

    internal class Piece
    {
        public bool FirstMove = true;
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

        public static sbyte GetX(sbyte b)
        {
            return (sbyte) (b & 0xF);
        }

        public static sbyte GetY(sbyte b)
        {
            return (sbyte) (b >> 4);
        }

        public static sbyte GetCoords(sbyte x, sbyte y)
        {
            return (sbyte) ((y << 4) | (x & 0xF));
        }
        //TODO: Add Shogi Pieces
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
                case PieceTypes.ShogiAngryBoar:
                    break;
                case PieceTypes.ShogiBishop:
                    break;
                case PieceTypes.ShogiTiger:
                    break;
                case PieceTypes.ShogiNekomata:
                    break;
                case PieceTypes.ShogiCopper:
                    break;
                case PieceTypes.ShogiDragonHorse:
                    break;
                case PieceTypes.ShogiDragonKing:
                    break;
                case PieceTypes.ShogiElephant:
                    break;
                case PieceTypes.ShogiWolf:
                    break;
                case PieceTypes.ShogiLeopard:
                    break;
                case PieceTypes.ShogiDragon:
                    break;
                case PieceTypes.ShogiFlyingOx:
                    break;
                case PieceTypes.ShogiStag:
                    break;
                case PieceTypes.ShogiFreeBoar:
                    break;
                case PieceTypes.ShogiGoBetween:
                    break;
                case PieceTypes.ShogiGold:
                    break;
                case PieceTypes.ShogiFalcon:
                    break;
                case PieceTypes.ShogiIron:
                    break;
                case PieceTypes.ShogiKing:
                    break;
                case PieceTypes.ShogiKirin:
                    break;
                case PieceTypes.ShogiKnight:
                    break;
                case PieceTypes.ShogiLance:
                    break;
                case PieceTypes.ShogiLion:
                    break;
                case PieceTypes.ShogiPawn:
                    break;
                case PieceTypes.ShogiPhoenix:
                    break;
                case PieceTypes.ShogPrince:
                    break;
                case PieceTypes.ShogiChariot:
                    break;
                case PieceTypes.ShogiRook:
                    break;
                case PieceTypes.ShogiSideMover:
                    break;
                case PieceTypes.ShogiSilver:
                    break;
                case PieceTypes.ShogiStone:
                    break;
                case PieceTypes.ShogiEagle:
                    break;
                case PieceTypes.ShogiVerticalMover:
                    break;
                case PieceTypes.ShogiViolentOx:
                    break;
                case PieceTypes.ShogiWhale:
                    break;
                case PieceTypes.ShogiWhiteHorse:
                    break;
                case PieceTypes.ShogiQueen:
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
        public enum GameTypes
        {
            Chess,

            // ReSharper disable once InconsistentNaming
            CRRB,
            Shogi,
            ChuShogi,
            DaiShogi
        }

        //Board Size, changing this will probably break something
        //But we may need to change this to 48 when we start working on mapping the board to other surfaces
        public const sbyte BoardSize = 8;

        // ReSharper disable once InconsistentNaming
        public static readonly Piece[,] _board;

        public static GameTypes GameType = GameTypes.Chess;

        static Board()
        {
            _board = new Piece[BoardSize, BoardSize];
            for (sbyte x = 0; x < BoardSize; x++)
            for (sbyte y = 0; y < BoardSize; y++)
                _board[x, y] = new Piece();
        }

        // Print the board to console
        // TODO: make this handle vanilla chess first
        public static void PrintBoard()
        {
            //Print Column Labels
            Console.Write("   ||");
            for (var i = 0; i < BoardSize; i++) Console.Write(" " + (char) (i + 65) + " |");
            //Print line break
            Console.WriteLine();

            for (var i = 0; i < 5 + 4 * BoardSize; i++) Console.Write('-');
            Console.WriteLine();
            for (var y = BoardSize; y > 0; y--)
            {
                Console.Write(y);
                Console.Write(y >= 10 ? " ||" : "  ||");
                for (sbyte x = 0; x < BoardSize; x++) Console.Write(" " + _board[x, y - 1] + " |");

                Console.WriteLine();
            }
        }


        // Performs initial setup of the board
        //TODO: Test DaiShogi
        public static void SetUpBoard()
        {
            //Chess on a Really Big Board Setup
            switch (GameType)
            {
                case GameTypes.Chess:
                    Piece.PieceTypes[] chesspieceArray =
                    {
                        Piece.PieceTypes.Rook,
                        Piece.PieceTypes.Knight,
                        Piece.PieceTypes.Bishop,
                        Piece.PieceTypes.Queen,
                        Piece.PieceTypes.King,
                        Piece.PieceTypes.Bishop,
                        Piece.PieceTypes.Knight,
                        Piece.PieceTypes.Rook
                    };

                    for (sbyte x = 0; x < BoardSize; x++)
                    {
                        _board[x, 0] = new Piece(chesspieceArray[x], Piece.Teams.White);
                        _board[x, 1] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.White);
                        _board[x, 7] = new Piece(chesspieceArray[x], Piece.Teams.Black);
                        _board[x, 6] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.Black);
                    }

                    break;

                case GameTypes.CRRB:
                    // ReSharper disable once InconsistentNaming
                    Piece.PieceTypes[] CRRBPieceArray =
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

                    for (sbyte x = 0; x < BoardSize; x++)
                    {
                        _board[x, 0] = new Piece(CRRBPieceArray[x], Piece.Teams.White);
                        _board[x, 1] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.White);
                        _board[x, 15] = new Piece(CRRBPieceArray[x], Piece.Teams.Black);
                        _board[x, 14] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.Black);
                    }

                    break;

                case GameTypes.Shogi:
                    Piece.PieceTypes[] shogiArray1 =
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
                    Piece.PieceTypes[] shogiArray2 =
                    {
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn
                    };
                    Piece.PieceTypes[] shogiArray3 =
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
                    Piece.PieceTypes[][] shogiGrid = {shogiArray1, shogiArray2, shogiArray3};

                    for (var x = 0; x < BoardSize; x++)
                    for (var y = 0; y < shogiGrid.Length; y++)
                    {
                        _board[x, y] = new Piece(shogiGrid[y][x], Piece.Teams.Black);
                        _board[x, BoardSize - y - 1] = new Piece(shogiGrid[y][x], Piece.Teams.White);
                    }

                    break;
                case GameTypes.ChuShogi:
                    throw new NotImplementedException();
                    break;
                case GameTypes.DaiShogi:
                    //Dai Shogi
                    Piece.PieceTypes[] daiShogiArray1 =
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
                    Piece.PieceTypes[] daiShogiArray2 =
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
                    Piece.PieceTypes[] daiShogiArray3 =
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
                    Piece.PieceTypes[] daiShogiArray4 =
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
                    Piece.PieceTypes[] daiShogiArray5 =
                    {
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn,
                        Piece.PieceTypes.ShogiPawn
                    };
                    Piece.PieceTypes[] daiShogiArray6 =
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
                        Piece.PieceTypes.Undefined
                    };
                    Piece.PieceTypes[][] daiShogiGrid =
                    {
                        daiShogiArray1, daiShogiArray2, daiShogiArray3, daiShogiArray4, daiShogiArray5, daiShogiArray6
                    };

                    for (var x = 0; x < BoardSize; x++)
                    for (var y = 0; y < daiShogiGrid.Length; y++)
                    {
                        _board[x, y] = new Piece(daiShogiGrid[y][x], Piece.Teams.Black);
                        _board[x, BoardSize - y - 1] = new Piece(daiShogiGrid[y][x], Piece.Teams.White);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            //TODO: Chu Shogi
        }
    }
}