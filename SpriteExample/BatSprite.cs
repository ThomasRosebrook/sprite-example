using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Security.Cryptography.X509Certificates;

namespace SpriteExample
{
    public enum Direction
    {
        Down,
        Right,
        Up,
        Left
    }

    /// <summary>
    /// This class represents a bat sprite
    /// </summary>
    public class BatSprite
    {
        private Texture2D texture;

        private double directionTimer;

        private double animationTimer;

        private short animationFrame = 1;

        /// <summary>
        /// The direction of the bat
        /// </summary>
        public Direction direction;

        public Vector2 Position;

        /// <summary>
        /// Loads the bat sprite texture
        /// </summary>
        /// <param name="content">The content manager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("32x32-bat-sprite");

        }

        /// <summary>
        /// Updates the bat sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            // Update the direction timer
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Switch directions every 2 seconds
            if(directionTimer > 2.0)
            {
                switch (direction) 
                {
                    case Direction.Up:
                        direction = Direction.Down;
                        break;
                    case Direction.Down:
                        direction = Direction.Right;
                        break;
                    case Direction.Right:
                        direction = Direction.Left;
                        break;
                    case Direction.Left:
                        direction = Direction.Up;
                        break;
                }
                directionTimer -= 2.0;
            }

            // Move the bat in the direction it is flying
            switch (direction)
            {
                case Direction.Up:
                    Position += new Vector2(0, -1) * 100 * (float)(gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case Direction.Down:
                    Position += new Vector2(0, 1) * 100 * (float)(gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case Direction.Left:
                    Position += new Vector2(-1, 0) * 100 * (float)(gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case Direction.Right:
                    Position += new Vector2(1, 0) * 100 * (float)(gameTime.ElapsedGameTime.TotalSeconds);
                    break;
            }
        }

        /// <summary>
        /// Draws the animated bat sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The SpriteBatch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Update animation frame
            if (animationTimer > 0.3)
            {
                animationFrame++;
                if (animationFrame > 3) animationFrame = 1;
                animationTimer -= 0.3;
            }

            var source = new Rectangle(animationFrame*32, (int)direction * 32, 32, 32);
            spriteBatch.Draw(texture, Position, source, Color.White);
        }


    }
}
