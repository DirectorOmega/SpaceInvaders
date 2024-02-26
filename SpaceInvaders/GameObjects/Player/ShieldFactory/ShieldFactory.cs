using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.PCS;
using System.Diagnostics;

namespace SpaceInvaders.GameObjects
{
    //TODO: Setup shield factory to pull shield pieces from the ghost manager.

    class ShieldFactory
    {
        private SpriteBatch pBatch;
        private PCSTree pTree;
        private PCSNode pHead;

        public ShieldFactory(SpriteBatch batch, PCSTree tree)
        {
            Debug.Assert(batch != null);

            this.pBatch = batch;
            this.pTree = tree;
        }

        public void setShieldRoot(ShieldRoot pHead)
        {
            this.pHead = pHead;
            GameObjectManager.AttachTree(pHead, this.pTree);
            this.pBatch.Attach(pHead.getPSprite());
            pHead.ActivateCollisionSprite();
            pHead.setName(GameObjectTypeEnum.ShieldRoot);
            //  Debug.Assert(this.pHead != null);
        }
        //TODO: Maybe?
        //TODO: tweak this and the game object manager so the reserve list is 2 dimensional like the batch manager, so objects can be recieved by the game object manager and reused or created only when necessary.
        public Shield Create(float posX = 0.0f, float posY = 0.0f)
        {
            Shield pShield = null;

            pShield = new Shield(SpriteID.NullSprite, posX, posY);
            pShield.setName(GameObjectTypeEnum.Shield);
            //GameObjectManager.AttachTree(pShield, this.pTree);

            this.pTree.Insert(pShield, this.pHead);

            this.pBatch.Attach(pShield.getPSprite());
            // this.setParent(pShield);


            BoxSprite brickBoxSprite = BoxSpriteManager.Find(SpriteID.ShieldBrick);
            float xoffset = brickBoxSprite.getScreenRect().height;
            float yoffset = brickBoxSprite.getScreenRect().width;

            ShieldColumn scr;
            ShieldColumn scl;

            ShieldBrick sb;

            //for (int i = 4; i < 8; i++)
            for (int i = 0; i < 10; i++)
            {
               

                //if (i < 8)
                if (i == 0)
                {
                    scr = new ShieldColumn(SpriteID.NullSprite, posX + i * xoffset, posY);
                    scr.setName(GameObjectTypeEnum.ShieldColumn);
                    this.pBatch.Attach(scr.getPSprite());
                    this.pTree.Insert(scr, pShield);
                    scr.ActivateCollisionSprite();

                    scr.UpdateColObj();

                    for (int k = 0; k < 9; k++)
                    {
                        sb = new ShieldBrick(SpriteID.ShieldBrick, posX + i * xoffset, posY - (k + 6) * yoffset);
                        this.pBatch.Attach(sb.getPSprite());
                        this.pTree.Insert(sb, scr);

                        sb.setName(GameObjectTypeEnum.ShieldBrick);

                        sb.getCollisionObject().setColRect(brickBoxSprite);
                        sb.ActivateCollisionSprite();
                    }
                }
                else
                {
                    scr = new ShieldColumn(SpriteID.NullSprite, posX + i * xoffset, posY);
                    scl = new ShieldColumn(SpriteID.NullSprite, posX + i * xoffset, posY);
                    scr.setName(GameObjectTypeEnum.ShieldColumn);
                    scl.setName(GameObjectTypeEnum.ShieldColumn);
                    this.pBatch.Attach(scr.getPSprite());
                    this.pBatch.Attach(scl.getPSprite());

                    this.pTree.Insert(scr, pShield);
                    this.pTree.Insert(scl, pShield);


                    scr.ActivateCollisionSprite();

                    scr.UpdateColObj();
                    scl.ActivateCollisionSprite();

                    scl.UpdateColObj();

                    if (i > 5)
                    {
                        for (int k = 0; k < 20 - i; k++)
                        {
                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX - i * xoffset, posY - (i + k) * yoffset);
                            this.pBatch.Attach(sb.getPSprite());
                            this.pTree.Insert(sb, scl);
                            sb.setName(GameObjectTypeEnum.ShieldBrick);
                            sb.getCollisionObject().setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();

                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX + i * xoffset, posY - (i + k) * yoffset);
                            this.pBatch.Attach(sb.getPSprite());
                            this.pTree.Insert(sb, scr);
                            sb.setName(GameObjectTypeEnum.ShieldBrick);
                            sb.getCollisionObject().setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();

                        }
                    }

                    else if (i > 2)
                    {
                        for (int k = 7; k > 0 - i; k--)
                        {
                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX + i * xoffset, posY - (k + i + 5) * yoffset);
                            this.pBatch.Attach(sb.getPSprite());
                            this.pTree.Insert(sb, scr);
                            sb.setName(GameObjectTypeEnum.ShieldBrick);
                            sb.getCollisionObject().setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();

                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX - i * xoffset, posY - (k + i + 5) * yoffset);
                            this.pBatch.Attach(sb.getPSprite());
                            this.pTree.Insert(sb, scl);
                            sb.setName(GameObjectTypeEnum.ShieldBrick);
                            sb.getCollisionObject().setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();
                        }
                    }
                    else
                    {
                        for (int k = 9; k > 0; k--)
                        {
                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX + i * xoffset, posY - (k + 5) * yoffset);
                            this.pBatch.Attach(sb.getPSprite());
                            this.pTree.Insert(sb, scr);
                            sb.setName(GameObjectTypeEnum.ShieldBrick);
                            sb.getCollisionObject().setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();

                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX - i * xoffset, posY - (k + 5) * yoffset);
                            this.pBatch.Attach(sb.getPSprite());
                            this.pTree.Insert(sb, scl);
                            sb.setName(GameObjectTypeEnum.ShieldBrick);
                            sb.getCollisionObject().setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();
                        }
                    }

                }
            }
            pShield.ActivateCollisionSprite();
            //pShield.Update();

            pShield.UpdateColObj();

            return pShield;
        }

    }
}

