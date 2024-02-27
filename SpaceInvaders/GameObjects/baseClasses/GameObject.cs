using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.CollisionSystem;
using System.Diagnostics;

namespace SpaceInvaders.GameObjects
{
    abstract class GameObject : ColVistor
    {
        private GameObjectType name;
        protected float x, y;
        protected CollisionObject poColObj;
        //private baseSprite pSprite;
        private ProxySprite pSprite;
        private bool MarkedForDeath;

        public void ActivateCollisionSprite()
        {
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.pSprite != null);
            SpriteBatchManager.Find(SpriteBatchID.CBox).Attach(poColObj.GetColSprite());
            //pSprite.getSBNode().getSBNM().Attach(poColObj.getColSprite());
            //pSprite.getSBNode().getSBNM().Attach(poColObj.getColSprite());
            CollisionObject.GetColRect().x = x;
            CollisionObject.GetColRect().y = y;
        }

        public void MarkForDeath() => MarkedForDeath = true;
        public void ClearMark() => MarkedForDeath = false;
        public bool IsMarked() => MarkedForDeath;
        public CollisionObject CollisionObject => poColObj;
        override public System.Enum getName() => name;

        public float GetX() => x;
        public float getY() => y;

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
            MarkedForDeath = false;
        }

        public void setName(GameObjectType n)
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

        public baseSprite getPSprite() => pSprite;

        public virtual void Update()
        {
            Debug.Assert(pSprite != null);
            pSprite.setCoords(x, y);
            Debug.Assert(poColObj != null);
            poColObj.UpdatePos(x, y);
            // this.poColObj.pColSprite.Update();
        }

        public void dClean()
        {
            name = GameObjectType.Undef;
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
            Debug.Assert(pSprite != null);
            Debug.Assert(pSprite.getSBNode() != null);

            pSprite.getSBNode().RemoveSelf();
            poColObj.GetColSprite().getSBNode().RemoveSelf();

            GameObjectManager.Remove(this);
            GhostManager.Attach(this);
        }
    }
}