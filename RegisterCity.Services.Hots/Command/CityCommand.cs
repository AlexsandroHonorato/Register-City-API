using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterCity.Services.Hots.Command {
    public class CityCommand {
        public string id { get; set; }
        public decimal ibge { get; set; }
        public string uf { get; set; }
        public string nameCity { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string region { get; set; }
    }
}
