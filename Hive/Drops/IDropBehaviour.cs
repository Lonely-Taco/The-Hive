using Hive.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Drops
{
    public interface IDropBehaviour
    {
        public Task<int> Claim(Counter nectarCounter);

    }
}
