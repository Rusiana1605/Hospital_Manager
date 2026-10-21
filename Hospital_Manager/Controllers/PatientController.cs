using Data_Hospital_Manager;
using Data_Hospital_Manager.Entities;
using Hospital_Manager.Core.Interfaces;
using Hospital_Manager.ViewModels.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace Hospital_Manager.Controllers
{
    public class PatientController : Controller
    {
        private readonly IDoctorService doctorserv;
        private readonly IPatientService patientserv;
        public PatientController(IDoctorService doctorserv, IPatientService patientserv)
        {
            this.doctorserv = doctorserv;
            this.patientserv = patientserv;
        }
        // get post - delete edit create
        public async Task<IActionResult> Index()
        {
            var patients = await patientserv.GetAllPatientsAsync();
            var model = new List<PatientIndexViewModel>();
            foreach (var patient in patients)
            {
                model.Add(new PatientIndexViewModel
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Email = patient.Email,
                    PhoneNumber = patient.PhoneNumber,
                    DoctorName = string.Join(", ", patient.DoctorPatients.Select(dp => dp.Doctor.FirstName + " " + dp.Doctor.LastName))
                });
            }
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            await LoadDoctors();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PatientCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
               foreach (var doctorId in model.DoctorIds)
                {
                    patient.DoctorPatients.Add(new DoctorPatient
                    {
                        DoctorId = doctorId,
                    });
                }
                await patientserv.AddPatientAsync(patient);
                return RedirectToAction(nameof(Index));
            }
            await LoadDoctors(model.DoctorIds);
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var patient = await patientserv.GetPatientWithDoctorsAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            var model = new PatientDetailsViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                DoctorName = string.Join(", ", patient.DoctorPatients.Select(dp => dp.Doctor.FirstName + " " + dp.Doctor.LastName))
            };
            return View(model);
        }
        public async Task LoadDoctors(List<int> selectedDoctors = null) // Helper method to load doctors for the multi-select list.  
        {
            var doctors = await doctorserv.GetAllDoctorsAsync();
            var dd =  doctors.Select(d => new
            {
                d.Id,
                Name = d.FirstName + " " + d.LastName
            }).ToList();
            ViewBag.Doctors = new MultiSelectList(dd, "Id", "Name", selectedDoctors);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await patientserv.GetPatientWithDoctorsAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            var model = new PatientEditViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                DoctorIds = patient.DoctorPatients.Select(dp => dp.DoctorId).ToList()
            };
            await LoadDoctors();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PatientEditViewModel model)
        {
            if(id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var patient = await patientserv.GetPatientWithDoctorsAsync(id);
                if (patient == null)
                {
                    return NotFound();
                }
                patient.FirstName = model.FirstName;
                patient.LastName = model.LastName;
                patient.Email = model.Email;
                patient.PhoneNumber = model.PhoneNumber;
                // Update doctor associations
                patient.DoctorPatients.Clear();
                foreach (var doctorId in model.DoctorIds)
                {
                    patient.DoctorPatients.Add(new DoctorPatient
                    {
                        DoctorId = doctorId,
                        PatientId = patient.Id
                    });
                }
                await patientserv.UpdatePatientAsync(patient, model.DoctorIds);
                return RedirectToAction(nameof(Index));
            }
            await LoadDoctors(model.DoctorIds);
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await patientserv.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            await patientserv.DeletePatientAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await patientserv.GetPatientByIdAsync(id);
            if (patient != null)
            {
                await patientserv.DeletePatientAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
