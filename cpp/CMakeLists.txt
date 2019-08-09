cmake_minimum_required(VERSION 3.14)
project(cpp)

set(CMAKE_CXX_STANDARD 17)

add_executable(cpp main.cpp Piece.cpp Piece.h Board.h Board.cpp)