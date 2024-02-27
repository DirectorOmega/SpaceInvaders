using SpaceInvaders.GameState;

namespace SpaceInvaders.Commands
{
    internal sealed class GameOverCMD : Command
    {
        public override void execute(float deltaTime) 
            => GameStateManager.setStateGameOver();
    }
}
