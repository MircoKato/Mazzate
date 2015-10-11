using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Mazzate
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spriteSheet;
        List<Giocatore> listaGiocatori = new List<Giocatore>();
        //List<Guerriero> tuttiGuerrieri = new List<Guerriero>();
        ManagerGuerrieri mngGuerrieri;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 576;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            int numeroGiocatori = 2;
            int guerrieriPerGiocatore = 3;

            mngGuerrieri = new ManagerGuerrieri(numeroGiocatori, this);

            for (int i = 0; i < numeroGiocatori; i++) { listaGiocatori.Add(new Giocatore((Colore)i)); }

            foreach (Giocatore giocatore in listaGiocatori)
            {
                mngGuerrieri.creaGuerrieri(giocatore.colore, guerrieriPerGiocatore);
                mngGuerrieri.posizionaGuerrieri(giocatore.colore, guerrieriPerGiocatore, graphics.GraphicsDevice.Viewport.Bounds );
            }

            Components.Add(mngGuerrieri);

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
            spriteSheet = Content.Load<Texture2D>(@"immagini\guerrieri_1.2");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mngGuerrieri.svecchiaPosizione();
            /*
            foreach (Guerriero guerriero in mngGuerrieri.arrayGuerrieri[0]) {
                guerriero.nemicoPiuVicino(mngGuerrieri.arrayGuerrieri[1]);
                guerriero.muoviVersoNemico(guerriero.obiettivo);
            }
            foreach (Guerriero guerriero in mngGuerrieri.arrayGuerrieri[1]) {
                guerriero.nemicoPiuVicino(mngGuerrieri.arrayGuerrieri[0]);
                guerriero.muoviVersoNemico(guerriero.obiettivo);
            }

            mngGuerrieri.sistemaCollisioni(mngGuerrieri.arrayGuerrieri);
            */
            mngGuerrieri.impedisciUscitaSchermo(mngGuerrieri.arrayGuerrieri, graphics.GraphicsDevice.Viewport.Bounds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);
            spriteBatch.Begin();

            Rectangle dstRect;
            Rectangle srcRect;
            Color colore;
            SpriteEffects flip;
            int j = 0;
            Vector2 origine;

            foreach (Giocatore giocatore in listaGiocatori)
            {
                int i = 0;
                switch (j)
                {
                    case 0: colore = Color.DarkRed; flip = SpriteEffects.FlipVertically; break;
                    case 1: colore = Color.DarkBlue; flip = SpriteEffects.None; break;
                    default: colore = Color.White; flip = SpriteEffects.None; break;
                }
                j++;
                foreach (Guerriero guerriero in mngGuerrieri.arrayGuerrieri[(int)giocatore.colore])
                {
                    dstRect = new Rectangle((int)guerriero.nuovaPosizione.X, (int)guerriero.nuovaPosizione.Y, 64, 64);
                    srcRect = new Rectangle(i * 64, 0, 64, 64);
                    origine = srcRect.Center.ToVector2();

                    spriteBatch.Draw(spriteSheet, dstRect, srcRect, colore, 0f/*guerriero.guardaNemico(guerriero.obiettivo), origine*/, new Vector2(), flip, 0f);
                    i++;
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
