using System.Diagnostics;
using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    class TextureManager : TexMan
    {
        private static TextureManager pInstance;
        private static Texture poRefTex = new Texture();

        private TextureManager(int numStart = 5, int deltaAdd = 3)
            : base(numStart,deltaAdd)
        {  }

        public static TextureManager getInstance()
        {
            if (pInstance == null)
            {
                TextureManager.Create();
            }
            return pInstance;
        }

        public static void Create(int numStart = 5,int deltaAdd = 3)
        {
            Debug.Assert(numStart > 0);
            Debug.Assert(deltaAdd > 0);
            Debug.Assert(pInstance == null);

            if(pInstance == null)
            {
                pInstance = new TextureManager(numStart, deltaAdd);
                TextureManager.Add(TextureID.Default, "HotPink.tga");
            }
        }

#if DEBUG
        public static Texture Add(TextureID Name, string pTextureName)
        {
            TextureManager pTMan = TextureManager.getInstance();
            Debug.Assert(pTMan != null);

            Texture pNode = (Texture)pTMan.baseAdd();
            Debug.Assert(pTextureName != null);
            
            pNode.Set(Name, new Azul.Texture(pTextureName));

            return pNode;
        }
#else
       public static Texture Add(TextureID Name, string pTextureName)
        {
            Texture pNode = (Texture) TextureManager.getInstance().baseAdd();   
            pNode.Set(Name, new Azul.Texture(pTextureName));
            return pNode;
        }


#endif


#if DEBUG
        public static Texture Add(TextureID Name, Azul.Texture tex )
        {
            TextureManager pTMan = TextureManager.getInstance();
            Debug.Assert(pTMan != null);

            Texture pNode = (Texture)pTMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(Name, tex);
            
            return pNode; 
        }


#else
        public static Texture Add(TextureID Name, Azul.Texture tex)
        {
            Texture pNode = (Texture) TextureManager.getInstance().baseAdd();
            pNode.Set(Name, tex);
            return pNode;
        }

#endif


#if DEBUG
        protected override DLink dCreateNode()
        {
            DLink newNode = new Texture();
            Debug.Assert(newNode != null);
            return newNode;
        }
#else
        protected override DLink dCreateNode()
        {
            return (DLink) new Texture();
        }
#endif

#if DEBUG
        public static Texture Find(TextureID texture)
        {
            TextureManager mrT = TextureManager.getInstance();
            Debug.Assert(mrT != null);
            Texture target = TextureManager.toFind(texture);
            return (Texture)mrT.baseFind(target);
        }
#else
        public static Texture Find(TextureID texture)
        {
          return (Texture) TextureManager.getInstance().baseFind(TextureManager.toFind(texture));
        }
#endif

        private static Texture toFind(TextureID invaders)
        {
            poRefTex.setName(invaders);
            return poRefTex;
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Texture left = (Texture)pLinkA;
            Texture right = (Texture)pLinkB;

            if (left.getName() == right.getName())
            {
                return true;
            }
            return false;
        }

        protected override void dClearNode(DLink pLink)
        {
            Texture p = (Texture)pLink;
            p.dClean();
        }
    }
}
