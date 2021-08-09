using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using RegisteCity.Data.Repository.Interfaces;
using RegisterCity.Application.Helper;
using RegisterCity.Application.Interfaces;
using RegisterCity.Application.ViewModel;
using RegisterCity.Domain.Entities;

namespace RegisterCity.Application {
    public class CityServices : ICityServices {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityServices(IMapper mapper,ICityRepository cityRepository) {
            this._mapper = mapper;
            this._cityRepository = cityRepository;
        }

        public async Task<AuthResponseObjHelper> City(string uf) {
            try {
                List<CityViewModel> city = this._mapper.Map<List<CityViewModel>>(await this._cityRepository.City(uf));

                if (city != null)
                    return new AuthResponseObjHelper() { status_code = 200, http_message = $"Dados retornada com sucesso", city_list = city };

                return new AuthResponseObjHelper() { status_code = 501, http_message = "Erro ao carregar os dados." };

            } catch (Exception error) {

                return new AuthResponseObjHelper() { status_code = 500, http_message = $"{error.Message}" };
            }
        }

        public async Task<AuthResponseObjHelper> CityList(CityViewModel cityModel) {
            try {
                City city = this._mapper.Map<City>(cityModel);

                List<CityViewModel> returnCity = this._mapper.Map<List<CityViewModel>>(await this._cityRepository.CityList(city));

                if (returnCity != null)
                    return new AuthResponseObjHelper() { status_code = 200, http_message = $"Lista retornada com sucesso", city_list = returnCity };

                return new AuthResponseObjHelper() { status_code = 501, http_message = "Erro ao carregar os dados." };

            } catch (Exception error) {

                return new AuthResponseObjHelper() { status_code = 500, http_message = $"{error.Message}" };
            }
        }

        public async Task<AuthResponseObjHelper> DeleteCity(String Id) {
            try {
                bool delete = await this._cityRepository.DeleteCity(Id);

                if(!delete)
                    return new AuthResponseObjHelper() { status_code = 200, http_message = $"Dados deltedados com sucesso" };

                return new AuthResponseObjHelper() { status_code = 501, http_message = "Erro ao deletar os dados." };

            } catch (Exception error) {

                return new AuthResponseObjHelper() { status_code = 500, http_message = $"{error.Message}" };
            }
        }

        public async Task<AuthResponseObjHelper> GetCity(string Id) {
            try {
                CityViewModel city = this._mapper.Map<CityViewModel>(await this._cityRepository.GetCity(Id));

                if (city != null)
                    return new AuthResponseObjHelper() { status_code = 200, http_message = $"Dados retornada com sucesso", city = city };

                return new AuthResponseObjHelper() { status_code = 501, http_message = "Erro ao carregar os dados." };

            } catch (Exception error) {

                return new AuthResponseObjHelper() { status_code = 500, http_message = $"{error.Message}" };
            }     
        }

        public async Task<AuthResponseObjHelper> InsertCity(CityViewModel cityModel) {
            try {
                City city = this._mapper.Map<City>(cityModel);

                City insetCity = await this._cityRepository.InsertCity(city);

                if (insetCity != null)
                    return new AuthResponseObjHelper() { status_code = 200, http_message = $"Dados da cidade {insetCity.nameCity}, salvos com sucesso." };

                return new AuthResponseObjHelper() { status_code = 501, http_message = "Erro ao inserir os Dados." };

            } catch (Exception error) {
                if (((System.Data.SqlClient.SqlException)error).Number == 2627)
                    return new AuthResponseObjHelper() { status_code = 200 ,http_message =  error.Message.Split("dbo.City'.")[1].Replace(".", "")};              

                return new AuthResponseObjHelper() { status_code = 500, http_message = $"{error.Message}" };
            }
        }

        public async Task<AuthResponseObjHelper> Region(string uf, string nameCity) {
            try {
                List<CityViewModel> region = this._mapper.Map<List<CityViewModel>>(await this._cityRepository.Region(uf, nameCity));

                if (region != null)
                    return new AuthResponseObjHelper() { status_code = 200, http_message = $"Dados retornada com sucesso", city_list = region };

                return new AuthResponseObjHelper() { status_code = 501, http_message = "Erro ao carregar os dados." };

            } catch (Exception error) {

                return new AuthResponseObjHelper() { status_code = 500, http_message = $"{error.Message}" };
            }
        }

        public async Task<AuthResponseObjHelper> UF() {
            try {
                List<CityViewModel> uf = this._mapper.Map<List<CityViewModel>>(await this._cityRepository.UF());

                if (uf != null)
                    return new AuthResponseObjHelper() { status_code = 200, http_message = $"Dados retornada com sucesso",  city_list = uf };

                return new AuthResponseObjHelper() { status_code = 501, http_message = "Erro ao carregar os dados." };

            } catch (Exception error) {

                return new AuthResponseObjHelper() { status_code = 500, http_message = $"{error.Message}" };
            }
        }

        public async Task<AuthResponseObjHelper> UpdateCity(CityViewModel cityModel) {
            try {
                City city = this._mapper.Map<City>(cityModel);

                bool update = await this._cityRepository.UpdateCity(city);

                if (!update)
                    return new AuthResponseObjHelper() { status_code = 200, http_message = $"Dados alterados com sucesso" };

                return new AuthResponseObjHelper() { status_code = 501, http_message = "Erro ao alterar os Dados." };

            } catch (Exception error) {

                return new AuthResponseObjHelper() { status_code = 500, http_message = $"{error.Message}" };
            }
        }
    }
}
