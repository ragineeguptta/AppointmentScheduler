﻿using AppointmentScheduler.Models.ViewModels;
using AppointmentScheduler.Services;
using AppointmentScheduler.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppointmentScheduler.Controllers.Api
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;
        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveCalenderData")]
        public IActionResult SaveCalenderData(AppointmentVM data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _appointmentService.AddUpdate(data).Result;
                if(commonResponse.status == 1)
                {
                    commonResponse.message = Helper.appointmentUpdated;
                }
                else if (commonResponse.status == 2)
                {
                    commonResponse.message = Helper.appointmentAdded;
                }
            }
            catch(Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string doctorId)
        {
            CommonResponse<List<AppointmentVM>> commonResponse = new CommonResponse<List<AppointmentVM>>();
            try
            {
                if (role == Helper.Patient)
                {
                    commonResponse.dataenum = _appointmentService.PatientsEventsById(loginUserId);
                    commonResponse.status = Helper.success_code;
                }
                else if (role == Helper.Doctor)
                {
                    commonResponse.dataenum = _appointmentService.DoctorsEventsById(loginUserId);
                    commonResponse.status = Helper.success_code;
                }
                else
                {
                    commonResponse.dataenum = _appointmentService.DoctorsEventsById(doctorId);
                    commonResponse.status = Helper.success_code;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(int id)
        {
            CommonResponse<AppointmentVM> commonResponse = new CommonResponse<AppointmentVM>();
            try
            {
                    commonResponse.dataenum = _appointmentService.GetById(id);
                    commonResponse.status = Helper.success_code;
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

    }
}
