
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc; 
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces; 
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase 
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }


        [HttpPost]
        public IActionResult CreateDoctor([FromBody] DoctorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ValidationException("Name requerido");
            if (string.IsNullOrWhiteSpace(dto.LicenseNumber))
                throw new ValidationException("LicenseNumber requerido");

            var speciality = _persistence.GetSpecialityById(dto.SpecialityId);
            if (speciality == null)
                throw new ValidationException("SpecialityId no existe");

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LicenseNumber = dto.LicenseNumber,
                Speciality = speciality,
                IsActive = true
            };

            _persistence.AddDoctor(doctor);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }



        [HttpGet]
        public IActionResult GetActiveDoctors()
        {
            var doctors = _persistence.GetAllActiveDoctors(); // coincide con tu interfaz
            return Ok(doctors);
        }



        [HttpGet("{id}")]
        public IActionResult GetDoctorById(Guid id)
        {
            var doctor = _persistence.GetActiveById(id); // coincide con tu interfaz
            if (doctor == null)
                return NotFound();

            return Ok(new
            {
                doctor.Name,
                doctor.LicenseNumber,
                SpecialityName = doctor.Speciality.Name
            });
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(Guid id)
        {
            var doctor = _persistence.GetActiveById(id);
            if (doctor == null)
                return NotFound();

            _persistence.DeactivateDoctor(id); // coincide con tu interfaz
            return NoContent();
        }


    }

    public class DoctorDto
    {
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public Guid SpecialityId { get; set; }
    }

}
