using Moor.Model.Model.Authorize;
using Moor.Model.Utilities;
using Moor.Model.Utilities.Authentication;

namespace Moor.Core.Services.MoorService
{
    public interface IAuthorizeService
    {
        LoginResponseModel Login(LoginModel loginModel);
        Task<DataResult> Register(PersonnelModel personnelModel);
    }
}
