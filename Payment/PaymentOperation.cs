namespace Payment
{
    public class PaymentOperation
    {
        public Guid Id { get; set; }
        public PaymentState State { get; set; }
        public DateTimeOffset? DateCreation { get; set; }
        public DateTimeOffset? DateConfirmation { get; set; }
        public DateTimeOffset? DateCancellation { get; set; }
    }
}
