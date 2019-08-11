//
// Created by Ajay Kristipati on 2019-08-08.
//

#ifndef CPP_PIECE_H
#define CPP_PIECE_H

#include <string>

class Piece {
public:

    
    enum class PieceTypes {
        piece_Undefined,

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
    };
    enum class Teams {
        teams_Undefined,
        teams_Black,
        teams_White
    };
    bool FirstMove = true;
    Teams Team;
    PieceTypes type;
    Piece(PieceTypes type, Teams team);
    Piece();
    static std::string CoordToString(signed char coord);
    static signed char GetX(signed char b);
    static signed char GetY(signed char b);
    static signed char GetCoords(signed char x, signed char y);




};
std::ostream& operator<<(std::ostream& out, const Piece& r);
#endif //CPP_PIECE_H
