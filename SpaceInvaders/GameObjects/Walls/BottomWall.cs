using System;
using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    class BottomWall : GameObject
    {
        public BottomWall(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {


        }

        public override void Accept(ColVistor other)
        {
          
        }

        public override void VisitBombRoot(BombRoot br)
        {
            Reactions.Reaction(br, this);
        }

        public override void VisitBomb(Bomb b)
        {
            Reactions.Reaction(b, this);
        }

        public override void cClean()
        {
            
        }
    }
}
