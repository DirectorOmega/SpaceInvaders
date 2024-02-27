using System;
using System.Diagnostics;

namespace SpaceInvaders.PCS
{
  public abstract class PCSNode
    {
        // Constructors: --------------------------------
        public PCSNode()
        {
            this.parent = null;
            this.child = null;
            this.sibling = null;
        }

        public PCSNode getChild()
        {
            return child;
        }

        public PCSNode getParent()
        {
            return parent;
        }

        public PCSNode getSibling()
        {
            return sibling;
        }

        public virtual void setChild(PCSNode c)
        {
            child = c;
        }

        public void setParent(PCSNode p)
        {
            parent = p;
        }

        public void setSibling(PCSNode s)
        {
            sibling = s;
        }


        public PCSNode(PCSNode pNode)
        {
            this.parent = pNode.parent;
            this.child = pNode.child;
            this.sibling = pNode.sibling;
        }

        public PCSNode(PCSNode pParent, PCSNode pChild, PCSNode pSibling)
        {
            this.parent = pParent;
            this.child = pChild;
            this.sibling = pSibling;
        }

       // Methods: set/get -------------------------------

        abstract public Enum getName();

        // Methods: Dump ------------------

        public void dumpNode()
        {
            Debug.WriteLine("");
            Debug.WriteLine("    name: {0} {1}", this.getName(), this.GetHashCode());
            if (this.parent != null)
            {
                Debug.WriteLine("  parent: {0} {1}", this.parent.getName(), this.parent.GetHashCode());
            }
            else
            {
                Debug.WriteLine("  parent: ------");
            }
            if (this.child != null)
            {
                Debug.WriteLine("   child: {0} {1}", this.child.getName(), this.child.GetHashCode());
            }
            else
            {
                Debug.WriteLine("   child: ------");
            }
            if (this.sibling != null)
            {
                Debug.WriteLine(" sibling: {0} {1}", this.sibling.getName(), this.sibling.GetHashCode());
            }
            else
            {
                Debug.WriteLine(" sibling: ------");
            }

        }

        // data ----------------------
        private PCSNode parent;
        private PCSNode child;
        private PCSNode sibling;

    }
}
