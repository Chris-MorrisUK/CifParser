using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDFBase
{
    public class RDFClass : IComparable<RDFClass>
    {
        private string className;

        public string ClassName
        {
            get { return className; }
            set
            {
                className = value;              
            }
        }

        public void AddChild(RDFClass child)
        {
            if (children == null)
                children = new RDFClassCollection();
            children.Add(child);
        }

        public void AddParent(RDFClass parent)
        {
            if (parents == null)
                parents = new RDFClassCollection();
            parents.Add(parent);
        }

        #region IComparible members

        int IComparable<RDFClass>.CompareTo(RDFClass other)
        {
            if ((this.children == null) && (other.children == null))
                return 0;
            if(this.children == null)
                return other.children.Count * -1;
            if (other.children == null)
                return this.children.Count;
            return this.children.Count- other.children.Count;
        }

        #endregion

 
        private RDFClassCollection children;
        private RDFClassCollection parents;

        public RDFClassCollection Children { get { return children; } }
        public RDFClassCollection Parents { get { return parents; } }

    }
}
