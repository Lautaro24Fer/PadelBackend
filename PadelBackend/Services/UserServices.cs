using AutoMapper;
using PadelBackend.Models.User.Dto;
using PadelBackend.Repositories;

namespace PadelBackend.Services
{
    public class UserServices
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;

        public UserServices(IUserRepository userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
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
    }
}
