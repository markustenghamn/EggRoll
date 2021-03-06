using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Egg_roll.Menus
{
    class PauseMenu : Menu
    {
        Sprite transparentBackground; 
        Button btnResume;


        public PauseMenu()
        {
            btnBack = new Button("Buttons", new Vector2(200, 400), new Rectangle(0, 120 * 7, 250, 120), false, false);
            btnResume = new Button("Buttons", new Vector2(600, 400), new Rectangle(0, 120 * 3, 250, 120), false, false);
            transparentBackground = new Sprite("pixel");
            transparentBackground.Source = new Rectangle(0, 0, Stuff.Resolution.X, Stuff.Resolution.Y);
            transparentBackground.iColor.SetColor(0, 0, 0, 100);
            transparentBackground.Origin = Vector2.Zero; 
            LoadContent();
        }

        //Loads Content
        void LoadContent()
        {
       
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            Input.Update();
            btnBack.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            btnResume.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            Controls();

            base.Update(gameTime);
        }


        public override void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            spriteFont = Stuff.Content.Load<SpriteFont>("Fonts\\loadingfont");
            position = new Vector2(100, 30);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            transparentBackground.Draw(spriteBatch); 
            spriteBatch.DrawString(
                spriteFont,
                "Game Paused",
                position,
                fontColor);
            btnBack.Draw(spriteBatch);
            btnResume.Draw(spriteBatch);
            spriteBatch.End();

        }
        private void Controls()
        {
            if (btnBack.active)
            {
                Sound.StopMusic();
                Sound.PlayMenuTapSound();
                screenManager.CurrentMenu = -1;
                screenManager.ResetGame();
            }
            else if (btnResume.active)
            {
                Sound.ResumeMusic();
                Sound.PlayMenuTapSound();
                screenManager.CurrentMenu = 0;
            }

        }
    }
}
