using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment
{
    class PanningBackground : DrawableGameComponent
    {
        private Character character;
        private Rectangle section;
        private Texture2D image;
        private SpriteBatch sb;
        //private Rectangle core, whole;
        private Rectangle top, left, right, bottom;
        private int charH, charW;
        private float speed;

        public PanningBackground(Game g, Character character, Texture2D image, SpriteBatch sb) : base(g)
        {
            this.character = character;
            this.image = image;
            this.sb = sb;
            charH = character.GetHeight();
            charW = character.GetWidth();
            speed = 5;
        }

        public override void Initialize()
        {
            base.Initialize();
            section = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Width);
            /*core = new Rectangle((GraphicsDevice.Viewport.Width / 2) + 1, (GraphicsDevice.Viewport.Height / 2) + 1, image.Width - (GraphicsDevice.Viewport.Width), image.Height - (GraphicsDevice.Viewport.Height));
            whole = new Rectangle(0, 0, image.Width, image.Height);*/
            top = new Rectangle(0, 0, image.Width, GraphicsDevice.Viewport.Height / 2);
            bottom = new Rectangle(0, image.Height - (GraphicsDevice.Viewport.Height / 2), image.Width, GraphicsDevice.Viewport.Height / 2);
            left = new Rectangle(0, 0, GraphicsDevice.Viewport.Width / 2, image.Height);
            right = new Rectangle(image.Width - (GraphicsDevice.Viewport.Width / 2), 0, GraphicsDevice.Viewport.Width / 2, image.Height);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 l = character.GetLocation();
            int xCentre = (int)l.X;
            int yCentre = (int)l.Y;

            Rectangle lRect = new Rectangle(xCentre, yCentre, charW, charH);
            /*if (core.Intersects(lRect))
            {
                section = new Rectangle(xCentre - (GraphicsDevice.Viewport.Width / 2), yCentre - (GraphicsDevice.Viewport.Height / 2), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            }*/
            if (lRect.Intersects(top) || lRect.Intersects(bottom))
            {
                yCentre = section.Y + (GraphicsDevice.Viewport.Height / 2);
            }
            if (lRect.Intersects(left) || lRect.Intersects(right))
            {
                xCentre = section.X + (GraphicsDevice.Viewport.Width / 2);
            }
            section = new Rectangle(xCentre - (GraphicsDevice.Viewport.Width / 2), yCentre - (GraphicsDevice.Viewport.Height / 2), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            
    
            base.Update(gameTime);
        }

        public Vector2 ResolveLocation(Vector2 globalLocation)
        {
            return new Vector2(globalLocation.X - section.X, globalLocation.Y - section.Y);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(image, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), section, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
