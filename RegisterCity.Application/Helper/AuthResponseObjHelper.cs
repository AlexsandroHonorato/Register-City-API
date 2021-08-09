using System;
using System.Collections.Generic;
using System.Text;

using RegisterCity.Application.ViewModel;

namespace RegisterCity.Application.Helper {
    public class AuthResponseObjHelper {
        public int status_code { get; set; }
        public string http_message { get; set; }
        public string IBGE { get; set; }
        public List<CityViewModel> city_list{get; set;}
        public CityViewModel city { get; set; }
    }
}
