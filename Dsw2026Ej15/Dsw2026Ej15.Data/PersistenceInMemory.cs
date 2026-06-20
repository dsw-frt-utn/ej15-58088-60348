using System;
using System.Collections.Generic;
using System.Text;

using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System.Text.Json;
using System.Security;
using System.Linq; 

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence 
    {

        //Listas donde guardaremos todo "en la RAM" mientras la API corra [1].
        private readonly List<Doctor> _doctors = new();
        private readonly List<Speciality> _specialities = new(); 

        public PersistenceInMemory()
        {
            //Al arrancar, cargamos las especialidades desde el archivo [3]. 
            LoadSpecialities(); 
        }

        private void LoadSpecialities()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json"); 
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<List<Speciality>>(json);
                if (data != null) _specialities.AddRange(data); 
            }
        }

        // --- Implementación de las "promesas" de la Interfaz --- . 

        public List<Doctor> GetAllActiveDoctors() => _doctors.Where(d => d.IsActive).ToList();

        public Doctor GetActiveById(Guid id) => _doctors.FirstOrDefault(d => d.Id == id && d.IsActive); 

        public void AddDoctor(Doctor doctor)
        {
            doctor.Id = Guid.NewGuid(); //Asignamos un ID nuevo [1]. 
            doctor.IsActive = true; //se crea siempre activo [3]. 
            _doctors.Add(doctor); 
        }


        public void DeactivateDoctor(Guid id)
        {
            var doctor = GetActiveById(id);
            if (doctor != null) doctor.IsActive = false; // "Borado lógico" [5]
        }

        public List<Speciality> GetAllSpecialities() => _specialities;

        public Speciality GetSpecialityById(Guid id) => _specialities.FirstOrDefault(s => s.Id == id);   

    }
}
