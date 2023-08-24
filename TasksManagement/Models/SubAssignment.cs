using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TasksManagement.Models
{
    public class SubAssignment : Assignment
    {
        public virtual Assignment? Assignment{ get; set; }
    }
}
