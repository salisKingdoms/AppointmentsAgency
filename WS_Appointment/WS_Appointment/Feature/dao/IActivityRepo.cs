using WS_Appointment.Feature.dto;
using WS_Appointment.Feature.Model;

namespace WS_Appointment.Feature.dao
{
    public interface IActivityRepo
    {
        #region customer
        public Task<List<customersModel>> GetAllCustomer();
        public Task CreateCustomer(CustomerRequest request);
        public Task<customersModel> GetCustomerById(long id);
        public Task UpdateCustomer(customersModel request);
        public Task<bool> DeleteCustomerById(long id);

        #endregion

        #region public holiday
        public Task<List<publicHolidaysModel>> GetAllPublicHolidays();
        public Task CreatePublicHoliday(PublicHolidayRequest request);
        public Task<publicHolidaysModel> GetPubHolidayById(long id);
        public Task UpdatePublicHoliday(publicHolidaysModel request);
        public Task<bool> DeletePubHolidayById(long id);
        #endregion

        #region config
        public Task<List<configurationModel>> GetAllConfig();
        public Task CreateConfig(ConfigRequest request);
        public Task<configurationModel> GetConfigById(long id);
        public Task UpdateConfig(configurationModel request);
        public Task<bool> DeleteConfigById(long id);
        #endregion

        #region appointments
        public Task<List<appointmentsModel>> GetAllAppointments();
        public Task CreateAppointments(appointmentsModel request);
        public Task<appointmentsModel> GetAppointmentById(long id);
        public Task UpdateAppointment(appointmentsModel request);
        public Task<bool> DeleteAppointmentsById(long id);
        public Task<AppointmentCountMax> GetTotalAppointments(DateTime appointment_date);
        public Task<List<AppointmentHolidayDate>> GetListHolidayForBook(DateTime appointment_date);
        #endregion
    }
}
