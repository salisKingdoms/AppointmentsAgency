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
    }
}
