
namespace SpaceInvaders.InputSystem
{
    abstract class InputObserver : InLink
    {
        // define this in concrete
        abstract public void Notify();

        public InputSubject pSubject;
    }
}
