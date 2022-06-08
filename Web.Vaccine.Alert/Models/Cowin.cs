using Newtonsoft.Json;
using System.Collections.Generic;

namespace Web.Vaccine.Alert.Models
{
    public class Cowin
    {

    }

    public class Session
    {
        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("available_capacity")]
        public int AvailableCapacity { get; set; }

        [JsonProperty("min_age_limit")]
        public int MinAgeLimit { get; set; }

        [JsonProperty("vaccine")]
        public string Vaccine { get; set; }

        [JsonProperty("slots")]
        public List<string> Slots { get; set; }

        [JsonProperty("available_capacity_dose1")]
        public int AvailableCapacityDose1 { get; set; }

        [JsonProperty("available_capacity_dose2")]
        public int AvailableCapacityDose2 { get; set; }
    }

    public class Center
    {
        [JsonProperty("center_id")]
        public int CenterId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("state_name")]
        public string StateName { get; set; }

        [JsonProperty("district_name")]
        public string DistrictName { get; set; }

        [JsonProperty("block_name")]
        public string BlockName { get; set; }

        [JsonProperty("pincode")]
        public int Pincode { get; set; }

        [JsonProperty("lat")]
        public int Lat { get; set; }

        [JsonProperty("long")]
        public int Long { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("fee_type")]
        public string FeeType { get; set; }

        [JsonProperty("sessions")]
        public List<Session> Sessions { get; set; }
    }

    public class CoWin_centers
    {
        [JsonProperty("centers")]
        public List<Center> Centers { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class SessionByPin
    {
        [JsonProperty("center_id")]
        public int CenterId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("state_name")]
        public string StateName { get; set; }

        [JsonProperty("district_name")]
        public string DistrictName { get; set; }

        [JsonProperty("block_name")]
        public string BlockName { get; set; }

        [JsonProperty("pincode")]
        public int Pincode { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("lat")]
        public int Lat { get; set; }

        [JsonProperty("long")]
        public int Long { get; set; }

        [JsonProperty("fee_type")]
        public string FeeType { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("available_capacity_dose1")]
        public int AvailableCapacityDose1 { get; set; }

        [JsonProperty("available_capacity_dose2")]
        public int AvailableCapacityDose2 { get; set; }

        [JsonProperty("available_capacity")]
        public int AvailableCapacity { get; set; }

        [JsonProperty("fee")]
        public string Fee { get; set; }

        [JsonProperty("min_age_limit")]
        public int MinAgeLimit { get; set; }

        [JsonProperty("vaccine")]
        public string Vaccine { get; set; }

        [JsonProperty("slots")]
        public List<string> Slots { get; set; }
    }

    public class CoWin_SessionByPin
    {
        [JsonProperty("sessions")]
        public List<SessionByPin> Sessions { get; set; }
    }
}
