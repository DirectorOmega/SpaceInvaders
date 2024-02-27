using System.Diagnostics;

namespace SpaceInvaders.PCS
{
   public class PCSTree
    {
        public enum Name
        {
            Root,
            NotInitialized
        }

        // constructor
        public PCSTree()
        {
            this.root = null;
            this.maxNodeCount = 0;
            this.numNodes = 0;

            // create the root
            PCSNode pcsRoot = new PCSRootNode(PCSTree.Name.Root);
            this.Insert(pcsRoot, null);
        }

        public PCSTree(PCSNode RootNode)
        {
            this.root = null;
            this.maxNodeCount = 0;
            this.numNodes = 0;

            this.Insert(RootNode, null);
        }

        public PCSNode getRoot()
        {
            return this.root;
        }

        public void SetRoot(PCSNode pRoot)
        {
            Debug.Assert(pRoot != null);
            this.root = pRoot;
        }

        // insert
        public void Insert(PCSNode inNode, PCSNode pParent)
        {
            Debug.Assert(inNode != null);
            DumpNodeCount();
            // insert to root
            if (null == pParent)
            {
                this.root = inNode;
                inNode.setChild(null);
                inNode.setParent(null);
                inNode.setSibling(null);

                this.privInfoAddNode();
            }
            else  // insert inside the tree
            {
                if (pParent.getChild() == null)
                { // child is 0, just add child
                    pParent.setChild(inNode);

                    inNode.setParent(pParent);
                    inNode.setChild(null);
                    inNode.setSibling(null);

                    this.privInfoAddNode();
                }
                else
                { // add as sibling

                    // Get first child
                    PCSNode first = pParent.getChild();

                    inNode.setParent(pParent);
                    inNode.setChild(null);
                    inNode.setSibling(first);

                    pParent.setChild(inNode);

                    this.privInfoAddNode();
                }
            }
        }

        public void DumpNodeCount()
        {
            //Debug.WriteLine("");
            //Debug.WriteLine("TreeCount = {0}",numNodes);
        }

        // remove
        public void Remove(PCSNode inNode)
        {
            Debug.Assert(inNode != null);

            if (inNode.getChild() == null)
            {
                // last node
                if (inNode.getSibling() == null)
                {
                    // find the previous child
                    PCSNode pParent;
                    pParent = inNode.getParent();

                    // special case root
                    if (pParent == null)
                    {
                        this.root = null;
                    }
                    else
                    {   // no children, no younger siblings
                        privRemoveNodeNoYoungerSiblings(inNode, pParent);
                    }
                }
                else
                {   // No children, but has other younger siblings
                    privRemoveNodeHasYoungerSiblings(inNode);
                }

                inNode.setChild(null);
                inNode.setParent(null);
                inNode.setSibling(null);
                this.privInfoRemoveNode();
                DumpNodeCount();
                return;
            }
            else
            {
                // If we are here, recursively call
                PCSNode pTmp = inNode.getChild();
                Debug.Assert(pTmp != null);

                this.Remove(pTmp);
                this.Remove(inNode);
            }
        }

        private void privRemoveNodeNoYoungerSiblings(PCSNode inNode, PCSNode pParent)
        {
            Debug.Assert(pParent != null);

            PCSNode pTmp;
            // goto eldest child
            pTmp = pParent.getChild();
            Debug.Assert(pTmp != null);

            if (pTmp.getSibling() == null)
            {   // delete inNode so it's parent is 0
                // in child has no siblings
                pParent.setChild(null);
            }
            else
            {   // now iterate until child
                while (pTmp.getSibling() != inNode)
                {
                    pTmp = pTmp.getSibling();
                }
                // this point we found the sibling
                PCSNode pPrev = pTmp;
                pPrev.setSibling(null);
            }
        }

        private void privRemoveNodeHasYoungerSiblings(PCSNode inNode)
        {
            // I have a sibling to the right of me
            // find the previous child
            PCSNode pParent;
            pParent = inNode.getParent();
            Debug.Assert(pParent != null);

            PCSNode pTmp;

            // goto eldest child
            pTmp = pParent.getChild();
            Debug.Assert(pTmp != null);

            if (pTmp == inNode)
            {   // we are deleting a eldest child with siblings
                pParent.setChild(pTmp.getSibling());
            }
            else
            {   // now iterate until child
                while (pTmp.getSibling() != inNode)
                {
                    pTmp = pTmp.getSibling();
                }

                // this point we found the sibling
                PCSNode pPrev = pTmp;
                pPrev.setSibling(inNode.getSibling());
            }
        }

        public void dumpTree()
        {
            Debug.WriteLine("");
            Debug.WriteLine("dumpTree () -------------------------------");
            this.privDumpTreeDepthFirst(this.root);
        }


        private void privDumpTreeDepthFirst(PCSNode pNode)
        {
            PCSNode pChild = null;

            // dump
            pNode.dumpNode();

            // iterate through all of the active children 
            if (pNode.getChild() != null)
            {
                pChild = pNode.getChild();
                // make sure that allocation is not a child node 
                while (pChild != null)
                {
                    privDumpTreeDepthFirst(pChild);
                    // goto next sibling
                    pChild = pChild.getSibling();
                }
            }
            else
            {
                // bye bye exit condition
            }
        }

        private void privInfoAddNode()
        {
            this.numNodes += 1;

            if (this.numNodes > this.maxNodeCount)
            {
                this.maxNodeCount += 1;
            }
        }

        private void privInfoRemoveNode()
        {
            numNodes -= 1;
        }


        internal sealed class PCSRootNode : PCSNode
        {
            public PCSRootNode(PCSTree.Name treeName)
                : base()
            {
                this.name = treeName;
            }

            public override System.Enum getName()
            {
                return this.name;
            }

            private PCSTree.Name name;
        }

        // Data -----------------------------------------------------


        private PCSNode root;
        public int numNodes;
        public int maxNodeCount;
    }
}

