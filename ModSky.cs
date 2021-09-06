using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;

namespace Thinf
{
	public class ModSky : CustomSky
	{
		private Random _random = new Random();
		private bool _isActive;

		public override void OnLoad()
		{
		}

		public override void Update(GameTime gameTime)
		{
		}

		public override Color OnTileColor(Color inColor)
		{
			if (Main.LocalPlayer.GetModPlayer<MyPlayer>().ZoneChestWasteland)
			{
				return Color.DarkSlateGray;
			}
			return new Color(255, 209, 59);
		}

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.LocalPlayer.GetModPlayer<MyPlayer>().ZoneChestWasteland)
			{
				if (maxDepth >= 0 && minDepth < 0)
				{
					spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * 0.0f /*new Color(54.4156f, 183.0512f, 18.4832f) * 0.35f*/);
					return;
				}
			}

			if (maxDepth >= 0 && minDepth < 0)
			{
				spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(255, 209, 59) * 0.24f);
			}
		}

		public override float GetCloudAlpha()
		{
			return 0f;
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
		}

		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		public override void Reset()
		{
			this._isActive = false;
		}

		public override bool IsActive()
		{
			return this._isActive;
		}
	}
}