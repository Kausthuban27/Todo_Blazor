using TodoAPI.ViewModel;

namespace TodoAPI.Interface
{
    public interface IUser
    {
        bool AddUser(UserData user);
        bool GetUser(string username, string password);
        void SaveChanges();
    }
}
