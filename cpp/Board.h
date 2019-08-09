//
// Created by Ajay Kristipati on 2019-08-09.
//

#ifndef CPP_BOARD_H
#define CPP_BOARD_H

#include "Piece.h"
static class Board {


    //Board Size, changing this will probably break something
    //But we may need to change this to 48 when we start working on mapping the board to other surfaces

public:
    enum class GameTypes {
        Chess,

        // ReSharper disable once InconsistentNaming
                CRRB,
        Shogi,
        ChuShogi,
        DaiShogi,
        Testing
    };
    static signed char BoardSize;
    static Piece _board;
    static GameTypes GameType = GameTypes::Testing;

    static Board() {
        switch (GameType) {
            case GameTypes::Chess:
                BoardSize = 8;
                break;
            case GameTypes::CRRB:
                BoardSize = 16;
                break;
            case GameTypes::Shogi:
                BoardSize = 9;
                break;
            case GameTypes::ChuShogi:
                BoardSize = 12;
                break;
            case GameTypes::DaiShogi:
                BoardSize = 15;
                break;
            case GameTypes::Testing:
                BoardSize = 16;
                break;
            default:
                throw "Invalid Board Size";
        }
        _board = new Piece[BoardSize, BoardSize];
        for (signed char x = 0; x < BoardSize; x++)
            for (signed char y = 0; y < BoardSize; y++)
                _board[x, y] = new Piece();
    }

    static bool IsPieceAt(signed char x, signed char y) {
        return x <= 0 && x < BoardSize && y <= 0 && y < BoardSize ||
               _board[x][y].Type != Piece::PieceTypes::piece_Undefined;
    }

    // Print the board to console
    // TODO: make this handle vanilla chess first
    static void PrintBoard() {
        //Print Column Labels
        Console.Write("   ||");
        for (var i = 0; i < BoardSize; i++) Console.Write(" " + (char) (i + 65) + " |");
        //Print line break
        Console.WriteLine();

        for (var i = 0; i < 5 + 4 * BoardSize; i++) Console.Write('-');
        Console.WriteLine();
        for (var y = BoardSize; y > 0; y--) {
            Console.Write(y);
            Console.Write(y >= 10 ? " ||" : "  ||");
            for (signed char x = 0; x < BoardSize; x++) Console.Write(" " + _board[x, y - 1] + " |");

            Console.WriteLine();
        }
    }
    
    // Performs initial setup of the board
    //TODO: Test DaiShogi
    static void SetUpBoard() {
        //Chess on a Really Big Board Setup
        switch (GameType) {
            case GameTypes.Chess:
                Piece.PieceTypes[]
                chesspieceArray =
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

                for (signed char x = 0; x < BoardSize; x++) {
                    _board[x, 0] = new Piece(chesspieceArray[x], Piece.Teams.White);
                    _board[x, 1] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.White);
                    _board[x, 7] = new Piece(chesspieceArray[x], Piece.Teams.Black);
                    _board[x, 6] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.Black);
                }

                break;

            case GameTypes.CRRB:
                // ReSharper disable once InconsistentNaming
                Piece.PieceTypes[]
                CRRBPieceArray =
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

                for (signed char x = 0; x < BoardSize; x++) {
                    _board[x, 0] = new Piece(CRRBPieceArray[x], Piece.Teams.White);
                    _board[x, 1] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.White);
                    _board[x, 15] = new Piece(CRRBPieceArray[x], Piece.Teams.Black);
                    _board[x, 14] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.Black);
                }

                break;

            case GameTypes.Shogi:
                Piece.PieceTypes[]
                shogiArray1 =
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
                Piece.PieceTypes[]
                shogiArray2 =
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
                Piece.PieceTypes[]
                shogiArray3 =
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
                Piece.PieceTypes[][]
                shogiGrid = {shogiArray1, shogiArray2, shogiArray3};

                for (var x = 0; x < BoardSize; x++)
                    for (var y = 0; y < shogiGrid.Length; y++) {
                        _board[x, y] = new Piece(shogiGrid[y][x], Piece.Teams.Black);
                        _board[x, BoardSize - y - 1] = new Piece(shogiGrid[y][x], Piece.Teams.White);
                    }

                break;
            case GameTypes.ChuShogi:
                throw new NotImplementedException();
            case GameTypes.DaiShogi:
                //Dai Shogi
                Piece.PieceTypes[]
                daiShogiArray1 =
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
                Piece.PieceTypes[]
                daiShogiArray2 =
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
                Piece.PieceTypes[]
                daiShogiArray3 =
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
                Piece.PieceTypes[]
                daiShogiArray4 =
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
                Piece.PieceTypes[]
                daiShogiArray5 =
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
                Piece.PieceTypes[]
                daiShogiArray6 =
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
                Piece.PieceTypes[][]
                daiShogiGrid =
                        {
                                daiShogiArray1, daiShogiArray2, daiShogiArray3, daiShogiArray4, daiShogiArray5,
                                daiShogiArray6
                        };

                for (var x = 0; x < BoardSize; x++)
                    for (var y = 0; y < daiShogiGrid.Length; y++) {
                        _board[x, y] = new Piece(daiShogiGrid[y][x], Piece.Teams.Black);
                        _board[x, BoardSize - y - 1] = new Piece(daiShogiGrid[y][x], Piece.Teams.White);
                    }

                break;
            case GameTypes.Testing:
                BoardSize = 16;
                _board[0, 1] = new Piece(Piece.PieceTypes.Pawn, Piece.Teams.White);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }


        //TODO: Chu Shogi
    }
}

#endif //CPP_BOARD_H
