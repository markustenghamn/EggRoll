﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EggRollGameLib
{
    public class Character
    {
        public Sprite sprite;
        public Vector2 position;
        public float life;
        public bool remove, special;
        protected List<Character> characters; 

        public Character()
        {
        }

        public virtual void LoadContent()
        {
            life = 1;
        }

        public virtual void Update(float elaps)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.DrawAt(spriteBatch, position); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="amount"></param>
        /// <param name="rewind">if true, "rewinds" instead of just repeating the animation</param>
        /// <param name="name"></param>
        protected void LoadAnimation(int x, int y, int amount, bool rewind, string name)
        {
            if (sprite == null)
                sprite = new AnimatedSprite("Sheet");
            ((AnimatedSprite)sprite).Animations.Add(GetAnimation(x, y, amount, rewind, name));
        }
        protected Animation GetAnimation(int x, int y, int amount, bool rewind, string name)
        {
            return GetAnimation(250, 250, x, y, amount, rewind, true, name);
        }
        protected Animation GetAnimation(int spriteWidth, int spriteHeight, int x, int y, int amount, bool rewind, bool horizontalSheet, string name)
        {
            Animation an = new Animation(spriteWidth, spriteHeight);
            an.AnimationName = name;
            for (int i = 0; i < amount; i++)
            {
                if (horizontalSheet)
                    an.AddFrame(x + i, y);
                else
                    an.AddFrame(x, y + i);
            }
            if (rewind)
            {
                for (int i = amount - 1; i > 0; i--)
                {
                    if (horizontalSheet)
                        an.AddFrame(x + i, y);
                    else
                        an.AddFrame(x, y + i);
                }
            }
            return an;
        }

        public virtual void SpecialEvent()
        {
            special = true; 
        }
    }
}
