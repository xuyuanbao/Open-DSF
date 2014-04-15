using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDSF.Common
{
    public interface Entity
    {
        // This ID is unique for a given simulation
        long EntityID { get; }

        // Being a parent of an entity implies you "contain" it
        Entity Parent { get; set; }
    }
}
