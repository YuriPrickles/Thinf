using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;

namespace Thinf
{
    public class DeadSky : CustomSky
    {
        private readonly Random _random = new Random();
        private bool _isActive;

        public override void OnLoad()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        private float GetIntensity()
        {
            return 0.9f;
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0f && minDepth < 0f && !Main.dayTime)
            {
                float intensity = GetIntensity();
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * intensity);
            }
            else
            if (maxDepth >= 0f && minDepth < 0f)
            {
                float intensity = GetIntensity();
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White * intensity);
            }
        }

        public override Color OnTileColor(Color inColor)
        {
                return Color.Gray * 1f;
        }

        public override float GetCloudAlpha()
        {
            return 0f;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            _isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            _isActive = false;
        }

        public override void Reset()
        {
            _isActive = false;
        }

        public override bool IsActive()
        {
            return _isActive;
        }
    }
}