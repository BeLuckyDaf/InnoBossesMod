using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InnoBossesMod.NPCs.Mathking
{
    class Mathking : ModNPC
    {
        private const int maxMoveTime = 90;
        private int moveTimer;
        private float maxMoveSpeed = 6f;
        private float moveAcceleration = 0.22f;
        private int maxIdleTime = 300;
        private int idleTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Mathking");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 3000;
            npc.damage = 35;
            npc.defense = 5;
            npc.knockBackResist = 0f;
            npc.width = 100;
            npc.height = 145;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            music = MusicID.Boss2;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            base.OnHitPlayer(target, damage, crit);
            if (npc.ai[1] == 1)
            {
                npc.ai[1] = 0;
                moveTimer = maxMoveTime;
            }
        }

        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == 0f)
            {
                npc.netUpdate = true;
                npc.localAI[0] = 1f;
            }
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.velocity.Y = 10;
            }
            else
            {
                npc.TargetClosest(true);

                if (npc.ai[1] == 0) // Behaviour: float around
                {
                    idleTimer--;

                    if (npc.life < npc.lifeMax / 3)
                    {
                        idleTimer = maxIdleTime;
                        npc.ai[1] = 2;
                    }

                    if (idleTimer <= 0)
                    {
                        idleTimer = maxIdleTime;
                        npc.ai[1] = 1;
                    }

                    if (idleTimer % 120 == 0 && npc.life < npc.lifeMax / 2)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient && npc.life >= 0)
                        {
                            Vector2 spawnAt = npc.Center;
                            var tilePosition = spawnAt.ToTileCoordinates();
                            var tile = Main.tile[tilePosition.X, tilePosition.Y];
                            if (tile == null || !tile.active())
                            {
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<EquationPart>());
                            }
                        }
                    }

                    var target = Main.player[npc.target].position;
                    target.Y -= 300;
                    MoveTowards(target);
                }
                else if (npc.ai[1] == 1 || npc.ai[1] == 2) // Behaviour: follow and kill
                {
                    moveTimer--;

                    MoveTowards(Main.player[npc.target].position);

                    if (npc.ai[1] == 1 && moveTimer <= 0)
                    {
                        moveTimer = maxMoveTime;
                        npc.ai[1] = 0;
                    }
                }
            }
        }

        private void MoveTowards(Vector2 target)
        {
            Vector2 toTarget = target - (npc.position + (npc.Size / 2f));
            npc.velocity += toTarget.SafeNormalize(Vector2.Zero) * moveAcceleration;
            if (npc.velocity.Length() > maxMoveSpeed)
            {
                npc.velocity = npc.velocity.SafeNormalize(Vector2.Zero) * maxMoveSpeed;
            }
        }
    }
}
