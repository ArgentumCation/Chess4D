using System.Collections.Generic;

namespace Chess4D
{
    public class ChuShogiTiger : Piece
    {
        public ChuShogiTiger()
        {
            Type = PieceTypes.ChuShogiTiger;
        }

        public ChuShogiTiger(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiTiger;
        }

        public override string ToString()
        {
            return "虎BT";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiCopper : Piece
    {
        public ChuShogiCopper()
        {
            Type = PieceTypes.ChuShogiCopper;
        }

        public ChuShogiCopper(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiCopper;
        }

        public override string ToString()
        {
            return "銅 C";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiDragonHorse : Piece
    {
        public ChuShogiDragonHorse()
        {
            Type = PieceTypes.ChuShogiDragonHorse;
        }

        public ChuShogiDragonHorse(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiDragonHorse;
        }

        public override string ToString()
        {
            return "馬DH";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiDragonKing : Piece
    {
        public ChuShogiDragonKing()
        {
            Type = PieceTypes.ChuShogiDragonKing;
        }

        public ChuShogiDragonKing(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiDragonKing;
        }

        public override string ToString()
        {
            return "龍DK";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiElephant : Piece
    {
        public ChuShogiElephant()
        {
            Type = PieceTypes.ChuShogiElephant;
        }

        public ChuShogiElephant(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiElephant;
        }

        public override string ToString()
        {
            return "象DE";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiLeopard : Piece
    {
        public ChuShogiLeopard()
        {
            Type = PieceTypes.ChuShogiLeopard;
        }

        public ChuShogiLeopard(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiLeopard;
        }

        public override string ToString()
        {
            return "豹FL";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiFlyingOx : Piece
    {
        public ChuShogiFlyingOx()
        {
            Type = PieceTypes.ChuShogiFlyingOx;
        }

        public ChuShogiFlyingOx(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiFlyingOx;
        }

        public override string ToString()
        {
            return "牛FO";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiStag : Piece
    {
        public ChuShogiStag()
        {
            Type = PieceTypes.ChuShogiStag;
        }

        public ChuShogiStag(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiStag;
        }

        public override string ToString()
        {
            return "鹿FS";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiFreeBoar : Piece
    {
        public ChuShogiFreeBoar()
        {
            Type = PieceTypes.ChuShogiFreeBoar;
        }

        public ChuShogiFreeBoar(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiFreeBoar;
        }

        public override string ToString()
        {
            return "猪FB";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiGoBetween : Piece
    {
        public ChuShogiGoBetween()
        {
            Type = PieceTypes.ChuShogiGoBetween;
        }

        public ChuShogiGoBetween(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiGoBetween;
        }

        public override string ToString()
        {
            return "仲GB";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiFalcon : Piece
    {
        public ChuShogiFalcon()
        {
            Type = PieceTypes.ChuShogiFalcon;
        }

        public ChuShogiFalcon(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiFalcon;
        }

        public override string ToString()
        {
            return "鷹HF";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiKirin : Piece
    {
        public ChuShogiKirin()
        {
            Type = PieceTypes.ChuShogiKirin;
        }

        public ChuShogiKirin(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiKirin;
        }

        public override string ToString()
        {
            return "麒KR";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiLion : Piece
    {
        public ChuShogiLion()
        {
            Type = PieceTypes.ChuShogiLion;
        }

        public ChuShogiLion(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiLion;
        }

        public override string ToString()
        {
            return "獅LN";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiPhoenix : Piece
    {
        public ChuShogiPhoenix()
        {
            Type = PieceTypes.ChuShogiPhoenix;
        }

        public ChuShogiPhoenix(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiPhoenix;
        }

        public override string ToString()
        {
            return "鳳PH";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiPrince : Piece
    {
        public ChuShogiPrince()
        {
            Type = PieceTypes.ChuShogiPrince;
        }

        public ChuShogiPrince(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiPrince;
        }
        public override string ToString()
        {
            return "太PR";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiQueen : Piece
    {
        public ChuShogiQueen()
        {
            Type = PieceTypes.ChuShogiQueen;
        }

        public ChuShogiQueen(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiQueen;
        }
        public override string ToString()
        {
            return "奔 Q";
        }
        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiChariot : Piece
    {
        public ChuShogiChariot()
        {
            Type = PieceTypes.ChuShogiChariot;
        }

        public ChuShogiChariot(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiChariot;
        }

        public override string ToString()
        {
            return "反RC";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiSideMover : Piece
    {
        public ChuShogiSideMover()
        {
            Type = PieceTypes.ChuShogiSideMover;
        }

        public ChuShogiSideMover(Teams team) : base(team)
        {Type = PieceTypes.ChuShogiSideMover;
        }

        public override string ToString()
        {
            return "横SM";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiEagle : Piece
    {
        public ChuShogiEagle()
        {
            Type = PieceTypes.ChuShogiEagle;
        }

        public ChuShogiEagle(Teams team) : base(team)
        {Type = PieceTypes.ChuShogiEagle;
        }

        public override string ToString()
        {
            return "鷲SE";
        }
        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiVerticalMover : Piece
    {
        public ChuShogiVerticalMover()
        {Type = PieceTypes.ChuShogiVerticalMover;
        }

        public ChuShogiVerticalMover(Teams team) : base(team)
        {Type = PieceTypes.ChuShogiVerticalMover;
        }

        public override string ToString()
        {
            return "竪VM";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiWhale : Piece
    {
        public ChuShogiWhale()
        {
            Type = PieceTypes.ChuShogiWhale;
        }

        public ChuShogiWhale(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiWhale;
        }

        public override string ToString()
        {
            return "鯨WA";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ChuShogiWhiteHorse : Piece
    {
        public ChuShogiWhiteHorse()
        {
            Type = PieceTypes.ChuShogiWhiteHorse;
        }

        public ChuShogiWhiteHorse(Teams team) : base(team)
        {
            Type = PieceTypes.ChuShogiWhiteHorse;
        }

        public override string ToString()
        {
            return "駒WH";
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new System.NotImplementedException();
        }
    }
}