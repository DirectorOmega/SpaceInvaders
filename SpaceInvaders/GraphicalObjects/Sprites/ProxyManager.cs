using System.Diagnostics;
using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    class ProxyManager : PMan
    {
        private static ProxyManager pInstance;
        private static ProxySprite poRefSprite = new ProxySprite();

        private ProxyManager(int numStart = 5, int deltaAdd = 3)
            : base(numStart, deltaAdd)
        {
        }

        public static ProxySprite Add(GameSprite realSprite)
        {

            Debug.Assert(null != realSprite);

            ProxyManager pTMan = ProxyManager.getInstance();
            Debug.Assert(pTMan != null);

            ProxySprite pNode = (ProxySprite)pTMan.baseAdd();
            Debug.Assert(pNode != null);


            pNode.Set(realSprite);

            return pNode;
        }

        public static void Create(int numStart = 5, int deltaAdd = 3)
        {
            Debug.Assert(numStart > 0);
            Debug.Assert(deltaAdd > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new ProxyManager(numStart, deltaAdd);
            }
        }

        protected override void dClearNode(DLink pLink)
        {
            ProxySprite p = (ProxySprite)pLink;
            p.dClean();
        }
        
        //BAD Smell
        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(false);
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            ProxySprite left = (ProxySprite)pLinkA;
            ProxySprite right = (ProxySprite)pLinkB;

            //currently proxy objects can only be identical by reference.
            if (left == right)
            {
                return true;
            }
            return false;
        }

        protected override DLink dCreateNode()
        {
            DLink newNode = new ProxySprite();
            Debug.Assert(newNode != null);

            return newNode;
        }

        //public static GameSprite Find(SpriteID sprite)
        //{
        //    SpriteManager sMan = SpriteManager.getInstance();

        //    GameSprite target = SpriteManager.toFind(sprite);
        //    return (GameSprite)sMan.baseFind(target);

        //}

        //for find
        //private static GameSprite toFind(SpriteID name)
        //{
        //    poRefSprite.Name = name;
        //    return poRefSprite;
        //    // return new GameSprite(name);
        //}


        public static ProxyManager getInstance()
        {
            if (pInstance == null)
            {
                ProxyManager.Create();
            }
            return pInstance;
        }

    }
}
