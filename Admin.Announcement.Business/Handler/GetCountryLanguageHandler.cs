using Admin.Announcement.Models;
using Admin.Announcement.Models.Entities;
using Admin.Announcement.Repositoy.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Business.Handler
{
    public class GetCountryLanguageHandler : BaseHandler, IRequestHandler<GetCountryLanguageQuery, GenericResponse<IEnumerable<CountryLanguage>>>
    {
        private readonly ICountryLanguageRepository _countryLanguageRepository;
        public GetCountryLanguageHandler(ICountryLanguageRepository countryLanguageRepository)
        {
            _countryLanguageRepository = countryLanguageRepository;
        }

        public async Task<GenericResponse<IEnumerable<CountryLanguage>>> Handle(GetCountryLanguageQuery request, CancellationToken cancellationToken)
        {
            var result = new GenericResponse<IEnumerable<CountryLanguage>>();
            var response = await _countryLanguageRepository.GetCountryLanguage(cancellationToken);

            if (response != null && response.Any())
            {
                result.Result = response;
                result.IsValid = true;
                result.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            return GetNotFoundResponse<IEnumerable<CountryLanguage>>();
        }
    }
}
