using System;
using System.Collections.Generic;
using System.Linq;


namespace TravelExperts.Models
{
    public static class FilterPrefix
    {
        public const string PackageId = "packageId-";
        public const string PkgName = "pkgName-";
        public const string PkgStartDate = "pkgStartDate-";
        public const string PkgEndDate = "pkgEndDate-";
        public const string PkgDesc = "pkgDesc-";
        public const string PkgBasePrice = "pkgBasePrice-";
        public const string PkgAgencyCommission = "pkgAgencyCommission-";
    }

    public class RouteDictionary : Dictionary<string, string>
    {
        public int PageNumber {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

        public int PageSize {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

        public string SortField {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }

        public string SortDirection {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }

        public void SetSortAndDirection(string fieldName, RouteDictionary current) {
            this[nameof(GridDTO.SortField)] = fieldName;

            if (current.SortField.EqualsNoCase(fieldName) && 
                current.SortDirection == "asc")
                this[nameof(GridDTO.SortDirection)] = "desc";
            else
                this[nameof(GridDTO.SortDirection)] = "asc";
        }

        public string PkgNameFilter
        {
            get => Get(nameof(PackagesGridDTO.PkgName))?.Replace(FilterPrefix.PkgName, "");
            set => this[nameof(PackagesGridDTO.PkgName)] = value;
        }

        public string PackageIdFilter
        {
            get => Get(nameof(PackagesGridDTO.PackageId))?.Replace(FilterPrefix.PackageId, "");
            set => this[nameof(PackagesGridDTO.PackageId)] = value;
        }

        public string PkgStartDateFilter
        {
            get => Get(nameof(PackagesGridDTO.PkgStartDate))?.Replace(FilterPrefix.PkgStartDate, "");
            set => this[nameof(PackagesGridDTO.PkgStartDate)] = value;
        }

        public string PkgEndDateFilter
        {
            get => Get(nameof(PackagesGridDTO.PkgEndDate))?.Replace(FilterPrefix.PkgEndDate, "");
            set => this[nameof(PackagesGridDTO.PkgEndDate)] = value;
        }

        public string PkgDescFilter
        {
            get => Get(nameof(PackagesGridDTO.PkgDesc))?.Replace(FilterPrefix.PkgDesc, "");
            set => this[nameof(PackagesGridDTO.PkgDesc)] = value;
        }

        public string PkgBasePriceFilter
        {
            get => Get(nameof(PackagesGridDTO.PkgBasePrice))?.Replace(FilterPrefix.PkgBasePrice, "");
            set => this[nameof(PackagesGridDTO.PkgBasePrice)] = value;
        }

        public string PkgAgencyCommissionFilter
        {
            get => Get(nameof(PackagesGridDTO.PkgAgencyCommission))?.Replace(FilterPrefix.PkgAgencyCommission, "");
            set => this[nameof(PackagesGridDTO.PkgAgencyCommission)] = value;
        }

        public void ClearFilters() =>
            PkgNameFilter = PackageIdFilter = PkgStartDateFilter = PkgEndDateFilter = PkgDescFilter = PkgBasePriceFilter = PkgAgencyCommissionFilter = PackagesGridDTO.DefaultFilter;

        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys) {
                clone.Add(key, this[key]);
            }
            return clone;
        }
    }
}
