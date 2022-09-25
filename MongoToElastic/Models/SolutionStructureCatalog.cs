using MongoDB.Bson.Serialization.Attributes;
using Nest;
using Newtonsoft.Json;

namespace MongoToElastic.Models
{
    [BsonIgnoreExtraElements]
    [JsonObject(MemberSerialization.OptIn)]
    public class SolutionStructureCatalog
    {
        private string _solutionStructureId;
        [PropertyName("solutionStructureId")]
        [JsonProperty]
        public string SolutionStructureId
        {
            get
            {
                if (!string.IsNullOrEmpty(_solutionStructureId))
                {
                    return _solutionStructureId.ToUpper();
                }

                return string.Empty;
            }
            set
            {
                _solutionStructureId = value;
            }
        }

        [PropertyName("name")]
        [JsonProperty]
        public string Name { get; set; }

        [PropertyName("solutionStructureType")]
        [JsonProperty]
        public int SolutionStructureType { get; set; }

        private string _workFlowType;
        [PropertyName("workFlowType")]
        [JsonProperty]
        public string WorkFlowType
        {
            get
            {
                if (!string.IsNullOrEmpty(_workFlowType))
                {
                    return _workFlowType.ToUpper();
                }

                return string.Empty;
            }
            set
            {
                _workFlowType = value;
            }
        }

        [PropertyName("onlineCatalog")]
        [JsonProperty]
        public string OnlineCatalog { get; set; }

        [PropertyName("offlineCatalog")]
        [JsonProperty]
        public string OfflineCatalog { get; set; }

        [PropertyName("techSpecOrderCode")]
        [JsonProperty]
        public string TechSpecOrderCode { get; set; }

        [PropertyName("customerContext")]
        [JsonProperty]
        public CustomerContextModel CustomerContext { get; set; }

        [JsonProperty]
        [PropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty]
        [PropertyName("modifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [JsonProperty]
        [PropertyName("takeOffTheTree")]
        public bool TakeOffTheTree { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CustomerContextModel
    {
        private string _countryCode;
        [JsonProperty]
        [PropertyName("countryCode")]
        public string CountryCode
        {
            get
            {
                if (!string.IsNullOrEmpty(_countryCode))
                    return _countryCode.ToUpper();

                return string.Empty;
            }
            set
            {
                _countryCode = value;
            }
        }

        private string _customerSetId;
        [JsonProperty]
        [PropertyName("customerSetId")]
        public string CustomerSetId
        {
            get
            {
                if (!string.IsNullOrEmpty(_customerSetId))
                    return _customerSetId.ToUpper();

                return string.Empty;
            }
            set
            {
                _customerSetId = value;
            }
        }

        private string _languageCode;
        [JsonProperty]
        [PropertyName("languageCode")]
        public string LanguageCode
        {
            get
            {
                if (!string.IsNullOrEmpty(_languageCode))
                    return _languageCode.ToUpper();

                return string.Empty;
            }
            set
            {
                _languageCode = value;
            }
        }

        private string _region;
        [JsonProperty]
        [PropertyName("region")]
        public string Region
        {
            get
            {
                if (!string.IsNullOrEmpty(_region))
                    return _region.ToUpper();

                return string.Empty;
            }
            set
            {
                _region = value;
            }
        }
    }
}
