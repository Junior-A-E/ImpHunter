using ImpHunter.GameObjects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ImpHunter
{
    class PlayingState : GameObjectList
    {
        Cannon cannon;
        Crosshair crosshair;
        Fortress fortress;

        private GameObjectList cannonBalls;

        private const int SHOOT_COOLDOWN = 20;
        private int shootTimer = SHOOT_COOLDOWN;
        private const int MAX_IMPS = 3;
        private int score = 0;

        private List<Enemy> enemyRemove = new List<Enemy>();
        private List<Ball> ballRemove = new List<Ball>();

        /// <summary>
        /// PlayingState constructor which adds the different gameobjects and lists in the correct order of drawing.
        /// </summary>
        public PlayingState()
        {
            Add(new SpriteGameObject("spr_background"));

            Add(cannon = new Cannon());
            cannon.Position = new Vector2(GameEnvironment.Screen.X / 2, 490);

            Add(fortress = new Fortress());

            Add(cannonBalls = new GameObjectList());

            // Always draw the crosshair last.
            Add(crosshair = new Crosshair());

            TextGameObject scoreDisplay = new TextGameObject("GameFont");
            scoreDisplay.Position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y - 80);
            scoreDisplay.Text = score.ToString();
            Add(scoreDisplay);

            cannon.Barrel.LookAt(crosshair, -90);

            SpawnImps();
        }

        private void UpdateScoreDisplay()
        {
            foreach (var gameObj in Children)
            {
                if (gameObj is TextGameObject scoreDisplay)
                {
                    scoreDisplay.Text = score.ToString();
                    break;
                }
            }
        }

        private void SpawnImps()
        {
            Vector2 impStartPosition = new Vector2(GameEnvironment.Random.Next(200, 800), 0);
            Enemy enemy = new Enemy(impStartPosition,cannon);            

            Add(enemy);
        }

        /// <summary>
        /// Updates the PlayingState.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (SpriteGameObject tower in fortress.Towers.Children)
            {
                cannon.CheckBounce(tower);
            }

            foreach (Ball cannonBall in cannonBalls.Children)
            {
                foreach (SpriteGameObject tower in fortress.Towers.Children)
                {
                    cannonBall.CheckBounce(tower);
                }
                cannonBall.CheckBounce(fortress.Wall);
                foreach (GameObject impObj in Children)
                {
                    if (impObj is Enemy enemy && enemy.CollidesWith(cannonBall))
                    {
                        score++;
                        UpdateScoreDisplay();
                        ballRemove.Add(cannonBall);
                        enemyRemove.Add(enemy);
                        enemy.IsAlive = false;
                        enemy.Hit();                    
                    }
                }
            }

            foreach(Ball ballDelete in ballRemove)
            {
                cannonBalls.Remove(ballDelete);
            }

            foreach(Enemy enemyDelete in enemyRemove)
            {
                if (enemyDelete.Position.Y >= GameEnvironment.Screen.Y + enemyDelete.Height)
                {
                    Remove(enemyDelete);
                }              
            }

            var currentImpCount = Children.Count(obj => obj is Enemy);
            while (currentImpCount < MAX_IMPS)
            {
                SpawnImps();
                currentImpCount++;
            }

        }

        /// <summary>
        /// Allows the player to shoot after a cooldown.
        /// </summary>
        /// <param name="inputHelper"></param>
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            shootTimer++;

            if (inputHelper.MouseLeftButtonPressed() && shootTimer > SHOOT_COOLDOWN)
            {
                Vector2 mousePosition = new Vector2(inputHelper.MousePosition.X - crosshair.Width / 2, inputHelper.MousePosition.Y - crosshair.Height / 2);
                Vector2 direction = mousePosition - cannon.Barrel.GlobalPosition;
                direction.Normalize();

                Vector2 cannonBallStartPosition = cannon.Barrel.GlobalPosition + direction * cannon.Barrel.Width;

                Ball cannonBall = new Ball(cannonBallStartPosition, direction);
                cannonBalls.Add(cannonBall);

                crosshair.Expand(SHOOT_COOLDOWN);
                shootTimer = 0;
            }
        }
    }
}
