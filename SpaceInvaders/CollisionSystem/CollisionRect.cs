namespace SpaceInvaders.CollisionSystem
{
    public class CollisionRect : Azul.Rect
    {
        public CollisionRect(Azul.Rect pRect)
            : base(pRect) { }

        public static bool Intersect(CollisionRect ColRectA, CollisionRect ColRectB)
        {
            float A_minx = ColRectA.x - ColRectA.width / 2;
            float A_maxx = ColRectA.x + ColRectA.width / 2;
            float A_miny = ColRectA.y - ColRectA.height / 2;
            float A_maxy = ColRectA.y + ColRectA.height / 2;

            float B_minx = ColRectB.x - ColRectB.width / 2;
            float B_maxx = ColRectB.x + ColRectB.width / 2;
            float B_miny = ColRectB.y - ColRectB.height / 2;
            float B_maxy = ColRectB.y + ColRectB.height / 2;

            // Trivial reject
            if ((B_maxx < A_minx) || (B_minx > A_maxx) || (B_maxy < A_miny) || (B_miny > A_maxy))
                return false;
            return true;
        }

        public void Union(CollisionRect ColRect)
        {
            float minX;
            float maxX;
            float minY;
            float maxY;


            if ((x - width / 2) < (ColRect.x - ColRect.width / 2))
            {
                minX = (x - width / 2);
            }
            else
            {
                minX = (ColRect.x - ColRect.width / 2);
            }

            if ((x + width / 2) > (ColRect.x + ColRect.width / 2))
            {
                maxX = (x + width / 2);
            }
            else
            {
                maxX = (ColRect.x + ColRect.width / 2);
            }

            if ((y + height / 2) > (ColRect.y + ColRect.height / 2))
            {
                maxY = (y + height / 2);
            }
            else
            {
                maxY = (ColRect.y + ColRect.height / 2);
            }

            if ((y - height / 2) < (ColRect.y - ColRect.height / 2))
            {
                minY = (y - height / 2);
            }
            else
            {
                minY = (ColRect.y - ColRect.height / 2);
            }

            width = (maxX - minX);
            height = (maxY - minY);
            x = minX + width / 2;
            y = minY + height / 2;
        }
    }
}
