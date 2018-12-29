#include <utility>

#include <iostream>
#include <windows.h>
#include <string>
#include <algorithm>
//To-Do list
//DONE Create Constants
//DONE Create Piece Class
//DONE: Createboard
//DONE: output board
//DONE: get user input
//TODO: find where pieces can move
//TODO: handle check
//TODO: handle game end
//Defines
//Colors
#define BLACK 0
#define WHITE 1
#define NOCOLOR -1
//Fairy Pieces
#define FD 'D'
#define WFA 'W'
#define SUPER 'J'
#define ARCH 'A'
#define CHANC  'C'
#define ROSE 'O'

//Standard Pieces
#define ROOK 'R'
#define KING 'K'
#define QUEEN 'Q'
#define BISHOP 'B'
#define KNIGHT 'N'
#define PAWN 'P'

#define BOARD_SIZE 16

#if defined(WIN32) || defined(_WIN32) || defined(__WIN32) && !defined(__CYGWIN__)
#define WINDOWS
#define COLOR_BLACK 0
#define COLOR_LIGHTGREY 7
#define COLOR_DARKGREY 8
#define COLOR_WHITE 0XF
#define COLOR_RED 0xC
#define COLOR_BLUE 0x1

#endif




//Piece Struct

class Piece {
private:
    int8_t color;
    char letter;
public:
    Piece(char letter, bool color) {
        this->color = color;
        this->letter = letter;
    }

    Piece() {
        letter = ' ';
        color = NOCOLOR;
    }

    int8_t getColor() {
        return color;
    }

    char getLetter() {
        return letter;
    }
};

//Global variables
//The board
Piece board[BOARD_SIZE][BOARD_SIZE];
//The rows
const char files[] = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C',
                      'D',
                      'E', 'F'};
//The columns
const char ranks[] = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
                      'n',
                      'o', 'p'};
//The current turn + 1
uint8_t turn = WHITE;

//TODO:implement
//Returns true if game is over, false otherwise
bool gameOver() {
    //Too many turns
    if (turn - 1 > 100) {
        return true;
    }
    //No valid moves
    //if no valid moves, check for Checkmate
    //two kings left
    return false;
}

//Places pieces on board
void initBoard() {
    char pieceList[BOARD_SIZE] = {ROOK, KNIGHT, FD, WFA, BISHOP, ROSE, ARCH, QUEEN, KING,
                                  CHANC, SUPER, BISHOP, WFA, FD, KNIGHT, ROOK};
    //For every column
    for (uint8_t i = 0; i < BOARD_SIZE; i++) {
        //Place the regular pieces
        board[i][0] = Piece(pieceList[i], WHITE);
        board[i][BOARD_SIZE - 1] = Piece(pieceList[i], BLACK);
        //Place a pawn
        board[i][1] = Piece(PAWN, WHITE);
        board[i][BOARD_SIZE - 2] = Piece(PAWN, BLACK);
    }
}

//TODO: make all of these portable
//Windows only methods
#ifdef WINDOWS

//Utility Functions
//Sets the cursor position in the window
void setCursorPosition(int x, int y) {
    HANDLE hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);
    COORD c;
    c.X = x;
    c.Y = y;
    SetConsoleCursorPosition(hConsoleOutput, c);
}

//Clears the screen
void clearScreen() {
    system("cls");
}

//Clears the current line
void clearLine() {
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    HANDLE hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);
    GetConsoleScreenBufferInfo(hConsoleOutput, &csbi);
    COORD c = csbi.dwCursorPosition;
    //get number of columns
    int columns = csbi.srWindow.Right - csbi.srWindow.Left + 1;
    //Write ' ' to every position
    for (uint8_t i = 0; i < columns; i++) {
        std::cout << ' ';
    }
    //Reset cursor position
    SetConsoleCursorPosition(hConsoleOutput, c);
}

//Changes foreground and background color of current char
void changeColor(int k) {
    HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
    SetConsoleTextAttribute(hConsole, k);
}

//Game related functions
//Print the board
//TODO: make color changing portable
void printBoard() {
    clearScreen();
    for (uint8_t i = BOARD_SIZE - 1; i >= 0; i--) {
        changeColor((COLOR_BLACK << 4) + COLOR_WHITE);
        setCursorPosition(i + 1, 0);
        std::cout << ranks[i];
        setCursorPosition(0, i + 1);
        std::cout << files[BOARD_SIZE - 1 - i];
        for (uint8_t j = 0; j < BOARD_SIZE; j++) {
            setCursorPosition(i + 1, j + 1);
            int color = 0;
            if (i % 2 == j % 2) {
                color = (COLOR_LIGHTGREY << 4);
            } else {
                color = (COLOR_DARKGREY << 4);
            }
            if (board[i][BOARD_SIZE - 1 - j].getColor() == BLACK) {
                color += COLOR_BLUE;
            } else {
                color += COLOR_RED;
            }
            changeColor(color);
            std::cout << board[i][BOARD_SIZE - 1 - j].getLetter();

        }


    }
    changeColor((COLOR_BLACK << 4) + COLOR_WHITE);
}

