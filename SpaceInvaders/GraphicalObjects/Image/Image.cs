using System.Diagnostics;

namespace SpaceInvaders.GraphicalObjects
{
    class Image : ILink
    {
        private ImageID Name;
        private Texture pTex;
        private Azul.Rect poImageRect;

        public Image()
        {
            this.Name = ImageID.Undef;
            this.pTex = null;
            this.poImageRect = new Azul.Rect();
            Debug.Assert(poImageRect !=null);
        }

        public ImageID getName()
        {
            return Name;
        }

        public Azul.Texture getAzTex()
        {
            return pTex.getTex();
        }

        public Azul.Rect getAzRect()
        {
            return poImageRect;
        }
        
      
        internal void Set(ImageID name, Texture ptex,Azul.Rect ImageRect)
        {
            Debug.Assert(ptex != null);
            Debug.Assert(ImageRect != null);
            this.Name = name;
            this.pTex = ptex;
            this.poImageRect.Set(ImageRect);
        }

        public void setName(ImageID name)
        {
            this.Name = name;
        }

        public void setRect(Azul.Rect Rect)
        {
            poImageRect = Rect;
        }

        public override void dClean()
        {
            Name = ImageID.Undef;
            pTex = null;
            poImageRect.Clear();
        }
    }
}
