namespace Shoaib_Task.Entitties
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsApproved { get; set; }
    }
}
