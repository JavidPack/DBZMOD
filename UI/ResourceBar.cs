﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DBZMOD.UI
{
	internal enum ResourceBarMode
	{
		KI
	}
	class ResourceBar : UIElement
	{
		private ResourceBarMode stat;
		private float width;
		private float height;

		public ResourceBar(ResourceBarMode stat, int height, int width)
		{
			this.stat = stat;
			this.width = width;
			this.height = height;
		}
		private UIText text;

		Rectangle barDestination;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize()
		{
			Height.Set(height, 0f); //Set Height of element
			Width.Set(width, 0f);   //Set Width of element

			//assign color to panel depending on stat
			switch (stat)
			{
				case ResourceBarMode.KI:
					gradientA = new Color(0, 255, 255); //blue
					gradientB = Color.Red;
					break;
				default:
					break;
			}

			text = new UIText("0/0"); //text to show current hp or mana
			text.Width.Set(width, 0f);
			text.Height.Set(height, 0f);
			text.Top.Set(height / 2 + 10, 0f); //center the UIText
			text.Left.Set(width - 45, 0f);

			var BarTexture = GFX.KiBar;
			UIImage ki = new UIImage(BarTexture);
			ki.Top.Set(-8, 0f);
			ki.Width.Set(80, 0f);
			ki.Height.Set(18, 0f);
			Append(ki);

			Append(text);

			barDestination = new Rectangle(20, 0, (int)width, (int)height);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch); // draws nothing since this inherits from UIElement

			MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
			float quotient = 1f;
			//Calculate quotient
			switch (stat)
			{
				case ResourceBarMode.KI:
					quotient = (float)player.KiCurrent / (float)player.KiMax;
					quotient = Utils.Clamp(quotient, 0, 1);
					break;

				default:
					break;
			}

			Rectangle hitbox = GetInnerDimensions().ToRectangle();
			hitbox.X += barDestination.X;
			hitbox.Y += barDestination.Y;
			hitbox.Width = barDestination.Width;
			hitbox.Height = barDestination.Height;
			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1)
			{
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				spriteBatch.Draw(Main.magicPixel, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
			}
		}

		public override void Update(GameTime gameTime)
		{
			MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>(); //Get Player
			switch (stat)
			{
				case ResourceBarMode.KI:
					text.SetText("Ki:" + player.KiCurrent + " / " + player.KiMax); //Set Life
					break;

				default:
					break;
			}
			base.Update(gameTime);
		}

	}
}