using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TankGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Client client;
        GraphicsDevice device;
        Texture2D backgroundTexture;
        Texture2D stoneTexture;
        Texture2D brickTexture;
        Texture2D tankTexture;
        Texture2D waterTexture;
        Texture2D sandTexture;
        Texture2D coinpackTexture;
        Texture2D lifepackTexture;
        float timer = 10;         //Initialize a 10 second timer
        const float TIMER = 1;
        int screenWidth;
        int screenHeight;
        Rectangle[,] cells;
             
        public Game1()
        {

            client = new Client();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth =1000;
            graphics.PreferredBackBufferHeight = 725;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Tanks";
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            backgroundTexture = Content.Load<Texture2D>("background");
            stoneTexture = Content.Load<Texture2D>("stone");
            waterTexture = Content.Load<Texture2D>("water");
            sandTexture = Content.Load<Texture2D>("sand");
            brickTexture = Content.Load<Texture2D>("brick");
            tankTexture = Content.Load<Texture2D>("tank");
            lifepackTexture = Content.Load<Texture2D>("lifepack");
            coinpackTexture = Content.Load<Texture2D>("coinpack");
            screenHeight = device.PresentationParameters.BackBufferHeight;
            screenWidth = device.PresentationParameters.BackBufferWidth;
            cells = new Rectangle[10, 10];
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    cells[x, y] = new Rectangle((x * 48) + 50,(y * 48)+ 50,48,48);
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            String msg=processKeyBoard();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                if (msg != null)
                {
                    client.send(msg);
                    timer = TIMER;   //Reset Timer
                    msg = null;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            DrawScenery();
            DrawGrid();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected String processKeyBoard()
        {
            String msg="";
            KeyboardState keybState = Keyboard.GetState();
            if (keybState.IsKeyDown(Keys.Enter))
            {
                //client.send("JOIN#");
                msg = "JOIN#";
                return msg;
            }
            if (keybState.IsKeyDown(Keys.Left))
            {
                //client.send("LEFT#");
                msg = "LEFT#";
                return msg;
            }

            if (keybState.IsKeyDown(Keys.Right)){
                //client.send("RIGHT#");
                msg = "RIGHT#";
                return msg;
            }
            if (keybState.IsKeyDown(Keys.Up))
            {
                // client.send("UP#");
                msg = "UP#";
                return msg;
            }
            if (keybState.IsKeyDown(Keys.Down))
            {
                //client.send("DOWN#");
                msg = "DOWN#";
                return msg;
            }
            if (keybState.IsKeyDown(Keys.Space))
            {
                //client.send("SHOOT#");
                msg = "SHOOT#";
                return msg;
            }
            else
                return null;

        }

        protected void DrawScenery()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
        }
        private void DrawGrid()
        {
             for (int a = 0; a < 10; a++)
            {
                for (int b = 0; b < 10; b++)
                {

                    if (client.board.blocks[b, a] is Brick)
                        spriteBatch.Draw(brickTexture, cells[b, a], Color.White);
                    else if (client.board.blocks[b, a] is Stone)
                        spriteBatch.Draw(stoneTexture, cells[b, a], Color.White);
                    else if (client.board.blocks[b, a] is Water)
                        spriteBatch.Draw(waterTexture, cells[b, a], Color.White);
                    else if (client.board.blocks[b, a] is Player)
                        spriteBatch.Draw(tankTexture, cells[b, a], Color.White);
                    else if (client.board.blocks[b, a] is Coinpack)
                        spriteBatch.Draw(coinpackTexture, cells[b, a], Color.White);
                    else if (client.board.blocks[b, a] is Lifepack)
                        spriteBatch.Draw(lifepackTexture, cells[b, a], Color.White);
                    else
                        spriteBatch.Draw(sandTexture, cells[b, a], Color.White);
                }
            }
        }
    }
}
