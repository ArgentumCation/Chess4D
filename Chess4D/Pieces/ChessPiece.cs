using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess4D
{
    //Chess
    public class Rook : Piece
    {
        public Rook()
        {
            Type = PieceTypes.Rook;
        }

        public Rook(Teams team) : base(team)
        {
            Type = PieceTypes.Rook;
        }
        //Todo: test
        //todo check signs

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            var moves = new HashSet<sbyte>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            //horizontal moves

            for (sbyte i = 0; i < Board.BoardSize; i++)
            {
                var x = i;
                var y = currentY;
                if (x != currentX)
                    if (!AttackingAllies(x, y, team))
                        if (!MoveThroughPieces(x, y, currentX, currentY))
                            moves.Add(Piece.GetCoords(x, y));
            }

            //horizontal moves
            for (sbyte i = 0; i < Board.BoardSize; i++)
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

        public override string ToString()
        {
            return "　r ";
        }
    }

    public class Knight : Piece
    {
        public Knight()
        {
            Type = PieceTypes.Knight;
        }

        public Knight(Teams team) : base(team)
        {
            Type = PieceTypes.Knight;
        }

        //Todo Test:
        //todo check signs
        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            var moves = new HashSet<sbyte>();
            var currentX = GetX(coords);
            var currentY = GetY(coords);
            var team = Board._board[currentX, currentY].Team;

            for (sbyte i = -2; i < 3; i += 4)
            for (sbyte j = -1; j < 2; j += 2)
            {
                var x = (sbyte) (i + currentX);
                var y = (sbyte) (j + currentY);
                if (!AttackingAllies(x, y, team))
                    if (WithinBounds(x, y))
                        moves.Add(GetCoords(x, y));
            }

            for (sbyte i = -1; i < 2; i += 2)
            for (sbyte j = -2; j < 3; j += 4)
            {
                var x = (sbyte) (i + currentX);
                var y = (sbyte) (j + currentY);
                if (!AttackingAllies(x, y, team))
                    if (WithinBounds(x, y))
                        moves.Add(GetCoords(x, y));
            }

            return moves;
        }

        public override string ToString()
        {
            return "　n ";
        }
    }

    public class Bishop : Piece
    {        
        public Bishop()
        {
            Type = PieceTypes.Bishop;
        }

        public Bishop(Teams team) : base(team)
        {
            Type = PieceTypes.Bishop;
        }
        
        //Todo: test
        //todo check signs

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            var moves = new HashSet<sbyte>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            //Diagonal
            for (sbyte i = 0; i < Board.BoardSize; i++)
            {
                var x = i;
                var y = (sbyte) (currentY - (currentX - i));
                if (x != currentX)
                    if (WithinBounds(x, y))
                        if (!AttackingAllies(x, y, team))
                            if (!MoveThroughPieces(x, y, currentX, currentY))
                                moves.Add(Piece.GetCoords(x, y));
            }

            for (sbyte i = 0; i < Board.BoardSize; i++)
            {
                var x = (sbyte) (currentX + (currentY - i));
                var y = i;
                if (x != currentX)
                    if (WithinBounds(x, y))
                        if (!AttackingAllies(x, y, team))
                            if (!MoveThroughPieces(x, y, currentX, currentY))
                                moves.Add(Piece.GetCoords(x, y));
            }

            return moves;
        }

        public override string ToString()
        {
            return "　b ";
        }
    }

    public class Queen : Piece
    {
        public Queen()
        {
            Type = PieceTypes.Queen;
        }

        public Queen(Teams team) : base(team)
        {
            Type = PieceTypes.Queen;
        }

        //Todo:test
        //todo check signs

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            var bishopMoves = PieceFactory.GetPiece(PieceTypes.Bishop, Team).Move(coords);
            var knightMoves = PieceFactory.GetPiece(PieceTypes.Knight, Team).Move(coords);
            return bishopMoves.Union(knightMoves);
        }

        public override string ToString()
        {
            return "　q ";
        }
    }

//Todo handle check/checkmate
    public class King : Piece
    {
        public King()
        {
            Type = PieceTypes.King;
        }

        public King(Teams team) : base(team)
        {
            Type = PieceTypes.King;
        }

        //Todo: test
        //Todo: Check(mate)
        //todo Check signs
        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            var moves = new HashSet<sbyte>();
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

        public override string ToString()
        {
            return "　k ";
        }
    }

//Todo: en passant / attack move / Promotion
    public class Pawn : Piece
    {
        public Pawn()
        {
            Type = PieceTypes.Pawn;
        }

        public Pawn(Teams team) : base(team)
        {
            Type = PieceTypes.Pawn;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
             {
            var moves = new HashSet<sbyte>();
            var currentX = Piece.GetX(coords);
            var currentY = Piece.GetY(coords);
            var team = Board._board[currentX, currentY].Team;

            sbyte x;
            sbyte y;
            //Diagonal Moves
            for (sbyte i = -1; i < 2; i += 2)
            {
                x = (sbyte) (currentX + i);
                y = (sbyte) (team == Teams.White ? currentY + 1 : currentY - 1);

                //makes board reads safe
                if (!WithinBounds(x, y)) continue;
                var attacking = Board._board[x, y];
                // if there's an piece in killing range that isn't an ally
                if (attacking.Type != PieceTypes.Undefined &&
                    !AttackingAllies(x, y, team))
                    moves.Add(Piece.GetCoords(x, y));
            }

            //Forward Move
            x = currentX;
            y = (sbyte) (team == Teams.White ? currentY + 1 : currentY - 1);
            //if no piece and within bounds
            if (WithinBounds(x, y) && Board._board[x, y].Team == Teams.Undefined)
                moves.Add(Piece.GetCoords(x, y));
            //En Passant (should be merged with 
            if (team == Teams.White && currentY == Board.BoardSize / 2 - 1 ||
                team == Teams.Black && currentY == Board.BoardSize / 2)
                moves.Add(Piece.GetCoords(x, y));
            //Mid Board Jump
            //Todo verify with 16x16
            if (Board._board[currentX, currentY].FirstMove)
            {
                var targetY = Board.BoardSize / 2 + (team == Teams.White ? -1 : 0);
                if (team == Teams.White)
                {
                    for (var i = y; i <= targetY; i++)
                        if (!Board.IsPieceAt(x, i) && WithinBounds(x, i))
                            if (!MoveThroughPieces(x, i, currentX, currentY))
                                moves.Add(Piece.GetCoords(x, i));
                }
                else
                {
                    for (var i = y; i >= targetY; i--)
                        if (!Board.IsPieceAt(x, i) && WithinBounds(x, i))
                            if (!MoveThroughPieces(x, i, currentX, currentY))
                                moves.Add(Piece.GetCoords(x, i));
                }
            }

            return moves;
        }

        public override string ToString()
        {
            return "　p ";
        }
    }
}