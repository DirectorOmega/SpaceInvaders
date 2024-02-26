using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.InputSystem
{
    class OKeyObserver : InputObserver
    {
        public override void dClean()
        {

        }

        public override void Notify()
        {
            SpriteBatchManager.Toggle(SpriteBatchID.CBox);
        }
    }
}
