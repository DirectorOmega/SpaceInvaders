using SpaceInvaders.Manager;

namespace SpaceInvaders.GameObjects
{
   abstract class GhMan : baseManager
    {
        public GhMan(int numStart, int deltaAdd): base(numStart,deltaAdd)
        {
        }
    }
}
