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
        public delegate bool StartSelectingMove();

        //event 
        
        public delegate bool finishedSelectingMove();

        public delegate bool startedSelectingPiece();

        public delegate bool finishedSelectingPiece();
        public FNAApp()
        {
            using (var game = new Game1())
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
            throw new NotImplementedException();
        }

        public sbyte SelectPiece(Teams currentPlayer)
        {
            throw new NotImplementedException();
        }
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
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