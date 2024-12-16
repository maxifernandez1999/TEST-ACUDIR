namespace Acudir.Test.Core.Domain.Entities
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
