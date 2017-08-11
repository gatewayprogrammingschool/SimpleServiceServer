using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.SimpleServiceServer
{
    public interface IService
    {
        Guid ServiceUID { get; }

        bool CanPause { get; }
    }
}
