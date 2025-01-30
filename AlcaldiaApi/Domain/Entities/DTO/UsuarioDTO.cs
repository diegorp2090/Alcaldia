namespace AlcaldiaApi.Domain.Entities.DTO
{
    public class UsuarioDTO : LoginDTO
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string FechaNacimiento { get; set; }
        public string UserName { get; set; }

    }
}
