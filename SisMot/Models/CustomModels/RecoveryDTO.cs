namespace SisMot.Models.CustomModels
{
    public class RecoveryDTO
    {
        public int userId { get; set; }
        public string confirmPassword { get; set; }
        public string codeRecovery { get; set; }
        public string password { get; set; }
    }
}
