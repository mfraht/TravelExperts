using System;
using System.Collections.Generic;


namespace TravelExperts.Models
{
    public class PackageDTO
    {
        public int PackageId { get; set; }
        public string PkgName { get; set; }
        public DateTime? PkgStartDate { get; set; }
        public DateTime? PkgEndDate { get; set; }
        public string PkgDesc { get; set; }
        public decimal PkgBasePrice { get; set; }
        public decimal? PkgAgencyCommission { get; set; }

        public Dictionary<int, string> Bookings { get; set; }
        public Dictionary<int, string> PackagesProductsSuppliers { get; set; }


        public void Load(Package package)
        {
            PackageId = package.PackageId;
            PkgName = package.PkgName;
            PkgStartDate = package.PkgStartDate;
            PkgEndDate = package.PkgEndDate;
            PkgDesc = package.PkgDesc;
            PkgBasePrice = package.PkgBasePrice;
            PkgAgencyCommission = package.PkgAgencyCommission;

            Bookings = new Dictionary<int, string>();
            PackagesProductsSuppliers = new Dictionary<int, string>();
            
            foreach (PackagesProductsSupplier ba in package.PackagesProductsSuppliers)
            {
                PackagesProductsSuppliers.Add(ba.Package.PackageId, ba.Package.PkgName);
            }
        }
    }
}
