using AutoMapper;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;
using OnlineCourseApi.Core.Interfaces;
using OnlineCourseApi.Business.Exceptions;

namespace OnlineCourseApi.Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
            throw new NotFoundException("Kullanıcı bulunamadı!");

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        
        if (user == null)
            throw new NotFoundException("Kullanıcı bulunamadı!");

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto> UpdateProfileAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
            throw new NotFoundException("Kullanıcı bulunamadı!");

        // Güncelleme
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.ProfileImageUrl = dto.ProfileImageUrl;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
            throw new NotFoundException("Kullanıcı bulunamadı!");

        await _userRepository.DeleteAsync(user);
        await _userRepository.SaveChangesAsync();
    }
}
