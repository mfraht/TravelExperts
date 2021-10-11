using TravelExperts.Models;
using Microsoft.AspNetCore.Http;


namespace TravelExperts.Models
{
    public class PackagesGridBuilder : GridBuilder
    {
        public PackagesGridBuilder(ISession sess) : base(sess) { }
        public PackagesGridBuilder(ISession sess, PackagesGridDTO values, 
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.PkgName.IndexOf(FilterPrefix.PkgName) == -1;

            routes.PkgNameFilter = (isInitial) ? FilterPrefix.PkgName + values.PkgName : values.PkgName;
            routes.PackageIdFilter = (isInitial) ? FilterPrefix.PackageId + values.PackageId : values.PackageId;
            routes.PkgStartDateFilter = (isInitial) ? FilterPrefix.PkgStartDate + values.PkgStartDate : values.PkgStartDate;
            routes.PkgEndDateFilter = (isInitial) ? FilterPrefix.PkgEndDate + values.PkgEndDate : values.PkgEndDate;
            routes.PkgDescFilter = (isInitial) ? FilterPrefix.PkgDesc + values.PkgDesc : values.PkgDesc;
            routes.PkgBasePriceFilter = (isInitial) ? FilterPrefix.PkgBasePrice + values.PkgBasePrice : values.PkgBasePrice;
            routes.PkgAgencyCommissionFilter = (isInitial) ? FilterPrefix.PkgAgencyCommission + values.PkgAgencyCommission : values.PkgAgencyCommission;
        }

        public void ClearFilterSegments() => routes.ClearFilters();

        string def = PackagesGridDTO.DefaultFilter;   
        public bool IsFilterByPkgName => routes.PkgNameFilter != def;
        public bool IsFilterByPackageId => routes.PackageIdFilter != def;
        public bool IsFilterByPkgStartDate => routes.PkgStartDateFilter != def;
        public bool IsFilterByPkgEndDate => routes.PkgEndDateFilter != def;
        public bool IsFilterByPkgDesc => routes.PkgDescFilter != def;
        public bool IsFilterByPkgBasePrice => routes.PkgBasePriceFilter != def;
        public bool IsFilterBykgAgencyCommission => routes.PkgAgencyCommissionFilter != def;

        public bool IsSortByGenre =>
            routes.SortField.EqualsNoCase(nameof(Package.PkgName));
        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Package.PkgBasePrice));
    }
}
