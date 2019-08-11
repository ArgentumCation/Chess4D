#include <iostream>
#include <type_traits>
#include "Piece.h"
#include "Board.h"
//Todo: Add 4D Capability

// ReSharper disable once CommentTypo
//Todo check for piece movement and update piecemoved
//Todo add castling


namespace Chess4D
{
    static class Program
    {
        private static Piece.Teams _currentPlayer;


        //Special Rules


        // Checks if a piece is attacking it's allies
        // team is the team of the piece making the attack
        // x and y are target coords
    private static bool AttackingAllies(signed char x, signed char y, Piece.Teams team)
        {
            var attackedPiece = Board._board[x, y];
            if (attackedPiece.Team == Piece.Teams.Undefined) return false;
            return attackedPiece.Team == team;
        }

        //Find out if a movement would move through a piece
        // x,y are the target
        // currentX, currentY are coords of current piece
    private static bool MoveThroughPieces(signed char x, signed char y, signed char currentX, signed char currentY)
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
                if (Board._board[x, y].Team != Piece.Teams.Undefined) return true;

                i += directionX;
                j += directionY;
            }

            return false;
        }

    private static bool WithinBounds(signed char x, signed char y)
        {
            if (x >= 0 && y >= 0 && x < Board.BoardSize && y < Board.BoardSize) return true;

            return false;
        }

        // Get a piece given it's coordinates
        // Returns true if piece is valid, false otherwise
    private static bool GetPiece(ref signed char coords)
        {
            if (coords > Board.BoardSize) throw new ArgumentOutOfRangeException(nameof(coords));
            //Prompt user for coordinates
            Console.WriteLine(_currentPlayer + "'s Move? (c,r)");
            var input = Console.ReadLine()?.Split(",");

            //Convert input into coordinates

            //First coord is encoded as 4 LSB
            if (input != null)
            {
                coords = (signed char) (Convert.ToChar(input[0].ToUpper()) - 65);
                //Second coords is encoded as 4 MSB
                coords |= (signed char) ((Convert.ToInt32(input[1]) - 1) << 4);
            }

            //Console.WriteLine("[{0}]", string.Join(", ", coords));

            //check if coord is valid

            //check range
            if ((coords & 0xF) < 0 || (coords & 0xF) >= Board.BoardSize || coords >> 4 < 0 ||
                coords >> 4 >= Board.BoardSize)
                return false;

            //check team
            return Board._board[coords & 0xF, coords >> 4].Team == _currentPlayer;
        }

    private static IEnumerable<signed char> GetMoves(signed char coords)
        {
            IEnumerable<signed char> possibleMoves = new HashSet<signed char>();
            switch (Board._board[coords & 0xF, coords >> 4].Type)
            {
                case Piece.PieceTypes.Rook:
                    possibleMoves = RookMove(coords);
                    break;
                case Piece.PieceTypes.Knight:
                    possibleMoves = KnightMove(coords);
                    break;
                    // ReSharper disable once CommentTypo
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
                    // ReSharper disable once CommentTypo
                    //Todo: en passant / attack move / Promotion
                case Piece.PieceTypes.Pawn:
                    possibleMoves = PawnMove(coords);
                    break;
                case Piece.PieceTypes.Undefined:
                    throw new ArgumentOutOfRangeException();
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
        //todo Check signs
    private static IEnumerable<signed char> KingMove(signed char coords)
        {
            HashSet<signed char> moves = new HashSet<signed char>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            for (var i = -1; i < 2; i++)
                for (var j = -1; j < 2; j++)
                {
                    var x = currentX;
                    var y = currentY;
                    if (WithinBounds(x, y))
                        if (i != 0 || j != 0)
                            moves.Add(Piece.GetCoords(x, y));
                }

            return moves;
        }

        //Todo: test
        //todo check signs
    private static IEnumerable<signed char> RookMove(signed char coords)
        {
            HashSet<signed char> moves = new HashSet<signed char>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            //horizontal moves

            for (signed char i = 0; i < Board.BoardSize; i++)
            {
                var x = i;
                var y = currentY;
                if (x != currentX)
                    if (!AttackingAllies(x, y, team))
                        if (!MoveThroughPieces(x, y, currentX, currentY))
                            moves.Add(Piece.GetCoords(x, y));
            }

            //horizontal moves
            for (signed char i = 0; i < Board.BoardSize; i++)
            {
                var x = i;
                var y = currentY;
                if (x != currentX)
                    if (!AttackingAllies(x, y, team))
                        if (!MoveThroughPieces(x, y, currentX, currentY))
                            moves.Add(Piece.GetCoords(x, y));
            }

            return moves;
        }

        //Todo: test
        //todo check signs
    private static IEnumerable<signed char> BishopMove(signed char coords)
        {
            HashSet<signed char> moves = new HashSet<signed char>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            //Diagonal
            for (signed char i = 0; i < Board.BoardSize; i++)
            {
                var x = i;
                var y = (signed char) (currentY - (currentX - i));
                if (x != currentX)
                    if (WithinBounds(x, y))
                        if (!AttackingAllies(x, y, team))
                            if (!MoveThroughPieces(x, y, currentX, currentY))
                                moves.Add(Piece.GetCoords(x, y));
            }

            for (signed char i = 0; i < Board.BoardSize; i++)
            {
                var x = (signed char) (currentX + (currentY - i));
                var y = i;
                if (x != currentX)
                    if (WithinBounds(x, y))
                        if (!AttackingAllies(x, y, team))
                            if (!MoveThroughPieces(x, y, currentX, currentY))
                                moves.Add(Piece.GetCoords(x, y));
            }

            return moves;
        }

        //Todo:test
        //todo check signs
    private static IEnumerable<signed char> QueenMove(signed char coords)
        {
            var possibleMoves = RookMove(coords);
            possibleMoves = possibleMoves.Union(BishopMove(coords));
            return possibleMoves;
        }

        //Todo Test:
        //todo check signs
    private static IEnumerable<signed char> KnightMove(signed char coords)
        {
            HashSet<signed char> moves = new HashSet<signed char>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;

            for (signed char i = -2; i < 3; i += 4)
                for (signed char j = -1; j < 2; j += 2)
                {
                    var x = (signed char) (i + currentX);
                    var y = (signed char) (j + currentY);
                    if (!AttackingAllies(x, y, team))
                        if (WithinBounds(x, y))
                            moves.Add(Piece.GetCoords(x, y));
                }

            for (signed char i = -1; i < 2; i += 2)
                for (signed char j = -2; j < 3; j += 4)
                {
                    var x = (signed char) (i + currentX);
                    var y = (signed char) (j + currentY);
                    if (!AttackingAllies(x, y, team))
                        if (WithinBounds(x, y))
                            moves.Add(Piece.GetCoords(x, y));
                }

            return moves;
        }

        //Todo test
    private static IEnumerable<signed char> PawnMove(signed char coords)
        {
            HashSet<signed char> moves = new HashSet<signed char>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;

            signed char x;
            signed char y;
            //Diagonal Moves
            for (signed char i = -1; i < 2; i += 2)
            {
                x = (signed char) (currentX + i);
                y = (signed char) (team == Piece.Teams.White ? currentY + 1 : currentY - 1);

                //makes board reads safe
                if (WithinBounds(x, y))
                {
                    var attacking = Board._board[x, y];
                    // if there's an piece in killing range that isn't an ally
                    if (attacking.Type != Piece.PieceTypes.Undefined && !AttackingAllies(x, y, team))
                        moves.Add(Piece.GetCoords(x, y));
                }
            }

            //Forward Move
            x = currentX;
            y = (signed char) (team == Piece.Teams.White ? currentY + 1 : currentY - 1);
            //if no piece and within bounds
            if (WithinBounds(x, y) && Board._board[x, y].Team == Piece.Teams.Undefined)
                moves.Add(Piece.GetCoords(x, y));
            //En Passant (should be merged with 
            if (team == Piece.Teams.White && currentY == Board.BoardSize / 2 - 1 ||
                team == Piece.Teams.Black && currentY == Board.BoardSize / 2)
                moves.Add(Piece.GetCoords(x, y));
            //Mid Board Jump
            //Todo verify with 16x16
            if (Board._board[currentX, currentY].FirstMove)
            {
                var targetY = Board.BoardSize / 2 + (team == Piece.Teams.White ? -1 : 0);
                if (team == Piece.Teams.White)
                {
                    for (signed char i = y; i <= targetY; i ++)
                    {
                        if (!Board.IsPieceAt(x, i) && WithinBounds(x, i))
                            if (!MoveThroughPieces(x, i, currentX, currentY))
                                moves.Add(Piece.GetCoords(x, i));
                    }

                }
                else
                {
                    for (signed char i = y; i >= targetY; i--)
                    {
                        if (!Board.IsPieceAt(x, i) && WithinBounds(x, i))
                            if (!MoveThroughPieces(x, i, currentX, currentY))
                                moves.Add(Piece.GetCoords(x, i));
                    }
                }


            }

            return moves;
        }

        //Todo Test
    private static IEnumerable<signed char> SuperKnightMoves(signed char coords)
        {
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            var possibles = new HashSet<signed char>()
            {
                //camel
                Piece.GetCoords((signed char) (currentX+3),(signed char) (currentY+1)),
                        Piece.GetCoords((signed char) (currentX+3),(signed char) (currentY-1)),
                        Piece.GetCoords((signed char) (currentX-3),(signed char) (currentY+1)),
                        Piece.GetCoords((signed char) (currentX-3),(signed char) (currentY-1)),
                        Piece.GetCoords((signed char) (currentX+1),(signed char) (currentY+3)),
                        Piece.GetCoords((signed char) (currentX+1),(signed char) (currentY-3)),
                        Piece.GetCoords((signed char) (currentX-1),(signed char) (currentY+3)),
                        Piece.GetCoords((signed char) (currentX-1),(signed char) (currentY-3)),
                        //Zebra
                        Piece.GetCoords((signed char) (currentX+3),(signed char) (currentY+2)),
                        Piece.GetCoords((signed char) (currentX+3),(signed char) (currentY-2)),
                        Piece.GetCoords((signed char) (currentX-3),(signed char) (currentY+2)),
                        Piece.GetCoords((signed char) (currentX-3),(signed char) (currentY-2)),
                        Piece.GetCoords((signed char) (currentX+2),(signed char) (currentY+3)),
                        Piece.GetCoords((signed char) (currentX+2),(signed char) (currentY-3)),
                        Piece.GetCoords((signed char) (currentX-2),(signed char) (currentY+3)),
                        Piece.GetCoords((signed char) (currentX-2),(signed char) (currentY-3)),
            };
            var moves = (HashSet<signed char>) KnightMove(coords);
            foreach (var move in possibles)
            {
                if (!AttackingAllies(Piece.GetX(move), Piece.GetY(move), team) &&
                    WithinBounds(Piece.GetX(move), Piece.GetY(move)))
                {
                    moves.Add(move);
                }
            }

            return moves;
        }


    private static IEnumerable<signed char> RoseMove(signed char coords)
        {
            throw new NotImplementedException();
        }


        // ReSharper disable once InconsistentNaming
        //Todo Test
    private static IEnumerable<signed char> WFAMove(signed char coords)
        {
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            var possibles = new HashSet<signed char>()
            {
                //alfil
                Piece.GetCoords((signed char) (currentX+2),(signed char) (currentY+2)),
                        Piece.GetCoords((signed char) (currentX+2),(signed char) (currentY-2)),
                        Piece.GetCoords((signed char) (currentX-2),(signed char) (currentY+2)),
                        Piece.GetCoords((signed char) (currentX-2),(signed char) (currentY-2)),
            };
            var moves = (HashSet<signed char>) KingMove(coords);
            foreach (var move in possibles)
            {
                if (!AttackingAllies(Piece.GetX(move), Piece.GetY(move), team) &&
                    WithinBounds(Piece.GetX(move), Piece.GetY(move)))
                {
                    moves.Add(move);
                }
            }

            return moves;;
        }

        // ReSharper disable once InconsistentNaming
        //Todo test
    private static IEnumerable<signed char> FDMove(signed char coords)
        {
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            var possibles = new HashSet<signed char>()
            {
                Piece.GetCoords((signed char) (currentX-2),currentY),
                        Piece.GetCoords((signed char) (currentX-1),(signed char) (currentY-1)),
                        Piece.GetCoords(currentX,(signed char) (currentY-2)),
                        Piece.GetCoords((signed char) (currentX+1),(signed char) (currentY-1)),
                        Piece.GetCoords((signed char) (currentX+2),currentY),
                        Piece.GetCoords((signed char) (currentX+1), (signed char) (currentY+1)),
                        Piece.GetCoords(currentX,(signed char) (currentY+2)),
                        Piece.GetCoords((signed char) (currentX-1),(signed char) (currentY+1))
            };
            var moves = new HashSet<signed char>();
            foreach (var move in possibles)
            {
                if (!AttackingAllies(Piece.GetX(move), Piece.GetY(move), team) &&
                    WithinBounds(Piece.GetX(move), Piece.GetY(move)))
                {
                    moves.Add(move);
                }
            }

            return moves;
        }


    public static void Main(string[] args)
        {
//            RenderWindow window = new RenderWindow(new VideoMode(200,200), "test");
//            CircleShape cs = new CircleShape(100.0f);
//            cs.FillColor = Color.Green;
//            window.SetActive();
//            while (window.IsOpen)
//            {
//                window.Clear();
//                window.DispatchEvents();
//                window.Draw(cs);
//                window.Display();
//            }
            _currentPlayer = Piece.Teams.White;
            //Set Up Board
            Board.SetUpBoard();
            Board.PrintBoard();
            //Prompt Player for move\
            var moveValid = false;
            while (!moveValid)
            {
                signed char coords = 0x00;
                // Select a piece
                moveValid = GetPiece(ref coords);
                if (moveValid)
                {
                    //find moves
                    var moves = GetMoves(coords);
                    moveValid = SelectMove(Board._board[Piece.GetX(coords), Piece.GetY(coords)], moves);
                }
            }
        }

    private static bool SelectMove(Piece p, IEnumerable<signed char> moves)
        {
            foreach (var v in moves)
            {
                p.FirstMove = false; //Todo: move this after move is selected
                Console.Write(Piece.CoordToString(v));
            }

            throw new NotImplementedException();
        }
    };

   

            
}



int main() {
    std::cout << "Hello, World!" << std::endl;
    return 0;
}