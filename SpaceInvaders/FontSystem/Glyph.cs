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
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.pSubRect = new Azul.Rect();
            this.key = 0;
        }

        ~Glyph()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Glyph():{0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.pSubRect = null;
            this.pTexture = null;
        }

        public void Set(Glyph.Name name, int key, TextureID textName, float x, float y, float width, float height)
        {
            Debug.Assert(this.pSubRect != null);
            this.name = name;

            this.pTexture = TextureManager.Find(textName);
            Debug.Assert(this.pTexture != null);

            this.pSubRect.Set(x, y, width, height);

            this.key = key;

        }

        override public void dClean()
        {
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.pSubRect.Set(0, 0, 1, 1);
            this.key = 0;
        }

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\tname: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("\t\t\tkey: {0}", this.key);
            if (this.pTexture != null)
            {
                Debug.WriteLine("\t\t   pTexture: {0}", this.pTexture.getName());
            }
            else
            {
                Debug.WriteLine("\t\t   pTexture: null");
            }
            Debug.WriteLine("\t\t      pRect: {0}, {1}, {2}, {3}", this.pSubRect.x, this.pSubRect.y, this.pSubRect.width, this.pSubRect.height);
        }

        public Azul.Rect GetAzulSubRect()
        {
            Debug.Assert(this.pSubRect != null);
            return this.pSubRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.pTexture != null);
            return this.pTexture.getTex();
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
