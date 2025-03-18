namespace SharedContracts
{
    public record BookRentedEvent(int BookId, DateTime StartDate, DateTime EndDate);
}
