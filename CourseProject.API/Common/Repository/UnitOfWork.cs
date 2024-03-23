using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CourseProject.API.Repositories;
using System.Data;

namespace CourseProject.API.Common.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public ILogEntryRepository LogEntryRepository { get; }
        DbSet<T> Set<T>() where T : class;
        int Commit();
        Task<int> CommitAsync();
        IEnumerable<T> SqlQuery<T>(string query, SqlParameter[]? array = null) where T : class, new();
        DataTable SqlQuery(string query, SqlParameter[]? array = null);
        Task<int> SqlCommand(string query, SqlParameter[]? array = null);
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
        public IEnumerable<T> SqlQuery<T>(string query, SqlParameter[]? array = null) where T : class, new()
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
        public DataTable SqlQuery(string query, SqlParameter[]? array = null)
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

        public async Task<int> SqlCommand(string query, SqlParameter[]? array = null)
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