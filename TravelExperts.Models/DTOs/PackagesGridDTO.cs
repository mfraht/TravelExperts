using Newtonsoft.Json;


namespace TravelExperts.Models
{
    public class PackagesGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string PackageId { get; set; } = DefaultFilter;
        public string PkgName { get; set; } = DefaultFilter;
        public string PkgStartDate { get; set; } = DefaultFilter;
        public string PkgEndDate { get; set; } = DefaultFilter;
        public string PkgDesc { get; set; } = DefaultFilter;
        public string PkgBasePrice { get; set; } = DefaultFilter;
        public string PkgAgencyCommission { get; set; } = DefaultFilter;

    }
}
