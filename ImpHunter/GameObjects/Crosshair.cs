using Microsoft.Xna.Framework;

namespace ImpHunter {
    class Crosshair : SpriteGameObject{
        static SpriteSheet[] sprites;

        int expandTimer = 0;
        bool isExpanding = false;

        private int expandCooldown = 10;
        
        /// <summary>
        /// Crosshair constructor, loads both sprites.
        /// </summary>
        public Crosshair() : base("spr_aim_small") {
            sprites = new SpriteSheet[] {new SpriteSheet("spr_aim_small"), new SpriteSheet("spr_aim_big")};
            Origin = Center;
        }

        /// <summary>
        /// Positions the crosshair at the position of the mouse.
        /// </summary>
        /// <param name="inputHelper"></param>
        public override void HandleInput(InputHelper inputHelper) {
            base.HandleInput(inputHelper);

            Position = inputHelper.MousePosition;
        }

        /// <summary>
        /// Updates the expand timer.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (IsExpanding) {
                expandTimer++;
                if (expandTimer > expandCooldown)
                    IsExpanding = false;
            }
        }

        /// <summary>
        /// Call this to expand the crosshair based on a cooldown.
        /// </summary>
        /// <param name="cooldown">Amount it ticks until the crosshair returns to normal.</param>
        public void Expand(int cooldown) {
            IsExpanding = true;
            expandCooldown = cooldown;
        }

        /// <summary>
        /// Changes the sprite when set.
        /// </summary>
        public bool IsExpanding {
            get { return isExpanding; }
            set {
                isExpanding = value;

                if (value == false) {
                    sprite = sprites[0];
                    Shade = Color.White;
                    
                } else {
                    sprite = sprites[1];
                    Shade = Color.Red;
                    expandTimer = 0;
                }
            }
        }
    }
}
