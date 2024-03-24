using CourseProject.API.Common.Repository;
using CourseProject.API.Common.Ulti;
using CourseProject.API.Repositories.Base;
using CourseProject.Model.BaseEntity;
using Microsoft.Data.SqlClient;

namespace CourseProject.API.Repositories
{
    public interface IAccountRepository: IBaseRepository<Account>
    {
        Task<Account> GetUserByUserNameAndPass(string email, string password);

        IEnumerable<Role> GetListRoleByAccId(Guid accId);
    }
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(CourseContext context):base(context)
        {
            
        }

        public async Task<Account> GetUserByUserNameAndPass(string email, string password)
        {
            var passwordEncode = HashCodeUlti.EncodePassword(password);
            var user = await FindBy(a => a.Email == email && a.Password == passwordEncode);
            return user.FirstOrDefault();
        }

        public IEnumerable<Role> GetListRoleByAccId(Guid accId)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter("@AccID", accId)
            };
            var roleList = ExecuteStoredProcedureObject<Role>("GetRoleByAccId", param);
            return roleList;
        }
    }
}
