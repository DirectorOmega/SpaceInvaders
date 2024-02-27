using SpaceInvaders.CollisionSystem;
using SpaceInvaders.Commands;
using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameState;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.Time;

namespace SpaceInvaders.GameObjects
{
    internal sealed class UFORoot : GameObject
    {

        //IrrKlang.ISound ufoDeath;
        // IrrKlang.ISound ufoLife;
        bool drop;
        UFODropBombCMD dropCMD;

        public UFORoot(SpriteID id, float posX = -80, float posY = -80) : base(id, posX, posY)
        {
            // ufoDeath = SndEngine.getSoundSource("")
            dropCMD = new UFODropBombCMD(this);
            SetBombCmd();
        }

        public override void Accept(ColVistor other) => other.VisitUFORoot(this);

        public void SetBombCmd() 
            => TimerManager.Add(TimeEventID.bombDrop, dropCMD, GameStateManager.getRandomNumber(3, 5));

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
             
                SetBombCmd();
            }
        }

        internal void DropBomb() 
            => drop = true;

        public override void Update()
        {
            if (null == getChild())
            {
                CollisionObject.GetColRect().Set(-280.0f, -80.0f, 0.0f, 0.0f);
                setCoords(-280, -80);
            }
            else
            {
                if (drop)
                {
                    pBombDrop();
                    drop = false;
                }
                UpdateChildren();
            }
            base.Update();
        }

        public override void Remove()
        {
            GameObject e = (GameObject) getChild();
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
            //GameObject cur = (GameObject)iterator.first();
            GameObject cur = (GameObject)getChild();
            while (null != cur)
            {
                cur.Update();
                cur = (GameObject)cur.getSibling();
            }
            UpdateColObj();
        }

        public void UpdateColObj()
        {
            GameObject r = (GameObject)getChild();
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
                CollisionObject.GetColRect().Set(ColTotal);
            } //else { this.getCollisionObject().getColRect().Set(0f, 0f, 0f, 0f); }

            x = poColObj.GetColRect().x;
            y = poColObj.GetColRect().y;
        }

        public override void cClean()
        {
        }
    }
}
