using System.Collections.Generic;
using api.Security.AuthTemplate;
using xSql.Interface;

namespace api.Database.Interface
{
    public interface IDatabaseHelper
    {
        ISqlHelper GetDefaultConnection();
        IEnumerable<Roles> GetRolesFromUser(string key);
        IEnumerable<Roles> GetRolesFromToken(string key);
    }
}