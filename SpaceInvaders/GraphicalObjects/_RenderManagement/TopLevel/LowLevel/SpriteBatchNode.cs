using System.Diagnostics;

namespace SpaceInvaders.GraphicalObjects
{
    internal sealed class SpriteBatchNode : SBNLink
    {
        private baseSprite pSprite;
        private SpriteBatchNodeManager pSBNM;
        // private SpriteBatchID name;

        public SpriteBatchNodeManager GetSBNM() => pSBNM;

        public SpriteBatchNode()
        { 
            pSprite = null;
            pSBNM = null;
        }

        public void Set(baseSprite sprite,SpriteBatchNodeManager parent)
        {
            Debug.Assert(sprite != null);
            pSprite = sprite;
            pSprite.setSBNode(this);
            pSBNM = parent;
        }

        public override void dClean() => pSprite = null;
        public void Render() => pSprite.Render();
        internal void RemoveSelf() => pSBNM.baseRemove(this);
    }
}
