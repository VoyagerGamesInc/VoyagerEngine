using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Framework
{
    public struct EntityId
    {
        public uint Id { get; internal set; }
        public bool IsValid => Id > 0;

        public void Reset()
        {
            Id = 0;
        }
    }
}
