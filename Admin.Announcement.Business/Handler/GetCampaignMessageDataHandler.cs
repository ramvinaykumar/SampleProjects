using Admin.Announcement.Business.Models;
using Admin.Announcement.Core.Constants;
using Admin.Announcement.Models;
using Admin.Announcement.Repositoy.Interface;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Business.Handler
{
    public class GetCampaignMessageDataHandler : BaseHandler, IRequestHandler<GetCampaignMessageDataQuery, GenericResponse<IEnumerable<LocalizedMessageModel>>>
    {
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly IMapper _mapper;

        public GetCampaignMessageDataHandler(ICampaignsRepository campaignsRepository, IMapper mapper)
        {
            _campaignsRepository = campaignsRepository;
            _mapper = mapper;
        }

        public async Task<GenericResponse<IEnumerable<LocalizedMessageModel>>> Handle(GetCampaignMessageDataQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Region))
            {
                return GetErrorResponse<IEnumerable<LocalizedMessageModel>>(new ErrorMessage
                {
                    ErrorType = ErrorType.Invalid,
                    Message = ApplicationConstants.RegionRequired
                }, StatusCodes.Status400BadRequest);
            }

            if (string.IsNullOrEmpty(request.LanguageCode))
            {
                return GetErrorResponse<IEnumerable<LocalizedMessageModel>>(new ErrorMessage
                {
                    ErrorType = ErrorType.Invalid,
                    Message = ApplicationConstants.LanguageRequired
                }, StatusCodes.Status400BadRequest);
            }

            var result = new GenericResponse<IEnumerable<LocalizedMessageModel>>();
            var localizedMessages = await _campaignsRepository.GetLocalizedMessageForWidget(request.Region, request.LanguageCode, request.Audience);
            var messages = _mapper.Map<List<LocalizedMessageModel>>(localizedMessages);

            if (messages != null && messages.Any())
            {
                result.Result = messages;
                result.IsValid = true;
                result.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            return GetNotFoundResponse<IEnumerable<LocalizedMessageModel>>();
        }
    }
}
