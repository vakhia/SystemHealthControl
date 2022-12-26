using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class UsersSpecification : BaseSpecification<User>
{
    public UsersSpecification(string id) : base(x => x.IdentityId == id)
    {
    }
}