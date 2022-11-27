using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Business.Handler
{
    public class UpdateCampaignCommandHandler : BaseHandler, IRequestHandler<UpdateCampaignCommand, GenericResponse<bool>>
    {
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly IMapper _mapper;
        public UpdateCampaignCommandHandler(ICampaignsRepository campaignsRepository, IMapper mapper)
        {
            _campaignsRepository = campaignsRepository;
            _mapper = mapper;
        }

        public async Task<GenericResponse<bool>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return GetErrorResponse<bool>(new ErrorMessage
                {
                    ErrorType = ErrorType.NullException
                }, StatusCodes.Status400BadRequest);
            }
            if (request.IsActive)
            {
                if (request.Regions == null || request.Regions.Count == 0)
                {
                    return GetErrorResponse<bool>(new ErrorMessage
                    {
                        ErrorType = ErrorType.Invalid,
                        Message = ApplicationConstants.RegionRequired
                    }, StatusCodes.Status400BadRequest);
                }

                if (request.Audiences == null || request.Audiences.Count == 0)
                {
                    return GetErrorResponse<bool>(new ErrorMessage
                    {
                        ErrorType = ErrorType.Invalid,
                        Message = ApplicationConstants.AudienceRequired
                    }, StatusCodes.Status400BadRequest);
                }

                if (request.LocalizedMessages.Any(lm => string.IsNullOrEmpty(lm.LanguageCode)))
                {
                    return GetErrorResponse<bool>(new ErrorMessage
                    {
                        ErrorType = ErrorType.Invalid,
                        Message = ApplicationConstants.LanguageRequired
                    }, StatusCodes.Status400BadRequest);
                }

                if (request.LocalizedMessages.Any(lm => string.IsNullOrWhiteSpace(lm.Message)))
                {
                    return GetErrorResponse<bool>(new ErrorMessage
                    {
                        ErrorType = ErrorType.Invalid,
                        Message = ApplicationConstants.MessageRequired
                    }
                    , StatusCodes.Status400BadRequest);
                }

                if (request.StartDateTime > request.EndDateTime)
                {
                    return GetErrorResponse<bool>(new ErrorMessage
                    {
                        ErrorType = ErrorType.Invalid,
                        Message = ApplicationConstants.DateTimeRangeInvalid
                    }
                    , StatusCodes.Status400BadRequest);
                }
            }

            var campaignEntity = _mapper.Map<CampaignEntity>(request);
            GenericResponse<bool> response = new GenericResponse<bool>();
            response.Result = await _campaignsRepository.UpdateCampaign(campaignEntity);
            if (response.Result)
            {
                response.IsValid = true;
                response.StatusCode = StatusCodes.Status200OK;
                return response;
            }
            return GetNotFoundResponse<bool>();
        }
    }
}
