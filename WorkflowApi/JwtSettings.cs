namespace WorkflowApi
{
    public class JwtSettings
    {
        public string jwtKey { get; set; }
        public int jwtExpire { get; set; }
        public int jwtIssuer { get; set; }
    }
}
