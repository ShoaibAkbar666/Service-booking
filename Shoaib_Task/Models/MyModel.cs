namespace Shoaib_Task.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class ViewBookingModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Servicename { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsApproved { get; set; }
    }
    public class AddServiceModel
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
