using AutoMapper;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;
using OnlineCourseApi.Core.Entities;
using OnlineCourseApi.Core.Interfaces;
using OnlineCourseApi.Business.Helpers;
using OnlineCourseApi.Business.Exceptions;

namespace OnlineCourseApi.Business.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly JwtTokenHelper _jwtTokenHelper;

    public AuthService(
        IUserRepository userRepository,
        IMapper mapper,
        JwtTokenHelper jwtTokenHelper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtTokenHelper = jwtTokenHelper;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
    {
        // 1. Email ile kullanıcıyı bul
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        
        if (user == null)
            throw new UnauthorizedException("Email veya şifre hatalı!");

        // 2. Şifre kontrolü (BCrypt ile)
        var isPasswordValid = _jwtTokenHelper.VerifyPassword(dto.Password, user.PasswordHash);
        
        if (!isPasswordValid)
            throw new UnauthorizedException("Email veya şifre hatalı!");

        // 3. JWT Token oluştur
        var token = _jwtTokenHelper.GenerateToken(user);

        // 4. Response oluştur
        return new LoginResponseDto
        {
            Token = token,
            User = _mapper.Map<UserResponseDto>(user)
        };
    }

    public async Task<UserResponseDto> RegisterAsync(RegisterRequestDto dto)
    {
        // 1. Email zaten kayıtlı mı kontrol et
        var emailExists = await _userRepository.EmailExistsAsync(dto.Email);
        
        if (emailExists)
            throw new BadRequestException("Bu email adresi zaten kayıtlı!");

        // 2. Şifreyi hash'le (BCrypt ile)
        var hashedPassword = _jwtTokenHelper.HashPassword(dto.Password);

        // 3. User entity oluştur
        var user = _mapper.Map<User>(dto);
        user.PasswordHash = hashedPassword;
        user.Role = "User"; // Default role
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        // 4. Database'e ekle
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        // 5. UserResponseDto döndür (PasswordHash olmadan!)
        return _mapper.Map<UserResponseDto>(user);
    }
}
