using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    internal sealed class Octo : Alien
    {
        public Octo(SpriteID octo, float posX, float posY)
            : base(octo, posX, posY)
        {

        }

        public override void Accept(ColVistor other)
        {
            other.VisitOcto(this);
        }

        public override int getScore()
        {
            return 30;
        }
        //public override Enum getName()
        //{
        //    throw new NotImplementedException();
        //}
    }
}