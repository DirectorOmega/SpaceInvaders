using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.GameState;
using SpaceInvaders.Time;
using SpaceInvaders.Commands;
using SpaceInvaders.GameObjects.Projectiles;

namespace SpaceInvaders.GameObjects
{
    class Column : Alien
    {
        //Bomb poB;
        DropBombCMD dropCMD;
        bool drop;

        public Column(SpriteID alien, float posX, float posY) : base(alien, posX, posY)
        {
            //poB = new Bomb(SpriteID.Missile);
            dropCMD = new DropBombCMD(this);
            CollisionObject.GetColSprite().setColor(0.0f, 0.0f, 255.0f);
            SetBombCmd();
        }

        public void SetBombCmd() 
            => TimerManager.Add(TimeEventID.bombDrop, dropCMD, (GameStateManager.getRandomNumber(10, 20) - 2 * GameStateManager.GetWaveMult()));

        internal void DropBomb()
        {
            drop = true;
        }

        public override void Accept(ColVistor other)
        {
            other.VisitColumn(this);
        }

        public override void VisitShield(Shield s)
        {
            Reactions.Reaction((Column)this, s);
        }

        public override void VisitShieldColumn(ShieldColumn sc)
        {
            Reactions.Reaction((Column)this, sc);
        }

        public override void VisitShieldBrick(ShieldBrick sb)
        {
            Reactions.Reaction((Column)this, sb);
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

        //todo fix bug in here.
        //I want to move the union calculation into another method that gets called only when baddies are killed or added.
        //Since the grid should just move each step normally untill something is shot.
        public void UpdateColObj()
        {
            GameObject r = (GameObject)this.getChild();
            if (null != r)
            {
                CollisionRect ColTotal = this.CollisionObject.GetColRect();
                ColTotal.Set(r.CollisionObject.GetColRect());

                r = (GameObject)r.getSibling();

                while (null != r)
                {
                    ColTotal.Union(r.CollisionObject.GetColRect());

                    r = (GameObject)r.getSibling();
                }
                this.                CollisionObject.GetColRect().Set(ColTotal);
            } //else { this.getCollisionObject().getColRect().Set(0f, 0f, 0f, 0f); }
            
            this.x = this.poColObj.GetColRect().x;
            this.y = this.poColObj.GetColRect().y;
        }

        //I might pull theese back out and use iterators
        //but theese remove the need for the reverse iterator.
        override public void incrementX(float delta)
        {
            //replace with iterator
            GameObject cur = (GameObject)this.getChild();


            while (cur != null)
            {
                cur.incrementX(delta);
                cur.Update();
                cur = (GameObject)cur.getSibling();

            }
            x += delta;
            //this.Update();
            UpdateColObj();
        }

        override public void incrementY(float delta)
        {
            GameObject cur = (GameObject)this.getChild();


            while (cur != null)
            {
                cur.incrementY(delta);
                cur.Update();
                cur = (GameObject)cur.getSibling();

            }
            //y += delta;
            //this.Update();
            UpdateColObj();
        }

        private void pBombDrop()
        {
            if (getChild() != null)
            {
                //move a lot of theese functions into the factory
                Alien pA = (Alien)getChild();

                Bomb B = BombFactory.getBomb();
                BombRoot br = GameStateManager.getActiveBombRoot();
                GameObjectManager.Insert(B, br);

                B.setCoords(pA.GetX(), pA.getY());

                br.getPSprite().getSBNode().GetSBNM().Attach(B.getPSprite());
                B.ActivateCollisionSprite();
                //gonna def have to tweak this below, bombs drop way to much.
                SetBombCmd();
                //Debug.Print("bomb dropping from x:{0}", this.getX());

            }

        }

        override public void Update()
        {
            if (null != this.getChild())
            {
                if (drop)
                {
                    pBombDrop();
                    drop = false;
                }
            }
            else
            {
                this.Remove();
            }

            base.Update();
        }
    }
}
