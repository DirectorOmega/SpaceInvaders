//using SpaceInvaders.GameObjects;
//using SpaceInvaders.GraphicalObjects;

//namespace SpaceInvaders.Commands
//{
//    class rezCMD : Command
//    {
//        GameObjectTypeEnum typeName;
//        Grid root;
//        Column parent;
//        float offsetx;
//        float offsety;

//        public rezCMD(GameObject ptoRez, Column parent, Grid root)
//        {
//            this.typeName = (GameObjectTypeEnum)ptoRez.getName();
//            this.parent = parent;
//            this.root = root;
//            offsetx = parent.getX() - ptoRez.getX();
//            offsety = parent.getY() - ptoRez.getY();
//        }

//        public override void execute(float deltaTime)
//        {

//            GameObject g = GhostManager.Rez(typeName);

//            SpriteBatchNodeManager pSBatch = g.getPSprite().getSBNode().getSBNM();
//            pSBatch.Attach(g.getPSprite());
//            g.ActivateCollisionSprite();

//            GameObjectManager.Insert(g, parent);
//            g.setCoords(parent.getX() + offsetx, parent.getY() - offsety);
//        }

//    }

//}
