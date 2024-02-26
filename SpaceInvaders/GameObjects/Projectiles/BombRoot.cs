using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.Observers;
using System.Diagnostics;

namespace SpaceInvaders.GameObjects
{
    class BombRoot : GameObject
    {
        public BombRoot(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
            this.getCollisionObject().getColSprite().setColor(0.0f, 255.0f, 0.0f);
        }

        public override void Remove()
        {
            GameObject e = (GameObject)this.getChild();
            GameObject ePrev;
            while (e != null)
            {
                if (e.getSibling() != null)
                {
                    ePrev = (GameObject)e;
                    e = (GameObject)e.getSibling();
                    ePrev.Remove();

                }
                else
                {

                    e.Remove();
                    e = null;

                }
            }
            base.Remove();
        }

        public void ClearChildren()
        {
            GameObject cur = (GameObject)this.getChild();
            RemoveObserver r;
            while (cur != null)
            {
                if (!cur.getMarked())
                {
                    cur.markForDeath();

                    //TODO: clean up this new
                    r = new RemoveObserver(cur);
                    DelayedObjectManager.Attach(r);
                }

                

                cur = (GameObject) cur.getSibling();

            }

        }

        public override void Update()
        {
            UpdateChildren();
            //base.Update();
        }

        public void UpdateChildren()
        {

            GameObject cur = (GameObject)this.getChild();

            while (cur != null)
            {

                cur.Update();
                cur = (GameObject)cur.getSibling();
            }

            UpdateColObj();
            base.Update();
            //this.Update();
            
        }

        public void UpdateColObj()
        {
            GameObject r = (GameObject)this.getChild();
            if (r != null)
            {
                CollisionRect ColTotal = this.getCollisionObject().getColRect();
                ColTotal.Set(r.getCollisionObject().getColRect());

                while (null != r)
                {
                    ColTotal.Union(r.getCollisionObject().getColRect());

                    r = (GameObject)r.getSibling();
                }

                this.getCollisionObject().getColRect().Set(ColTotal);
            }
            this.x = this.poColObj.getColRect().x;
            this.y = this.poColObj.getColRect().y;

        }

        public override void Accept(ColVistor other)
        {
            other.VisitBombRoot(this);
        }

        public override void VisitBottomWall(BottomWall s)
        {
            Reactions.Reaction(this, s);
        }

        public override void VisitMissile(Missile m)
        {
            Debug.Assert(false);
           // Reactions.Reaction(this, m);
        }

        public override void cClean()
        {
            
        }

        //public override Enum getName()
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}


