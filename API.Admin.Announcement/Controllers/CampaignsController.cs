using Admin.Announcement.Business.Models;
using Admin.Announcement.Core.Constants;
using Admin.Announcement.Models;
using Admin.Announcement.Models.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Admin.Announcement.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "solution-campaign-api.all")]
    [ApiController]
    public class CampaignsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IOptionsMonitor<ApplicationConfiguration> _applicationConfiguration;

        public CampaignsController(IMediator mediator, IOptionsMonitor<ApplicationConfiguration> options)
        {
            _mediator = mediator;
            _applicationConfiguration = options;
        }

        [HttpGet]
        public async Task<GenericResponse<IEnumerable<CampaignModel>>> GetCampaigns(bool? isActive, CampaignSection campaignType, [FromHeader] string OSCUserName)
        {
            if (!CheckUserCampaignAccess(OSCUserName))
                return GetErrorResponse<IEnumerable<CampaignModel>>(new ErrorMessage
                {
                    ErrorType = ErrorType.Forbidden,
                    Message = ""
                }
               , StatusCodes.Status403Forbidden);
            return await _mediator.Send(new GetCampaignsQuery { IsActive = isActive, CampaignType = campaignType });
        }

        [HttpGet("widget")]
        public async Task<GenericResponse<IEnumerable<LocalizedMessageModel>>> GetCampaignForUser(string region, string languageCode, string audience)
        {
            region = region.ToLowerInvariant().Equals("euro") ? "EMEA" : region;
            region = region.ToLowerInvariant().Equals("asia") ? "APJ" : region;

            return await _mediator.Send(new GetCampaignMessageDataQuery
            {
                Region = region?.ToUpperInvariant(),
                LanguageCode = languageCode?.ToUpperInvariant(),
                Audience = audience?.ToUpperInvariant()
            });
        }

        [HttpPost("create")]
        public async Task<GenericResponse<CampaignModel>> PostCampaign([FromBody] SaveCampaignCommand campaignRequest, [FromHeader] string OSCUserName)
        {
            if (!CheckUserCampaignAccess(OSCUserName))
                return GetErrorResponse<CampaignModel>(new ErrorMessage
                {
                    ErrorType = ErrorType.Forbidden,
                    Message = ""
                }
               , StatusCodes.Status403Forbidden);
            campaignRequest.CreatedBy = OSCUserName;
            return await _mediator.Send(campaignRequest);
        }

        [HttpPut("update")]
        public async Task<GenericResponse<bool>> PutCampaign([FromBody] UpdateCampaignCommand updateCampaignRequest, [FromHeader] string OSCUserName)
        {
            if (!CheckUserCampaignAccess(OSCUserName))
                return GetErrorResponse<bool>(new ErrorMessage
                {
                    ErrorType = ErrorType.Forbidden,
                    Message = ""
                }, StatusCodes.Status403Forbidden);
            updateCampaignRequest.ModifiedBy = OSCUserName;
            return await _mediator.Send<GenericResponse<bool>>(updateCampaignRequest);
        }

        [HttpPost("archive")]
        public async Task<GenericResponse<bool>> ArchiveCampaign([FromBody] ArchiveCampaignCommand request, [FromHeader] string OSCUserName, CancellationToken token)
        {
            if (!CheckUserCampaignAccess(OSCUserName))
                return GetErrorResponse<bool>(new ErrorMessage
                {
                    ErrorType = ErrorType.Forbidden,
                    Message = ""
                }, StatusCodes.Status403Forbidden);
            request.UserName = OSCUserName;
            return await _mediator.Send(request, token);
        }

        [HttpGet("regions")]
        public GenericResponse<IEnumerable<string>> GetRegions()
        {
            // TODO: need to keep these values in spring config or CMS
            return new GenericResponse<IEnumerable<string>>
            {
                Result = new List<string>() { "AMER", "EMEA", "APJ", "LA" },
                StatusCode = 200,
                IsValid = true
            };
        }
        [HttpGet("audiences")]
        public GenericResponse<Dictionary<string, string>> GetAudiences()
        {
            // TODO: need to keep these values in spring config or CMS
            //Result = new List<string>() { "ALL", "PARTNER", "TSR", "RESELLER" },
            return new GenericResponse<Dictionary<string, string>>
            {
                Result = CampaignConstants.Audiences,
                StatusCode = 200,
                IsValid = true
            };
        }

        [HttpGet("adminaccess")]
        public GenericResponse<bool> GetAdminAccess(string userId)
        {
            return new GenericResponse<bool>
            {
                Result = CheckUserCampaignAccess(userId),
                StatusCode = 200,
                IsValid = true
            };
        }

        private bool CheckUserCampaignAccess(string emailId)
        {
            if (!string.IsNullOrEmpty(emailId))
            {
                var allowedUserList = _applicationConfiguration.CurrentValue?.AllowedUserInfo;
                if (allowedUserList != null && allowedUserList.Count > 0)
                {
                    return allowedUserList.FirstOrDefault(x => x.EmailId.ToLowerInvariant() == emailId.ToLowerInvariant())?.HasCampaignAccess ?? false;
                }
            }
            return false;
        }

        private GenericResponse<T> GetErrorResponse<T>(ErrorMessage errorMessage, int statusCode)
        {
            return new GenericResponse<T>
            {
                Errors = new List<ErrorMessage>
                    {
                        errorMessage
                    },
                StatusCode = statusCode
            };
        }
    }
}
