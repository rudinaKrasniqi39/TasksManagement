namespace TasksManagement.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsCompleted{ get; set; }
        public DateTime DateCreated{ get; set; }
        public bool IsDeleted{ get; set; }
        public List<SubAssignment> SubAssignments { get; set; }
        public virtual Catalog? Catalog { get; set; }
    }
}
