﻿﻿using System;
using System.Linq;
using DBZMOD.Buffs;
using DBZMOD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace DBZMOD.UI
{
    internal class EasyMenu : UIState
    {
        public UIPanel backPanel;
        private Player player;

        public override void OnInitialize()
        {
            backPanel = new UIPanel();
            backPanel.Width.Set(360f, 0f);
            backPanel.Height.Set(240f, 0f);
            backPanel.Left.Set(Main.screenWidth / 2f - backPanel.Width.Pixels / 2f, 0f);
            backPanel.Top.Set(Main.screenHeight / 2f - backPanel.Height.Pixels / 2f, 0f);
            backPanel.BackgroundColor = new Color(0, 0, 0);
            backPanel.OnMouseDown += new MouseEvent(DragStart);
            backPanel.OnMouseUp += new MouseEvent(DragEnd);
            base.Append(backPanel);
            base.OnInitialize();
        }

        public void InitButton(ref UIImageButton buttonToInitialise, Texture2D buttonTexture, MouseEvent buttonOnClick, float offsetX = 0, float offsetY = 0, UIElement parentElement = null)
        {
            buttonToInitialise = new UIImageButton(buttonTexture);

            buttonToInitialise.Width.Set(buttonTexture.Width, 0.0f);
            buttonToInitialise.Height.Set(buttonTexture.Height, 0.0f);
            buttonToInitialise.Left.Set(offsetX, 0f);
            buttonToInitialise.Top.Set(offsetY, 0f);
            buttonToInitialise.OnClick += buttonOnClick;

            if (parentElement == null)
            {
                backPanel.Append(buttonToInitialise);
            }
            else
            {
                parentElement.Append(buttonToInitialise);
            }
        }

        public void InitText(ref UIText TextToInitialise, string text, float offsetX = 0, float offsetY = 0, Color textColour = default(Color), UIElement parentElement = null)
        {
            TextToInitialise = new UIText(text);

            TextToInitialise.Width.Set(16f, 0f);
            TextToInitialise.Height.Set(16f, 0f);
            TextToInitialise.Left.Set(offsetX, 0f);
            TextToInitialise.Top.Set(offsetY, 0f);
            TextToInitialise.TextColor = textColour;

            if (parentElement == null)
            {
                backPanel.Append(TextToInitialise);
            }
            else
            {
                parentElement.Append(TextToInitialise);
            }
        }


        Vector2 offset;
        public bool dragging = false;

        private void DragStart(UIMouseEvent evt, UIElement listeningElement)
        {
            offset = new Vector2(evt.MousePosition.X - backPanel.Left.Pixels, evt.MousePosition.Y - backPanel.Top.Pixels);
            dragging = true;
        }

        private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
        {
            Vector2 end = evt.MousePosition;
            dragging = false;

            backPanel.Left.Set(end.X - offset.X, 0f);
            backPanel.Top.Set(end.Y - offset.Y, 0f);

            Recalculate();
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Vector2 MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
            if (backPanel.ContainsPoint(MousePosition))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            if (dragging)
            {
                backPanel.Left.Set(MousePosition.X - offset.X, 0f);
                backPanel.Top.Set(MousePosition.Y - offset.Y, 0f);
                Recalculate();
            }
        }


    }
}