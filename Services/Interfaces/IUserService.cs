using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services
{
    public interface IUserService
    {
        List<UserGetRequest> Get(UserSearchRequest obj);
        UserGetRequest Get(int id);
        UserGetRequest Insert(UserInsertRequest obj);
        UserGetRequest Update(int id, UserUpdateRequest obj);
        bool ChangeUserStatus(int id, bool status);
        List<UserType> GetUserTypes();
        User Authenticate(string username, string pass);
        void SetCurrentUser(User currentUser);
        UserGetRequest GetMyProfile();
    }
}
