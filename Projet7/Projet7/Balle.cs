using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projet7
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Balle
    {
        private TennisPong Parent { get; set; }
        public Vector2 PositionBalle;
        public Vector2 TrajectoireBalle;
        private readonly Rectangle PositionTextureBalle;
        private readonly float ScaleBalle;
        private readonly Vector2 OrigineBalle;
        public Boolean AllerBalle { get; private set; }
        private event PointEndedEventHandler OnPointEnded;
        private event GameEndedEventHandler OnGameEnded;
        private event SetEndedEventHandler OnSetEnded;
        private event MatchEndedEventHandler onMatchEnded;

        public Balle(TennisPong parent)
        {
            this.Parent = parent;
            this.PositionTextureBalle = new Rectangle(128, 0, 64, 64);
            this.ScaleBalle = 0.5f;
            this.OrigineBalle = new Vector2(this.PositionTextureBalle.Width / 2f, this.PositionTextureBalle.Height / 2f);
            this.AllerBalle = true;
            this.OnPointEnded += new PointEndedEventHandler(this.Pong_OnPointEnded);
            this.OnGameEnded += new GameEndedEventHandler(this.Pong_OnGameEnded);
            this.OnSetEnded += new SetEndedEventHandler(this.Pong_OnSetEnded);
            this.onMatchEnded += new MatchEndedEventHandler(Pong_onMatchEnded);
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
            this.TrajectoireBalle = Vector2.Zero;
            this.PositionBalle = new Vector2(this.Parent.HumanGame.PositionTextureRaquette.Width,
                this.Parent.GraphicsDevice.Viewport.Height >> 1);
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
            if (this.Parent.Partie)
            {
                float tempLoc = this.Parent.BallGame.PositionBalle.X;
                if (this.AllerBalle)
                    this.Parent.BallGame.PositionBalle += this.Parent.BallGame.TrajectoireBalle *
                        (float)gameTime.ElapsedGameTime.Milliseconds;
                else
                    this.Parent.BallGame.PositionBalle -= this.Parent.BallGame.TrajectoireBalle *
                        (float)gameTime.ElapsedGameTime.Milliseconds;
                if (this.Parent.BallGame.PositionBalle.X >= this.Parent.GraphicsDevice.Viewport.Width ||
                    this.Parent.BallGame.PositionBalle.X <= 0f)
                {
                    this.Parent.Partie = false;
                }

                if (this.Parent.BallGame.PositionBalle.Y > this.Parent.GraphicsDevice.Viewport.Height)
                {
                    this.Parent.BallGame.PositionBalle.Y = this.Parent.GraphicsDevice.Viewport.Height;
                    this.Parent.BallGame.TrajectoireBalle.Y = -this.Parent.BallGame.TrajectoireBalle.Y;
                }
                else if (this.Parent.BallGame.PositionBalle.Y <= 0)
                {
                    this.Parent.BallGame.PositionBalle.Y = 0;
                    this.Parent.BallGame.TrajectoireBalle.Y = -this.Parent.BallGame.TrajectoireBalle.Y;
                }

                if (this.Parent.BallGame.PositionBalle.X < 64)
                    this.VerifyBallCollision(0, tempLoc >= 64f, gameTime);
                else if (this.Parent.BallGame.PositionBalle.X > this.Parent.GraphicsDevice.Viewport.Width - 64)
                    this.VerifyBallCollision(1, tempLoc <= this.Parent.GraphicsDevice.Viewport.Width - 64, gameTime);
            }
        }

        private void VerifyBallCollision(int i, bool reverse, GameTime gt)
        {
            GamePadState stateGamepad = GamePad.GetState((PlayerIndex)i);
            Vector2 location = i == 0 ? this.Parent.HumanGame.PositionRaquette : this.Parent.AiGame.PositionRaquette;
            if (this.Parent.BallGame.PositionBalle.Y < location.Y + 64 &&
                this.Parent.BallGame.PositionBalle.Y > location.Y - 64)
            {
                if (reverse)
                    this.Parent.BallGame.TrajectoireBalle.X = -this.Parent.BallGame.TrajectoireBalle.X;
                this.Parent.BallGame.TrajectoireBalle.Y = (this.Parent.BallGame.PositionBalle.Y - location.Y) * 0.01f;

                if (i == 0)
                {
                    if (this.Parent.Sons)
                        this.Parent.SdEffect2.Play();
                }

                else
                {
                    if (this.Parent.Sons)
                        this.Parent.SdEffect1.Play();
                }
            }
            else
            {
                if (i == 0)
                {
                    this.Parent.Partie = false;
                    this.Parent.ServiceJoueur1 = false;
                    this.Parent.ServiceJoueur2 = true;
                    this.AllerBalle = false;
                    PointEndedEventArgs peea = new PointEndedEventArgs(2);
                    this.OnPointEnded(this, peea);
                }

                else
                {
                    this.Parent.Partie = false;
                    this.Parent.ServiceJoueur1 = true;
                    this.Parent.ServiceJoueur2 = false;
                    this.AllerBalle = true;
                    PointEndedEventArgs peea = new PointEndedEventArgs(1);
                    this.OnPointEnded(this, peea);
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            this.Parent.SpritesBatch.Begin();
            this.Parent.SpritesBatch.Draw(this.Parent.SpritesTexture, this.PositionBalle,
                this.PositionTextureBalle, Color.White, 0f, this.OrigineBalle, this.ScaleBalle,
                SpriteEffects.None, 0f);
            this.Parent.SpritesBatch.End();
        }

        public void Pong_OnPointEnded(object sender, PointEndedEventArgs e)
        {
            if (e.Joueur == 1)
            {
                this.Parent.ScoreJoueur1.Point++;
                switch (this.Parent.ScoreJoueur1.Point)
                {
                    case 0:
                        this.Parent.Jeu.Score1.Point = 0;
                        break;
                    case 1:
                        this.Parent.Jeu.Score1.Point = 15;
                        break;
                    case 2:
                        this.Parent.Jeu.Score1.Point = 30;
                        break;
                    case 3:
                        this.Parent.Jeu.Score1.Point = 40;
                        break;
                    case 4:
                        this.Parent.ScoreJoueur1 = new Score();
                        GameEndedEventArgs geea = new GameEndedEventArgs(1, this.Parent.Jeu);
                        this.OnGameEnded(this, geea);
                        this.Parent.Jeu.Score1.Point = 0;
                        break;
                }
            }
            else
            {
                this.Parent.ScoreJoueur2.Point++;
                switch (this.Parent.ScoreJoueur2.Point)
                {
                    case 0:
                        this.Parent.Jeu.Score2.Point = 0;
                        break;
                    case 1:
                        this.Parent.Jeu.Score2.Point = 15;
                        break;
                    case 2:
                        this.Parent.Jeu.Score2.Point = 30;
                        break;
                    case 3:
                        this.Parent.Jeu.Score2.Point = 40;
                        break;
                    case 4:
                        this.Parent.ScoreJoueur2 = new Score();
                        GameEndedEventArgs geea = new GameEndedEventArgs(2, this.Parent.Jeu);
                        this.OnGameEnded(this, geea);
                        this.Parent.Jeu.Score2.Point = 0;
                        break;
                }
            }
        }

        public void Pong_OnGameEnded(object sender, GameEndedEventArgs e)
        {
            if (e.Joueur == 1)
            {
                if ((this.Parent.Set.Score1.Point == 6) && (this.Parent.Set.Score1.Point - 2 >= this.Parent.Set.Score2.Point))
                {
                    SetEndedEventArgs seea = new SetEndedEventArgs(1, this.Parent.Set);
                    this.OnSetEnded(this, seea);
                    this.Parent.Set.Score1.Point = 0;
                }
                else
                {
                    this.Parent.Set.ListeJeu.AddLast(new Jeu(e.Jeu));
                    if (e.Jeu.Score1.Point > e.Jeu.Score2.Point)
                        this.Parent.Set.Score1.Point++;
                    else if (e.Jeu.Score1.Point < e.Jeu.Score2.Point)
                        this.Parent.Set.Score2.Point++;
                    else
                        this.Parent.Set.Score1.Point++;
                }
            }
            else
            {
                if ((this.Parent.Set.Score2.Point == 6) && (this.Parent.Set.Score2.Point - 2 >= this.Parent.Set.Score1.Point))
                {
                    SetEndedEventArgs seea = new SetEndedEventArgs(2, this.Parent.Set);
                    this.OnSetEnded(this, seea);
                    this.Parent.Set.Score2.Point = 0;
                }
                else
                {
                    this.Parent.Set.ListeJeu.AddLast(new Jeu(e.Jeu));
                    if (e.Jeu.Score1.Point > e.Jeu.Score2.Point)
                        this.Parent.Set.Score1.Point++;
                    else if (e.Jeu.Score1.Point < e.Jeu.Score2.Point)
                        this.Parent.Set.Score2.Point++;
                    else
                        this.Parent.Set.Score2.Point++;
                }
            }
        }

        public void Pong_OnSetEnded(object sender, SetEndedEventArgs e)
        {
            if (e.Joueur == 1)
            {
                if (this.Parent.Match.Score1.Point == 3)
                {
                    MatchEndedEventArgs meea = new MatchEndedEventArgs(this.Parent.Match);
                    this.onMatchEnded(this, meea);
                    this.Parent.Match.Score1.Point = 0;
                }
                else
                {
                    this.Parent.Match.ListeSet.AddLast(new Set(e.Set));
                    if (e.Set.Score1.Point > e.Set.Score2.Point)
                        this.Parent.Match.Score1.Point++;
                    else if (e.Set.Score1.Point < e.Set.Score2.Point)
                        this.Parent.Match.Score2.Point++;
                    else
                        this.Parent.Match.Score1.Point++;
                }
            }
            else
            {
                if (this.Parent.Match.Score2.Point == 3)
                {
                    MatchEndedEventArgs meea = new MatchEndedEventArgs(this.Parent.Match);
                    this.onMatchEnded(this, meea);
                    this.Parent.Match.Score2.Point = 0;
                }
                else
                {
                    this.Parent.Match.ListeSet.AddLast(new Set(e.Set));
                    if (e.Set.Score1.Point > e.Set.Score2.Point)
                        this.Parent.Match.Score1.Point++;
                    else if (e.Set.Score1.Point < e.Set.Score2.Point)
                        this.Parent.Match.Score2.Point++;
                    else
                        this.Parent.Match.Score2.Point++;
                }
            }
        }

        public void Pong_onMatchEnded(object sender, MatchEndedEventArgs e)
        {
            this.Parent.ScoreJoueur1 = new Score();
            this.Parent.ScoreJoueur2 = new Score();
            this.Parent.Jeu = new Jeu();
            this.Parent.Set = new Set();
            this.Parent.Match = new Match();
        }
    }
}