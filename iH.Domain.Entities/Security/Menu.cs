namespace iH.Domain.Security.Entities
{
    using System;

    public class Menu
    {
        public Int64 MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public Int64? ParentId { get; set; }
        public Int16 DisplayOrder { get; set; }
    }
}
