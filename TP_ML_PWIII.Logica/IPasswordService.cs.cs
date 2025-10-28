namespace TP_ML_PWIII.Logica
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
  
}
