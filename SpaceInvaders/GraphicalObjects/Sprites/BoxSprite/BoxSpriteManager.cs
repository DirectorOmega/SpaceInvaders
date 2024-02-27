using System.Diagnostics;
using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    class BoxSpriteManager : BSMan
    {
        private static BoxSpriteManager pInstance;
        private static BoxSprite poRef = new BoxSprite();

        private BoxSpriteManager(int numStart = 5, int deltaAdd = 3)
            : base(numStart, deltaAdd)
        {
        }

        public static BoxSprite Add(SpriteID Name, Azul.Color color, Azul.Rect screenRect)
        {
            BoxSpriteManager pTMan = BoxSpriteManager.getInstance();
            Debug.Assert(pTMan != null);

            BoxSprite pNode = (BoxSprite)pTMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(Name, color, screenRect);

            return pNode;
        }

        public static BoxSprite Add(SpriteID Name,Azul.Rect screenRect)
        {
            BoxSpriteManager pTMan = BoxSpriteManager.getInstance();
            Debug.Assert(pTMan != null);

            BoxSprite pNode = (BoxSprite)pTMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(Name, screenRect);

            return pNode;
        }


        public static void Create(int numStart = 5, int deltaAdd = 3)
        {
            Debug.Assert(numStart > 0);
            Debug.Assert(deltaAdd > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new BoxSpriteManager(numStart, deltaAdd);
            }
        }

        protected override void dClearNode(DLink pLink)
        {
            BoxSprite p = (BoxSprite)pLink;
            p.dClean();
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

        protected override DLink dCreateNode()
        {
            DLink newNode = new BoxSprite();
            Debug.Assert(newNode != null);

            return newNode;
        }

        public static BoxSprite Find(SpriteID sprite)
        {
            BoxSpriteManager bsMan = BoxSpriteManager.getInstance();
            Debug.Assert(bsMan != null);


            BoxSprite target = BoxSpriteManager.toFind(sprite);
            return (BoxSprite)bsMan.baseFind(target);

        }

        //for find
        private static BoxSprite toFind(SpriteID name)
        {
            poRef.setName(name);
            return poRef;
        }


        public static BoxSpriteManager getInstance()
        {
            if (pInstance == null)
            {
                BoxSpriteManager.Create();
            }
            return pInstance;
        }
    }
}

