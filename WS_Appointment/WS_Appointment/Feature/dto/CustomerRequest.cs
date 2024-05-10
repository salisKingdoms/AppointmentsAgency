namespace WS_Appointment.Feature.dto
{
    public class CustomerRequest
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public DateTime? registration_date { get; set; }
    }

    public class CustomerUpdateRequest
    {
        public long id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public DateTime? registration_date { get; set; }
    }
}
