using System;
using System.Collections.Generic;

namespace Chess4D
{
    public class ShogiKing : Piece
    {
        public ShogiKing()
        {
            Type = PieceTypes.ShogiKing;
        }

        public ShogiKing(Teams team) : base(team)
        {
            Type = PieceTypes.ShogiKing;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Team == Teams.Black ? "玉 K" : "王 K";
        }
    }

    public class ShogiRook : Piece
    {
        public ShogiRook()
        {
            Type = PieceTypes.ShogiRook;
        }

        public ShogiRook(Teams team) : base(team)
        {
            Type = PieceTypes.ShogiRook;
        }

        

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "飛 R";
        }
    }

    public class ShogiBishop : Piece
    {
        public ShogiBishop()
        {Type = PieceTypes.ShogiBishop;
        }

        public ShogiBishop(Teams team) : base(team)
        {Type = PieceTypes.ShogiBishop;
        }

         

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "角 b";
        }
    }

    public class ShogiGold : Piece
    {
        public ShogiGold()
        {
            Type = PieceTypes.ShogiGold;
        }

        public ShogiGold(Teams team) : base(team)
        {
            Type = PieceTypes.ShogiGold;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "金 g";
        }
    }

    public class ShogiSilver : Piece
    {
        public ShogiSilver()
        {
            Type = PieceTypes.ShogiSilver;
        }

        public ShogiSilver(Teams team) : base(team)
        {
            Type = PieceTypes.ShogiSilver;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "銀 S";
        }
    }

    public class ShogiKnight : Piece
    {
        public ShogiKnight()
        {
            Type = PieceTypes.ShogiKnight;
        }

        public ShogiKnight(Teams team) : base(team)
        {
            Type = PieceTypes.ShogiKnight;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "桂 N";
        }
    }

    public class ShogiLance : Piece
    {
        public ShogiLance()
        {
            Type = PieceTypes.ShogiLance;
        }

        public ShogiLance(Teams team) : base(team)
        {
            Type = PieceTypes.ShogiLance;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "香 L";
        }
    }

    public class ShogiPawn : Piece
    {
        public ShogiPawn()
        {
            Type = PieceTypes.ShogiPawn;
        }

        public ShogiPawn(Teams team) : base(team)
        {
            Type = PieceTypes.ShogiPawn;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "歩 P";
        }
    }
}