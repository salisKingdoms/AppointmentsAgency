namespace WS_Appointment.Feature.Model
{
    public class configurationModel
    {
        public long id { get; set; }
        public string config_value { get; set; }
        public DateTime created_on { get; set; }
        public string created_by { get; set; }
        public DateTime updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
