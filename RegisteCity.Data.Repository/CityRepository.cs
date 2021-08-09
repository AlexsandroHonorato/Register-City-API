using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Dapper;

using RegisteCity.Data.Repository.Interfaces;
using RegisterCity.Domain.Entities;

namespace RegisteCity.Data.Repository {
    public class CityRepository : ICityRepository {
        private readonly IRepositoryBase _repositoryBase;

        public CityRepository(IRepositoryBase repositoryBase) {
            this._repositoryBase = repositoryBase;

        }
        public async Task<List<City>> CityList(City city) {
            try {
                string lstrSelect = string.Empty;
                string lstrWhere = string.Empty;

                using (var connection = new SqlConnection(this._repositoryBase.ConnectionString)) {

                    lstrSelect =   $"select                             " +
                                    "convert(nvarchar(50),[Id]) as id,  " +
                                    "[Ibge]             as ibge,        " +
                                    "[UF]               as uf,          " +
                                    "[NameCity]         as nameCity,    " +
                                    "[Latitude]         as latitude,    " +
                                    "[Longitude]        as longitude,   " +
                                    "[Region]           as region       " +
                                    "from [City]                        ";
                    lstrWhere = getWhereFilter(city);

                    if (lstrWhere != string.Empty)
                        lstrSelect += " WHERE " + Environment.NewLine + lstrWhere;                 

                    return (List<City>)await connection.QueryAsync<City>(lstrSelect);
                 
                }

            } catch (Exception error) {

                throw error;
            }
        }

        private string getWhereFilter(City city) {
            try {
                string lstrWhere = string.Empty;

                if (city.uf != String.Empty) 
                    lstrWhere = $" [UF] = '{city.uf}'" + Environment.NewLine;

                if (city.nameCity != String.Empty) {
                    if (lstrWhere != string.Empty)
                        lstrWhere += " and ";

                    lstrWhere += $"[NameCity] = '{city.nameCity}'" + Environment.NewLine;
                }

                if (city.region != String.Empty) {
                    if (lstrWhere != string.Empty)
                        lstrWhere += " and ";

                    lstrWhere += $"[Region] = '{city.region}'"+ Environment.NewLine;
                }

                return lstrWhere;

            } catch (Exception error) {

                throw error;
            }
        }

        public async Task<bool> DeleteCity(String Id) {
            try {
                using (var connection = new SqlConnection(this._repositoryBase.ConnectionString)) {
                    return await connection.ExecuteScalarAsync<bool>(
                        "delete from [City] where [Id] = @id ",
                        new { id = Id });
                }

            } catch (Exception error) {

                throw error;
            }
        }

        public async Task<City> GetCity(string Id) {
            try {
                using (var connection = new SqlConnection(this._repositoryBase.ConnectionString)) {
                    return await connection.QuerySingleAsync<City>(
                        "select                             " +
                        "convert(nvarchar(50),[Id]) as id,  " +
                        "[Ibge]             as ibge,        " +
                        "[UF]               as uf,          " +
                        "[NameCity]         as nameCity,    " +
                        "[Latitude]         as latitude,    " +
                        "[Longitude]        as longitude,   " +
                        "[Region]           as region       " +
                        "from [City]  where [Id] = @id      ", 
                        new { id = Id });
                }

            } catch (Exception error) {

                throw error;
            }
        }

        public async Task<City> InsertCity(City city) {
            try {

                using (var connection = new SqlConnection(this._repositoryBase.ConnectionString)) {

                    return await connection.QuerySingleAsync<City>(
                    "insert into [City]                                                                 " +
                    "( [Ibge], [UF], [NameCity], [Latitude], [Longitude], [Region] )                    " +
                    "OUTPUT CONVERT(nvarchar(50), INSERTED.Id) aS Id, INSERTED.Ibge as ibge,            " +
                    "INSERTED.UF as uf, INSERTED.NameCity as nameCity, INSERTED.Latitude as latitude,   " +
                    "INSERTED.Longitude as longitude, INSERTED.Region as region                         " +
                    "values (@ibge, @uf, @nameCity, @latitude, @longitude, @region )",
                    new {
                        ibge = city.ibge,
                        uf = city.uf.ToUpper().Trim(),
                        nameCity = city.nameCity.ToUpper().Trim(),
                        latitude = city.latitude.Trim(),
                        longitude = city.longitude.Trim(),
                        region = city.region.ToUpper().Trim()

                    });
                }

            } catch (Exception error) {

                throw error;
            }

        }

        public async Task<bool> UpdateCity(City city) {
            try {
                using (var connection = new SqlConnection(this._repositoryBase.ConnectionString)) {
                    return await connection.ExecuteScalarAsync<bool>(
                        "update [City]  set             "+
                        "[Ibge]         = @ibge,        " +
                        "[UF]           = @uf,          " +
                        "[NameCity]     = @nameCity,    " +
                        "[Latitude]     = @latitude,    " +
                        "[Longitude]    = @longitude,   " +
                        "[Region]       = @region       " +
                        "where[Id]      = @id           ",
                        new { 
                            id = city.id,
                            ibge = city.ibge,
                            uf = city.uf,
                            nameCity = city.nameCity,
                            latitude = city.latitude,
                            longitude = city.longitude,
                            region = city.region
                        });
                }

            } catch (Exception error) {

                throw error;
            }
        }

        public async Task<List<City>> UF() {
            try {
                using (var connection = new SqlConnection(this._repositoryBase.ConnectionString)) {
                    return (List<City>)await connection.QueryAsync<City>(
                        "select [UF] as uf from [City] ");
                }

            } catch (Exception error) {

                throw error;
            }
        }

        public async Task<List<City>> City(string uf) {
            try {
                using (var connection = new SqlConnection(this._repositoryBase.ConnectionString)) {
                    return (List<City>)await connection.QueryAsync<City>(
                        "select                         " +
                        "[NameCity]         as nameCity " +
                        "from [City]  where [UF] = @UF  ",
                        new { UF = uf });
                }

            } catch (Exception error) {

                throw error;
            }
        }

        public async Task<List<City>> Region(string uf, string nameCity) {
            try {
                using (var connection = new SqlConnection(this._repositoryBase.ConnectionString)) {
                    return (List<City>)await connection.QueryAsync<City>(
                        "select                 " +
                        "[Region]   as region   " +
                        "from [City]            " +
                        "where [UF] = @UF       " +
                        "and [NameCity]  =  @City",
                        new { UF = uf, City = nameCity });
                }

            } catch (Exception error) {

                throw error;
            }
        }
    }
}
