namespace EVM.Data.Models.PaymentsFeature;

public static class Consts
{
    public static class SubscriptionStatus
    {
        public static readonly string Active = "active";
        public static readonly string All = "all";
        public static readonly string Incomplete = "incomplete";
    }

    public static class SubscriptionExpand
    {
        public static readonly string LatestInvoicePaymentIntent = "latest_invoice.payment_intent";
        public static readonly string DataItems = "data.items";
    }

    public static class PaymentBehavior
    {
        public static readonly string DefaultIncomplete = "default_incomplete";
    }

    public static class ProductExpand
    {
        public static readonly string DefaultPrice = "default_price";
    }

    public static class CustomerExpand
    {
        public static readonly string SubscriptionsDataItems = "subscriptions.data.items";
    }
}
