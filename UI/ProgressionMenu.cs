﻿using System;
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
    internal class ProgressionMenu : EasyMenu
    {
        public static bool menuvisible = false;
        private Player player;

        //UI Buttons
        private UIImageButton button_IncreaseMaxKi;

        //UI Texts
        private UIText text_PlayerStatsTitle;
        private UIText text_UpgradeTitle;

        private UIText text_IncreaseMaxKi;
        private UIText text_KiRechargeRate;
        private UIText text_KiExp;


        //UI Values?
        const int COST_INCREASE_KIPOWERUPRATE = 50;

        public override void OnInitialize()
        {
            base.OnInitialize();
            backPanel.BackgroundColor = new Color(50, 50, 50);
            backPanel.Width.Set(600f, 0f);
            backPanel.Height.Set(300f, 0f);

            //Initialise the UI Elements

            //Player Stats
            InitText(ref text_PlayerStatsTitle, "Player Ki Statistics", 0, 0, Color.White);
            InitText(ref text_KiRechargeRate, "Power Up Ki Rate: " + "?" + " per tick", 0, 30, Color.LightBlue, text_PlayerStatsTitle);
            InitText(ref text_KiExp, "Ki Exp: X", 0, 30, Color.LightBlue, text_KiRechargeRate);

            //Upgrade section
            InitText(ref text_UpgradeTitle, "Upgrades", 0, 100, Color.White);

            //Increase Ki CHARGE RATE
            InitButton(ref button_IncreaseMaxKi, GFX.KiChargingButtonImage, new MouseEvent(OnClick_IncreaseKiMax), 0, 150);
            InitText(ref text_IncreaseMaxKi, "Ki Power Up Rate: +1", 75, 20, Color.LightBlue, button_IncreaseMaxKi);
            InitText(ref text_IncreaseMaxKi, "Cost: " + COST_INCREASE_KIPOWERUPRATE + " Ki Exp", 0, 20, Color.DarkRed, text_IncreaseMaxKi);
        }

        private void OnClick_IncreaseKiMax(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            if(player.KiExperience >= COST_INCREASE_KIPOWERUPRATE)
            {
                player.KiExperience -= COST_INCREASE_KIPOWERUPRATE;

                Main.PlaySound(SoundID.Unlock);
                player.KiRegenRate += 1;
            }

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MyPlayer myplayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            text_KiRechargeRate.SetText("Ki Recharge Rate: " + myplayer.KiRegenRate + " per tick");
            text_KiExp.SetText("Ki Exp: " + myplayer.KiExperience);
        }

        //protected override void DrawSelf(SpriteBatch spriteBatch)
        //{
        //    Vector2 MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
        //    if (backPanel.ContainsPoint(MousePosition))
        //    {
        //        Main.LocalPlayer.mouseInterface = true;
        //    }
        //    if (dragging)
        //    {
        //        backPanel.Left.Set(MousePosition.X - offset.X, 0f);
        //        backPanel.Top.Set(MousePosition.Y - offset.Y, 0f);
        //        Recalculate();
        //    }
        //}
    }
}