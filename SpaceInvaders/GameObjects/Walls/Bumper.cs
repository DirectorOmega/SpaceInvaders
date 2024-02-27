using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    internal sealed class Bumper : GameObject
    {

        public Bumper(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
        }

        public override void Accept(ColVistor other)
        {
            other.VisitBumper(this);
        }

        public override void cClean()
        {


        }

        public override void VisitShip(Ship p)
        {
            Reactions.Reaction(this, p);
        }

        //    class RightBumper : GameObject
        //{
        //    public RightBumper(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        //    {
        //    }

        //    public override void Accept(ColVistor other)
        //    {
        //        other.VisitRightBumper(this);
        //    }

        //    public override void cClean()
        //    {

        //    }

        //}
        //class LeftBumper : GameObject
        //{
        //    public LeftBumper(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        //    {
        //    }

        //    public override void Accept(ColVistor other)
        //    {
        //        other.VisitLeftBumper(this);
        //    }

        //    public override void cClean()
        //    {

        //    }
        //}
    }
}
