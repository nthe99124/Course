using CourseProject.API.Common.Repository;
using CourseProject.API.Repositories.Base;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.ViewModel.Course;

namespace CourseProject.API.Repositories
{
    public interface ILessionRepository : IBaseRepository<Lession>
    {
        
    }
    public class LessionRepository : BaseRepository<Lession>, ILessionRepository
    {
        public LessionRepository(CourseContext context) : base(context)
        {

        }
    }
}
