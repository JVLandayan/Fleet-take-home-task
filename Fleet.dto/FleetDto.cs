using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.dto
{

    public class FleetRequests
    {

    }

    public class FleetResponse
    {
        public class ReadFleet
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }


}
