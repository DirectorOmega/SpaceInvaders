using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GraphicalObjects;
using System.Diagnostics;

namespace SpaceInvaders.GameObjects
{
    internal sealed class ShieldRoot : GameObject
    {
        public ShieldRoot(SpriteID nullSprite, float posX, float posY) : base(nullSprite, posX, posY)
        {
            this.            CollisionObject.GetColSprite().setColor(255.0f, 255.0f, 0.0f);
        }

        public override void Update()
        {
            UpdateChildren();
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

        private void UpdateChildren()
        {
            GameObject cur = (GameObject)this.getChild();
            if (null != cur)
            {
                while (cur != null)
                {
                    cur.Update();
                    cur = (GameObject)cur.getSibling();
                }
            }
            else
            {
                this.setCoords(-41, -41);
                this.                CollisionObject.GetColRect().Set(0.0f, 0.0f, 0.0f, 0.0f);
            }
            UpdateColObj();
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
            other.VisitShieldRoot(this);
        }

        public override void VisitGrid(Grid g)
        {
            Reactions.Reaction(g, this);
        }

        public override void VisitNoiseRoot(GNoiseRoot noiseRoot)
        {
            Reactions.Reaction(noiseRoot, this);
        }

        public override void VisitBombRoot(BombRoot br)
        {
            Reactions.Reaction(br, this);
        }

        public override void VisitBomb(Bomb b)
        {
            Reactions.Reaction(b, this);
        }

        public override void VisitMissile(Missile m)
        {
            Reactions.Reaction(m, this);
        }

        public override void cClean()
        {

        }

    }
    class Shield : GameObject
    {
        public Shield(SpriteID nullSprite, float posX, float posY) : base(nullSprite, posX, posY)
        {
            this.            CollisionObject.GetColSprite().setColor(0.0f, 255.0f, 0.0f);
        }

        public override void Update()
        {    
            UpdateChildren();
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

        private void UpdateChildren()
        {
            GameObject cur = (GameObject)this.getChild();
            if (cur != null)
            {
                while (cur != null)
                {
                    cur.Update();
                    cur = (GameObject)cur.getSibling();
                }
            }else { this.Remove(); }
            UpdateColObj();
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
            //Debug.Print("Shield Rect x{0},y{1},w{2},h{3}", this.poColObj.getColRect().x, this.poColObj.getColRect().y, this.poColObj.getColRect().width, this.poColObj.getColRect().height);

        }

        public override void Accept(ColVistor other)
        {
            other.VisitShield(this);
        }

        public override void VisitGrid(Grid g)
        {
            Reactions.Reaction(g, this);
        }

        public override void VisitColumn(Column c)
        {
            Reactions.Reaction(c, this);
        }
        public override void VisitBombRoot(BombRoot br)
        {
            Debug.Assert(false);
        }

        public override void VisitBomb(Bomb b)
        {
            Reactions.Reaction(b, this);
        }

        public override void VisitMissile(Missile m)
        {
            Reactions.Reaction(m, this);
        }

        public override void VisitNoiseRoot(GNoiseRoot noiseRoot)
        {
            Reactions.Reaction(noiseRoot, this);
        }

        public override void VisitNoisePoint(GNoisePoint gnp)
        {
            Reactions.Reaction(gnp, this);
        }

        public override void cClean()
        {

        }
    }

    class ShieldColumn : GameObject
    {
        public ShieldColumn(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
            this.            CollisionObject.GetColSprite().setColor(0.0f, 0.0f, 255.0f);
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

        public override void Accept(ColVistor other)
        {
            other.VisitShieldColumn(this);
        }

        public override void VisitColumn(Column c)
        {
            Reactions.Reaction(c, this);
        }

        public override void VisitNoisePoint(GNoisePoint gnp)
        {
            Reactions.Reaction(gnp, this);
        }

        public override void VisitBombRoot(BombRoot br)
        {
            Debug.Assert(false);
        }

        public override void VisitBomb(Bomb b)
        {
            Reactions.Reaction(b, this);
        }

        public override void VisitMissile(Missile m)
        {
            Reactions.Reaction(m, this);
        }

        public override void Update()
        {
            UpdateChildren();
            base.Update();
        }

        private void UpdateChildren()
        {
            GameObject cur = (GameObject)this.getChild();
            if (cur != null)
            {
                while (cur != null)
                {
                    cur.Update();
                    cur = (GameObject)cur.getSibling();
                }
            }
            else { this.Remove(); }

            UpdateColObj();
        }


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
            } 

            this.x = this.poColObj.GetColRect().x;
            this.y = this.poColObj.GetColRect().y;

          //  Debug.Print("Column Rect x{0},y{1},w{2},h{3}",this.poColObj.getColRect().x,this.poColObj.getColRect().y,this.poColObj.getColRect().width,this.poColObj.getColRect().height);
        }

        public override void cClean()
        {

        }
    }

    class ShieldBrick : GameObject
    {
        public ShieldBrick(SpriteID sb, float posX, float posY)
            : base(sb, posX, posY)
        {

        }

        public override void Accept(ColVistor other)
        {
            other.VisitShieldBrick(this);
        }


        public override void VisitColumn(Column c)
        {
            Reactions.Reaction(c, this);
        }
 

        public override void VisitBombRoot(BombRoot br)
        {
            Debug.Assert(false);
        }

        public override void VisitBomb(Bomb b)
        {
            Reactions.Reaction(b, this);
        }

        public override void VisitMissile(Missile m)
        {
            Reactions.Reaction(m, this);
        }
        public override void VisitNoisePoint(GNoisePoint gnp)
        {
            Reactions.Reaction(gnp, this);
        }
        public override void cClean()
        {

        }
    }

}
