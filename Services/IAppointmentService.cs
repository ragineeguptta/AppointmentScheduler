using AppointmentScheduler.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Services
{
    public interface IAppointmentService
    {
        public List<DoctorVM> GetDoctorList();
        public List<PatientVM> GetPatientList();
        public Task<int> AddUpdate(AppointmentVM modal);

        public List<AppointmentVM> DoctorsEventsById(string doctorId);
        public List<AppointmentVM> PatientsEventsById(string patientId);

        public AppointmentVM GetById(int id);

        public Task<int> Delete(int id);
        public Task<int> ConfirmEvent(int id);

    }
}
