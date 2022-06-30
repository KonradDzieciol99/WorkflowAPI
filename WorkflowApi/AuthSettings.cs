namespace WorkflowApi
{
    public class AuthSettings
    {
        public string JwtKey { get; set; }
        public int jwtExpire { get; set; }
        public string jwtIssuer { get; set; }

    }
}
