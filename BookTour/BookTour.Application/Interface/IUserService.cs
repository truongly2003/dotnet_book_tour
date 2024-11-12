using BookTour.Application.Dto;
using BookTour.Domain.Entity;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IUserService
    {
        Task<Page<User>> GetAllUserAsync(int page, int size);

        Task<User> AddUser();

        Task<UserDTO> Login(UserDTO request);

        Task<TokenInfo> GenerateToken(User user);  // Sửa thành TokenInfo
    }
}
