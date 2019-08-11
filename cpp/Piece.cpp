//
// Created by Ajay Kristipati on 2019-08-08.
//

#include "Piece.h"
std::string Piece::CoordToString(signed char coord) {
    return "(" + std::string(1,char(GetX(coord) + 65)) + ", " + std::to_string((GetY(coord) + 1)) + ")";
}

Piece::Piece(PieceTypes type, Teams team) {
Team = team;
type = type;
}

Piece::Piece() {
    Team = Teams::teams_Undefined;
    type = PieceTypes::piece_Undefined;
}

signed char Piece::GetX(signed char b) {
    return (signed char) (b & 0xF);
}

signed char Piece::GetY(signed char b) {
    return (signed char) (b >> 4);
}

signed char Piece::GetCoords(signed char x, signed char y) {
    return (signed char) ((y << 4) | (x & 0xF));
}
//Todo handle shogi
std::ostream &operator<<(std::ostream &out, const Piece &r) {
    std::string res = "";
    switch (r.type) {

        case Piece::PieceTypes::piece_Undefined:
            res += " ";
            break;
        case Piece::PieceTypes::Rook:
            res += "r";
            break;
        case Piece::PieceTypes::Knight:
            res += "n";
            break;
        case Piece::PieceTypes::CRRBFD:
            res += "d";
            break;
        case Piece::PieceTypes::CRRBWFA:
            res += "w";
            break;
        case Piece::PieceTypes::Bishop:
            res += "b";
            break;
        case Piece::PieceTypes::CRRBRose:
            res += "o";
            break;
        case Piece::PieceTypes::CRRBArchBishop:
            res += "A";
            break;
        case Piece::PieceTypes::Queen:
            res += "q";
            break;
        case Piece::PieceTypes::King:
            res += "k";
            break;
        case Piece::PieceTypes::CRRBChancellor:
            res += "c";
            break;
        case Piece::PieceTypes::CRRBSuperKnight:
            res += "j";
            break;
        case Piece::PieceTypes::Pawn:
            res += "p";
            break;
        case Piece::PieceTypes::ShogiAngryBoar:
            break;
        case Piece::PieceTypes::ShogiBishop:
            break;
        case Piece::PieceTypes::ShogiTiger:
            break;
        case Piece::PieceTypes::ShogiNekomata:
            break;
        case Piece::PieceTypes::ShogiCopper:
            break;
        case Piece::PieceTypes::ShogiDragonHorse:
            break;
        case Piece::PieceTypes::ShogiDragonKing:
            break;
        case Piece::PieceTypes::ShogiElephant:
            break;
        case Piece::PieceTypes::ShogiWolf:
            break;
        case Piece::PieceTypes::ShogiLeopard:
            break;
        case Piece::PieceTypes::ShogiDragon:
            break;
        case Piece::PieceTypes::ShogiFlyingOx:
            break;
        case Piece::PieceTypes::ShogiStag:
            break;
        case Piece::PieceTypes::ShogiFreeBoar:
            break;
        case Piece::PieceTypes::ShogiGoBetween:
            break;
        case Piece::PieceTypes::ShogiGold:
            break;
        case Piece::PieceTypes::ShogiFalcon:
            break;
        case Piece::PieceTypes::ShogiIron:
            break;
        case Piece::PieceTypes::ShogiKing:
            break;
        case Piece::PieceTypes::ShogiKirin:
            break;
        case Piece::PieceTypes::ShogiKnight:
            break;
        case Piece::PieceTypes::ShogiLance:
            break;
        case Piece::PieceTypes::ShogiLion:
            break;
        case Piece::PieceTypes::ShogiPawn:
            break;
        case Piece::PieceTypes::ShogiPhoenix:
            break;
        case Piece::PieceTypes::ShogPrince:
            break;
        case Piece::PieceTypes::ShogiChariot:
            break;
        case Piece::PieceTypes::ShogiRook:
            break;
        case Piece::PieceTypes::ShogiSideMover:
            break;
        case Piece::PieceTypes::ShogiSilver:
            break;
        case Piece::PieceTypes::ShogiStone:
            break;
        case Piece::PieceTypes::ShogiEagle:
            break;
        case Piece::PieceTypes::ShogiVerticalMover:
            break;
        case Piece::PieceTypes::ShogiViolentOx:
            break;
        case Piece::PieceTypes::ShogiWhale:
            break;
        case Piece::PieceTypes::ShogiWhiteHorse:
            break;
        case Piece::PieceTypes::ShogiQueen:
            break;
        default:
            throw "Invalid Piece Type";
    }

    if (r.Team == Piece::Teams::teams_Black) {
        std::transform(res.begin(), res.end(), res.rbegin(), toupper);
    }
    out << res;
    return out;
}