//Runs if a move is invalid
//TODO: replace sleep with portable version
void invalidMove() {
    setCursorPosition(0, 20);
    clearLine();
    std::cout << "Invalid Move";
    Sleep(500);
}

#endif

//Prompt the player for  a pieces location
std::string getPieceLocation() {
    std::string input;
    setCursorPosition(0, 20);
    if (turn % 2 == WHITE) {
        std::cout << "White's turn, ";
    } else {
        std::cout << "Black's turn, ";
    }
    std::cout << "Enter Piece (Row,Col): ";
    std::cin >> input;
    return input;
}


int8_t find(const char *arr, char val) {
    for (int8_t i = 0; i < BOARD_SIZE; i++) {
        if (arr[i] == val) {
            return i;
        }
    }
    return -1;
}


bool pieceLocationIsValid(std::string input) {
    //if input is the wrong length
    if (input.size() == 2) {

        //if the values are in range
        int8_t row = find(files, input[0]);
        //std::find(files[0], std::end(files), input[0]);
        int8_t col = find(files, input[1]);
        if ((row >= 0) && (col >= 0) && (row <= BOARD_SIZE) && (col <= BOARD_SIZE)) {
            //if the piece exists and matches the player's colorwe
            if (board[col][row].getColor() == turn % 2) {
                return true;
            }
        }
    }
    invalidMove();
    return false;
}

//given coords as a string get indices as a single uint8_t
uint8_t getPositionFromString(std::string str) {
    int8_t col = find(files, str[1]);
    int8_t row = find(ranks, str[0]);
    return (uint8_t(col) << 4) | row;
}


std::string getPieceLocationString() {
    std::string pieceLocation;
    //loop until valid piece is entered
    do {
        setCursorPosition(0, 25);
        clearLine();
        pieceLocation = getPieceLocation();
    } while (!pieceLocationIsValid(pieceLocation));
}

//Prompts for a the location of a piece and returns it if it is valid, else prompts until a valid piece is chosen
Piece getPiece(std::string pieceLocation) {

    //Get the row and column of the piece
    uint8_t coord = getPositionFromString(std::move(pieceLocation));
    //return the piece
    return board[coord >> 4][0x0F & coord];
}

std::string coordToString(uint8_t col, uint8_t row) {
    //that's disgusting
    return std::string(&ranks[col]) + std::string(&files[row]);
}





void pawnMoves(uint8_t col, uint8_t row, std::vector<std::string> moveList) {
    //if the piece is white
    if (turn % 2 == WHITE) {
        //check ahead
        //if next row is in range and not white
        if (row + 1 < BOARD_SIZE) {
            //normal move
            if (board[col][row + 1].getColor() != WHITE) {
                moveList.push_back(coordToString(col, row + 1));
                //en passant
                if (board[col][row + 1].getColor() == NOCOLOR &&
                    board[col][row + 2].getColor() != WHITE) {
                    moveList.push_back(coordToString(col, row + 2));
                }
            }
            //check diagonals
            if (col + 1 < BOARD_SIZE && board[col + 1][row + 1].getColor() == BLACK) {
                moveList.push_back(coordToString(col + 1, row + 1));
            }
            if (col - 1 >= 0 && board[col - 1][row + 1].getColor() == BLACK) {
                moveList.push_back(coordToString(col - 1, row + 1));
            }
        }
    } else {
        //check ahead
        //if next row is in range and not white
        if (row - 1 >= 0) {
            //normal move
            if (board[col][row - 1].getColor() != BLACK) {
                moveList.push_back(coordToString(col, row - 1));
                //en passant
                if (board[col][row - 1].getColor() == NOCOLOR &&
                    board[col][row - 2].getColor() != BLACK) {
                    moveList.push_back(coordToString(col, row - 2));
                }
            }
            //check diagonals
            if (col + 1 < BOARD_SIZE && board[col - 1][row - 1].getColor() == WHITE) {
                moveList.push_back(coordToString(col + 1, row - 1));
            }
            if (col - 1 >= 0 && board[col - 1][row - 1].getColor() == WHITE) {
                moveList.push_back(coordToString(col - 1, row - 1));
            }
        }
    }
}

