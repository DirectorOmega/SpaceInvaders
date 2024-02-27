using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;
using System.Diagnostics;
using SpaceInvaders.FontSystem;
using SpaceInvaders.PCS;
using SpaceInvaders.GameState;

namespace SpaceInvaders.GameObjects
{
    internal sealed class LifeCounter : GameObject
    {
        //int numLives;
        int startx,starty,offsetx;
        GameSprite ShipSprite;
        Font NumeralCount;
        SpriteBatch pBatch;
        PCSTree pTree;
        GhostTypeNode pGhostNode;
        //TODO: create null object for collision object so I can remove the collision object
        //from the ship life counter.
        //though with how I am doing self removal I would need to tweak to have the collision object
        //removal be done internally instead of calling out the spritebatch node.
        public LifeCounter(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
            startx = 160;
            starty = 25;
            offsetx = 70;

            pBatch = SpriteBatchManager.Find(SpriteBatchID.Shield);

            NumeralCount = FontManager.Find(FontName.LifeCount);
            ShipSprite = GameSpriteManager.Find(SpriteID.Hero);
            pGhostNode = GhostManager.Find(GameObjectType.ExtraLife) ;

            pTree = new PCSTree();
            //pTree.Insert(this, null);
            GameObjectManager.AttachTree(this, pTree);
            Debug.Assert(null != pBatch);
            Debug.Assert(null != NumeralCount);
            Debug.Assert(null != ShipSprite);
            ActivateCollisionSprite();
            this.pBatch.Attach(this.getPSprite());
        }

        public override void Accept(ColVistor other)
        {
            Debug.WriteLine("If you collided with the life counter: you have done something very wrong/n");
            Debug.Assert(false);  
        }


        //bad smell, lives need updating into places.
        //possibility for desync
        public void setLives()
        {
            //this.numLives = numLives;
            int numLives = GameStateManager.getLifeCount();
            NumeralCount.UpdateMessage("" + numLives);
            GameObject p; 

            for (int i = 0; i < numLives-1; i++)
            {
                p = pGhostNode.Detatch();
                if (null == p)
                {
                    p = new Ship(SpriteID.Hero);
                    p.setName(GameObjectType.ExtraLife);
                }
                p.setCoords(startx + i * offsetx, starty);
                p.ActivateCollisionSprite();
                p.Update();
                pTree.Insert(p, this);
                this.pBatch.Attach(p.getPSprite());
            }
        }

        public void addLife()
        {

            int numLives = GameStateManager.getLifeCount();
            NumeralCount.UpdateMessage("" + numLives);
            GameObject p;

                p = pGhostNode.Detatch();
                if (null == p)
                {
                    p = new Ship(SpriteID.Hero);
                    p.setName(GameObjectType.ExtraLife);
                }
                p.setCoords(startx + (numLives-2) * offsetx, starty);
                p.ActivateCollisionSprite();
                p.Update();
                pTree.Insert(p, this);
                this.pBatch.Attach(p.getPSprite());
           
        }

        public void removeLife()
        {
            NumeralCount.UpdateMessage("" + GameStateManager.getLifeCount());
            
            GameObject toRemove = (GameObject) this.getChild();
            if (null != toRemove)
            {
                toRemove.Remove();
            }

        }
        

        public override void cClean()
        {
            throw new NotImplementedException();
        }
    }
}
