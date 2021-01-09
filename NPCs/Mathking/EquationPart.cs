using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InnoBossesMod.NPCs.Mathking
{
    class EquationPart : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Equation Part");
		}

		public override void SetDefaults()
		{
			npc.aiStyle = 2;
			npc.lifeMax = 10;
			npc.damage = 10;
			npc.defense = 3;
			npc.knockBackResist = 0f;
			npc.width = 30;
			npc.height = 30;
			npc.noGravity = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
		}
	}
}
