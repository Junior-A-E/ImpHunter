using Microsoft.Xna.Framework;

namespace ImpHunter {
    class Fortress : GameObjectList {
        public GameObjectList towers;
        SpriteGameObject wall;
        private const int TOWER_OFFSET = 20;

        /// <summary>
        /// Fortress constructor, draws the wall and both towers.
        /// </summary>
        public Fortress() {
            Add(towers = new GameObjectList());

            SpriteGameObject towerLeft = new SpriteGameObject("spr_tower_bare");
            towerLeft.Position = new Vector2(-towerLeft.Width * 0.25f, GameEnvironment.Screen.Y - towerLeft.Height - TOWER_OFFSET);
            towers.Add(towerLeft);

            SpriteGameObject towerRight = new SpriteGameObject("spr_tower_bare");
            towerRight.Position = new Vector2(GameEnvironment.Screen.X - towerRight.Width * 0.75f, GameEnvironment.Screen.Y - towerRight.Height - TOWER_OFFSET);
            towers.Add(towerRight);

            wall = new SpriteGameObject("spr_wall_bare");
            wall.Position = new Vector2(0, GameEnvironment.Screen.Y - wall.Height);
            Add(wall);
        }

        /// <summary>
        /// Checks if there's a collision between all child objects and a given object.
        /// </summary>
        /// <param name="other">SpriteGameObject with which to check collision.</param>
        public bool CollidesWith(SpriteGameObject other) {
            foreach(SpriteGameObject child in Children) {
                if (child.CollidesWith(other))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if there's a collision between all towers objects and a given object.
        /// </summary>
        /// <param name="other">SpriteGameObject with which to check collision.</param>
        public bool CollidesWithTowers(SpriteGameObject other) {
            foreach (SpriteGameObject child in towers.Children) {
                if (child.CollidesWith(other))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a list of towers.
        /// </summary>
        public GameObjectList Towers {
            get { return towers; }
        }

        /// <summary>
        /// Returns the wall as a SpriteGameObject.
        /// </summary>
        public SpriteGameObject Wall {
            get { return wall; }
        }
    }
}
