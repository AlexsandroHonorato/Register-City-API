using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;


using RegisterCity.Application.Helper;
using RegisterCity.Services.Hots.Command;
using RegisterCity.Application.ViewModel;
using RegisterCity.Application.Interfaces;

namespace RegisterCity.Services.Hots.Controllers {
    [Route("api/[controller]")]
    [ApiController]

    public class RegisterCityController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly ICityServices _cityServices;

        public RegisterCityController(IMapper mapper, ICityServices cityServices) {
            this._mapper = mapper;
            this._cityServices = cityServices;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCity([FromBody] CityCommand  cityCommand) {
            try {

                CityViewModel city = this._mapper.Map<CityViewModel>(cityCommand);

                AuthResponseObjHelper authResult = await this._cityServices.InsertCity(city);

                if(authResult.status_code == 200)
                    return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message });

                return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message });


            } catch (Exception error) {          
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {error.Message}");
            }
        }

        [HttpPost("List")]
        [AllowAnonymous]
        public async Task<IActionResult> ListCity([FromBody] CityCommand cityCommand) {
            try {
                CityViewModel city = this._mapper.Map<CityViewModel>(cityCommand);

                AuthResponseObjHelper authResult = await this._cityServices.CityList(city);

                if (authResult.status_code == 200)
                    return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message, ListCity =  authResult.city_list});

                return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message });


            } catch (Exception error) {        
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {error.Message}");
            }
        }

        [HttpPost("GetCity")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCity([FromBody] CityCommand cityCommand) {
            try {

                AuthResponseObjHelper authResult = await this._cityServices.GetCity(cityCommand.id);

                if (authResult.status_code == 200)
                    return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message, City = authResult.city });

                return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message });


            } catch (Exception error) { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {error.Message}");
            }
        }


        [HttpGet("UF")]
        [AllowAnonymous]
        public async Task<IActionResult> UF() {
            try {

                AuthResponseObjHelper authResult = await this._cityServices.UF();

                if (authResult.status_code == 200)
                    return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message, uf = authResult.city_list }); ;

                return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message });


            } catch (Exception error) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {error.Message}");
            }
        }


        [HttpPost("City")]
        [AllowAnonymous]
        public async Task<IActionResult> City([FromBody] CityCommand cityCommand) {
            try {

                AuthResponseObjHelper authResult = await this._cityServices.City(cityCommand.uf);

                if (authResult.status_code == 200)
                    return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message, City = authResult.city_list });

                return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message });


            } catch (Exception error) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {error.Message}");
            }
        }


        [HttpPost("Region")]
        [AllowAnonymous]
        public async Task<IActionResult> Region([FromBody] CityCommand cityCommand) {
            try {

                AuthResponseObjHelper authResult = await this._cityServices.Region(cityCommand.uf, cityCommand.nameCity);

                if (authResult.status_code == 200)
                    return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message, region = authResult.city_list });

                return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message });


            } catch (Exception error) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {error.Message}");
            }
        }

        [HttpDelete("DeleteCity")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCity([FromBody] CityCommand cityCommand) {
            try {

                AuthResponseObjHelper authResult = await this._cityServices.DeleteCity(cityCommand.id);

                if (authResult.status_code == 200)
                    return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message});

                return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message });


            } catch (Exception error) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {error.Message}");
            }
        }

        [HttpPut("UpdateCity")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCity([FromBody] CityCommand cityCommand) {
            try {

                CityViewModel city = this._mapper.Map<CityViewModel>(cityCommand);

                AuthResponseObjHelper authResult = await this._cityServices.UpdateCity(city);

                if (authResult.status_code == 200)
                    return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message });

                return StatusCode(authResult.status_code, new { StatusDescription = authResult.http_message });


            } catch (Exception error) {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {error.Message}");
            }
        }
    }
}
