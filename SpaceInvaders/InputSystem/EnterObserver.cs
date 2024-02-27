using SpaceInvaders.GameState;

namespace SpaceInvaders.InputSystem
{
    internal sealed class EnterObserver : InputObserver
    {
        public override void dClean()
        {
           
        }

        public override void Notify()
        {
            GameStateManager.Enter();
        }
    }
}
