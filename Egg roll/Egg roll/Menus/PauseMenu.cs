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
        Button btnResume;

        SoundEffect MenuTapFX;

        public PauseMenu()
        {
            btnBack = new Button("pixel", new Vector2(710, 420), new Rectangle(0, 0, 150, 100));
            btnResume = new Button("pixel", new Vector2(710, 300), new Rectangle(0, 0, 150, 100));
            LoadContent();
        }

        //Loads Content
        void LoadContent()
        {
            MenuTapFX = Stuff.Content.Load<SoundEffect>("Sound\\MenuTap");
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
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
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
                Sound.PlaySoundEffect(MenuTapFX);
                screenManager.CurrentMenu = -1;
                screenManager.ResetGame();
            }
            else if (btnResume.active)
            {
                Sound.PlaySoundEffect(MenuTapFX);
                screenManager.CurrentMenu = 0;
            }

        }
    }
}
