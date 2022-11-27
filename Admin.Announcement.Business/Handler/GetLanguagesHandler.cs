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
    public class GetLanguagesHandler : BaseHandler, IRequestHandler<GetLanguagesQuery, GenericResponse<IEnumerable<Language>>>
    {
        private readonly ILanguagesRepository _campaignsRepository;
        public GetLanguagesHandler(ILanguagesRepository campaignsRepository)
        {
            _campaignsRepository = campaignsRepository;
        }
        public async Task<GenericResponse<IEnumerable<Language>>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var result = new GenericResponse<IEnumerable<Language>>();
            var response = await _campaignsRepository.GetLanguages(cancellationToken);

            if (response != null && response.Any())
            {
                result.Result = response;
                result.IsValid = true;
                result.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            return GetNotFoundResponse<IEnumerable<Language>>();
        }
    }
}
