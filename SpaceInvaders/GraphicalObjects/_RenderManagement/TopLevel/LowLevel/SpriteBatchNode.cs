
namespace SpaceInvaders.GraphicalObjects
{
    class SpriteBatchNode : SBNLink
    {
        private baseSprite pSprite;
        private SpriteBatchNodeManager pSBNM;
        // private SpriteBatchID name;
        
        public SpriteBatchNodeManager getSBNM()
        {
            return pSBNM;
        }

        public SpriteBatchNode()
        { 
            pSprite = null;
            pSBNM = null;
        }

        public void Set(baseSprite sprite,SpriteBatchNodeManager parent)
        {
            pSprite = sprite;
            pSprite.setSBNode(this);
            pSBNM = parent;
        }

        public override void dClean()
        {
            pSprite = null;
        }

        public void Render()
        {
            pSprite.Render();
        }

        internal void RemoveSelf()
        {
            pSBNM.baseRemove(this);
        }
    }
}
