using System.ComponentModel.DataAnnotations;

namespace GraphQL_MongoDB.ViewModels
{
    public class UserInput
    {
        public record LoginInput([Required] string UserName, [Required] string Password);

        public record RenewTokenInput([Required] string AccessToken, [Required] string RefreshToken);

        public record CreateUserInput([Required] string UserName, 
                                        [Required] string Password, 
                                        [Required] string Bio, 
                                        [Required] string ProfileImageUrl, 
                                        [Required] string ProfileBannerUrl,
                                        [Required] string EmailAddress, 
                                        [Required] string WalletAddress, 
                                        [Required] string WalletType);

        public record UpdateUserInput([Required] string UserName,
                                        [Required] string Password,
                                        [Required] string Bio,
                                        [Required] string ProfileImageUrl,
                                        [Required] string ProfileBannerUrl,
                                        [Required] string EmailAddress,
                                        [Required] string WalletAddress,
                                        [Required] string WalletType);
    }
}