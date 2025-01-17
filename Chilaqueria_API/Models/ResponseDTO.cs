namespace Chilaqueria_API.Models
{

    public class UserLoginQueryResponse
    {
        public int UserId { get; set; }
        public string email { get; set; } = default!;
        public double Price { get; set; }
    }

}
