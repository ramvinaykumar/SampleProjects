using Admin.Announcement.Business.Models;
using Admin.Announcement.Models;
using Admin.Announcement.Repositoy.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Business.Handler
{
    public class GetCampaignDataHandler : BaseHandler, IRequestHandler<GetCampaignsQuery, GenericResponse<IEnumerable<CampaignModel>>>
    {
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly IMapper _mapper;
        public GetCampaignDataHandler(ICampaignsRepository campaignsRepository, IMapper mapper)
        {
            _campaignsRepository = campaignsRepository;
            _mapper = mapper;
        }
        public async Task<GenericResponse<IEnumerable<CampaignModel>>> Handle(GetCampaignsQuery request, CancellationToken cancellationToken)
        {
            var result = new GenericResponse<IEnumerable<CampaignModel>>();
            var response = await _campaignsRepository.GetCampaigns(request.IsActive, request.CampaignType);
            var campaigns = _mapper.Map<List<CampaignModel>>(response);

            if (campaigns != null && campaigns.Any())
            {
                var sortedCampaigns = campaigns.OrderByDescending(campaign =>
                                      campaign.ModifiedOn != null ? campaign.ModifiedOn : campaign.CreatedOn
                                      );
                result.Result = sortedCampaigns;
                result.IsValid = true;
                result.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            return GetNotFoundResponse<IEnumerable<CampaignModel>>();
        }
    }
}
