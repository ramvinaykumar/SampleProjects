using Admin.Announcement.Repositoy.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Business.Handler
{
    public class GetCmsKeyValueHandler : BaseHandler, IRequestHandler<GetCmsDataQuery, Dictionary<string, string>>
    {
        private readonly IMicroContentService _microContentService;

        public GetCmsKeyValueHandler(IMicroContentService microContentService)
        {
            _microContentService = microContentService;
        }

        public async Task<Dictionary<string, string>> Handle(GetCmsDataQuery request, CancellationToken cancellationToken)
        {
            return await _microContentService.GetCmsCollectionFromProvider(request.ContentRequest, cancellationToken);
        }
    }
}
