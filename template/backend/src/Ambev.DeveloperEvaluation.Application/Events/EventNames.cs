namespace Ambev.DeveloperEvaluation.Application.Events
{
    /// <summary>
    /// Provides the names of the domain events published by the application.
    /// </summary>
    public static class EventNames
    {
        public const string SALE_CREATED = "SALE-CREATED";
        public const string SALE_UPDATED = "SALE-UPDATED";
        public const string SALE_CANCELLED = "SALE-CANCELLED";
        public const string SALE_ITEM_CANCELLED = "SALE-ITEM-CANCELLED";
    }
}
