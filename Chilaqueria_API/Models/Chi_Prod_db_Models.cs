using System.ComponentModel.DataAnnotations;

namespace Chilaqueria_API.Models
{
    public class Chi_Prod_db_Models
    {
        public class Prod_Users
        {
            public Guid User_guid { get; set; }

            public string User_username { get; set; }

            public string User_name { get; set; }

            public string User_mail { get; set; }

            public string User_pass { get; set; }

            public bool User_active { get; set; }

        }


        public class Prod_Products
        {
            [Key]
            public Guid Product_guid { get; set; }

            public string Product_name { get; set; }

            public string Product_description { get; set; }

            public bool Product_active { get; set; }

            public DateTime? Product_creation_date { get; set; }

            public Guid Product_user_create { get; set; }

        }


    }
}
