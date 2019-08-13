using System;
using System.Collections.Generic;

namespace Chess4D
{
    public class DaiShogiAngryBoar : Piece
    {
        public DaiShogiAngryBoar()
        {
            Type = PieceTypes.DaiShogiAngryBoar;
        }

        public DaiShogiAngryBoar(Teams team) : base(team)
        {
            Type = PieceTypes.DaiShogiAngryBoar;
        }

        public override string ToString()
        {
            return "嗔AB";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }
    }

    public class DaiShogiCatSword : Piece
    {
        public DaiShogiCatSword()
        {
            Type = PieceTypes.DaiShogiCatSword;
        }

        public DaiShogiCatSword(Teams team) : base(team)
        {
            Type = PieceTypes.DaiShogiCatSword;
        }

        public override string ToString()
        {
            return "猫CS";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }
    }

    public class DaiShogiWolf : Piece
    {
        public DaiShogiWolf()
        {
            Type = PieceTypes.DaiShogiWolf;
        }

        public DaiShogiWolf(Teams team) : base(team)
        {
            Type = PieceTypes.DaiShogiWolf;
        }

        public override string ToString()
        {
            return "狼EW";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }
    }

    public class DaiShogiFlyingDragon : Piece
    {
        public DaiShogiFlyingDragon()
        {
            Type = PieceTypes.DaiShogiFlyingDragon;
        }

        public DaiShogiFlyingDragon(Teams team) : base(team)
        {
            Type = PieceTypes.DaiShogiFlyingDragon;
        }

        public override string ToString()
        {
            return "❌FD";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }
    }

    public class DaiShogiIron : Piece
    {
        public DaiShogiIron()
        {
            Type = PieceTypes.DaiShogiIron;
        }

        public DaiShogiIron(Teams team) : base(team)
        {
            Type = PieceTypes.DaiShogiIron;
        }

        public override string ToString()
        {
            return "鉄IG";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }
    }

    public class DaiShogiStone : Piece
    {
        public DaiShogiStone()
        {
            Type = PieceTypes.DaiShogiStone;
        }

        public DaiShogiStone(Teams team) : base(team)
        {
            Type = PieceTypes.DaiShogiStone;
        }

        public override string ToString()
        {
            return "石SG";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }
    }

    public class ViolentOx : Piece
    {
        public ViolentOx()
        {
            Type = PieceTypes.DaiShogiViolentOx;
        }

        public ViolentOx(Teams team) : base(team)
        {
            Type = PieceTypes.DaiShogiViolentOx;
        }

        public override string ToString()
        {
            return "猛VO";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new NotImplementedException();
        }
    }
}