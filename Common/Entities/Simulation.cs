using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenDSF.Common;
using System.Runtime.Serialization;
using Common.Entities;
using System.Security.Permissions;

namespace OpenDSF.Common.Entities
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
            private set // for serialization
            {
                _EntityID = value;
            }
        }

        // Simulations have no parent (they're the root of the "containment" object tree)
        public Entity Parent
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException("Simulation entities cannot have a parent!");
            }
        }
        #endregion

        #region CompositeEntity interface implementation 

        // Note this is just the collection of all the top-level entities (i.e. those directly contained by the simulation object, not objects at lower levels)
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

        #region ISerializable implementation
        protected Simulation(SerializationInfo info, StreamingContext context)
        {
            _EntityID = info.GetInt64("EntityID");
            for (Int32 i = 0; i < info.GetInt32("EntityCount"); ++i)
            {
                string EntityName = "Entity[" + i.ToString() + "]";
                // _entities is a polymorphic collection, so need to read the run-time type of each element
                Add(EntityFactory.GetEntity(info.GetString(EntityName + ".Type"), info, context));
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("EntityID", _EntityID);
            info.AddValue("EntityCount", _entities.Count);
            for (Int32 i = 0; i < _entities.Count; ++i)
            {
                string EntityName = "Entity[" + i.ToString() + "]";
                info.AddValue(EntityName + ".Type", _entities[i].GetType().FullName);
                _entities[i].GetObjectData(info, context);
            }
        }
        #endregion
    }
}
