
using SpaceInvaders.GraphicalObjects;
using System.Diagnostics;

namespace SpaceInvaders.GameObjects
{
    abstract class Alien : GameObject
    {
        // private SpriteID crab;
        //int dir = 1;

        public Alien(SpriteID alien, float posX, float posY) : base(alien, posX, posY)
        {

        }

        public override void cClean()
        {
         //   dir = 0;
        }

        public override void Update()
        {
            base.Update();
        }

        public virtual int getScore()
        {
            Debug.Assert(false);
            return -1;
        }
    }
}