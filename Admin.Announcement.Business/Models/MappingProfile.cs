using Admin.Announcement.Models.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Business.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LocalizedMessage, LocalizedMessageModel>();

            CreateMap<LocalizedMessageModel, LocalizedMessage>();

            CreateMap<SaveCampaignCommand, CampaignEntity>();

            CreateMap<UpdateCampaignCommand, CampaignEntity>();

            CreateMap<CampaignModel, CampaignEntity>();

            CreateMap<CampaignEntity, CampaignModel>()
                .ForMember(des => des.Regions, opt => opt.MapFrom(s => string.Join(", ", s.Regions)))
                .ForMember(des => des.Audiences, opt => opt.MapFrom((s, d) =>
                {
                    var list = new List<string>();
                    s.Audiences.ForEach(a =>
                    {
                        list.Add(CampaignConstants.AudiencesLookup[a].Single());
                    });
                    return string.Join(", ", list);
                }));
        }
    }
}
