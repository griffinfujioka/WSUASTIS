/* 
 * 
 * Person.cs
 * Author: Griffin Fujioka 
 * CptS 422 - Fall 2012 
 * 
 * This file contains the definition of a Person and two inherited objects a Manager and a User.
 */
namespace WSUASTIS
{
    #region user type - manager | user
    public enum userType{
        manager,
        user
    };
    #endregion 

    #region Person object
    public class Person
    {
        private string _name; 
        public string name
        {
            get { return _name; }
            set
            { _name = value; }
        }

        private userType _userType;
        public userType userType
        {
            get { return _userType; }
            set {_userType = value; }
        }


    };
    #endregion 

    #region Manager object
    public class Manager : Person
    {
        private string _password;
        public string password
        {
            get { return _password; }
            set { _password = value; } 
        }
    }; 
    #endregion 

    #region User object
    public class User : Person 
    {
        private string _emailAddress;
        public string email
        {
            get { return _emailAddress; }
            set { _emailAddress = value; } 
        }
    };
    #endregion 
}