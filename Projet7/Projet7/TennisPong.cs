using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Projet7
{
    public delegate void PointEndedEventHandler(object sender, PointEndedEventArgs e);
    public delegate void GameEndedEventHandler(object sender, GameEndedEventArgs e);
    public delegate void SetEndedEventHandler(object sender, SetEndedEventArgs e);
    public delegate void MatchEndedEventHandler(object sender, MatchEndedEventArgs e);

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TennisPong : Game
    {
        private GraphicsDeviceManager Graphics { get; set; }
        public SpriteBatch SpritesBatch { get; private set; }
        public SpriteFont SpritesScore { get; private set; }
        public Texture2D SpritesTexture { get; private set; }
        private Texture2D BackgroundTexture { get; set; }
        private Rectangle BackgroundRectangle { get; set; }
        public SoundEffect SdEffect1 { get; private set; }
        public SoundEffect SdEffect2 { get; private set; }
        public Humain HumanGame { get; private set; }
        public Balle BallGame { get; private set; }
        public Ai AiGame { get; private set; }
        public Boolean Partie { get; set; }
        public Boolean ServiceJoueur1 { get; set; }
        public Boolean ServiceJoueur2 { get; set; }
        public Boolean VibrationsJoueur1 { get; private set; }
        public Boolean VibrationsJoueur2 { get; private set; }
        public Boolean Sons { get; private set; }
        public Score ScoreJoueur1 { get; set; }
        public Score ScoreJoueur2 { get; set; }
        public Jeu Jeu { get; set; }
        public Set Set { get; set; }
        public Match Match { get; set; }

        public TennisPong()
        {
            this.Window.Title = "TennisPong";
            this.Graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.HumanGame = new Humain(this);
            this.BallGame = new Balle(this);
            this.AiGame = new Ai(this);
            this.Partie = false;
            this.ServiceJoueur1 = true;
            this.ServiceJoueur2 = false;
            this.VibrationsJoueur1 = true;
            this.VibrationsJoueur2 = true;
            this.Sons = true;
            this.ScoreJoueur1 = new Score();
            this.ScoreJoueur2 = new Score();
            this.Jeu = new Jeu();
            this.Set = new Set();
            this.Match = new Match();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.BackgroundRectangle = new Rectangle(0, 0, this.GraphicsDevice.Viewport.Width,
                this.GraphicsDevice.Viewport.Height);
            this.HumanGame.Initialize();
            this.AiGame.Initialize();
            this.BallGame.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            // TODO: use this.Content to load your game content here
            this.SpritesBatch = new SpriteBatch(this.GraphicsDevice);
            this.SpritesScore = this.Content.Load<SpriteFont>("Fonts/Score");
            this.SpritesTexture = this.Content.Load<Texture2D>("Textures/sprites");
            this.BackgroundTexture = Content.Load<Texture2D>("Textures/background");
            this.SdEffect1 = Content.Load<SoundEffect>("Sounds/4360__NoiseCollector__ponblipG_5");
            this.SdEffect2 = Content.Load<SoundEffect>("Sounds/4359__NoiseCollector__PongBlipF4");
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
            this.HumanGame.Update(gameTime);
            this.AiGame.Update(gameTime);
            this.BallGame.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            this.GraphicsDevice.Clear(Color.OrangeRed);
            this.SpritesBatch.Begin();
            this.SpritesBatch.Draw(this.BackgroundTexture, this.BackgroundRectangle, Color.White);
            this.SpritesBatch.End();
            this.SpritesBatch.Begin();
            this.SpritesBatch.DrawString(this.SpritesScore, this.ScoreJoueur1.Point.ToString() + " - " +
                this.Jeu.Score1.Point.ToString() + " - " + this.Set.Score1.Point.ToString() + " - " +
                this.Match.Score1.Point.ToString(), new Vector2(125, 25), Color.White);
            this.SpritesBatch.End();
            this.SpritesBatch.Begin();
            this.SpritesBatch.DrawString(this.SpritesScore, this.ScoreJoueur2.Point.ToString() + " - " +
                this.Jeu.Score2.Point.ToString() + " - " + this.Set.Score2.Point.ToString() + " - " +
                this.Match.Score2.Point.ToString(), new Vector2(525, 25), Color.White);
            this.SpritesBatch.End();
            this.HumanGame.Draw(gameTime);
            this.AiGame.Draw(gameTime);
            this.BallGame.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}