using AutoMapper;
using PadelBackend.Exceptions;
using PadelBackend.Models.Auth;
using PadelBackend.Models.Auth.Dto;
using PadelBackend.Models.User;
using PadelBackend.Models.User.Dto;
using PadelBackend.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace PadelBackend.Services
{
    public interface IUsersServices
    {
        public Task<List<UsersDto>> GetManyUsers();
        public Task<UserDto> GetOneUser(int id);
        public Task<LoginValidationDto> GetOneUserByUserNameOrEmail(string input);
        public Task<LoginValidationDto> ValidateCredentials(Login login);
        public Task<UserDto> CreateOneUser(CreateUserDto createUser);

        public Task<UserDto> UpdateOneUser(UpdateUserDto updateUser, int id);
    }
    public class UserServices : IUsersServices
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;
        private readonly IEncoderService encoderService;

        public UserServices(IUserRepository userRepo, IMapper mapper, IEncoderService encoderService)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
            this.encoderService = encoderService;
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
        public async Task<UserDto> CreateOneUser(CreateUserDto createUser)
        {
            createUser.UserName = createUser.UserName.Trim();
            if (!IsValidUserNameFormat(createUser.UserName))
            {
                throw new Exception($"The username format can only have a-z letters or _ and not start with numbers");
            }
            // validacion si el correo o el nombre de usuario unicos ya estan en el sistema
            var validUserName = await GetOneUserByUserNameOrEmail(createUser.UserName);
            var validEmail = await GetOneUserByUserNameOrEmail(createUser.Email);
            if(validUserName.Status)
            {
                throw new Exception($"The username '{createUser.UserName}' is currently in use");
            }
            if (validEmail.Status)
            {
                throw new Exception($"The email '{createUser.Email}' is currently in use");
            }
            var createUserMapped = mapper.Map<User>(createUser);
            createUserMapped.Password = encoderService.Encode(createUserMapped.Password);
            await userRepo.CreateOne(createUserMapped);
            return mapper.Map<UserDto>(createUserMapped);
        }
        public async Task<LoginValidationDto> GetOneUserByUserNameOrEmail(string input)
        {
            var userStatus = new LoginValidationDto();


            if (input.Contains('@'))
            {
                if (!IsValidAddress(input))
                {
                    userStatus.Status = false;
                    userStatus.Message = "The email address has not the correct format";
                    return userStatus;
                }
                var userByEmail = await userRepo.GetOne(u => u.Email == input);
                if (userByEmail == null)
                {
                    userStatus.Status = false;
                    userStatus.Message = $"No user founded with de email {input}";
                    return userStatus;
                }
                userStatus.Status = true;
                userStatus.User = userByEmail;
                return userStatus;
            }
            if (!IsValidUserNameFormat(input))
            {
                userStatus.Message = "Username invalid";
                return userStatus;
            }
            var userByUserName = await userRepo.GetOne(u => u.UserName == input);
            if(userByUserName == null)
            {
                userStatus.Status = false;
                userStatus.Message = $"User by username '{input}' not founded";
                userStatus.User = userByUserName;
                return userStatus;
            }
            userStatus.Status = true;
            userStatus.Message = "UserName valid founded";
            userStatus.User = userByUserName;
            return userStatus;
        }
        public async Task<LoginValidationDto> ValidateCredentials(Login login)
        {

            var user = await GetOneUserByUserNameOrEmail(login.UsernameOrMailAddress);
            if (!user.Status)
            {
                throw new Exception(user.Message);
            }

            string userHashPassword = user.User.Password;
            string loginPassword = login.Password;
            if (!encoderService.Verify(login.Password, userHashPassword))
            {
                user.Status = false;
                user.Message = "The credentials do not match";
                return user;
            }
            return user;
        }

        public bool IsValidAddress(string input)
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(input);
        }

        public bool IsValidUserNameFormat(string input)
        {
            // Solo tomará nombres de usuario con letras minusculas y mayusculas y numeros. 
            // El guion bajo (_) es el unico simbolo permitido
            // El primer caracter debe de ser obligatoriamente una letra
            string usernamePattern = @"^[a-zA-Z][a-zA-Z0-9_]*$";
            return Regex.IsMatch(input, usernamePattern);
        }

        public async Task<UserDto> UpdateOneUser(UpdateUserDto updateUser, int id)
        {
            var userExists = await userRepo.GetOne(u => u.Id == id);
            if(userExists == null)
            {
                throw new NotFoundCustomEx();
            }
            if(updateUser.Email != null)
            {
                var emailInUse = await GetOneUserByUserNameOrEmail(updateUser.Email);
                if(emailInUse.User != null)
                {
                    throw new Exception($"The email '{updateUser.Email}' is currently in use");
                }
            }
            if (updateUser.UserName != null)
            {
                var userNameInUse = await GetOneUserByUserNameOrEmail(updateUser.UserName);
                if (userNameInUse.User != null)
                {
                    throw new Exception($"The username '{updateUser.UserName}' is currently in use");
                }
            }
            var userUpdated = mapper.Map(updateUser, userExists);
            return mapper.Map<UserDto>(await userRepo.Update(userUpdated));
        }
    }
}
