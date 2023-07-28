using System.Diagnostics;


namespace ImpHunter
{
    public class LosingScreen : GameObjectList
    {

        public LosingScreen()
        {
            Add(new SpriteGameObject("spr_gameover"));
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.MouseLeftButtonPressed())
            {
                GameEnvironment.GameStateManager.SwitchTo("Start"); // Switch back to the starting screen after losing.()
                Reset();
            }
        }
        void CloseGameWindow()
        {
            Process currentProcess = Process.GetCurrentProcess();
            if (currentProcess != null)
            {
                currentProcess.CloseMainWindow();
            }
        }
    }
}

