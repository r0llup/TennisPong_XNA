using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projet7
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Humain
    {
        private TennisPong Parent { get; set; }
        public readonly Rectangle PositionTextureRaquette;
        public Vector2 PositionRaquette;
        public readonly Vector2 OrigineRaquette;

        public Humain(TennisPong parent)
        {
            this.Parent = parent;
            this.PositionTextureRaquette = new Rectangle(0, 0, 64, 128);
            this.OrigineRaquette = new Vector2(this.PositionTextureRaquette.Width / 2f,
                this.PositionTextureRaquette.Height / 2f);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization logic here
            this.PositionRaquette = new Vector2(32, this.Parent.GraphicsDevice.Viewport.Height >> 1);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back) ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Parent.Exit();

            GamePadState stateGamepad = GamePad.GetState(PlayerIndex.One);
            KeyboardState stateKeyboard = Keyboard.GetState(PlayerIndex.One);

            if (!this.Parent.Partie)
            {
                if (this.Parent.ServiceJoueur1)
                {
                    this.Parent.BallGame.PositionBalle.X = this.PositionRaquette.X + 32;
                    this.Parent.BallGame.PositionBalle.Y = this.PositionRaquette.Y;
                }

                if (stateGamepad.IsButtonDown(Buttons.X) ||
                    stateGamepad.IsButtonDown(Buttons.B) ||
                    stateKeyboard.IsKeyDown(Keys.Space))
                {
                    if (this.Parent.ServiceJoueur1)
                    {
                        this.Parent.Partie = true;
                        this.Parent.BallGame.TrajectoireBalle.X = 0.65f;
                        this.Parent.BallGame.TrajectoireBalle.Y = 0f;
                    }
                }
            }

            this.PositionRaquette.Y -= stateGamepad.ThumbSticks.Left.Y *
                (float)gameTime.ElapsedGameTime.Milliseconds * 0.5f;
            if (stateKeyboard.IsKeyDown(Keys.Up))
                this.PositionRaquette.Y -= 1f * (float)gameTime.ElapsedGameTime.Milliseconds * 0.5f;
            if (stateKeyboard.IsKeyDown(Keys.Down))
                this.PositionRaquette.Y += 1f * (float)gameTime.ElapsedGameTime.Milliseconds * 0.5f;
            if (this.PositionRaquette.Y < 64f)
                this.PositionRaquette.Y = 64f;
            else if (this.PositionRaquette.Y > this.Parent.GraphicsDevice.Viewport.Height - 64)
                this.PositionRaquette.Y = this.Parent.GraphicsDevice.Viewport.Height - 64;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            this.Parent.SpritesBatch.Begin();
            this.Parent.SpritesBatch.Draw(this.Parent.SpritesTexture, this.PositionRaquette,
                this.PositionTextureRaquette, Color.White, 0f, this.OrigineRaquette, 1f,
                SpriteEffects.None, 0f);
            this.Parent.SpritesBatch.End();
        }
    }
}