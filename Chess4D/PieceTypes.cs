namespace Chess4D
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
            //Shogi
            ShogiKing,
            ShogiRook,
            ShogiBishop,
            ShogiGold,
            ShogiSilver,
            ShogiKnight,         
            ShogiLance,
            ShogiPawn,        
            //Chu Shogi
            ChuShogiTiger,
            ChuShogiCopper,
            ChuShogiDragonHorse,
            ChuShogiDragonKing,
            ChuShogiElephant,
            ChuShogiLeopard,
            ChuShogiFlyingOx,
            ChuShogiStag,
            ChuShogiFreeBoar,
            ChuShogiGoBetween,
            ChuShogiFalcon,
            ChuShogiKirin,
            ChuShogiLion,
            ChuShogiPhoenix,
            ChuShogiPrince,
            ChuShogiQueen,
            ChuShogiChariot,
            ChuShogiSideMover,
            ChuShogiEagle,
            ChuShogiVerticalMover,
            ChuShogiWhale,
            ChuShogiWhiteHorse,
            //Dai Shogi
            DaiShogiAngryBoar,
            DaiShogiCatSword,
            DaiShogiWolf,
            DaiShogiFlyingDragon,
            DaiShogiIron,
            DaiShogiStone,
            DaiShogiViolentOx,
            
            
        }
}