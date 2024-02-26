
using System.Diagnostics;

namespace SpaceInvaders.PCS
{
    //trying something a little different with inheritence.
 public abstract class PCSIterator
    {
        //depth first
        public abstract bool isDone();

        public abstract PCSNode first();

        public abstract PCSNode next();

        public abstract PCSNode currentItem();

    }

   public class PCSTreeIterator:PCSIterator
    {
        private PCSNode pRoot;
        private PCSNode pCur;

     public PCSTreeIterator(PCSNode root)
        {
            Debug.Assert(root != null);
            this.pRoot = root;
            this.pCur = root;
        }

        public override bool isDone()
        {
            return (this.pCur == null);
        }

        public override PCSNode first()
        {
            this.pCur = pRoot;
            return this.pCur;
        }

        private PCSNode privGetNext(PCSNode c, bool UseChild = true)
        {
            if (c != null)
            {
                if ((c.getChild() != null) && UseChild)
                {
                    c = c.getChild();
                }
                else if (c.getSibling() != null)
                {
                    c = c.getSibling();
                }
                else if (c.getParent() != this.pRoot)
                {
                    c = this.privGetNext(c.getParent(), false);
                }
                else
                {
                    c = null;
                }
            }
            return c;

        }

        public override PCSNode next()
        {
            this.pCur = privGetNext(this.pCur);
           return this.pCur;
        }

        public override PCSNode currentItem()
        {
            return this.pCur;
        }
    }
}
