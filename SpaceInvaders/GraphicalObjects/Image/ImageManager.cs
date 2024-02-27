using System.Diagnostics;
using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    internal sealed class ImageManager : IMan
    {

       private static ImageManager pInstance;
       private static Image poRefImage = new Image();

        private ImageManager(int numStart = 5, int deltaAdd = 3)
            : base(numStart,deltaAdd)
        {    
        }

        public static void Create(int numStart = 5, int deltaAdd = 3)
        {
            Debug.Assert(numStart > 0);
            Debug.Assert(deltaAdd > 0);
            Debug.Assert(pInstance == null);

            if  (null == pInstance)
            {
                pInstance = new ImageManager(numStart, deltaAdd);
                ImageManager.Add(ImageID.Default, TextureID.Default, new Azul.Rect(0, 0, 128, 128));
                Debug.Assert(null != ImageManager.Find(ImageID.Default));
            }
        }

        public static Image Add(ImageID Name, TextureID TName, Azul.Rect texRect)
        { 
            ImageManager IMan = ImageManager.getInstance();
            Debug.Assert(IMan != null);

            Texture pTex = TextureManager.Find(TName);
            Debug.Assert(pTex != null);

            Image pNode = (Image)IMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(Name, pTex, texRect);

            return pNode;
        }

        public static Image Add(ImageID Name,Azul.Rect rect,Texture pTex)
        {
            ImageManager piMan = ImageManager.getInstance();
            Debug.Assert(piMan != null);

            Image pNode = (Image)piMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(Name,pTex,rect);

            return pNode;
        }

        public static ImageManager getInstance()
        {
            if (pInstance == null)
            {
                // pInstance = new ImageManager();
                ImageManager.Create();
                Debug.Assert(pInstance != null);
            }
            return pInstance;
        }

        protected override void dClearNode(DLink pLink)
        {
            Image p = (Image)pLink;
            p.dClean();
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Image left = (Image)pLinkA;
            Image right = (Image)pLinkB;

            if (left.getName() == right.getName())
            {
                return true;
            }
            return false;
        }

        protected override DLink dCreateNode()
        {
            DLink newNode = new Image();
            Debug.Assert(newNode != null);

            return newNode;
        }

        public static Image Find(ImageID image)
        {
            ImageManager piMan = ImageManager.getInstance();
            Debug.Assert(piMan != null);

            Image target = ImageManager.toFind(image);
            return (Image)piMan.baseFind(target);
        }

        private static Image toFind(ImageID invaders)
        {  
            poRefImage.setName(invaders);
            return poRefImage;
        }
    }
}
