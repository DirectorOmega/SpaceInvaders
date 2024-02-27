using SpaceInvaders.Manager;

namespace SpaceInvaders.FontSystem
{
    abstract class GMan : baseManager
    {

#if DEBUG
        private GLink pActive;
        private GLink pReserved;
#endif

        public GMan(int numStart, int deltaAdd): base(numStart,deltaAdd)
        {
        }
    }
}
