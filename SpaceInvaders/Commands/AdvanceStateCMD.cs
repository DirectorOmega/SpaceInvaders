using SpaceInvaders.GameState;


namespace SpaceInvaders.Commands
{
    class AdvanceStateCMD : Command
    {
        public override void execute(float deltaTime)
        {
            GameStateManager.Enter();
        }
    }
}
