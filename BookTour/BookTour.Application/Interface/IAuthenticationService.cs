using BookTour.Application.Dto;
using BookTour.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IAuthenticationService
    {
        Task<LoginDTO> Login(LoginRequestDTO request);

        Task<Dictionary<string, object>> DecodeTokenAsync(string token);
        Task<Dictionary<string, object>> HandleGoogleLogin(string code);

    
        Task<Dictionary<string, object>> HandleFacebookLogin(string code);

       
        public Task<TokenInfo> GenerateToken(User user);

     
        Task<string> GetAccessTokenFromGoogle(string code);

        
        Task<Dictionary<string, object>> GetUserInfoFromGoogle(string accessToken);

      
        Task<string> GetAccessTokenFromFacebook(string code);

   
        Task<Dictionary<string, object>> GetUserInfoFromFacebook(string accessToken);

     
        Task<User> CreateUserFromGoogleInfo(string name, string email);

        
        Task<User> CreateUserFromFacebookInfo(string name, string email);

        void CreateCustomer(User user);


        Task<Dictionary<string, object>> GenerateUserTokenResponse(User user, string picture, string name, string email);

        public Task<string> VerifyToken(string token, bool isRefresh);
    }
}
