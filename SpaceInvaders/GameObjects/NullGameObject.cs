using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.CollisionSystem;

namespace SpaceInvaders.GameObjects
{
    class NullGameObject : GameObject
    {
        public NullGameObject() : base(SpriteID.NullSprite, 0.0f, 0.0f)
        {
        }

        //public NullGameObject(float x, float y) : base(x, y)
        //{
        //}

        public override void Accept(ColVistor other)
        {

        }

        public override void cClean()
        {
        }


        public override void Update()
        {
        }
    }
}