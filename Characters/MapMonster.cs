using FluffyFighters.Others;
using FluffyFighters.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FluffyFighters.Characters
{
    public class MapMonster : AnimatedSprite
    {
        // Constants
        private const int MONSTER_ROWS = 4;
        private const int MONSTER_COLUMNS = 4;
        private const int WALK_LEFT_ROW = 1;
        private const int WALK_RIGHT_ROW = 3;
        private const int WALK_UP_ROW = 2;
        private const int WALK_DOWN_ROW = 0;

        public Map map { get; set; }
        public Vector2 velocity { get; set; }
        private float patrolTime;
        private float idleTime;
        private float speed;
        private float maxPatrolTime;
        private Color defaultColor = Color.White;
        private Color hoverColor = Color.LightGray;
        private Rectangle rectangle => new Rectangle(width * currentColumn, height * currentRow, width, height);

        public bool isHovering
        {
            get
            {
                var mouseState = Mouse.GetState();
                var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                Rectangle newRectangle = new Rectangle((int)position.X - (int)map.Offset.X, (int)position.Y - (int)map.Offset.Y, width, height);
                return mouseRectangle.Intersects(newRectangle);
            }
        }

        public bool isClicked => Mouse.GetState().LeftButton == ButtonState.Pressed;

        // Clicked event
        public event EventHandler OnClicked;

        public MapMonster(Game game, Map map, string assetPath) :
            base(game, game.Content.Load<Texture2D>(assetPath), MONSTER_ROWS, MONSTER_COLUMNS)
        {
            this.map = map;
            animationSpeed = 0.4f;
            position = new Vector2(100, 100);
            speed = GetRandomSpeed();
            maxPatrolTime = GetRandomMaxPatrolTime();
        }


        public override void Update(GameTime gameTime)
        {
            if (isClicked && isHovering)
                Clicked();

            Patrol(gameTime);

            base.Update(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            var color = GetColor();
            Vector2 pos = position - map.Offset;
            float layerDepth = 0.4f;
            spriteBatch.Draw(texture, pos, rectangle, color, 0f, Vector2.Zero, InGameScreen.GAME_SCALE_FACTOR, SpriteEffects.None, layerDepth);
        }


        private void Clicked() => OnClicked?.Invoke(this, new EventArgs());


        public void DrawCollider(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = GetCollider();

            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.Aqua });
            spriteBatch.Draw(texture, rectangle, Color.Aqua);
        }


        public Rectangle GetCollider()
        {
            return new Rectangle((int)position.X + 32, (int)position.Y + 64, 32, 32);
        }


        private Color GetColor() =>isHovering ? hoverColor : defaultColor;
        

        public void Patrol(GameTime gameTime)
        {
            if (patrolTime > 0)
            {
                position += velocity * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                patrolTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                idleTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (idleTime >= maxPatrolTime)
                {
                    ChooseRandomDirection();
                    patrolTime = maxPatrolTime;
                    idleTime = 0f;
                }
            }

            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            if (velocity.X > 0)
                currentRow = WALK_RIGHT_ROW;
            else if (velocity.X < 0)
                currentRow = WALK_LEFT_ROW;
            else if (velocity.Y > 0)
                currentRow = WALK_DOWN_ROW;
            else if (velocity.Y < 0)
                currentRow = WALK_UP_ROW;
        }


        private float GetRandomSpeed()
        {
            Random random = new Random();
            return random.Next(40, 100);
        }


        private float GetRandomMaxPatrolTime()
        {
            Random random = new Random();
            return random.Next(1, 5);
        }


        private void ChooseRandomDirection()
        {
            Random random = new Random();
            int direction = random.Next(0, 4);
            switch (direction)
            {
                case 0:
                    velocity = new Vector2(0, -1);
                    break;
                case 1:
                    velocity = new Vector2(0, 1);
                    break;
                case 2:
                    velocity = new Vector2(-1, 0);
                    break;
                case 3:
                    velocity = new Vector2(1, 0);
                    break;
            }
        }
    }
}
