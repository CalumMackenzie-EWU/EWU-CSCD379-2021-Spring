namespace SecretSanta.Api.Dto
{
    // cal: Dto stands for Domain Transfer Object
    public class FullUser
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}