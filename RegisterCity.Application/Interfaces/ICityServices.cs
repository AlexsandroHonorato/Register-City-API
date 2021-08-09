using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

using RegisterCity.Application.Helper;
using RegisterCity.Application.ViewModel;

namespace RegisterCity.Application.Interfaces {
    public interface ICityServices {
        Task<AuthResponseObjHelper> InsertCity(CityViewModel city);
        Task<AuthResponseObjHelper> DeleteCity(String Id);
        Task<AuthResponseObjHelper> UpdateCity(CityViewModel city);
        Task<AuthResponseObjHelper> CityList(CityViewModel city);
        Task<AuthResponseObjHelper> GetCity(String Id);
        Task<AuthResponseObjHelper> UF();
        Task<AuthResponseObjHelper> City(String uf);
        Task<AuthResponseObjHelper> Region(String uf, String nameCity);
    }
}
