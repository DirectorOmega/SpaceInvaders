using SpaceInvaders.Manager;

namespace SpaceInvaders.FontSystem
{
    abstract class FMan : baseManager
    {
#if DEBUG
        private FLink pActive;
        private FLink pReserved;
#endif
        public FMan(int numStart, int deltaAdd): base(numStart,deltaAdd)
        {
        }
    }
}
