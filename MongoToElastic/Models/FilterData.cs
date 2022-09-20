using MongoToElastic.Models.Enums;

namespace MongoToElastic.Models
{
    public class FilterData
    {
        public FilterData()
        {
        }

        public FilterData(int month, int year, int count)
        {
            Month = month;
            Year = year;
            Count = count;
            StartDate = new DateTime(Year, Month, 1);
            EndDate = StartDate.AddMonths(1);
        }

        public int Month { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    /// <summary>
    /// Model for StructureCatalogModel
    /// </summary>
    public class ModifiedOrAddedSSCM
    {
        public bool Response { get; set; }
        public OperationType typeOfOperation { get; set; }
        /// <summary>
        /// New StructureCatalogModel
        /// </summary>
        public SolutionStructureCatalog NewSSCModel { get; set; }
        /// <summary>
        /// old StructureCatalogModel
        /// </summary>
        public SolutionStructureCatalog OldSSCModel { get; set; }
    }

    /// <summary>
    /// Model for StructureCatalogModel
    /// </summary>
    public class GenericModifiedOrAddedModel<T>
    {
        public bool Response { get; set; }
        public OperationType typeOfOperation { get; set; }
        /// <summary>
        /// New StructureCatalogModel
        /// </summary>
        public T NewSSCModel { get; set; }
        /// <summary>
        /// old StructureCatalogModel
        /// </summary>
        public T OldSSCModel { get; set; }
    }
}
