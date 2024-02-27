namespace SpaceInvaders.GraphicalObjects
{
    internal sealed class ImageHolder : IHLink
    {
        private Image pImage;

        public ImageHolder()
        {
            pImage = null;
        }

        public override void dClean()
        {
            pImage = null;
        }

        public void setImage(Image pImage)
        {

            this.pImage = pImage;
        }

        public Image getImage()
        {
            return pImage;
        }

    }
}
