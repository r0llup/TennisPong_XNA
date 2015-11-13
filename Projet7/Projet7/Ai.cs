using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projet7
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Ai
    {
        private TennisPong Parent { get; set; }
        public readonly Rectangle PositionTextureRaquette;
        public Vector2 PositionRaquette;
        public readonly Vector2 OrigineRaquette;

        public Ai(TennisPong parent)
        {
            this.Parent = parent;
            this.PositionTextureRaquette = new Rectangle(64, 0, 64, 128);
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
            this.PositionRaquette = new Vector2(this.Parent.GraphicsDevice.Viewport.Width -
                this.PositionTextureRaquette.Width + 32, this.Parent.GraphicsDevice.Viewport.Height >> 1);
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
            if (!this.Parent.Partie)
            {
                if (this.Parent.ServiceJoueur2)
                {
                    this.Parent.BallGame.PositionBalle.X = this.PositionRaquette.X - 32;
                    this.Parent.BallGame.PositionBalle.Y = this.PositionRaquette.Y;
                    this.Parent.Partie = true;
                    this.Parent.BallGame.TrajectoireBalle.X = 0.65f;
                    this.Parent.BallGame.TrajectoireBalle.Y = 0f;
                }
            }

            if (this.Parent.BallGame.PositionBalle.Y < this.PositionRaquette.Y)
                this.PositionRaquette.Y -= 25;
            if (this.Parent.BallGame.PositionBalle.Y > this.PositionRaquette.Y)
                this.PositionRaquette.Y += 25;
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