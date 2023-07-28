namespace ImpHunter
{
    class StartGame : GameObjectList
    {
        public StartGame()
        {
            Add(new SpriteGameObject("spr_titlescreen"));
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.MouseLeftButtonPressed())
            {
                GameEnvironment.GameStateManager.SwitchTo("Play"); // Switch back to the starting screen after losing.()
                Reset();
            }
        }
    }
}
