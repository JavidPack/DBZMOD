using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBZMOD.Items.Consumables
{
	public class SenzuBean : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 20;
			item.healLife = 9001;
            item.healMana = 9001;
			item.consumable = true;
			item.maxStack = 3;
			item.UseSound = SoundID.Item3;
			item.useStyle = 2;
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.value = 10000;
			item.rare = 5;
			item.potion = false;
		}
    
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Senzu Bean");
            Tooltip.SetDefault("Restores your body and Ki");
        }
        

        public override bool UseItem(Player player)
        {
            if(MyPlayer.ModPlayer(player).senzuBag)
            {
                player.AddBuff(mod.BuffType("SenzuCooldown"), 14400);
            }
            else
            {
                player.AddBuff(mod.BuffType("SenzuCooldown"), 18000);
            }
            MyPlayer.ModPlayer(player).KiCurrent = MyPlayer.ModPlayer(player).KiMax;
            return true;
            
        }
        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(mod.BuffType("SenzuCooldown")))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
