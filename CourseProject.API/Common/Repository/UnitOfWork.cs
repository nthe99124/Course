using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CourseProject.API.Repositories;
using System.Data;

namespace CourseProject.API.Common.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public ILogEntryRepository LogEntryRepository { get; }
        public IAccountRepository AccountRepository { get; }
        public ICourseRepository CourseRepository { get; }
        public ICourseTagRepository CourseTagRepository { get; }
        public ITagRepository TagRepository { get; }
        public IRoleAccountRepository RoleAccountRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IChapterRepository ChapterRepository { get; }
        public ILessionRepository LessionRepository { get; }

        DbSet<T> Set<T>() where T : class;
        int Commit();
        Task<int> CommitAsync();
        IEnumerable<T> SqlQuery<T>(string query, SqlParameter[] array = null) where T : class, new();
        DataTable SqlQuery(string query, SqlParameter[] array = null);
        Task<int> SqlCommand(string query, SqlParameter[] array = null);
    }

    public class UnitOfWork : IUnitOfWork
    {
        readonly CourseContext _context;
        private bool _isDisposed;

        
        public UnitOfWork(CourseContext context)
        {
            _context = context;
        }

        #region Khai repository (không inject qua ctor nữa mà xử lý new theo từng lần sử dụng tránh cấp phát quá nhiều trong 1 lần khởi tạo UnitOfWork)

        private ILogEntryRepository _logEntryRepository;
        public ILogEntryRepository LogEntryRepository
        {
            get
            {
                if (_logEntryRepository == null)
                {
                    _logEntryRepository = new LogEntryRepository(_context);
                }
                return _logEntryRepository;
            }
        }

        private IAccountRepository _accountRepository;
        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_context);
                }
                return _accountRepository;
            }
        }

        private ICourseTagRepository _courseTagRepository;
        public ICourseTagRepository CourseTagRepository
        {
            get
            {
                if (_courseTagRepository == null)
                {
                    _courseTagRepository = new CourseTagRepository(_context);
                }
                return _courseTagRepository;
            }
        }

        private ICourseRepository _courseRepository;
        public ICourseRepository CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new CourseRepository(_context);
                }
                return _courseRepository;
            }
        }

        private ITagRepository _tagRepository;
        public ITagRepository TagRepository
        {
            get
            {
                if (_tagRepository == null)
                {
                    _tagRepository = new TagRepository(_context);
                }
                return _tagRepository;
            }
        }

        private IRoleAccountRepository _roleAccountRepository;
        public IRoleAccountRepository RoleAccountRepository
        {
            get
            {
                if (_roleAccountRepository == null)
                {
                    _roleAccountRepository = new RoleAccountRepository(_context);
                }
                return _roleAccountRepository;
            }
        }

        private IRoleRepository _roleRepository;
        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RoleRepository(_context);
                }
                return _roleRepository;
            }
        }

        private IChapterRepository _chapterRepository;
        public IChapterRepository ChapterRepository
        {
            get
            {
                if (_chapterRepository == null)
                {
                    _chapterRepository = new ChapterRepository(_context);
                }
                return _chapterRepository;
            }
        }

        private ILessionRepository _lessionRepository;
        public ILessionRepository LessionRepository
        {
            get
            {
                if (_lessionRepository == null)
                {
                    _lessionRepository = new LessionRepository(_context);
                }
                return _lessionRepository;
            }
        }
        

        #endregion

        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _context.Dispose();
        }

        /// <summary>
        /// IEnumerable type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public IEnumerable<T> SqlQuery<T>(string query, SqlParameter[] array = null) where T : class, new()
        {
            try
            {
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                _context.Database.SetCommandTimeout(1800);
                IEnumerable<T> obj;
                if (array != null)
                {
                    obj = Set<T>().FromSqlRaw(query, array).ToList();
                }
                else
                {
                    obj = Set<T>().FromSqlRaw(query).ToList();
                }

                return obj;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public DataTable SqlQuery(string query, SqlParameter[] array = null)
        {
            var dt = new DataTable();
            try
            {
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                _context.Database.SetCommandTimeout(1800);

                var conn = _context.Database.GetDbConnection();
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open) conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        if (array != null && array.Any())
                        {
                            cmd.Parameters.AddRange(array);
                        }
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // error handling
                    throw;
                }
                finally
                {
                    if (connectionState != ConnectionState.Closed) conn.Close();
                }

                return dt;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> SqlCommand(string query, SqlParameter[] array = null)
        {
            try
            {
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                _context.Database.SetCommandTimeout(1800);
                int rs;
                if (array != null)
                {
                    rs = await _context.Database.ExecuteSqlRawAsync(query, array);
                }
                else
                {
                    rs = await _context.Database.ExecuteSqlRawAsync(query);
                }

                return rs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}