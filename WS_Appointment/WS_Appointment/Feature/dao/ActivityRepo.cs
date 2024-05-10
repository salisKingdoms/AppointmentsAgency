﻿using Microsoft.EntityFrameworkCore;
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
    }
}
