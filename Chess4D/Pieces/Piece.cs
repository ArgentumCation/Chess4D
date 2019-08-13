using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Chess4D
{
    
    public abstract class Piece
    {
        
        public bool FirstMove = true;
        
        public Teams Team;
        
        //public abstract PieceTypes Type { get; protected set; }
        public PieceTypes Type;

        // public HashSet<sbyte> Moves;

        //This function does nothing but the code needs it to work
        protected Piece()
        {
            Team = Teams.Undefined;
        }

        protected Piece(Teams team)
        {
            Team = team;
        }

//        protected Piece(PieceTypes type, Teams team,  bool firstMove)
//        {
//            Type = type;
//            FirstMove = firstMove;
//            Team = team;
//        }

        public static string CoordToString(sbyte coord)
        {
            return "(" + (char) (GetX(coord) + 65) + ", " + (GetY(coord) + 1) + ")";
        }

        public static sbyte GetX(sbyte b)
        {
            return (sbyte) (b & 0xF);
        }

        public static sbyte GetY(sbyte b)
        {
            return (sbyte) (b >> 4);
        }

        public static sbyte GetCoords(sbyte x, sbyte y)
        {
            return (sbyte) ((y << 4) | (x & 0xF));
        }

        public abstract IEnumerable<sbyte> Move(sbyte coords);

        // Checks if a piece is attacking it's allies
        // team is the team of the piece making the attack
        // x and y are target coords
        protected static bool AttackingAllies(sbyte x, sbyte y, Teams team)
        {
            var attackedPiece = Board._board[x, y];
            if (attackedPiece.Team == Teams.Undefined) return false;
            return attackedPiece.Team == team;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        //Find out if a movement would move through a piece
        // x,y are the target
        // currentX, currentY are coords of current piece
        protected static bool MoveThroughPieces(sbyte x, sbyte y, sbyte currentX,
            sbyte currentY)
        {
            //Get direction of movement
            var directionX = x - currentX;
            if (directionX > 0)
                directionX = 1;
            else if (directionX < 0) directionX = -1;

            var directionY = y - currentY;
            if (directionY > 0)
                directionY = 1;
            else if (directionY < 0) directionY = -1;

            //Check if every square is empty
            int i = currentX;
            int j = currentY;
            //move once to avoid checking self
            i += directionX;
            j += directionY;
            while (i != x || j != y)
            {
                if (Board._board[x, y].Team != Teams.Undefined) return true;

                i += directionX;
                j += directionY;
            }

            return false;
        }

        protected static bool WithinBounds(sbyte x, sbyte y)
        {
            if (x >= 0 && y >= 0 && x < Board.BoardSize && y < Board.BoardSize)
                return true;

            return false;
        }
    }

    public class Undefined : Piece
    {
        public Undefined()
        {
            Type = PieceTypes.Undefined;
        }

        public Undefined(Teams team) : base(team)
        {
            Type = PieceTypes.Undefined;
        }

        public override IEnumerable<sbyte> Move(sbyte coords)
        {
            throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return " 　 ";
        }
    }


    public static class PieceFactory
    {
        private static readonly Dictionary<PieceTypes, Type> _pieceNames;

        static PieceFactory()
        {
            var pieceTypes = Assembly.GetAssembly(typeof(Piece)).GetTypes()
                .Where(myType =>
                    myType.IsClass && !myType.IsAbstract &&
                    myType.IsSubclassOf(typeof(Piece)));
            _pieceNames = new Dictionary<PieceTypes, Type>();
            foreach (var type in pieceTypes)
            {
                var temp = Activator.CreateInstance(type) as Piece;
                //Console.WriteLine(temp.Type + " - " + type);
                _pieceNames.Add(temp.Type, type);
            }
        }

        public static Piece GetPiece(PieceTypes pieceType)
        {
            if (_pieceNames.ContainsKey(pieceType))
            {
                var type = _pieceNames[pieceType];
                var piece = Activator.CreateInstance(type) as Piece;
                return piece;
            }

            return null;
        }

        
        public static Piece GetPiece(PieceTypes pieceType, Teams team)
        {
            if (_pieceNames.ContainsKey(pieceType))
            {
                var type = _pieceNames[pieceType];
                var piece =
                    Activator.CreateInstance(type, team) as Piece;
                return piece;
            }
            throw new InvalidEnumArgumentException();
            return null;
        }
        
        public static Piece GetPiece(PieceTypes pieceType, Teams team, bool firstMove)
        {
            if (_pieceNames.ContainsKey(pieceType))
            {
                var type = _pieceNames[pieceType];
                var piece =
                    Activator.CreateInstance(type, pieceType, team, firstMove) as Piece;
                return piece;
            }

            return null;
        }
    }
}