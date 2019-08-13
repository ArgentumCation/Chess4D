namespace Chess4D
{
    public class SelectPieceCommand : Command
    {
        private readonly Player _currentPlayer;
        private readonly IFrontend _frontend;


        public SelectPieceCommand(Player currentPlayer, IFrontend frontend)
        {
            _currentPlayer = currentPlayer;
            _frontend = frontend;
        }

        //TODO: add support for dropping pieces
        public override void Execute()
        {
            _currentPlayer.SelectedPiece = _frontend.SelectPiece(_currentPlayer.Team);
        }

        public override void Undo()
        {
            _currentPlayer.SelectedPiece = null;
        }
    }
}