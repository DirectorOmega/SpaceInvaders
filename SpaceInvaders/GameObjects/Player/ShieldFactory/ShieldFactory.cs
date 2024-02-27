using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.PCS;
using System.Diagnostics;

namespace SpaceInvaders.GameObjects
{
    //TODO: Setup shield factory to pull shield pieces from the ghost manager.
    internal sealed class ShieldFactory
    {
        private SpriteBatch pBatch;
        private PCSTree pTree;
        private PCSNode pHead;

        public ShieldFactory(SpriteBatch batch, PCSTree tree)
        {
            Debug.Assert(batch != null);

            pBatch = batch;
            pTree = tree;
        }

        public void setShieldRoot(ShieldRoot pHead)
        {
            this.pHead = pHead;
            GameObjectManager.AttachTree(pHead, pTree);
            pBatch.Attach(pHead.getPSprite());
            pHead.ActivateCollisionSprite();
            pHead.setName(GameObjectType.ShieldRoot);
            //  Debug.Assert(this.pHead != null);
        }
        //TODO: Maybe?
        //TODO: tweak this and the game object manager so the reserve list is 2 dimensional like the batch manager, so objects can be recieved by the game object manager and reused or created only when necessary.
        public Shield Create(float posX = 0.0f, float posY = 0.0f)
        {
            Shield pShield = new Shield(SpriteID.NullSprite, posX, posY);
            pShield.setName(GameObjectType.Shield);
            //GameObjectManager.AttachTree(pShield, this.pTree);

            pTree.Insert(pShield, pHead);
            pBatch.Attach(pShield.getPSprite());
            // this.setParent(pShield);

            BoxSprite brickBoxSprite = BoxSpriteManager.Find(SpriteID.ShieldBrick);
            float xOffset = brickBoxSprite.getScreenRect().height;
            float yOffset = brickBoxSprite.getScreenRect().width;

            ShieldColumn scr;
            ShieldColumn scl;

            ShieldBrick sb;
            //TODO:: clean this up
            //for (int i = 4; i < 8; i++)
            for (int i = 0; i < 10; i++)
            {
                //if (i < 8)
                if (i == 0)
                {
                    scr = new ShieldColumn(SpriteID.NullSprite, posX + i * xOffset, posY);
                    scr.setName(GameObjectType.ShieldColumn);
                    pBatch.Attach(scr.getPSprite());
                    pTree.Insert(scr, pShield);
                    scr.ActivateCollisionSprite();

                    scr.UpdateColObj();

                    for (int k = 0; k < 9; k++)
                    {
                        sb = new ShieldBrick(SpriteID.ShieldBrick, posX + i * xOffset, posY - (k + 6) * yOffset);
                        pBatch.Attach(sb.getPSprite());
                        pTree.Insert(sb, scr);

                        sb.setName(GameObjectType.ShieldBrick);

                        sb.CollisionObject.setColRect(brickBoxSprite);
                        sb.ActivateCollisionSprite();
                    }
                }
                else
                {
                    scr = new ShieldColumn(SpriteID.NullSprite, posX + i * xOffset, posY);
                    scl = new ShieldColumn(SpriteID.NullSprite, posX + i * xOffset, posY);
                    scr.setName(GameObjectType.ShieldColumn);
                    scl.setName(GameObjectType.ShieldColumn);
                    pBatch.Attach(scr.getPSprite());
                    pBatch.Attach(scl.getPSprite());

                    pTree.Insert(scr, pShield);
                    pTree.Insert(scl, pShield);

                    scr.ActivateCollisionSprite();

                    scr.UpdateColObj();
                    scl.ActivateCollisionSprite();

                    scl.UpdateColObj();

                    if (i > 5)
                    {
                        for (int k = 0; k < 20 - i; k++)
                        {
                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX - i * xOffset, posY - (i + k) * yOffset);
                            pBatch.Attach(sb.getPSprite());
                            pTree.Insert(sb, scl);
                            sb.setName(GameObjectType.ShieldBrick);
                            sb.CollisionObject.setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();

                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX + i * xOffset, posY - (i + k) * yOffset);
                            pBatch.Attach(sb.getPSprite());
                            pTree.Insert(sb, scr);
                            sb.setName(GameObjectType.ShieldBrick);
                            sb.CollisionObject.setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();
                        }
                    }
                    else if (i > 2)
                    {
                        for (int k = 7; k > 0 - i; k--)
                        {
                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX + i * xOffset, posY - (k + i + 5) * yOffset);
                            pBatch.Attach(sb.getPSprite());
                            pTree.Insert(sb, scr);
                            sb.setName(GameObjectType.ShieldBrick);
                            sb.CollisionObject.setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();

                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX - i * xOffset, posY - (k + i + 5) * yOffset);
                            pBatch.Attach(sb.getPSprite());
                            pTree.Insert(sb, scl);
                            sb.setName(GameObjectType.ShieldBrick);
                            sb.CollisionObject.setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();
                        }
                    }
                    else
                    {
                        for (int k = 9; k > 0; k--)
                        {
                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX + i * xOffset, posY - (k + 5) * yOffset);
                            pBatch.Attach(sb.getPSprite());
                            pTree.Insert(sb, scr);
                            sb.setName(GameObjectType.ShieldBrick);
                            sb.CollisionObject.setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();

                            sb = new ShieldBrick(SpriteID.ShieldBrick, posX - i * xOffset, posY - (k + 5) * yOffset);
                            pBatch.Attach(sb.getPSprite());
                            pTree.Insert(sb, scl);
                            sb.setName(GameObjectType.ShieldBrick);
                            sb.CollisionObject.setColRect(brickBoxSprite);
                            sb.ActivateCollisionSprite();
                        }
                    }
                }
            }

            pShield.ActivateCollisionSprite();
            pShield.UpdateColObj();

            return pShield;
        }
    }
}

