using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDSF.Common
{
    public interface Entity
    {
        public long EntityID { get; set; }

        public Entity Parent { get; set; }
    }
}
