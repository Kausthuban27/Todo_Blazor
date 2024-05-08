namespace Todo_Blazor.SharedService
{
    public class UserState_Management_Service
    {
        public string Username { get; set; } = null!;
        public bool IsLoggedIn { get; set; }

        public void Clear()
        {
            Username = string.Empty;
            IsLoggedIn = false ;
        }
    }
}
