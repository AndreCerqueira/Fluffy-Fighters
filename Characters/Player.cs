using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace FluffyFighters.Characters
{
    public class Player : AnimatedSprite
    {
        // Constants
        private const string PLAYER_ASSET_PATH = "sprites/player/player_spritesheet"; //
        private const int PLAYER_ROWS = 5;
        private const int PLAYER_COLUMNS = 4;
        private const float SPEED = 100f;
        private const int IDLE_ROW = 0;
        private const int WALK_LEFT_ROW = 4;
        private const int WALK_RIGHT_ROW = 3;
        private const int WALK_UP_ROW = 2;
        private const int WALK_DOWN_ROW = 1;

        public Vector2 velocity { get; set; }

        public Player(Game game) : 
            base(game, game.Content.Load<Texture2D>(PLAYER_ASSET_PATH), PLAYER_ROWS, PLAYER_COLUMNS) 
        { 
        }

        public override void Update(GameTime gameTime)
        {
            // Atualize a velocidade do jogador com base nas teclas pressionadas
            Vector2 playerInput = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
                playerInput.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S))
                playerInput.Y += 1;
            if (keyboardState.IsKeyDown(Keys.A))
                playerInput.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D))
                playerInput.X += 1;

            // Normalize o vetor de entrada para manter a velocidade constante independentemente da direção
            if (playerInput != Vector2.Zero)
                playerInput.Normalize();

            velocity = playerInput * SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += velocity;

            // Atualize a animação do jogador com base na direção
            if (playerInput.X < 0)
                currentRow = WALK_LEFT_ROW;
            else if (playerInput.X > 0)
                currentRow = WALK_RIGHT_ROW;
            else if (playerInput.Y < 0)
                currentRow = WALK_UP_ROW;
            else if (playerInput.Y > 0)
                currentRow = WALK_DOWN_ROW;
            else
                currentRow = IDLE_ROW;

            base.Update(gameTime);
        }
    }
}
