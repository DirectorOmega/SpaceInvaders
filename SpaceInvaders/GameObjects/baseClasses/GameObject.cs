using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.CollisionSystem;
using System.Diagnostics;

namespace SpaceInvaders.GameObjects
{
    abstract class GameObject : ColVistor
    {
        private GameObjectTypeEnum name;
        protected float x, y;
        protected CollisionObject poColObj;
        //private baseSprite pSprite;
        private ProxySprite pSprite;
        private bool MarkedforDeath;

        public void ActivateCollisionSprite()
        {
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.pSprite != null);
            SpriteBatchManager.Find(SpriteBatchID.CBox).Attach(poColObj.getColSprite());
            //pSprite.getSBNode().getSBNM().Attach(poColObj.getColSprite());
            this.getCollisionObject().getColRect().x = this.x;
            this.getCollisionObject().getColRect().y = this.y;

        }

        public void markForDeath()
        {
            MarkedforDeath = true;
        }

        public void clearMark()
        {
            MarkedforDeath = false;
        }

        public bool getMarked()
        {
            return MarkedforDeath;
        }

        public CollisionObject getCollisionObject()
        {
            return poColObj;
        }

        override public System.Enum getName()
        {
            return name;
        }

        public float getX()
        {
            return x;
        }

        public float getY()
        {
            return y;
        }

        public GameObject(SpriteID id, float posX = 0.0f, float posY = 0.0f)
        {
            //I want to add a nullsprite shortcut because so many things use nullsprites.
            //Actually most of the things tend to use nullsprites.
            x = posX;
            y = posY;
            Debug.Assert(null != GameSpriteManager.Find(id));
            //need to optimize this.
            pSprite = ProxyManager.Add(GameSpriteManager.Find(id));
            Debug.Assert(pSprite != null);
            poColObj = new CollisionObject(pSprite);
            MarkedforDeath = false;

        }

        public void setName(GameObjectTypeEnum n)
        { name = n; }

        //public void setParentNode(GameObjectNode pPNode)
        //{
        //    pParentNode = pPNode;
        //}

        //public GameObjectType getName()
        //{
        //   // return name;
        //}

        //public ProxySprite getPSprite()
        //{
        //    return pSprite;
        //}

        public baseSprite getPSprite()
        {
            return pSprite;
        }

        public virtual void Update()
        {
            Debug.Assert(this.pSprite != null);
            pSprite.setCoords(x, y);
            Debug.Assert(this.poColObj != null);
            this.poColObj.UpdatePos(this.x, this.y);
            // this.poColObj.pColSprite.Update();
        }

        public void dClean()
        {
            name = GameObjectTypeEnum.Undef;
            x = 0f;
            y = 0f;
            cClean();
        }
        //bad smell
        public virtual void incrementX(float deltaX)
        {
            x += deltaX;
            //    setCoords(x, y);
        }

        public virtual void incrementY(float deltaY)
        {
            y += deltaY;
            //  setCoords(x, y);
        }

        public virtual void setCoords(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public abstract void cClean();

        //public 
        //public virtual void Revivie()
        //{
        //   // Debug.Assert(this.pSprite != null);
        //   // Debug.Assert(this.pSprite.getSBNode() != null);

        //    SpriteBatchManager.Find(SpriteBatchID.CBox).Attach(poColObj.getColSprite());

        //}
        public virtual void Remove()
        {
            //Remove from SpriteBatch
            Debug.Assert(this.pSprite != null);
            Debug.Assert(this.pSprite.getSBNode() != null);

            pSprite.getSBNode().RemoveSelf();
            poColObj.getColSprite().getSBNode().RemoveSelf();

            GameObjectManager.Remove(this);
            GhostManager.Attach(this);
        }

    }
}