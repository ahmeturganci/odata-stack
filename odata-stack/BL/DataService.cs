using odata_stack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace odata_stack.BL
{
    public class DataService
    {
        public List<Project> Projects
        {
            get { return m_Projects; }
        }

        public Project Find(int id)
        {
            return Projects.Where(m => m.Id == id).FirstOrDefault();
        }
       
        public Project Save(Project project)
        {
            if (project == null) throw new ArgumentNullException("project");

            Project projectInstance = Projects.Where(m => m.Id == project.Id).FirstOrDefault();

            if (projectInstance == null) throw new ArgumentException(string.Format("Did not find movie with Id: {0}", project.Id));

            lock (_lock)
            {
                return projectInstance.CopyFrom(project);
            }
        }

        public Project Add(Project Project)
        {
            if (Project == null)
                throw new ArgumentNullException("Project cannot be null");

            if (string.IsNullOrEmpty(Project.Name))
                throw new ArgumentException("Project must have a title");

            if (m_Projects.Exists(m => m.Name == Project.Summary))
                throw new InvalidOperationException("Project already present in catalog");

            lock (_lock)
            {
                Project.Id = m_Projects.Max(m => m.Id) + 1;
                m_Projects.Add(Project);
            }

            return Project;
        }

        public bool Remove(int id)
        {
            int index = -1;

            for (int n = 0; n < Projects.Count && index == -1; n++) if (Projects[n].Id == id) index = n;

            bool result = false;

            if (index != -1)
            {
                lock (_lock)
                {
                    Projects.RemoveAt(index);
                    result = true;
                }
            }

            return result;
        } 

        private static List<Project> m_Projects = new Project[]
        {
            new Project { Id = 1, Name="asd",Summary ="asdasd", CategoryId = 1, YearId = 4 },
            new Project { Id = 2, Name="qwe",Summary ="qwe", CategoryId = 2 , YearId = 1},
            new Project { Id = 3, Name="fgh",Summary ="fgh", CategoryId = 4 , YearId = 5},
            new Project { Id = 4, Name="bnm",Summary ="bnm", CategoryId = 1 , YearId = 2 }
            
        }.ToList();

        private object _lock = new object();
    }
}
