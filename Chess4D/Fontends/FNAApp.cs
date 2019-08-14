using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chess4D
{
    public class FNAApp : IFrontend
    {
        //Create startSelectPiece event
        public delegate void startSelectPiece();
        public event startSelectPiece startSelectPieceEvent;
        //Receive finishSelectPiece event
        //Send  startSelectMove event
        //receive finishSelectMove event
        public FNAApp()
        {
            using (var game = new Game1(this))
            {
                Task.Factory.StartNew(() => game.Run());
            }
        }

        public void PrintBoard()
        {
            throw new NotImplementedException();
        }

        public sbyte? SelectMove(IEnumerable<sbyte> moves)
        {
            //Send startSelectPieceEvent
            startSelectPieceEvent?.Invoke();
            throw new NotImplementedException();
        }

        public sbyte SelectPiece(Teams currentPlayer)
        {
            throw new NotImplementedException();
        }
    }

    public class Game1 : Game
    {
        
        //FNApp used to set up events
        private FNAApp _fnaApp;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private bool _needToPromptUserForPiece = false;
        public Game1(FNAApp fnaApp)
        {
            _fnaApp = fnaApp;
            //call function when receive startSelectPieceEvent
            fnaApp.startSelectPieceEvent += () => { _needToPromptUserForPiece = true; };
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //Load Non content stuff
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            //Handle prompt
            if (_needToPromptUserForPiece)
            {
                //Handle here
                _needToPromptUserForPiece = false;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
    
    
 
    
}