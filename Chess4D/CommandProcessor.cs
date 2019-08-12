using System.Collections.Generic;

namespace Chess4D
{

    public static class CommandProcessor
    {
        private static readonly List<Command> _commands = new List<Command>();
        private static int _currentCommandIndex;
        
        public static void ExecuteCommand(Command command)
        {
            _commands.Add(command);
            command.Execute();
            _currentCommandIndex =  _commands.Count - 1;
            
        }

        public static void Undo()
        {
            if (_currentCommandIndex < 0)
            {
                return;
            }
            _commands[_currentCommandIndex].Undo();
            _commands.RemoveAt(_currentCommandIndex);
            _currentCommandIndex--; 
        }
    }
}