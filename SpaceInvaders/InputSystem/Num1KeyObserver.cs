using SpaceInvaders.GameState;

namespace SpaceInvaders.InputSystem
{
    class Num1KeyObserver : InputObserver
    {
        public override void dClean()
        {

        }

        public override void Notify()
        {
            GameStateManager.addCredit();
        }
    }
}
