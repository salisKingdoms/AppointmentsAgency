using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WS_Appointment.Feature;
using WS_Appointment.Feature.dao;
using WS_Appointment.Feature.dto;
using WS_Appointment.Feature.Model;

namespace WS_AppointmentTest
{
    public class ActivityTest
    {
        public ActivityController controller;
        private Mock<IActivityRepo> actDao;

        private int expected = 200;
        private string dummyT = "test";
        private string userNIP = "12345";

        public ActivityTest()
        {
            actDao = new Mock<IActivityRepo>();
            actDao.CallBase = true;
            controller = new ActivityController(actDao.Object);
        }

        [Fact]
        public async void GetAppointmentList()
        {
            var res = (OkObjectResult)controller.GetAppointmentList().Result;
            Assert.Equal(expected, res.StatusCode);
        }

        [Fact]
        public async void GetHolidaysList()
        {
            var res = (OkObjectResult)controller.GetHolidaysList().Result;
            Assert.Equal(expected, res.StatusCode);
        }

        [Fact]
        public async void GetConfigList()
        {
            var res = (OkObjectResult)controller.GetConfigList().Result;
            Assert.Equal(expected, res.StatusCode);
        }

        [Fact]
        public async void GetCustomerList()
        {
            var res = (OkObjectResult)controller.GetCustomerList().Result;
            Assert.Equal(expected, res.StatusCode);
        }
    }
}
