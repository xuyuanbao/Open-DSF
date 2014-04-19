using OpenDSF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDSF.Common.Entities
{
    interface CompositeEntity :  Entity, ICollection<Entity>
    {
    }
}
