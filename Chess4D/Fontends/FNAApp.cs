using System.Collections.Generic;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chess4D
{
    public class FNAApp : IFrontend
    {
        public FNAApp()
        {
            using(Game1 game = new Game1())
            {
                 game.Run();
            }
        }

        public void PrintBoard()
        {
            throw new System.NotImplementedException();
        }

        public sbyte? SelectMove(IEnumerable<sbyte> moves)
        {
            throw new System.NotImplementedException();
        }

        public sbyte SelectPiece(Teams currentPlayer)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Game1 : Microsoft.Xna.Framework.Game
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
                this.Exit();
 
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