using System.Diagnostics;
using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    internal sealed class TextureManager : TexMan
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

        public static Texture Add(TextureID Name, string pTextureName)
        {
            TextureManager pTMan = TextureManager.getInstance();
            Debug.Assert(pTMan != null);

            Texture pNode = (Texture)pTMan.BaseAdd();
            Debug.Assert(pTextureName != null);
            
            pNode.Set(Name, new Azul.Texture(pTextureName));

            return pNode;
        }

        public static Texture Add(TextureID Name, Azul.Texture tex )
        {
            TextureManager pTMan = TextureManager.getInstance();
            Debug.Assert(pTMan != null);

            Texture pNode = (Texture)pTMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(Name, tex);
            
            return pNode; 
        }

        protected override DLink dCreateNode()
        {
            DLink newNode = new Texture();
            Debug.Assert(newNode != null);
            return newNode;
        }

        public static Texture Find(TextureID texture)
        {
            TextureManager mrT = TextureManager.getInstance();
            Debug.Assert(mrT != null);
            Texture target = TextureManager.toFind(texture);
            return (Texture)mrT.BaseFind(target);
        }

        private static Texture toFind(TextureID invaders)
        {
            poRefTex.SetName(invaders);
            return poRefTex;
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Texture left = (Texture)pLinkA;
            Texture right = (Texture)pLinkB;

            return left.GetName() == right.GetName();
        }

        protected override void dClearNode(DLink pLink)
        {
            Texture p = (Texture)pLink;
            p.dClean();
        }
    }
}
