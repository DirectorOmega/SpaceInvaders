using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
   abstract class IMan : baseManager
    {
#if DEBUG
        private ILink contains;
        private TexMan asksForReusable;

#endif
        public IMan(int numStart, int deltaAdd): base(numStart,deltaAdd)
        {

        }
    }
}
