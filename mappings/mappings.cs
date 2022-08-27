using apiWebFlutter.Models;

namespace apiWebFlutter.mappings
{
        public static partial class Mapper
        {
            public static plantaDTO ToDTO(this Plantum model)  // Convierte de ModelContext a DTO
            {
                return new plantaDTO()
                {
                    Id = model.Id,
                    RutaImagen = model.RutaImagen,
                    Descripcion = model.Descripcion,
                    EstadoPlanta = model.EstadoPlanta,
                    Recomendacion = model.Recomendacion,
                    IdUsers = model.IdUsers,

                };
            }
        }

        public static partial class Mapper
        {
            public static Plantum ToDatabase(this plantaDTO dto)  // Convierte de DTO a ModelContext
            {
                return new Plantum()
                {
                    Id = dto.Id,
                    RutaImagen = dto.RutaImagen,
                    Descripcion = dto.Descripcion,
                    EstadoPlanta = dto.EstadoPlanta,
                    Recomendacion = dto.Recomendacion,
                    IdUsers = dto.IdUsers,

                };
            }
        }
}
