namespace SalesRecordManagementSystem.Dto
{
    public class BusinessDto
    {
        public BusinessDto(string id, string name, string description, string type, string email)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            Email = email;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
    }
}
