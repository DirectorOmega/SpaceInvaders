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
        public Alien Create(GameObjectType type, float posX = 0.0f, float posY = 0.0f)
        {
            Alien pAlien = null;

            switch (type)
            {
                case GameObjectType.Crab:
                    pAlien = (Crab) GhostManager.Find(GameObjectType.Crab).Detatch();
                    if (pAlien == null)
                    {
                        pAlien = new Crab(SpriteID.Crab, posX, posY);
                        pAlien.setName(GameObjectType.Crab);
                    }
                    this.pTree.Insert(pAlien, this.pHead);
                    break;

                case GameObjectType.Squid:
                    pAlien = (Squid)GhostManager.Find(GameObjectType.Squid).Detatch();
                    if (pAlien == null)
                    {
                        pAlien = new Squid(SpriteID.Squid, posX, posY);
                        pAlien.setName(GameObjectType.Squid);
                       
                    }
                    this.pTree.Insert(pAlien, this.pHead);
                    break;

                case GameObjectType.Octo:
                    pAlien = (Octo)GhostManager.Find(GameObjectType.Octo).Detatch();
                    if (pAlien == null)
                    {
                        pAlien = new Octo(SpriteID.Octo, posX, posY);
                        pAlien.setName(GameObjectType.Octo);
                       
                    }
                    this.pTree.Insert(pAlien, this.pHead);
                    break;

                case GameObjectType.Column:
                    //pAlien = (Column)GhostManager.Find(GameObjectTypeEnum.Column).detatch();
                    //if (pAlien == null)
                   // {
                        pAlien = new Column(SpriteID.NullSprite, posX, posY);
                        pAlien.setName(GameObjectType.Column);
                   // }
                    this.pTree.Insert(pAlien, this.pHead);
                    break;

                case GameObjectType.Grid:
                    pAlien = new Grid(SpriteID.NullSprite, posX, posY);
                    pAlien.setName(GameObjectType.Grid);
                    GameObjectManager.AttachTree(pAlien,this.pTree);
                    this.pTree.SetRoot(pAlien);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            //this.pTree.Insert(pAlien, this.pHead);
            pAlien.ClearMark();
            pAlien.setCoords(posX, posY);
            this.pBatch.Attach(pAlien.getPSprite());
            pAlien.ActivateCollisionSprite();
            return pAlien;
        }
    }
}
