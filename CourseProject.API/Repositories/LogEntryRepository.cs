using CourseProject.API.Common.Repository;
using CourseProject.API.Repositories.Base;
using CourseProject.Model.BaseEntity;

namespace CourseProject.API.Repositories
{
    public interface ILogEntryRepository : IBaseRepository<LogEntry>
    {
        
    }
    public class LogEntryRepository : BaseRepository<LogEntry>, ILogEntryRepository
    {
        public LogEntryRepository(CourseContext context) : base(context)
        {

        }
    }
}
