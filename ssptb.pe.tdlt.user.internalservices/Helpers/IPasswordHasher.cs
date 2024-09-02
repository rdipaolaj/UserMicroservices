namespace ssptb.pe.tdlt.user.internalservices.Helpers;
public interface IPasswordHasher
{
    (string HashedPassword, string Salt) HashPassword(string password);
}