using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.DAL.Domain
{
    public class Project
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public DateTime ProjectStart { get; set; }
        public DateTime ProjectEnd { get; set; }

        public int ManagerId { get; set; }

        public Project() { }
        /// <summary>
        /// Project содержит в себе информацию об имени исполнителя, его id и id его менеджера, датах начала и конца задачи и её описании
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="managerid"></param>
        /// <param name="description"></param>
        /// <param name="projectstart"></param>
        /// <param name="projectend"></param>
        public Project(int id, string name, int managerid, string description, DateTime projectstart, DateTime projectend)
        {
            this.Id = id;
            this.Name = name;
            this.ManagerId = managerid;
            this.Description = description;
            this.ProjectStart = projectstart;
            this.ProjectEnd = projectend;
        }
    }
}
