using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.PCS;

namespace SpaceInvaders.GameObjects
{
    class GameObjectNode : GoNoLink
    {
        private GameObject pGameObj;
        private PCSTree pTree;
        //this is a bad smell but it allows me to place and remove things for the player swap
        private SpriteBatchNodeManager sbnm;
        ~GameObjectNode()
        {
            pGameObj = null;
        }

        public GameObjectTypeEnum getName()
        {
            return (GameObjectTypeEnum)pGameObj.getName();
        }

        public override void dClean()
        {
            //probably? pGameObj.dClean();
            //pGameObj.dClean();
            pTree = null;
            pGameObj = null;
            sbnm = null;
        }

        internal SpriteBatchNodeManager getSBNM()
        {
            return sbnm;
        }

        internal PCSNode getGameObject()
        {
            return pGameObj;
        }

        internal void Set(GameObject gO, PCSTree pTree)
        {
            pGameObj = gO;
            this.pTree = pTree;
           
            //pGameObj.setParentNode(this);
        }



        internal PCSTree getTree()
        {
            return pTree;
        }

        internal void Update()
        {
            pGameObj.Update();
        }

        internal void setName(GameObjectTypeEnum name)
        {
            this.pGameObj.setName(name);
        }
    }
}
