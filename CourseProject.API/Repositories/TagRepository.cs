using CourseProject.API.Common.Repository;
using CourseProject.API.Repositories.Base;
using CourseProject.Model.BaseEntity;

namespace CourseProject.API.Repositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        
    }
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(CourseContext context) : base(context)
        {

        }
    }
}
