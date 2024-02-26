using System;
using SpaceInvaders.Manager;
using System.Diagnostics;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.FontSystem
{
    class FontManager : FMan
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private FontManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.pRefNode = (Font)this.dCreateNode();
        }
        ~FontManager()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~FontMan():{0}", this.GetHashCode());
#endif
            this.pRefNode = null;
            FontManager.pInstance = null;
        }

        //----------------------------------------------------------------------
        // Static Manager methods can be implemented with base methods 
        // Can implement/specialize more or less methods your choice
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new FontManager(reserveNum, reserveGrow);
            }
        }
//        public static void Destroy()
//        {
//            // Get the instance
//            FontManager pMan = FontManager.privGetInstance();
//#if(TRACK_DESTRUCTOR)
//            Debug.WriteLine("--->FontMan.Destroy()");
//#endif
//            //pMan.baseDestroy();
//        }

        public static Font Add(FontName name, SpriteBatchID SB_Name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            FontManager pMan = FontManager.privGetInstance();

            Font pNode = (Font)pMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, pMessage, glyphName, xStart, yStart);

            // Add to sprite batch
            SpriteBatch pSB = SpriteBatchManager.Find(SB_Name);
            Debug.Assert(pSB != null);
            Debug.Assert(pNode.pFontSprite != null);
            pSB.Attach(pNode.pFontSprite);

            return pNode;
        }

        public static void AddXml(Glyph.Name glyphName, String assetName, TextureID textName)
        {
            GlyphManager.AddXml(glyphName, assetName, textName);
        }

        public static void Remove(Glyph pNode)
        {
            Debug.Assert(pNode != null);
            FontManager pMan = FontManager.privGetInstance();
            pMan.baseRemove(pNode);
        }
        public static Font Find(FontName name)
        {
            FontManager pMan = FontManager.privGetInstance();

            // Compare functions only compares two Nodes
            pMan.pRefNode.name = name;

            Font pData = (Font)pMan.baseFind(pMan.pRefNode);
            return pData;
        }


        public static void Dump()
        {
            FontManager pMan = FontManager.privGetInstance();
            Debug.Assert(pMan != null);

           // Debug.WriteLine("------ Font Manager ------");
           // pMan.baseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Font pDataA = (Font)pLinkA;
            Font pDataB = (Font)pLinkB;

            Boolean status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }
        override protected DLink dCreateNode()
        {
            DLink pNode = new Font();
            Debug.Assert(pNode != null);
            return pNode;
        }
        override protected void dClearNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Font pNode = (Font)pLink;
            pNode.dClean();
        }

        //override protected void derivedDumpNode(MLink pLink)
        //{
        //    Debug.Assert(pLink != null);
        //    Font pNode = (Font)pLink;

        //    Debug.Assert(pNode != null);
        //    pNode.Dump();
        //}

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static FontManager privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data
        //----------------------------------------------------------------------
        private static FontManager pInstance = null;
        private Font pRefNode;
    }
    
}
