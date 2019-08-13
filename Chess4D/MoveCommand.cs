using System;

namespace Chess4D
{
    internal class MoveCommand : Command
    {
        public MoveCommand(sbyte? currentPlayerSeletedMove)
        {
            throw new NotImplementedException();
        }
        //Move a piece to a new square
        public override void Execute()
        {
            //Store whatever is in current square
            //Store whether or not it's moved
            
            //Store what is in target position
            //Store whether or not it's moved
            
            //Set target squ to current piece
            
            //Set 
            
            throw new NotImplementedException();
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}