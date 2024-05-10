using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WS_Appointment.Config;
using WS_Appointment.EFCore;
using WS_Appointment.Feature.dao;
using WS_Appointment.Feature.dto;
using WS_Appointment.Feature.Model;

namespace WS_Appointment.Feature
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : Controller
    {
        IActivityRepo _actDao;
        public ActivityController(IActivityRepo actDao)
        {
            _actDao = actDao;
        }

        #region appointments

        [HttpGet]
        [Route("GetAppointmentList")]
        public async Task<IActionResult> GetAppointmentList()
        {

            var result = new APIResultList<List<appointmentsModel>>();

            try
            {
                var data = await _actDao.GetAllAppointments();
                result.is_ok = true;
                result.message = "Success";
                result.data = data.ToList();
                result.totalRow = data.Count;
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data Not Found";
            }

            return Ok(result);

        }

        [HttpPost]
        [Route("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {

            var result = new APIResult<appointments>();
            try
            {
                if (request.customer_id == 0)
                {
                    result.is_ok = false;
                    result.message = "customer must be filled";
                    result.data = null;
                    return Ok(result);
                }

                if (request.appointment_date == null)
                {
                    result.is_ok = false;
                    result.message = "appointment date must be filled";
                    result.data = null;
                    return Ok(result);
                }

                if (string.IsNullOrEmpty(request.status))
                {
                    result.is_ok = false;
                    result.message = "status must be filled";
                    result.data = null;
                    return Ok(result);
                }

                if (string.IsNullOrEmpty(request.serviceType))
                {
                    result.is_ok = false;
                    result.message = "service description must be filled";
                    result.data = null;
                    return Ok(result);
                }
                //to create new appointment no to other days if today maximal
                int appno = 0;
                DateTime appdate_fix = request.appointment_date;

                //get all holidays date next from appointment date
                var nextHolidays = await _actDao.GetListHolidayForBook(request.appointment_date);
                foreach (var holiday in nextHolidays)
                {
                    if (appdate_fix == holiday.holiday_date)
                    {
                        appdate_fix.AddDays(1);
                    }
                }

                //get max queue for datechoosen
                var dataCountApp = await _actDao.GetTotalAppointments(appdate_fix);
                //generate app no
                if (dataCountApp.totalAppToday < dataCountApp.totalMax)
                {
                    appno = dataCountApp.totalAppToday + 1;
                }
                else
                {
                    appno += 1;
                }

                appointmentsModel objApp = new appointmentsModel();
                objApp.appointmentNo = appno.ToString();
                objApp.appointment_date = appdate_fix;
                objApp.customer_id = request.customer_id;
                objApp.status = request.status;
                objApp.serviceType = request.serviceType;
                objApp.token = CreateRandomToken();
                objApp.created_by = "sys";
                objApp.created_on = DateTime.Now.Date;
                objApp.updated_by = String.Empty;
                objApp.updated_on = null;


                await _actDao.CreateAppointments(objApp);
                result.is_ok = true;
                result.message = "Success";

            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to submit, please contact administrator";
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateAppointment")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentUpdateRequest request)
        {

            var result = new APIResult<customersModel>();

            try
            {
                if (request != null && request.id > 0)
                {
                    //get detail customer existing
                    var dataExist = await _actDao.GetAppointmentById(request.id);
                    if (dataExist == null)
                    {
                        result.is_ok = false;
                        result.message = "Appointment with id:" + request.id + " cannot find in database.";
                        return Ok(result);
                    }

                    if (request.customer_id == 0)
                    {
                        result.is_ok = false;
                        result.message = "customer must be filled";
                        result.data = null;
                        return Ok(result);
                    }

                    if (request.appointment_date == null)
                    {
                        result.is_ok = false;
                        result.message = "appointment date must be filled";
                        result.data = null;
                        return Ok(result);
                    }

                    if (string.IsNullOrEmpty(request.status))
                    {
                        result.is_ok = false;
                        result.message = "status must be filled";
                        result.data = null;
                        return Ok(result);
                    }

                    if (string.IsNullOrEmpty(request.serviceType))
                    {
                        result.is_ok = false;
                        result.message = "service description must be filled";
                        result.data = null;
                        return Ok(result);
                    }
                    //to create new appointment no to other days if today maximal
                    int appno = (request.appointment_date != dataExist.appointment_date) ? 0: int.Parse(dataExist.appointmentNo);
                    DateTime appdate_fix = (request.appointment_date!= dataExist.appointment_date) ?  request.appointment_date : dataExist.appointment_date;
                    if (request.appointment_date != dataExist.appointment_date)
                    {
                        //get all holidays date next from appointment date
                        var nextHolidays = await _actDao.GetListHolidayForBook(request.appointment_date);
                        foreach (var holiday in nextHolidays)
                        {
                            if (appdate_fix == holiday.holiday_date)
                            {
                                appdate_fix.AddDays(1);
                            }
                        }

                        //get max queue for datechoosen
                        var dataCountApp = await _actDao.GetTotalAppointments(appdate_fix);
                        //generate app no
                        if (dataCountApp.totalAppToday < dataCountApp.totalMax)
                        {
                            appno = dataCountApp.totalAppToday + 1;
                        }
                        else
                        {
                            appno += 1;
                        }
                    }

                    appointmentsModel objApp = new appointmentsModel();
                    objApp.appointmentNo = appno.ToString();
                    objApp.appointment_date = appdate_fix;
                    objApp.customer_id = request.customer_id;
                    objApp.status = request.status;
                    objApp.serviceType = request.serviceType;
                    objApp.token = dataExist.token;
                    objApp.created_by = dataExist.created_by;
                    objApp.created_on = dataExist.created_on;
                    objApp.updated_by = "sys";
                    objApp.updated_on = DateTime.Now.Date;
                    objApp.id = dataExist.id;

                    await _actDao.UpdateAppointment(objApp);
                    result.is_ok = true;
                    result.message = "Success";

                }
                else
                {
                    result.is_ok = false;
                    result.message = "payload must be filled.";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to update, please contact administrator";
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDetailAppointmentById")]
        public async Task<IActionResult> GetDetailAppointmentById(long id)
        {
            var result = new APIResult<appointmentsModel>();
            try
            {
                if (id == 0)
                {
                    result.is_ok = false;
                    result.message = "id must be filled and must be greater than 0";
                    result.data = null;
                    return Ok(result);
                }
                var data = await _actDao.GetAppointmentById(id);
                result.data = data;
                result.is_ok = true;
                result.message = "Success";
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data not found, please contact administrator";
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteAppointmentById")]
        public async Task<IActionResult> DeleteAppointmentById(long id)
        {
            var result = new APIResult<appointmentsModel>();
            try
            {
                if (id == 0)
                {
                    result.is_ok = false;
                    result.message = "id must be filled and greater than 0";
                    result.data = null;
                    return Ok(result);
                }

                await _actDao.DeleteAppointmentsById(id);
                result.is_ok = true;
                result.message = "Success";
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to delete, please contact administrator";
            }
            return Ok(result);
        }

        private string CreateRandomToken(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        #endregion

        #region customer

        [HttpGet]
        [Route("Customer/GetCustomerList")]
        public async Task<IActionResult> GetCustomerList()
        {

            var result = new APIResultList<List<customersModel>>();

            try
            {
                var data = await _actDao.GetAllCustomer();
                result.is_ok = true;
                result.message = "Success";
                result.data = data.ToList();
                result.totalRow = data.Count;
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data Not Found";
            }

            return Ok(result);

        }

        [HttpPost]
        [Route("Customer/CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequest request)
        {

            var result = new APIResult<customers>();
            try
            {
                if (string.IsNullOrEmpty(request.name))
                {
                    result.is_ok = false;
                    result.message = "name must be filled";
                    result.data = null;
                    return Ok(result);
                }

                if (string.IsNullOrEmpty(request.email))
                {
                    result.is_ok = false;
                    result.message = "email must be filled";
                    result.data = null;
                    return Ok(result);
                }

                if (string.IsNullOrEmpty(request.phone))
                {
                    result.is_ok = false;
                    result.message = "phone must be filled";
                    result.data = null;
                    return Ok(result);
                }

                await _actDao.CreateCustomer(request);
                result.is_ok = true;
                result.message = "Success";

            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to submit, please contact administrator";
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("Customer/GetDetailCustomerById")]
        public async Task<IActionResult> GetDetailCustomerById(long id)
        {
            var result = new APIResult<customersModel>();
            try
            {
                if (id == 0)
                {
                    result.is_ok = false;
                    result.message = "id must be filled and must be greater than 0";
                    result.data = null;
                    return Ok(result);
                }
                var data = await _actDao.GetCustomerById(id);
                result.data = data;
                result.is_ok = true;
                result.message = "Success";
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data not found, please contact administrator";
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Customer/UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateRequest request)
        {

            var result = new APIResult<customersModel>();

            try
            {
                if (request != null && request.id > 0)
                {
                    //get detail customer existing
                    var dataExist = await _actDao.GetCustomerById(request.id);
                    if (dataExist == null)
                    {
                        result.is_ok = false;
                        result.message = "Customer with id:" + request.id + " cannot find in database.";
                        return Ok(result);
                    }

                    //mapping data existing with request for update
                    var dataUpdate = new customersModel
                    {
                        id = request.id,
                        name = request.name,
                        email = request.email,
                        phone = request.phone,
                        address = request.address,
                        registration_date = request.registration_date,
                        created_by = dataExist.created_by,
                        created_on = dataExist.created_on,
                        updated_by = "sys",
                        updated_on = DateTime.Now.Date,
                    };
                    var update = _actDao.UpdateCustomer(dataUpdate);
                    result.is_ok = true;
                    result.message = "Success";

                }
                else
                {
                    result.is_ok = false;
                    result.message = "payload must be filled.";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to update, please contact administrator";
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("Customer/DeleteCustomerbyId")]
        public async Task<IActionResult> DeleteCustomerbyId(long id)
        {
            var result = new APIResult<customersModel>();
            try
            {
                if (id == 0)
                {
                    result.is_ok = false;
                    result.message = "id must be filled and greater than 0";
                    result.data = null;
                    return Ok(result);
                }

                await _actDao.DeleteCustomerById(id);
                result.is_ok = true;
                result.message = "Success";
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to delete, please contact administrator";
            }
            return Ok(result);
        }


        #endregion

        #region public holiday

        [HttpGet]
        [Route("PublicHolidays/GetHolidaysList")]
        public async Task<IActionResult> GetHolidaysList()
        {

            var result = new APIResultList<List<publicHolidaysModel>>();

            try
            {
                var data = await _actDao.GetAllPublicHolidays();
                result.is_ok = true;
                result.message = "Success";
                result.data = data.ToList();
                result.totalRow = data.Count;
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data Not Found";
            }

            return Ok(result);

        }

        [HttpPost]
        [Route("PublicHolidays/CreateHolidaysDate")]
        public async Task<IActionResult> CreateHolidaysDate([FromBody] PublicHolidayRequest request)
        {

            var result = new APIResult<public_holidays>();
            try
            {
                if (request.holiday_date == null)
                {
                    result.is_ok = false;
                    result.message = "holiday_date must be filled";
                    result.data = null;
                    return Ok(result);
                }

                if (string.IsNullOrEmpty(request.description))
                {
                    result.is_ok = false;
                    result.message = "description must be filled";
                    result.data = null;
                    return Ok(result);
                }

                await _actDao.CreatePublicHoliday(request);
                result.is_ok = true;
                result.message = "Success";

            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to submit, please contact administrator";
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("PublicHolidays/GetDetailHolidayById")]
        public async Task<IActionResult> GetDetailHolidayById(long id)
        {
            var result = new APIResult<publicHolidaysModel>();
            try
            {
                if (id == 0)
                {
                    result.is_ok = false;
                    result.message = "id must be filled and must be greater than 0";
                    result.data = null;
                    return Ok(result);
                }
                var data = await _actDao.GetPubHolidayById(id);
                result.data = data;
                result.is_ok = true;
                result.message = "Success";
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data not found, please contact administrator";
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("PublicHolidays/UpdateHolidaysDate")]
        public async Task<IActionResult> UpdateHolidaysDate([FromBody] PublicHolidayUpdateRequest request)
        {

            var result = new APIResult<publicHolidaysModel>();

            try
            {
                if (request != null && request.id > 0)
                {
                    //get detail customer existing
                    var dataExist = await _actDao.GetPubHolidayById(request.id);
                    if (dataExist == null)
                    {
                        result.is_ok = false;
                        result.message = "Holiday with date on:" + request.holiday_date + " cannot find in database.";
                        return Ok(result);
                    }

                    //mapping data existing with request for update
                    var dataUpdate = new publicHolidaysModel
                    {
                        id = request.id,
                        holiday_date = request.holiday_date,
                        description = request.description,
                        created_by = dataExist.created_by,
                        created_on = dataExist.created_on,
                        updated_by = "sys",
                        updated_on = DateTime.Now.Date,
                    };
                    var update = _actDao.UpdatePublicHoliday(dataUpdate);
                    result.is_ok = true;
                    result.message = "Success";

                }
                else
                {
                    result.is_ok = false;
                    result.message = "payload must be filled.";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to update, please contact administrator";
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("PublicHolidays/DeleteHolidaysById")]
        public async Task<IActionResult> DeleteHolidaysById(long id)
        {
            var result = new APIResult<publicHolidaysModel>();
            try
            {
                if (id == 0)
                {
                    result.is_ok = false;
                    result.message = "id must be filled and greater than 0";
                    result.data = null;
                    return Ok(result);
                }

                await _actDao.DeletePubHolidayById(id);
                result.is_ok = true;
                result.message = "Success";
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to delete, please contact administrator";
            }
            return Ok(result);
        }

        #endregion

        #region config
        [HttpGet]
        [Route("Configuration/GetConfigList")]
        public async Task<IActionResult> GetConfigList()
        {

            var result = new APIResultList<List<configurationModel>>();

            try
            {
                var data = await _actDao.GetAllConfig();
                result.is_ok = true;
                result.message = "Success";
                result.data = data.ToList();
                result.totalRow = data.Count;
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data Not Found";
            }

            return Ok(result);

        }

        [HttpPost]
        [Route("Configuration/CreateConfig")]
        public async Task<IActionResult> CreateConfig([FromBody] ConfigRequest request)
        {

            var result = new APIResult<configuration>();
            try
            {
                if (string.IsNullOrEmpty(request.config_value))
                {
                    result.is_ok = false;
                    result.message = "config_value must be filled";
                    result.data = null;
                    return Ok(result);
                }

                if (string.IsNullOrEmpty(request.description))
                {
                    result.is_ok = false;
                    result.message = "description must be filled";
                    result.data = null;
                    return Ok(result);
                }

                await _actDao.CreateConfig(request);
                result.is_ok = true;
                result.message = "Success";

            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to submit, please contact administrator";
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("Configuration/GetConfigById")]
        public async Task<IActionResult> GetConfigById(long id)
        {
            var result = new APIResult<configurationModel>();
            try
            {
                if (id == 0)
                {
                    result.is_ok = false;
                    result.message = "id must be filled and must be greater than 0";
                    result.data = null;
                    return Ok(result);
                }
                var data = await _actDao.GetConfigById(id);
                result.data = data;
                result.is_ok = true;
                result.message = "Success";
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data not found, please contact administrator";
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Configuration/UpdateConfig")]
        public async Task<IActionResult> UpdateConfig([FromBody] ConfigUpdateRequest request)
        {

            var result = new APIResult<configurationModel>();

            try
            {
                if (request != null && request.id > 0)
                {
                    var dataExist = await _actDao.GetConfigById(request.id);
                    if (dataExist == null)
                    {
                        result.is_ok = false;
                        result.message = "Config with value:" + request.config_value + " cannot find in database.";
                        return Ok(result);
                    }

                    //mapping data existing with request for update
                    var dataUpdate = new configurationModel
                    {
                        id = request.id,
                        config_value = request.config_value,
                        description = request.description,
                        created_by = dataExist.created_by,
                        created_on = dataExist.created_on,
                        updated_by = "sys",
                        updated_on = DateTime.Now.Date,
                    };
                    var update = _actDao.UpdateConfig(dataUpdate);
                    result.is_ok = true;
                    result.message = "Success";

                }
                else
                {
                    result.is_ok = false;
                    result.message = "payload must be filled.";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to update, please contact administrator";
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("Configuration/DeleteConfigById")]
        public async Task<IActionResult> DeleteConfigById(long id)
        {
            var result = new APIResult<configurationModel>();
            try
            {
                if (id == 0)
                {
                    result.is_ok = false;
                    result.message = "id must be filled and greater than 0";
                    result.data = null;
                    return Ok(result);
                }

                await _actDao.DeleteConfigById(id);
                result.is_ok = true;
                result.message = "Success";
            }
            catch (Exception ex)
            {
                result.is_ok = false;
                result.message = "Data failed to delete, please contact administrator";
            }
            return Ok(result);
        }

        #endregion
    }
}
