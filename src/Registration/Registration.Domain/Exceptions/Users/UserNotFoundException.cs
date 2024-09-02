namespace Registration.Domain.Exceptions.Users
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException()
        {
            TitleMessage = "User Not Found!";
        }
    }
}