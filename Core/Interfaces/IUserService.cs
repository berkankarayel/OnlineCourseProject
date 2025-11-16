using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;

namespace OnlineCourseApi.Core.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> GetByIdAsync(int id);
    Task<UserResponseDto> GetByEmailAsync(string email);
    Task<UserResponseDto> UpdateProfileAsync(int id, UpdateUserDto dto);
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    Task DeleteUserAsync(int id);
}
