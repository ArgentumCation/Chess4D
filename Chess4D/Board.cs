using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Chess4D
{
    public static class Board
    {
        public enum GameTypes
        {
            Chess,

            // ReSharper disable once InconsistentNaming
            CRRB,
            Shogi,
            ChuShogi,
            DaiShogi,
            Testing
        }

        //Board Size, changing this will probably break something
        //But we may need to change this to 48 when we start working on mapping the board to other surfaces
        public static sbyte BoardSize;

        // ReSharper disable once InconsistentNaming
        public static Piece[,] _board;

        private static readonly GameTypes GameType = GameTypes.DaiShogi;

        static Board()
        {
            switch (GameType)
            {
                case GameTypes.Chess:
                    BoardSize = 8;
                    break;
                case GameTypes.CRRB:
                    BoardSize = 16;
                    break;
                case GameTypes.Shogi:
                    BoardSize = 9;
                    break;
                case GameTypes.ChuShogi:
                    BoardSize = 12;
                    break;
                case GameTypes.DaiShogi:
                    BoardSize = 15;
                    break;
                case GameTypes.Testing:
                    BoardSize = 16;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _board = new Piece[BoardSize, BoardSize];
            for (sbyte x = 0; x < BoardSize; x++)
            for (sbyte y = 0; y < BoardSize; y++)
                _board[x, y] = PieceFactory.GetPiece(PieceTypes.Undefined);
        }

        public static bool IsPieceAt(sbyte x, sbyte y)
        {
            return x <= 0 && x < BoardSize && y <= 0 && y < BoardSize ||
                   _board[x, y].Type != PieceTypes.Undefined;
        }

        public static void LoadBoard(string board)
        {
            var settings = new JsonSerializerSettings
                {TypeNameHandling = TypeNameHandling.All};
            _board = JsonConvert.DeserializeObject(board, settings) as Piece[,];
        }

        public static void SaveBoard(string filename)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Converters = {new StringEnumConverter()}
            };
            var docPath = AppDomain.CurrentDomain.BaseDirectory;
            File.WriteAllText(Path.Combine(docPath, filename),JsonConvert.SerializeObject(_board, settings));
            Console.WriteLine("Saved Board");
        }

        // Performs initial setup of the board
        //TODO: Test DaiShogi
        public static void SetUpBoard()
        {
            string board = "";
            //Chess on a Really Big Board Setup
            switch (GameType)
            {
                case GameTypes.Chess:
                    board = File.ReadAllText(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory, "vanilla-chess.json"));
                    break;

                case GameTypes.CRRB:
                    board = File.ReadAllText(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory, "chess-on-a-really-big-board.json"));
                    break;

                case GameTypes.Shogi:
                    board = File.ReadAllText(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory, "shogi.json"));
                    break;
                case GameTypes.ChuShogi:
                    throw new NotImplementedException();
                    board = File.ReadAllText(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory, "chu-shogi.json"));
                    break;
                case GameTypes.DaiShogi:
                    board = File.ReadAllText(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory, "dai-shogi.json"));

                    break;
                case GameTypes.Testing:
                    BoardSize = 16;
                    _board[0, 1] = PieceFactory.GetPiece(PieceTypes.Pawn, Teams.White);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            LoadBoard(board);
            //TODO: Chu Shogi
        }
    }
}