using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess4D
{
    // Chess on a really big board

    // ReSharper disable once InconsistentNaming
    public class CRRB_FD : Piece
    {
        public CRRB_FD()
        {
            Type = PieceTypes.CRRBFD;
        }

        public CRRB_FD(Teams team) : base(team)
        {
            Type = PieceTypes.CRRBFD;
        }

        //Todo test
        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            var currentX = GetX(coords);
            var currentY = GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            var possibles = new HashSet<sbyte>
            {
                GetCoords((sbyte) (currentX - 2), currentY),
                GetCoords((sbyte) (currentX - 1), (sbyte) (currentY - 1)),
                GetCoords(currentX, (sbyte) (currentY - 2)),
                GetCoords((sbyte) (currentX + 1), (sbyte) (currentY - 1)),
                GetCoords((sbyte) (currentX + 2), currentY),
                GetCoords((sbyte) (currentX + 1), (sbyte) (currentY + 1)),
                GetCoords(currentX, (sbyte) (currentY + 2)),
                GetCoords((sbyte) (currentX - 1), (sbyte) (currentY + 1))
            };
            var moves = new HashSet<sbyte>();
            foreach (var move in possibles)
                if (!AttackingAllies(GetX(move), GetY(move), team) &&
                    WithinBounds(GetX(move), GetY(move)))
                    moves.Add(move);

            return moves;
        }

        public override string ToString()
        {
            return "　d ";
        }
    }

    // ReSharper disable once InconsistentNaming
    public class CRRB_WFA : Piece
    {
        public CRRB_WFA()
        {
            Type = PieceTypes.CRRBWFA;
        }

        public CRRB_WFA(Teams team) : base(team)
        {
            Type = PieceTypes.CRRBWFA;
        }

        //Todo Test
        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            var currentX = GetX(coords);
            var currentY = GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            var possibles = new HashSet<sbyte>
            {
                //alfil
                GetCoords((sbyte) (currentX + 2), (sbyte) (currentY + 2)),
                GetCoords((sbyte) (currentX + 2), (sbyte) (currentY - 2)),
                GetCoords((sbyte) (currentX - 2), (sbyte) (currentY + 2)),
                GetCoords((sbyte) (currentX - 2), (sbyte) (currentY - 2))
            };
            var moves = (HashSet<sbyte>) PieceFactory.GetPiece(PieceTypes.King,team).Move(coords);
            foreach (var move in possibles)
                if (!AttackingAllies(GetX(move), GetY(move), team) &&
                    WithinBounds(GetX(move), GetY(move)))
                    moves.Add(move);

            return moves;
            ;
        }

        public override string ToString()
        {
            return "　w ";
        }
    }

    // ReSharper disable once InconsistentNaming
    public class CRRB_Rose : Piece
    {
        public CRRB_Rose()
        {
            Type = PieceTypes.CRRBRose;
        }

        public CRRB_Rose(Teams team) : base(team)
        {
            Type = PieceTypes.CRRBRose;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "　o ";
        }
    }

    // ReSharper disable once InconsistentNaming
    public class CRRB_Archbishop : Piece
    {
        public CRRB_Archbishop()
        {
            Type = PieceTypes.CRRBArchBishop;
        }

        public CRRB_Archbishop(Teams team) : base(team)
        {
            Type = PieceTypes.CRRBArchBishop;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            var bishopMoves = PieceFactory.GetPiece(PieceTypes.Bishop, Team).Move(coords);
            var knightMoves = PieceFactory.GetPiece(PieceTypes.Knight, Team).Move(coords);
            return bishopMoves.Union(knightMoves);
        }

        public override string ToString()
        {
            return "　a ";
        }
    }

    // ReSharper disable once InconsistentNaming
    public class CRRB_Chancellor : Piece
    {
        public CRRB_Chancellor()
        {
            Type = PieceTypes.CRRBChancellor;
        }

        public CRRB_Chancellor(Teams team) : base(team)
        {
            Type = PieceTypes.CRRBChancellor;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            var rookMoves = PieceFactory.GetPiece(PieceTypes.Rook, Team).Move(coords);
            var knightMoves = PieceFactory.GetPiece(PieceTypes.Knight, Team).Move(coords);
            return rookMoves.Union(knightMoves);
        }

        public override string ToString()
        {
            return "　c ";
        }
    }

    // ReSharper disable once InconsistentNaming
    public class CRRB_SuperKnight : Piece
    {
        public CRRB_SuperKnight()
        {
            Type = PieceTypes.CRRBSuperKnight;
        }

        public CRRB_SuperKnight(Teams team) : base(team)
        {
            Type = PieceTypes.CRRBSuperKnight;
        }

        //Todo Test
        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            var currentX = GetX(coords);
            var currentY = GetY(coords);
            var team = Board._board[currentX, currentY].Team;
            var possibles = new HashSet<sbyte>
            {
                //camel
                GetCoords((sbyte) (currentX + 3), (sbyte) (currentY + 1)),
                GetCoords((sbyte) (currentX + 3), (sbyte) (currentY - 1)),
                GetCoords((sbyte) (currentX - 3), (sbyte) (currentY + 1)),
                GetCoords((sbyte) (currentX - 3), (sbyte) (currentY - 1)),
                GetCoords((sbyte) (currentX + 1), (sbyte) (currentY + 3)),
                GetCoords((sbyte) (currentX + 1), (sbyte) (currentY - 3)),
                GetCoords((sbyte) (currentX - 1), (sbyte) (currentY + 3)),
                GetCoords((sbyte) (currentX - 1), (sbyte) (currentY - 3)),
                //Zebra
                GetCoords((sbyte) (currentX + 3), (sbyte) (currentY + 2)),
                GetCoords((sbyte) (currentX + 3), (sbyte) (currentY - 2)),
                GetCoords((sbyte) (currentX - 3), (sbyte) (currentY + 2)),
                GetCoords((sbyte) (currentX - 3), (sbyte) (currentY - 2)),
                GetCoords((sbyte) (currentX + 2), (sbyte) (currentY + 3)),
                GetCoords((sbyte) (currentX + 2), (sbyte) (currentY - 3)),
                GetCoords((sbyte) (currentX - 2), (sbyte) (currentY + 3)),
                GetCoords((sbyte) (currentX - 2), (sbyte) (currentY - 3))
            };
            var moves = (HashSet<sbyte>) PieceFactory.GetPiece(PieceTypes.Knight, team)
                .Move(coords);
            foreach (var move in possibles)
                if (!AttackingAllies(GetX(move), GetY(move), team) &&
                    WithinBounds(GetX(move), GetY(move)))
                    moves.Add(move);

            return moves;
        }

        public override string ToString()
        {
            return "　j ";
        }
    }
}