using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.PCS;

namespace SpaceInvaders.GameObjects
{
    internal sealed class GameObjectNode : GoNoLink
    {
        private GameObject pGameObj;
        private PCSTree pTree;
        //this is a bad smell but it allows me to place and remove things for the player swap
        private SpriteBatchNodeManager sbnm;
        ~GameObjectNode()
        {
            pGameObj = null;
        }

        public GameObjectType GetName() 
            => (GameObjectType)pGameObj.getName();

        public override void dClean()
        {
            //probably? pGameObj.dClean();
            //pGameObj.dClean();
            pTree = null;
            pGameObj = null;
            sbnm = null;
        }

        internal SpriteBatchNodeManager GetSBNM() => sbnm;

        internal PCSNode GetGameObject() => pGameObj;

        internal void Set(GameObject gO, PCSTree pTree)
        {
            pGameObj = gO;
            this.pTree = pTree;
        }

        internal PCSTree GetTree() => pTree;
        internal void Update() => pGameObj.Update();
        internal void SetName(GameObjectType name) => this.pGameObj.setName(name);
    }
}
