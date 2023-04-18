using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FluffyFighters.Others
{
    static public class Utils
    {
        public static Texture2D ResizeTexture(Texture2D texture, float scaleFactor)
        {
            int newWidth = (int)(texture.Width * scaleFactor);
            int newHeight = (int)(texture.Height * scaleFactor);

            // Create a new texture with the desired dimensions
            Texture2D resizedTexture = new Texture2D(texture.GraphicsDevice, newWidth, newHeight);

            // Scale the original texture onto the new texture
            Color[] textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);

            Color[] resizedData = new Color[newWidth * newHeight];
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    int index = (int)(y / scaleFactor) * texture.Width + (int)(x / scaleFactor);
                    resizedData[y * newWidth + x] = textureData[index];
                }
            }

            resizedTexture.SetData(resizedData);

            return resizedTexture;
        }
    }
}
