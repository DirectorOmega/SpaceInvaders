using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.PCS;
using System.Diagnostics;

namespace SpaceInvaders.GameObjects
{
    class AlienFactory
    {
        private SpriteBatch pBatch;
        private PCSTree pTree;
        private PCSNode pHead;

        public AlienFactory(SpriteBatch batch , PCSTree tree)
        {
            Debug.Assert(batch != null);
          
            this.pBatch = batch;
            this.pTree = tree;
        }

        public void setParent(PCSNode pHead)
        {
            this.pHead = pHead;
          //Debug.Assert(this.pHead != null);
        }

        //TODO: Maybe?
        //TODO: tweak this and the game object manager so the reserve list is 2 dimensional like the batch manager, so objects can be recieved by the game object manager and reused or created only when necessary.
        public Alien Create(GameObjectTypeEnum type, float posX = 0.0f, float posY = 0.0f)
        {
            Alien pAlien = null;

            switch (type)
            {
                case GameObjectTypeEnum.Crab:
                    pAlien = (Crab) GhostManager.Find(GameObjectTypeEnum.Crab).detatch();
                    if (pAlien == null)
                    {
                        pAlien = new Crab(SpriteID.Crab, posX, posY);
                        pAlien.setName(GameObjectTypeEnum.Crab);
                    }
                    this.pTree.Insert(pAlien, this.pHead);
                    break;

                case GameObjectTypeEnum.Squid:
                    pAlien = (Squid)GhostManager.Find(GameObjectTypeEnum.Squid).detatch();
                    if (pAlien == null)
                    {
                        pAlien = new Squid(SpriteID.Squid, posX, posY);
                        pAlien.setName(GameObjectTypeEnum.Squid);
                       
                    }
                    this.pTree.Insert(pAlien, this.pHead);
                    break;

                case GameObjectTypeEnum.Octo:
                    pAlien = (Octo)GhostManager.Find(GameObjectTypeEnum.Octo).detatch();
                    if (pAlien == null)
                    {
                        pAlien = new Octo(SpriteID.Octo, posX, posY);
                        pAlien.setName(GameObjectTypeEnum.Octo);
                       
                    }
                    this.pTree.Insert(pAlien, this.pHead);
                    break;

                case GameObjectTypeEnum.Column:
                    //pAlien = (Column)GhostManager.Find(GameObjectTypeEnum.Column).detatch();
                    //if (pAlien == null)
                   // {
                        pAlien = new Column(SpriteID.NullSprite, posX, posY);
                        pAlien.setName(GameObjectTypeEnum.Column);
                   // }
                    this.pTree.Insert(pAlien, this.pHead);
                    break;

                case GameObjectTypeEnum.Grid:
                    pAlien = new Grid(SpriteID.NullSprite, posX, posY);
                    pAlien.setName(GameObjectTypeEnum.Grid);
                    GameObjectManager.AttachTree(pAlien,this.pTree);
                    this.pTree.SetRoot(pAlien);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            //this.pTree.Insert(pAlien, this.pHead);
            pAlien.clearMark();
            pAlien.setCoords(posX, posY);
            this.pBatch.Attach(pAlien.getPSprite());
            pAlien.ActivateCollisionSprite();
            return pAlien;
        }

    }
}
