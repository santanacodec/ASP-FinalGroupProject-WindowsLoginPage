using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MB_Model
{
    [DataContract]
    public partial  class DTO_UserInfo
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Fname { get; set; }
        [DataMember]
        public string Lname { get; set;  }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public bool InvalidUser { get; set; }
        [DataMember]
        public bool PasswordInvalid { get; set; }
    }

    [DataContract]
    public class DTO_Login
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
    }

    [DataContract]
    public class DTO_UserID
    {
        [DataMember]
        public int UserID { get; set; }
    }

    [DataContract]
    public class DTO_RecipeHeader
    {
        [DataMember]
        public int RecipeID { get; set; }
        [DataMember]
        public String Title { get; set; }
        [DataMember]
        public String Picture { get; set; }

    }
    [DataContract]
    public class DTO_RecipeList
    {
        [DataMember]
        public  List<DTO_RecipeHeader> Recipes { get; set; }

    }
    [DataContract]
    public class DTO_RecipeID
    {
        [DataMember]
        public int MB_RecipeID { get; set; }
    }
    [DataContract]
    public class DTO_Recipe
    {
        [DataMember]
        public int MB_RecipeID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Instructions { get; set; }
        [DataMember]
        public int Savings { get; set; }
        [DataMember]
        public int CookingTime { get; set; }
        [DataMember]
        public int PrepTime { get; set; }
        [DataMember]
        public string Picture { get; set; }
        [DataMember]
        public bool Favorite { get; set; }
        [DataMember]
        public List<DTO_Ingredient>  Ingredients {get; set;}
    }
    [DataContract]
    public class DTO_Ingredient 
    {
        [DataMember]
        public int MB_IngredientID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Unites { get; set; }
        [DataMember]
        public string MeasurementUnit { get; set; }
    }

}
