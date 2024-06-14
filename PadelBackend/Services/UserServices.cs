using AutoMapper;
using PadelBackend.Models.Auth;
using PadelBackend.Models.Auth.Dto;
using PadelBackend.Models.User;
using PadelBackend.Models.User.Dto;
using PadelBackend.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Http;

namespace PadelBackend.Services
{
    public interface IUsersServices
    {
        public Task<List<UsersDto>> GetManyUsers();
        public Task<UserDto> GetOneUser(int id);
        public Task<LoginValidationDto> GetOneUserByUserNameOrEmail(string input);

    }
    public class UserServices : IUsersServices
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;
        private readonly IAuthServices authServices;

        public UserServices(IUserRepository userRepo, IMapper mapper, IAuthServices authServices)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
            this.authServices = authServices;
        }

        public async Task<List<UsersDto>> GetManyUsers()
        {
            var users = await userRepo.Get();
            return mapper.Map<List<UsersDto>>(users);
        }
        public async Task<UserDto> GetOneUser(int id)
        {
            var user = await userRepo.GetOne(u => u.Id == id);
            return mapper.Map<UserDto>(user);
        }

        public async Task<LoginValidationDto> GetOneUserByUserNameOrEmail(string input)
        {
            var userStatus = new LoginValidationDto();


            if (input.Contains('@'))
            {
                if (!authServices.IsValidAddress(input))
                {
                    userStatus.Message = "The email address has not the correct format";
                    return userStatus;
                }
                var userByEmail = await userRepo.GetOne(u => u.Email == input);
                if (userByEmail == null)
                {
                    userStatus.Message = $"No user founded with de email {input}";
                    return userStatus;
                }
                userStatus.Status = true;
                userStatus.User = userByEmail;
                return userStatus;
            }
            if (!authServices.IsValidUserNameFormat(input))
            {
                userStatus.Message = "Username invalid";
                return userStatus;
            }
            var userByUserName = await userRepo.GetOne(u => u.UserName == input);
            userStatus.Status = true;
            userStatus.User = userByUserName;
            return userStatus;
        }

        
    }
}
