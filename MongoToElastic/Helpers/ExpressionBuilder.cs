using MongoToElastic.Models;
using Nest;
using System.Linq.Expressions;

namespace MongoToElastic.Helpers
{
    public static class ExpressionBuilder
    {
        public static Func<QueryContainerDescriptor<SolutionStructureCatalog>, QueryContainer> SolutionCatalogFilterQuery(string columnName, FilterData pagingData)
        {
            return q => q.Bool(b => b.Filter(f => f.DateRange(dr => dr.Field(columnName).GreaterThanOrEquals(pagingData.StartDate)),
              f => f.DateRange(dr => dr.Field(columnName).LessThanOrEquals(pagingData.EndDate)))
                  );
        }

        public static Func<QueryContainerDescriptor<SolutionStructureCatalog>, QueryContainer> UpdateQueryFilter(SolutionStructureCatalog OldSSCModel)
        {
            return p => p.Bool(dr => dr.Must(q => q.Match(c => c.Field(p => p.SolutionStructureId).Query(OldSSCModel.SolutionStructureId))
               && q.Match(c => c.Field(p => p.CustomerContext.CountryCode).Query(OldSSCModel.CustomerContext.CountryCode))
               && q.Match(c => c.Field(p => p.CustomerContext.CustomerSetId).Query(OldSSCModel.CustomerContext.CustomerSetId))
            && q.Match(c => c.Field(p => p.CustomerContext.LanguageCode).Query(OldSSCModel.CustomerContext.LanguageCode))
            && q.Match(c => c.Field(p => p.CustomerContext.Region).Query(OldSSCModel.CustomerContext.Region))));
        }

        public static Expression<Func<SolutionStructureCatalog, DateTime>> BuildExpressionForUpdate(string columnName)
        {
            if (columnName.ToLower() == "modifieddate")
            {
                return x => x.ModifiedDate;
            }
            else
            {
                return x => x.CreatedDate;
            }
        }

        public static Func<SortDescriptor<SolutionStructureCatalog>, IPromise<IList<ISort>>> SortByForSSCS()
        {
            return x => x.Ascending(x => x.ModifiedDate);
        }

        public static Func<Group, string> GenerateKeyForGroups()
        {
            return x => string.Format("{0}|{1}|{2}", x.Description, x.Id, x.Name);
        }

        public static Func<OrderCodesToRoleGroups, string> GenerateKeyForOrderCodesToRoleGroups()
        {
            return x => string.Format("{0}|{1}|{2}", x.OrderCodeId, x.Region, Convert.ToString(x.State));
        }

        public static Func<OrderCodesToRoleGroups, OrderCodesToRoleGroups, bool> CompareOrderCodesToRoleGroups()
        {
            return (primaryData, secondaryData) =>
            {
                bool result = true;
                foreach (var item in primaryData.RoleGroups)
                {
                    if (!secondaryData.RoleGroups.Any(x => x == item))
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            };
        }
        public static Func<RightGroup, string> GenerateKeyForRightGroup()
        {
            return x => string.Format("{0}|{1}", x.Id, x.Name);
        }
        public static Func<RightGroup, RightGroup, bool> CompareRightGroups()
        {
            return (primaryData, secondaryData) =>
            {
                bool result = true;
                foreach (var item in primaryData.Groups)
                {
                    if (!secondaryData.Groups.Any(x => x == item))
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            };
        }
        public static Func<RoleGroup, string> GenerateKeyForRoleGroup()
        {
            return x => string.Format("{0}|{1}|{2}|{3}", x.Name, x.Region, x.RoleId, x.Description);
        }

        public static Func<VariantToGroups, string> GenerateKeyForVariantToGroups()
        {
            return x => string.Format("{0}", x.Id);
        }

        public static Func<VariantToGroups, VariantToGroups, bool> CompareVariantToGroups()
        {
            return (primaryData, secondaryData) =>
            {
                bool result = true;
                foreach (var item in primaryData.Groups)
                {
                    if (!secondaryData.Groups.Any(x => x == item))
                    {
                        result = false;
                        break;
                    }
                }
                if (primaryData.PageNumber != secondaryData.PageNumber || primaryData.PageSize != secondaryData.PageSize || primaryData.Region != secondaryData.Region || primaryData.State != secondaryData.State)
                {
                    result = false;
                }
                return result;
            };
        }

        public static Func<GiiProductAccessAdminOrderCode, string> GenerateKeyForGiiProductAccessAdminOrderCode()
        {
            return x => string.Format("{0}|{1}", x.OrderCodeId, x.Region);
        }

        public static Func<CatalogSource, string> GenerateKeyForCatalogSource()
        {
            return x => string.Format("{0}", x.Id);
        }

        public static Func<CatalogSource, CatalogSource, bool> CompareCatalogSources()
        {
            return (primaryData, secondaryData) =>
            {
                bool result = true;
                if (primaryData.Language != secondaryData.Language
                || primaryData.Region != secondaryData.Region
                || primaryData.Segment != secondaryData.Segment
                || primaryData.CustomerSet != secondaryData.CustomerSet
                || primaryData.Country != secondaryData.Country
                || primaryData.CurrencyCode != secondaryData.CurrencyCode
                || primaryData.CustomerSetMode != secondaryData.CustomerSetMode
                || primaryData.CustomerSetModeTest != secondaryData.CustomerSetModeTest)
                {
                    result = false;
                }
                return result;
            };
        }

        public static Func<SolutionStructureCatalog, string> GenerateKeyForSolutionStructureCatalogModel()
        {
            return x => string.Format("{0}|{1}|{2}|{3}|{4}|{5}", Convert.ToString(x.CreatedDate.Ticks), x.CustomerContext.CountryCode, x.CustomerContext.CustomerSetId, x.CustomerContext.LanguageCode, x.CustomerContext.Region, x.SolutionStructureId);
        }

    }
}
