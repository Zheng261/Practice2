using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MarketLearning.Models.Entities
{
    public class Error : IEntity
    {
        public int Id { get; set; }
        public DateTime Occurance { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Location { get; set; }
        [DisplayName("Error End")]
        public bool ClientSide { get; set; }

        public Error()
        {
            DateTime now = DateTime.Now;
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            now = TimeZoneInfo.ConvertTime(now, zone);
            Occurance = now;
        }

        public Error(string username, string message, string location, bool clientSide)
        {
            DateTime now = DateTime.Now;
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            now = TimeZoneInfo.ConvertTime(now, zone);
            Occurance = now;
            Username = username;
            Message = message;
            Location = location;
            ClientSide = clientSide;
        }
    }
}