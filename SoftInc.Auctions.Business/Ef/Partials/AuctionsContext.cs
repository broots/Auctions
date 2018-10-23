using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftInc.Auctions.Business.Ef
{
    public partial class AuctionsContext
    {
        public AuctionsContext(string connString) : base($"name={connString}")
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}
