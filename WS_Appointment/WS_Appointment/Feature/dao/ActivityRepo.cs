using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WS_Appointment.EFCore;
using WS_Appointment.Feature.dto;
using WS_Appointment.Feature.Model;

namespace WS_Appointment.Feature.dao
{

    public class ActivityRepo : IActivityRepo
    {
        private EF_DataContext _context;
        public ActivityRepo(EF_DataContext context)
        {
            _context = context;

        }

        public async Task<List<customersModel>> GetAllCustomer()
        {
            List<customersModel> result = new List<customersModel>();
            var data = _context.Customers.ToList();
            var customerList = data.Select(c => new customersModel
            {
                id = c.id,
                name = c.name,
                email = c.email,
                phone = c.phone,
                address = c.address,
                registration_date = c.registration_date,
                created_by = c.created_by,
                created_on = c.created_on,
                updated_by = c.updated_by,
                updated_on = c.updated_on != null ? c.updated_on.Value : null,
            }).ToList();
            result = customerList;

            return result;
        }

        public async Task CreateCustomer(CustomerRequest request)
        {
            customers dataSubmit = new customers();
            try
            {
                dataSubmit.name = request.name;
                dataSubmit.email = request.email;
                dataSubmit.phone = request.phone;
                dataSubmit.address = request.address;
                dataSubmit.registration_date = request.registration_date.Value;
                dataSubmit.created_by = "sys";
                dataSubmit.created_on = DateTime.Now.Date;
                dataSubmit.updated_on = null;
                dataSubmit.updated_by = string.Empty;
                dataSubmit.id = 0;
                var save = _context.Customers.Add(dataSubmit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public async Task<customersModel> GetCustomerById(long id)
        {
            customersModel result = new customersModel();
            try
            {
                var data = await _context.Customers.AsNoTracking().FirstOrDefaultAsync();
                result.id = data.id;
                result.name = data.name;
                result.email = data.email;
                result.phone = data.phone;
                result.address = data.address;
                result.registration_date = data.registration_date;
                result.created_by = data.created_by;
                result.created_on = data.created_on;
                result.updated_on = data.updated_on;
                result.updated_by = data.updated_by;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }

        public async Task UpdateCustomer(customersModel request)
        {
            customers dataSubmit = new customers();
            try
            {
                dataSubmit.name = request.name;
                dataSubmit.email = request.email;
                dataSubmit.phone = request.phone;
                dataSubmit.address = request.address;
                dataSubmit.registration_date = request.registration_date.Value;
                dataSubmit.created_by = request.created_by;
                dataSubmit.created_on = request.created_on;
                dataSubmit.updated_on = request.updated_on;
                dataSubmit.updated_by = request.updated_by;
                dataSubmit.id = request.id;
                var save = _context.Customers.Update(dataSubmit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public async Task<bool> DeleteCustomerById(long id)
        {
            bool result = false;
            try
            {
                var existing = _context.Customers.Where(d => d.id.Equals(id)).FirstOrDefault();
                if (existing != null)
                {
                    _context.Customers.Remove(existing);
                    _context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }
            return result;
        }

        public async Task<List<publicHolidaysModel>> GetAllPublicHolidays()
        {
            List<publicHolidaysModel> result = new List<publicHolidaysModel>();
            var data = _context.Public_Holidays.ToList();
            var dataList = data.Select(c => new publicHolidaysModel
            {
                id = c.id,
                holiday_date = c.holiday_date,
                description = c.description,
                created_by = c.created_by,
                created_on = c.created_on != null ? c.created_on.Value : null,
                updated_by = c.updated_by,
                updated_on = c.updated_on != null ? c.updated_on.Value : null,
            }).ToList();
            result = dataList;

            return result;
        }

        public async Task CreatePublicHoliday(PublicHolidayRequest request)
        {
            public_holidays dataSubmit = new public_holidays();
            try
            {
                dataSubmit.holiday_date = request.holiday_date;
                dataSubmit.description = request.description;
                dataSubmit.created_by = "sys";
                dataSubmit.created_on = DateTime.Now.Date;
                dataSubmit.updated_on = null;
                dataSubmit.updated_by = string.Empty;
                dataSubmit.id = 0;
                var save = _context.Public_Holidays.Add(dataSubmit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public async Task<publicHolidaysModel> GetPubHolidayById(long id)
        {
            publicHolidaysModel result = new publicHolidaysModel();
            try
            {
                var data = await _context.Public_Holidays.AsNoTracking().FirstOrDefaultAsync();
                result.id = data.id;
                result.holiday_date = data.holiday_date;
                result.description = data.description;
                result.created_by = data.created_by;
                result.created_on = data.created_on;
                result.updated_on = data.updated_on;
                result.updated_by = data.updated_by;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }

        public async Task UpdatePublicHoliday(publicHolidaysModel request)
        {
            public_holidays dataSubmit = new public_holidays();
            try
            {
                dataSubmit.holiday_date = request.holiday_date;
                dataSubmit.description = request.description;
                dataSubmit.created_by = request.created_by;
                dataSubmit.created_on = request.created_on;
                dataSubmit.updated_on = request.updated_on;
                dataSubmit.updated_by = request.updated_by;
                dataSubmit.id = request.id;
                var save = _context.Public_Holidays.Update(dataSubmit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public async Task<bool> DeletePubHolidayById(long id)
        {
            bool result = false;
            try
            {
                var existing = _context.Public_Holidays.Where(d => d.id.Equals(id)).FirstOrDefault();
                if (existing != null)
                {
                    _context.Public_Holidays.Remove(existing);
                    _context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }
            return result;
        }

        public async Task<List<configurationModel>> GetAllConfig()
        {
            List<configurationModel> result = new List<configurationModel>();
            var data = _context.Configurations.ToList();
            var dataList = data.Select(c => new configurationModel
            {
                id = c.id,
                config_value = c.config_value,
                created_by = c.created_by,
                created_on = c.created_on != null ? c.created_on.Value : null,
                updated_by = c.updated_by,
                updated_on = c.updated_on != null ? c.updated_on.Value : null,
            }).ToList();
            result = dataList;

            return result;
        }

        public async Task CreateConfig(ConfigRequest request)
        {
            configuration dataSubmit = new configuration();
            try
            {
                dataSubmit.config_value = request.config_value;
                dataSubmit.description = request.description;
                dataSubmit.created_by = "sys";
                dataSubmit.created_on = DateTime.Now.Date;
                dataSubmit.updated_on = null;
                dataSubmit.updated_by = string.Empty;
                dataSubmit.id = 0;
                var save = _context.Configurations.Add(dataSubmit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public async Task<configurationModel> GetConfigById(long id)
        {
            configurationModel result = new configurationModel();
            try
            {
                var data = await _context.Configurations.AsNoTracking().FirstOrDefaultAsync();
                result.id = data.id;
                result.config_value = data.config_value;
                result.description = data.description;
                result.created_by = data.created_by;
                result.created_on = data.created_on;
                result.updated_on = data.updated_on;
                result.updated_by = data.updated_by;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }

        public async Task UpdateConfig(configurationModel request)
        {
            configuration dataSubmit = new configuration();
            try
            {
                dataSubmit.config_value = request.config_value;
                dataSubmit.description = request.description;
                dataSubmit.created_by = request.created_by;
                dataSubmit.created_on = request.created_on;
                dataSubmit.updated_on = request.updated_on;
                dataSubmit.updated_by = request.updated_by;
                dataSubmit.id = request.id;
                var save = _context.Configurations.Update(dataSubmit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public async Task<bool> DeleteConfigById(long id)
        {
            bool result = false;
            try
            {
                var existing = _context.Configurations.Where(d => d.id.Equals(id)).FirstOrDefault();
                if (existing != null)
                {
                    _context.Configurations.Remove(existing);
                    _context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }
            return result;
        }

        public async Task<List<appointmentsModel>> GetAllAppointments()
        {
            List<appointmentsModel> result = new List<appointmentsModel>();
            var data = _context.Appointments.ToList();
            var dataList = data.Select(c => new appointmentsModel
            {
                id = c.id,
                customer_id = c.customer_id,
                appointment_date = c.appointment_date,
                status = c.status,
                serviceType = c.serviceType,
                token = c.token,
                appointmentNo = c.appointmentNo,
                created_by = c.created_by,
                created_on = c.created_on != null ? c.created_on.Value : null,
                updated_by = c.updated_by,
                updated_on = c.updated_on != null ? c.updated_on.Value : null,
            }).ToList();
            result = dataList;

            return result;
        }

        public async Task CreateAppointments(appointmentsModel request)
        {
            appointments dataSubmit = new appointments();
            try
            {
                dataSubmit.customer_id = request.customer_id;
                dataSubmit.appointment_date = request.appointment_date;
                dataSubmit.status = request.status;
                dataSubmit.serviceType = request.serviceType;
                dataSubmit.token = request.token;
                dataSubmit.appointmentNo = request.appointmentNo;
                dataSubmit.created_by = "sys";
                dataSubmit.created_on = DateTime.Now.Date;
                dataSubmit.updated_on = null;
                dataSubmit.updated_by = string.Empty;
                dataSubmit.id = 0;
                var save = _context.Appointments.Add(dataSubmit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public async Task<appointmentsModel> GetAppointmentById(long id)
        {
            appointmentsModel result = new appointmentsModel();
            try
            {
                var data = await _context.Appointments.AsNoTracking().FirstOrDefaultAsync();
                result.id = data.id;
                result.customer_id = data.customer_id;
                result.appointment_date = data.appointment_date;
                result.status = data.status;
                result.serviceType = data.serviceType;
                result.token = data.token;
                result.appointmentNo = data.appointmentNo;
                result.created_by = data.created_by;
                result.created_on = data.created_on;
                result.updated_on = data.updated_on;
                result.updated_by = data.updated_by;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }

        public async Task UpdateAppointment(appointmentsModel request)
        {
            appointments dataSubmit = new appointments();
            try
            {
                dataSubmit.customer_id = request.customer_id;
                dataSubmit.appointment_date = request.appointment_date;
                dataSubmit.status = request.status;
                dataSubmit.serviceType = request.serviceType;
                dataSubmit.token = request.token;
                dataSubmit.appointmentNo = request.appointmentNo;
                dataSubmit.created_by = request.created_by;
                dataSubmit.created_on = request.created_on;
                dataSubmit.updated_on = request.updated_on;
                dataSubmit.updated_by = request.updated_by;
                dataSubmit.id = request.id;
                var save = _context.Appointments.Update(dataSubmit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public async Task<bool> DeleteAppointmentsById(long id)
        {
            bool result = false;
            try
            {
                var existing = _context.Appointments.Where(d => d.id.Equals(id)).FirstOrDefault();
                if (existing != null)
                {
                    _context.Appointments.Remove(existing);
                    _context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }
            return result;
        }

        public async Task<AppointmentCountMax> GetTotalAppointments(DateTime appointment_date)
        {
            AppointmentCountMax result = new AppointmentCountMax();
            var appToday = _context.Appointments.Where(x => x.appointment_date == appointment_date).ToList();
            var maxConfig = _context.Configurations.FirstOrDefault();
            var dataMap = new AppointmentCountMax
            {
                totalAppToday = appToday.Count,
                totalMax = int.Parse(maxConfig.config_value)
            };
            result = dataMap;

            return result;
        }

        public async Task<List<AppointmentHolidayDate>> GetListHolidayForBook(DateTime appointment_date)
        {
            List<AppointmentHolidayDate> result = new List<AppointmentHolidayDate>();
            try
            {
                var nextHoliday = _context.Public_Holidays.Where(x => x.holiday_date >= appointment_date).OrderBy(x => x.holiday_date).ToList();
                foreach (var holiday in nextHoliday)
                {
                    var dataHoliday = new AppointmentHolidayDate
                    {
                        holiday_date = holiday.holiday_date
                    };
                    result.Add(dataHoliday);
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                result = null;
            }
           

            return result;
        }
    }
}
