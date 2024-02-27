using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    internal sealed class Squid : Alien
    {
        public Squid(SpriteID octo, float posX, float posY)
             : base(octo, posX, posY) { }

        public override void Accept(ColVistor other) => other.VisitSquid(this);
        public override int getScore() => 10;
    }
}
