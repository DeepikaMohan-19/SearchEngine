namespace SearchEngine.Web.Domain.Entites
{
    using System;

    public class BaseEntity()
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedByEmail { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? IsDeleted { get; set; } = false;
    }
}
