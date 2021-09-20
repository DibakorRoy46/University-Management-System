using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Data.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        public IDepartmentRepository Department { get;}
        public ICourseTypeRepository CourseType { get; }
        public ICourseProtoTypeRepository CourseProtoType { get; }
        public ICourseRepository Course { get;  }
        public ICoursePrerequisiteRepository CoursePrerequisite { get; }
        public ICourseToCoursePrerequisiteRepository CourseToCoursePrerequisite { get; }
        Task SaveAsync();
    }
}
