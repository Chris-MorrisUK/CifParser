using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF.Query;
using VDS.RDF;

namespace RDFBase
{
    public class RDFClassCollection: ICollection<RDFClass>
    {

        public RDFClassCollection()
        {
            backingStore = new Dictionary<string, RDFClass>();
        }

        Dictionary<string, RDFClass> backingStore;


        public void Add(RDFClass toAdd)
        {
            backingStore.Add(toAdd.ClassName, toAdd);
        }

        public static RDFClassCollection CreateFromSparql(IEnumerable<SparqlResult> toAdd)
        {
            RDFClassCollection res = new RDFClassCollection();
            res.PopulateFromSparql(toAdd);
            return res;
        }

        public void PopulateFromSparql(IEnumerable<SparqlResult> toAdd)
        {
            if (toAdd.Count<SparqlResult>() == 0)
                return;
            foreach (SparqlResult res in toAdd)
            {
                foreach (KeyValuePair<string, INode> v in res)
                {
                    RDFClass described = new RDFClass();
                    
                    if (v.Value != null)
                        described.ClassName = v.Value.ToString();
                    else
                        described.ClassName = "Annon";
                    backingStore.Add(described.ClassName, described);
                }

            }
        }



        public void PopulateAllLinksFromSparql(IEnumerable<SparqlResult> links)
        {
            if (links.Count<SparqlResult>() == 0)
                return; ;
            foreach (SparqlResult res in links)
            {
                KeyValuePair<string, INode> parentNode = res.SingleOrDefault(x => x.Key == "parent");
                KeyValuePair<string, INode> childNode = res.SingleOrDefault(x => x.Key == "child");
                INode parent = parentNode.Value;
                if ((!string.IsNullOrEmpty(parent.ToString())) && (!string.IsNullOrEmpty(childNode.Value.ToString())))
                {
                    if ((backingStore.ContainsKey(parentNode.Value.ToString())) && (backingStore.ContainsKey(childNode.Value.ToString())))
                    {
                        backingStore[parentNode.Value.ToString()].AddChild(backingStore[childNode.Value.ToString()]);
                        backingStore[childNode.Value.ToString()].AddParent(backingStore[parentNode.Value.ToString()]);
                    }
                }

            }
        }

        /// <summary>
        /// This recreates the list in a new order
        /// Not overly fast, nor any use for searching
        /// as number of children isn't the key
        /// </summary>
        public void SortByChildCount()
        {
            List<RDFClass> values = backingStore.Values.ToList();
            backingStore.Clear();
            values.Sort();//default sort orders by child count
            int nVals = values.Count - 1;
            for (int i = nVals; i >= 0; i--)
            {
                this.Add(values[i]);
            }
        }
        #region ICollection implementation
        public void Clear()
        {
            backingStore.Clear();
        }

        public bool Contains(RDFClass item)
        {
            return backingStore.ContainsKey(item.ClassName);
        }

        public void CopyTo(RDFClass[] array, int arrayIndex)
        {
            backingStore.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return backingStore.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(RDFClass item)
        {
            return backingStore.Remove(item.ClassName);
        }

        public IEnumerator<RDFClass> GetEnumerator()
        {
            return backingStore.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return backingStore.GetEnumerator();
        }
        #endregion
    }
}
