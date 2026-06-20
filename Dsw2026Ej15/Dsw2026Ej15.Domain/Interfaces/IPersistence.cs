using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {

        //Promesa 1: Podremos obtener todos los médicos activos [4]
        List<Doctor> GetAllActiveDoctors();

        //Promesa 2: Podremos buscar un médico activo por su ID [4]
        Doctor GetActiveById(Guid id);

        //Promesa 3: Podremos guardar un nuevo médico [5]
        void AddDoctor(Doctor doctor);

        //Promesa 4: Podremos dar de baja (desactivar) a un médico [6]
        void DeactivateDoctor(Guid id);

        //Promesa 5: Podremos ver las especialidades (necesario para validar) [1, 5]
        List<Speciality> GetAllSpecialities();

        //Promesa 6: Podremos buscar una especialidad por ID para saber si existe [5]
        Speciality GetSpecialityById(Guid id); 
    }
}
