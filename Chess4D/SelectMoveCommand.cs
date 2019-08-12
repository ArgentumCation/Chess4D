using System;
using System.Collections.Generic;

namespace Chess4D
{
    internal class SelectMoveCommand : Command
    {
        private IEnumerable<sbyte> _moves;
        private Player _player;
        private IFrontend _frontend;
        public SelectMoveCommand(IEnumerable<sbyte> moves, Player player, IFrontend frontend)
        {
            _moves = moves;
            _player = player;
            _frontend = frontend;
        }

        public override void Execute()
        {
            var move = _frontend.SelectMove(_moves);
            if (move == null)
            {
                _player.SelectedPiece = null;
            }

            _player.SeletedMove = move;

        }

        public override void Undo()
        {
            _player.SeletedMove = null;
        }
    }
}