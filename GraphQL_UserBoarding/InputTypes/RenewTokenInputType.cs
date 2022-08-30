namespace GraphQL_UserBoarding.InputTypes
{
    public class RenewTokenInputType
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}