using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ImpHunter.GameObjects
{
    class Ball : PhysicsObject
    {
        float BallSpeed;

        bool isToRemove = false;
        public Ball(Vector2 startPosition, Vector2 direction) : base("spr_cannon_ball")
        {
            Origin = Center;
            Position = startPosition;
            BallSpeed -= (direction.Y * 600);
            Velocity = direction * BallSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 gravity = new Vector2(0, 5f);
            Velocity += gravity;

            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!IsWithinScreenBounds())
            {
                isToRemove = true;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        private bool IsWithinScreenBounds()
        {
            return Position.X >= 0 && Position.X <= GameEnvironment.Screen.X
                && Position.Y >= 0 && Position.Y <= GameEnvironment.Screen.Y;
        }

        public bool IsMarkedForRemoval()
        {
            return isToRemove;
        }

        public void CheckBounce(SpriteGameObject other)
        {
            if (!CollidesWith(other)) return;

            CollisionResult side = CollisionSide(other);

            switch (side)
            {
                case CollisionResult.TOP:
                case CollisionResult.BOTTOM:
                    velocity.Y *= -0.9f;
                    break;
                case CollisionResult.LEFT:
                case CollisionResult.RIGHT:
                    velocity.X *= -0.9f;
                    break;
            }
        }

    }
}
