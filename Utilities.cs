using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Ecormerce
{
	public class Utilities
	{


        // MySQL Connection objects
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataAdapter sda;
        MySqlDataReader sdr;
        DataTable dt;

        public static string getConnection()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        } 

        public static bool isValidExtension(string fileName)
        {
            bool isValid = false;
            string[] fileExtension = { ".jpg", ".png", ".jpeg", };
            foreach(string file in fileExtension)
            {
                if (fileName.Contains(file))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

      public static string getUniqueId()
        {
            Guid guid = Guid.NewGuid(); // Correct way to instantiate a GUID
            return guid.ToString(); // Return the GUID as a string
        }
    }
}