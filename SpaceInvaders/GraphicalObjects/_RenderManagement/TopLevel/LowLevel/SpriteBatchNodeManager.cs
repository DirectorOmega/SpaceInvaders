using System.Diagnostics;
using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    internal sealed class SpriteBatchNodeManager : SBNMan
    {
        SpriteBatchID Name;
        bool Active, universal;
        private SpriteBatch pParent;
        private SpriteBatchNode poSpriteBatchRef = new SpriteBatchNode();

        public SpriteBatchNodeManager()
        {
            Name = SpriteBatchID.Undef;
            Active = true;
            universal = false;
        }

        //horribble I know
        //add universal checks
        public void StorePlayerA()
        {
            if (!universal)
            {
                baseStorePlayerA();
            }
        }

        public void SetPlayerA()
        {
            if (!universal)
            {
                baseSetPlayerA();
            }
        }

        public void StorePlayerB()
        {
            if (!universal)
            {
                baseStorePlayerB();
            }
        }

        public void SetPlayerB()
        {
            if (!universal)
            {
                baseSetPlayerB();
            }
        }

        public void ClearStored()
        {
            if (!universal)
            {
                nullHeads();
            }
        }

        public void setUniversal(bool f)
        {
            universal = f;
        }
        public SpriteBatchNodeManager(SpriteBatchID name)
        {
            Name = name;
        }

        public void Attach(baseSprite toAttach)
        {
            SpriteBatchNode pNode = (SpriteBatchNode)this.BaseAdd();
            //pNode.pSprite = toAttach;
            pNode.Set(toAttach, this);
        }

        internal void Toggle()
        {
            Active = !Active;
        }

        //bad smell, doesn't do anything currently yet has a lot of refrences.
        protected override void dClearNode(DLink pLink)
        {

        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            BoxSprite left = (BoxSprite)pLinkA;
            BoxSprite right = (BoxSprite)pLinkB;

            if (left.getName() == right.getName())
            {
                return true;
            }
            return false;
        }

        internal void Set(SpriteBatchID id, SpriteBatch parent)
        {
            Name = id;
            pParent = parent;
        }

        protected override DLink dCreateNode()
        {
            DLink newNode = new SpriteBatchNode();
            Debug.Assert(newNode != null);

            return newNode;
        }

        //TODO: switch to iterator
        internal void Draw()
        {
            if (Active)
            {
                SpriteBatchNode cur = (SpriteBatchNode)GetActiveHead();
                //no assert because a batchCAN be empty
                while (cur != null)
                {
                    //cur.pSprite.Render();
                    cur.Render();
                    cur = (SpriteBatchNode)cur.pNext;
                }
            }
        }
    }
}
