using DemoTask.Business.BusinessInterface;
using DemoTask.DAL.BaseRepository;
using DemoTask.DAL.Models;
using DemoTask.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoTask.Business.BusinessImplementation
{
    public class LoginTokenBusiness : ILoginTokenBusiness
    {
        private readonly IBaseRepository<LoginToken> _baseRepository;
        public LoginTokenBusiness(IBaseRepository<LoginToken> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public LoginTokenDto AddLoginTokenForUser(LoginTokenDto loginTokenDto)
        {
            try
            {
                if (loginTokenDto == null || string.IsNullOrEmpty(loginTokenDto.UserName) ||
                    string.IsNullOrEmpty(loginTokenDto.Token) || string.IsNullOrEmpty(loginTokenDto.RefreshToken))
                {
                    loginTokenDto.Success = false;
                    loginTokenDto.Message = "value is null or empty";
                    return loginTokenDto;
                }

                LoginToken loginToken = new LoginToken()
                {
                    RefreshToken = loginTokenDto.RefreshToken,
                    RefreshTokenExpiryTime = loginTokenDto.RefreshTokenExpiryTime,
                    UserName = loginTokenDto.UserName
                };

                _baseRepository.AddNew(loginToken);

                loginTokenDto.Success = true;
                loginTokenDto.Message = "Added successfully";
                loginTokenDto.Success = true;

                return loginTokenDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool CheckIfUserExist(LoginTokenDto loginTokenDto)
        {
            var user = _baseRepository.GetWhere(x => x.UserName == loginTokenDto.UserName).FirstOrDefault();
            if (user == null) return false;
            else return true;
        }

        // Do we need an update fucntion to prevent duplicate add?

        public LoginTokenDto GetLoginTokenForUser(LoginTokenDto loginTokenDto)
        {
            try
            {
                if (loginTokenDto == null || string.IsNullOrEmpty(loginTokenDto.UserName) ||
               string.IsNullOrEmpty(loginTokenDto.Token) || string.IsNullOrEmpty(loginTokenDto.RefreshToken))
                {
                    loginTokenDto.Success = false;
                    loginTokenDto.Message = "value is null or empty";
                    return loginTokenDto;
                }

                var result = _baseRepository.GetWhere(x => x.UserName == loginTokenDto.UserName).FirstOrDefault();

                LoginTokenDto loginTokenObj = new LoginTokenDto()
                {
                    RefreshToken = result.RefreshToken,
                    UserName = result.UserName,
                    RefreshTokenExpiryTime = result.RefreshTokenExpiryTime,
                    Success = true
                };

                return loginTokenObj;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool UpdateRefreshToken(string newRefreshToken, string userName)
        {

            try
            {
                if (string.IsNullOrEmpty(newRefreshToken) || string.IsNullOrEmpty(userName))
                {
                    return false;
                }
                var user = _baseRepository.GetWhere(x => x.UserName == userName).FirstOrDefault();
                user.RefreshToken = newRefreshToken;
                _baseRepository.Update(user);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
