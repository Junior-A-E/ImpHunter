using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ImpHunter.GameObjects
{
    class Enemy : AnimatedGameObject
    {
        Cannon cannon;
        float Speed = 50f;
        const float ArrivalRadius = 100f;
        private bool isAlive = false;

        private const int SHOOT_COOLDOWN = 20;
        private int shootTimer = 0;

        public bool IsAlive
        {
            get => isAlive;
            set => isAlive = value;
            
        }
        public Enemy(Vector2 position, Cannon cannon) : base()
        {
            this.cannon = cannon;
            this.position = position;

            LoadAnimation("spr_imp_flying", "fly", true);
            LoadAnimation("spr_imp_hit", "hit", true);
            LoadAnimation("spr_imp_falling", "fall", true);
            PlayAnimation("fly");

            isAlive = true;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isAlive)
            {
                Vector2 directionToCannon = cannon.Position - Position;
                float distanceToCannon = directionToCannon.Length();

                directionToCannon.Normalize();

                if (distanceToCannon > ArrivalRadius)
                {
                    Velocity = directionToCannon * Speed;
                }
                else
                {
                    Velocity = directionToCannon * (Speed * distanceToCannon / ArrivalRadius);
                }
            }
            else
            {
                if (shootTimer > SHOOT_COOLDOWN)
                {
                    Die();
                    shootTimer = 0;
                }
                shootTimer++;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void Die()
        {            
            Velocity = new Vector2(0, 400);
            PlayAnimation("fall");
        }

        public void Hit()
        {
            PlayAnimation("hit");
        }
    }
}
