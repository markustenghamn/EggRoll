using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Egg_roll.Menus;
using Egg_roll.Server;

namespace Egg_roll
{
    public class MainScene
    {
        GraphicsDeviceManager graphics;
        OnScreenMessages onScreenMessages;
        float elaps, totaltPlayTime;
        Input input;
        List<Character> characters;
        Player player;

        Bird bird;
        bool birdFirstCall = true;

        protected Color fontColor = Color.White;
        protected SpriteFont spriteFont;

        Rectangle DrawRect;

        bool playerOnGround = false;

        public bool SetResetPlayer
        {
            set { player.adjustPlayer = value; }
        }

        public static float yaccel;

        public MainScene(ContentManager content, GraphicsDeviceManager graphicsManager)
        {
            WorldGen.Initialize(content);
            Stuff.Initialize(content, graphicsManager);
            Sound.LoadContent();
            graphics = graphicsManager;
            onScreenMessages = new OnScreenMessages();
            Camera2d.Position = Stuff.ScreenCenter;
            input = new Input();
            characters = new List<Character>();
            player = new Player(characters);
            bird = new Bird();
            spriteFont = Stuff.Content.Load<SpriteFont>("Fonts\\loadingfont");
        }

        public void Update(GameTime gameTime, ScreenManager screenManager)
        {
            DrawRect = new Rectangle((int)player.Position.X - graphics.PreferredBackBufferWidth - 250, (int)player.Position.Y - (graphics.PreferredBackBufferHeight) - 250, (graphics.PreferredBackBufferWidth *2) + 500, (graphics.PreferredBackBufferHeight * 2) + 500);
            WorldGen.Update(DrawRect, player.Position, ref player.coin);
            elaps = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totaltPlayTime += elaps;
            Input.Update();
            onScreenMessages.Update(elaps);
            int c = characters.Count;
            for (int i = 0; i < c; i++)
            {
                characters[i].Update(elaps);
            }

            if (player.Position.X > 2000)
                bird.Update(player, elaps);

            player.Update(elaps, yaccel, screenManager);

            //playerOnGround = player.IsOnGround;
        }

        

        public static void AccelData(Vector3 AccelerationReading)
        {
            yaccel = AccelerationReading.Y;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, bool gameActive)
        {
            graphics.Clear(Color.CornflowerBlue);
            Matrix camera = Camera2d.GetTransformation(graphics);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, camera);
            onScreenMessages.Draw(spriteBatch);
            WorldGen.Draw(spriteBatch);
            int c = characters.Count;
            for (int i = 0; i < c; i++)
            {
                if (DrawRect.Contains((int)characters[i].Position.X, (int)characters[i].Position.Y))
                    characters[i].Draw(spriteBatch);
            }

            bird.Draw(spriteBatch);
            player.Draw(spriteBatch);


            spriteBatch.DrawString(
                spriteFont,
                "Score: " + GetPlayerScore().ToString(),
                Camera2d.GetPositionInWorld(new Vector2(100, 0)),
                fontColor);

            //spriteBatch.DrawString(
            //    spriteFont,
            //    "Current Tile X: " + player.CurrentTile.X.ToString(),
            //    Camera2d.GetPositionInWorld(new Vector2(100, 75)),
            //    fontColor);

            //spriteBatch.DrawString(
            //    spriteFont,
            //    "Current Tile Y: " + player.CurrentTile.Y.ToString(),
            //    Camera2d.GetPositionInWorld(new Vector2(100, 100)),
            //    fontColor);

            spriteBatch.End();

            if (gameActive)
                player.DrawUI(spriteBatch);


        }

        public int GetPlayerScore()
        {
            return player.Score;
        }

        public void ResetGame()
        {
            characters = new List<Character>();
            player = new Player(characters);
            bird = new Bird();
        }
    }
}
