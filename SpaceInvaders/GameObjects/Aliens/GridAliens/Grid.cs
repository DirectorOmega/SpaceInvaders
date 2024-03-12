using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.PCS;

namespace SpaceInvaders.GameObjects
{
    internal sealed class Grid : Alien
    {
        
        float delta = 15;
        float verticalDelta = -15;
        PCSTreeIterator iterator;

        public void reverse()
        {
            delta *= -1;
        }
       
        
        //public float getDelta()
        //{
        //    return delta;
        //}

        public Grid(SpriteID nullSprite, float posX, float posY) : base(nullSprite, posX, posY)
        {
            this.            CollisionObject.GetColSprite().setColor(0.0f, 255.0f, 0.0f);
            iterator = new PCSTreeIterator(this);
        }

        public override void Update()
        {
            if (this.getChild() != null)
            {
                UpdateChildren();
                
            }
            else
            {
                this.Remove();
                //Simulation.SetState(Simulation.State.Pause);
                //stop move cmd, animation cmd, and sound.
            }
                base.Update();
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

        //replace with iterator
        private void UpdateChildren()
        {
            GameObject cur = (GameObject)iterator.first();
            cur = (GameObject)iterator.next();

            if (null != cur)
            {
                cur = (GameObject)iterator.next();

                while (!iterator.isDone())
                {
                    cur.Update();
                    cur = (GameObject)iterator.next();

                }

            }
            //else
            //{
            //    this.Remove();
            //}
        }

        //internal float getTimeDelta()
        //{
        //    return 1.0f;
        //}


        public void MoveGrid()
        {
            //numChildren = 0;
            GameObject cur = (GameObject)this.getChild();

            while (cur != null)
            {
                cur.incrementX(delta);
              
                cur.Update();
                
                cur = (GameObject)cur.getSibling();
            }

            this.x += delta;
            UpdateColObj();

        }

        public void MoveGridDown()
        {
           
            GameObject cur = (GameObject)this.getChild();

            while (cur != null)
            {
                cur.incrementY(verticalDelta);

                cur.Update();
                cur = (GameObject)cur.getSibling();
            }

            //this.y += verticalDelta;
            this.Update();
            UpdateColObj();
        }

        //bad smell, using a specfic type.
        public void RestartBombDrop()
        {

            Column cur = (Column)this.getChild();

            while (cur != null)
            {

                cur.SetBombCmd();
                cur = (Column)cur.getSibling();
            }

        }

        public void UpdateColObj()
        {
            GameObject r = (GameObject)this.getChild();
            if (r != null)
            {
                CollisionRect ColTotal = this.CollisionObject.GetColRect();
                ColTotal.Set(r.CollisionObject.GetColRect());

                while (null != r)
                {
                    ColTotal.Union(r.CollisionObject.GetColRect());

                    r = (GameObject)r.getSibling();
                }

                this.
                CollisionObject.GetColRect().Set(ColTotal);
            }
            this.x = this.poColObj.GetColRect().x;
            this.y = this.poColObj.GetColRect().y;

        }
      
        public override void Accept(ColVistor other)
        {
            other.VisitGrid(this);
        }

        public override void VisitSidewall(Sidewall s)
        {
            Reactions.Reaction(this, s);
        }

        public override void VisitMissile(Missile m)
        {
            Reactions.Reaction(this, m);
        }

        public override void VisitShieldRoot(ShieldRoot shieldRoot)
        {
            Reactions.Reaction((Grid)this, shieldRoot);
        }

        public override void VisitShield(Shield s)
        {
            Reactions.Reaction((Grid)this, s);
        }

        

        //public override Enum getName()
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}