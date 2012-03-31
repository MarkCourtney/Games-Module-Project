using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment
{
    class Character : DrawableGameComponent
    {
        private Texture2D picture;
        private float rotation;
        private float speed;
        private Vector2 rotationVector;
        private Vector2 location;
        private Vector2 localLocation;
        private SpriteBatch sb;
        private bool hasRotated;
        private PanningBackground background;

        public Character(Game g, Texture2D picture, float startX, float startY, float speed, SpriteBatch sb) : base(g)
        {
            this.picture = picture;
            this.speed = speed;
            this.sb = sb;
            rotation = 0.0f;
            rotationVector = new Vector2(0.0f, 0.01f);
            location = new Vector2(startX, startY);
            hasRotated = false;
        }

        public Vector2 GetLocation()
        {
            return location;
        }

        public int GetHeight()
        {
            return picture.Height;
        }

        public int GetWidth()
        {
            return picture.Width;
        }

        public void SetBackground(PanningBackground background)
        {
            this.background = background;
        }

        public float GetRotation()
        {
            return rotation;
        }
        
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState k = Keyboard.GetState();

            // If turning left
            if (k.IsKeyDown(Keys.Left))
            {
                rotation -= 0.01f;
                rotationVector.X = (float) Math.Sin(rotation);
                rotationVector.Y = (float) -Math.Cos(rotation);
                hasRotated = true;
            }

            // If turning right 
            else if (k.IsKeyDown(Keys.Right))
            {
                rotation += 0.01f;
                rotationVector.X = (float) Math.Sin(rotation);
                rotationVector.Y = (float) -Math.Cos(rotation);
                hasRotated = true;
            }

            // Forwards
            else if (k.IsKeyDown(Keys.Up))
            {
                location.X += speed * rotationVector.X;
                if (hasRotated)
                {
                    location.Y += speed * rotationVector.Y;
                }
                else
                {
                    location.Y -= speed;
                }
            }

            // Backwards
            else if (k.IsKeyDown(Keys.Down))
            {
                location.X -= speed * rotationVector.X;
                if (hasRotated)
                {
                    location.Y -= speed * rotationVector.Y;
                }
                else
                {
                    location.Y += speed;
                }
            }

            if (background != null) localLocation = background.ResolveLocation(location);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (background == null) return;
            sb.Begin();
            sb.Draw(picture, new Rectangle((int)localLocation.X, (int)localLocation.Y, picture.Width, picture.Height), null, Color.White, rotation, new Vector2(picture.Width / 2, picture.Height / 2), SpriteEffects.None, 0);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
