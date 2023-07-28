using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ImpHunter
{

    public class ImpHunter : GameEnvironment
    {
        public ImpHunter()
        {
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Screen = new Point(800, 600);
            ApplyResolutionSettings();

            // Add the game states.
            GameStateManager.AddGameState("Start", new StartGame());
            GameStateManager.AddGameState("Play", new PlayingState());
            GameStateManager.AddGameState("Losing", new LosingScreen());

            // Start with the StartGame.
            GameStateManager.SwitchTo("Start");
        }

    }
}
