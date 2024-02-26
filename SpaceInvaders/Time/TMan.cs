using SpaceInvaders.Manager;

namespace SpaceInvaders.Time
{
    abstract class TMan : baseManager
    {
#if DEBUG
        private TLink pActive;
        private TLink pReserve;
#endif
        public TMan(int numStart = 5, int deltaAdd = 3) : base (numStart,deltaAdd)
        { }
    }
}