void rookMoves(uint8_t col, uint8_t row, std::vector<std::string> moveList) {
    if (turn % 2 == WHITE) {
        //check right
        int x = col + 1;
        int y = row;
        //all empty spaces on right
        while (x < BOARD_SIZE && board[x][y].getColor() == NOCOLOR) {
            moveList.push_back(coordToString(x, y));
            x++;
        }
        //Opposing piece on right
        if (x < BOARD_SIZE && board[x][y].getColor() == BLACK) {
            moveList.push_back(coordToString(x, y));
        }
        //check left
        x = col - 1;
        while (x >= 0 && board[x][y].getColor() == NOCOLOR) {
            moveList.push_back(coordToString(x, y));
            x--;
        }
        if (x >= 0 && board[x][y].getColor() == BLACK) {
            moveList.push_back(coordToString(x, y));
        }
        //check front
        x = col;
        y = row + 1;
        //all empty spaces on right
        while (y < BOARD_SIZE && board[x][y].getColor() == NOCOLOR) {
            moveList.push_back(coordToString(x, y));
            y++;
        }
        //Opposing piece on right
        if (y < BOARD_SIZE && board[x][y].getColor() == BLACK) {
            moveList.push_back(coordToString(x, y));
        }
        //check back
        y = col - 1;
        while (y >= 0 && board[x][y].getColor() == NOCOLOR) {
            moveList.push_back(coordToString(x, y));
            y--;
        }
        if (y >= 0 && board[x][y].getColor() == BLACK) {
            moveList.push_back(coordToString(x, y));
        } else {
            //check right
            int x = col + 1;
            int y = row;
            //all empty spaces on right
            while (x < BOARD_SIZE && board[x][y].getColor() == NOCOLOR) {
                moveList.push_back(coordToString(x, y));
                x++;
            }
            //Opposing piece on right
            if (x < BOARD_SIZE && board[x][y].getColor() == WHITE) {
                moveList.push_back(coordToString(x, y));
            }
            //check left
            x = col - 1;
            while (x >= 0 && board[x][y].getColor() == NOCOLOR) {
                moveList.push_back(coordToString(x, y));
                x--;
            }
            if (x >= 0 && board[x][y].getColor() == WHITE) {
                moveList.push_back(coordToString(x, y));
            }
            //check front
            x = col;
            y = row + 1;
            //all empty spaces on right
            while (y < BOARD_SIZE && board[x][y].getColor() == NOCOLOR) {
                moveList.push_back(coordToString(x, y));
                y++;
            }
            //Opposing piece on right
            if (y < BOARD_SIZE && board[x][y].getColor() == WHITE) {
                moveList.push_back(coordToString(x, y));
            }
            //check back
            y = col - 1;
            while (y >= 0 && board[x][y].getColor() == NOCOLOR) {
                moveList.push_back(coordToString(x, y));
                y--;
            }
            if (y >= 0 && board[x][y].getColor() == WHITE) {
                moveList.push_back(coordToString(x, y));
            }
        }

    }
}

void bishopMoves(uint8_t col, uint8_t row, std::vector<std::string> moveList){
    //TODO: implement
}

void knightMoves(uint8_t col, uint8_t row, std::vector<std::string> moveList){
    //TODO: implement
}

void queenMoves(uint8_t col, uint8_t row, std::vector<std::string> moveList){
    rookMoves(col,row,moveList);
    bishopMoves(col,row,moveList);
}

auto getFunctionFromPiece(Piece piece){
    if (piece.getLetter() == FD) {
        //TODO:
    } else if (piece.getLetter() == WFA) {
        //TODO:
    } else if (piece.getLetter() == SUPER) {
        //TODO:
    } else if (piece.getLetter() == ARCH) {
        //TODO:
    } else if (piece.getLetter() == CHANC) {
        //TODO:
    } else if (piece.getLetter() == ROSE) {
        //TODO:
    } else if (piece.getLetter() == ROOK) {
        //TODO:
    } else if (piece.getLetter() == KING) {
        //TODO:
    } else if (piece.getLetter() == QUEEN) {
        return queenMoves;
    } else if (piece.getLetter() == BISHOP) {
        return bishopMoves;
    } else if (piece.getLetter() == KNIGHT) {
        return knightMoves;
    } else if (piece.getLetter() == PAWN) {
        return pawnMoves;
    }
}
//Get possible moves for piece
std::vector<std::string> getMoves(Piece piece, const std::string &pieceLocation) {
    std::vector<std::string> moveList;
    uint8_t col = uint8_t(getPositionFromString(pieceLocation)) >> 4;
    uint8_t row = uint8_t(getPositionFromString(pieceLocation)) & uint8_t(0x0F);
    //TODO: remove any that would cause check
    if (piece.getLetter() == FD) {
        //TODO:
    } else if (piece.getLetter() == WFA) {
        //TODO:
    } else if (piece.getLetter() == SUPER) {
        //TODO:
    } else if (piece.getLetter() == ARCH) {
        //TODO:
    } else if (piece.getLetter() == CHANC) {
        //TODO:
    } else if (piece.getLetter() == ROSE) {
        //TODO:
    } else if (piece.getLetter() == ROOK) {
        //TODO:
    } else if (piece.getLetter() == KING) {
        //TODO:
    } else if (piece.getLetter() == QUEEN) {
        //TODO:
    } else if (piece.getLetter() == BISHOP) {
        //TODO:
    } else if (piece.getLetter() == KNIGHT) {
        //TODO:
    } else if (piece.getLetter() == PAWN) {
        pawnMoves(col, row, moveList);
    }
    return moveList;
}

int main() {

    //initialize board
    initBoard();

    //Game loop
    while (!gameOver()) {
        printBoard();
        std::string pieceLocation = getPieceLocation();
        Piece piece = getPiece(pieceLocation);
        //TODO:list where this piece can move
        getMoves(piece, pieceLocation);
        //TODO: loop to figure out where to move the piece
        //TODO: find a way to go back to picking pieces if user does not want to move current piece
        //next turn
        turn++;
    }
    return 0;
}