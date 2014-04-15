using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenDSF.Common;

namespace Common.Entities
{
    public class Simulation : CompositeEntity
    {
        #region Entity interface implementation
        long? _EntityID = null;
        public long EntityID
        {
            get
            {
                // It's never valid to get the EntityID until it's been initialised
                return _EntityID.Value;
            }
        }

        public Entity Parent
        {
            get
            {
                return null;
            }
            set
            {
                // Simulations have no parent
                throw new NotImplementedException("Simulation entities cannot have a parent!");
            }
        }
        #endregion

        #region CompositeEntity interface implementation 

        // Note this is just the collection of all the top-level entities (i.e. those directly parented by the simulation object)
        private Dictionary<long, Entity> _entities = new Dictionary<long, Entity>();
        
        public void Add(Entity e)
        {
            _entities.Add(e.EntityID, e);
        }

        public void Clear()
        {
            _entities.Clear();
        }

        public bool Contains(Entity e)
        {
            return _entities.Contains(new KeyValuePair<long, Entity>(e.EntityID, e));
        }

        public void CopyTo(Entity[] arr, int arrayIndex)
        {
            _entities.Values.CopyTo(arr, arrayIndex);
        }

        public int Count
        {
            get
            {
                return _entities.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(Entity e)
        {
            return _entities.Remove(e.EntityID);
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return _entities.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _entities.Values.GetEnumerator();
        }

        #endregion
    }
}
