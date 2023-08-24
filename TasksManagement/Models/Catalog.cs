namespace TasksManagement.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Assignment> Assignemnt{ get; set; }
    }
}
