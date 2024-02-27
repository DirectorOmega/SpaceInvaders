using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.InputSystem
{
    internal sealed class OKeyObserver : InputObserver
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
