namespace Registration.Domain.Exceptions.Users
{
    public class UserAlreadyExistsException : AlreadyExistsException
    {
        public UserAlreadyExistsException()
        {
            TitleMessage = "User Already Exists!";
        }
    }
}