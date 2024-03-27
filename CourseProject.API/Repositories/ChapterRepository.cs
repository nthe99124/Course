using CourseProject.API.Common.Repository;
using CourseProject.API.Repositories.Base;
using CourseProject.Model.BaseEntity;

namespace CourseProject.API.Repositories
{
    public interface IChapterRepository : IBaseRepository<Chapter>
    {
        
    }
    public class ChapterRepository : BaseRepository<Chapter>, IChapterRepository
    {
        public ChapterRepository(CourseContext context) : base(context)
        {

        }
    }
}
