namespace Security.Entities
{
    public class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Permission Permission { get; set; }
    }
}