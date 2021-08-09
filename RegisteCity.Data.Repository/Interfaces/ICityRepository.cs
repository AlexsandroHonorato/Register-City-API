using RegisterCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RegisteCity.Data.Repository.Interfaces {
    public interface ICityRepository {
        Task<City> InsertCity(City city);
        Task<bool> DeleteCity(String Id);
        Task<bool> UpdateCity(City city);
        Task<List<City>> CityList(City city);
        Task<City> GetCity(String Id);
        Task<List<City>> UF();
        Task<List<City>> City(String uf);
        Task<List<City>> Region(String uf, String nameCity);
    }
}
