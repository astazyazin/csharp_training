using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_project_tests
{ 
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public int CompareTo(ProjectData other)
        {
            if (other is null)
            {
                return 1;
            }
            if (ProjectName.CompareTo(other.ProjectName) == 0)
            {
                return Description.CompareTo(other.Description);
            }
            return ProjectName.CompareTo(other.ProjectName);
        }

        public bool Equals(ProjectData other)
        {
            if (other is null)
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return ProjectName.Equals(other.ProjectName) && Description.Equals(other.Description);
        }
    }
}
