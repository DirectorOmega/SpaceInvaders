using SpaceInvaders.GraphicalObjects;
using System.Diagnostics;

namespace SpaceInvaders.FontSystem
{
    class Glyph : GLink
    {
        public enum Name
        {
            Consolas36pt,
            NullObject,
            Uninitialized,
            InvadersText
        }

        public Glyph()
            : base()
        {
            name = Name.Uninitialized;
            pTexture = null;
            pSubRect = new Azul.Rect();
            key = 0;
        }

        ~Glyph()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Glyph():{0} ", this.GetHashCode());
#endif
            name = Name.Uninitialized;
            pSubRect = null;
            pTexture = null;
        }

        public void Set(Glyph.Name name, int key, TextureID textName, float x, float y, float width, float height)
        {
            Debug.Assert(this.pSubRect != null);
            this.name = name;

            pTexture = TextureManager.Find(textName);
            Debug.Assert(this.pTexture != null);

            pSubRect.Set(x, y, width, height);
            this.key = key;
        }

        override public void dClean()
        {
            name = Name.Uninitialized;
            pTexture = null;
            pSubRect.Set(0, 0, 1, 1);
            key = 0;
        }

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\tname: {0} ({1})", name, GetHashCode());
            Debug.WriteLine("\t\t\tkey: {0}", key);
            if (pTexture != null)
                Debug.WriteLine("\t\t   pTexture: {0}", pTexture.GetName());
            else
                Debug.WriteLine("\t\t   pTexture: null");
            Debug.WriteLine("\t\t      pRect: {0}, {1}, {2}, {3}", pSubRect.x, pSubRect.y, pSubRect.width, pSubRect.height);
        }

        public Azul.Rect GetAzulSubRect()
        {
            Debug.Assert(pSubRect != null);
            return pSubRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(pTexture != null);
            return pTexture.GetTex();
            //return this.pTexture.getAzulTexture();
        }

        // ----------------------------------------------------------------
        // Data 
        // ----------------------------------------------------------------
        public Name name;
        public int key;
        private Azul.Rect pSubRect;
        private Texture pTexture;
    }
}
