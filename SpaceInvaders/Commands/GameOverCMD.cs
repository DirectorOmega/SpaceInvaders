using SpaceInvaders.GameState;

namespace SpaceInvaders.Commands
{
    class GameOverCMD : Command
    {
        public override void execute(float deltaTime)
        {
            GameStateManager.setStateGameOver();
        }
    }
}
