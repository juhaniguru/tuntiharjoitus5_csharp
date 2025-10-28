using API.DTOs;
using API.Interfaces;

namespace API.Services
{
    public class AuthService(IAuthRepo _authRepo, ILogRepo _logRepo) : IAuthService
    {
        public async Task<LoginRes> Login(LoginReq requestData)
        {
            var user = await _authRepo.Login(requestData.UserName, requestData.Password);
            if (user == null)
            {
                throw new Exception("user not found");
            }

            if (user.Password != requestData.Password)
            {
                throw new Exception("user not found");
            }

            await _logRepo.Create(new AddLogEntryReq
            {
                UserName = user.Username
            });

            return new LoginRes
            {
                Token = "jwt"
            };
        }
    }
}