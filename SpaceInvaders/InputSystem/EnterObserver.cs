using SpaceInvaders.GameState;

namespace SpaceInvaders.InputSystem
{
    class EnterObserver : InputObserver
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
