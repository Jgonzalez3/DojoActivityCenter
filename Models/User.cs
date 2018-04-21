using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models{
    public class User{
        public int UserId {get;set;}
        public string firstname {get;set;}
        public string lastname {get;set;}
        public string email {get;set;}
        public string password {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        public List<Event> events {get;set;}
        public List<Participant> participants {get;set;}
        public User(){
            events = new List<Event>();
            participants = new List<Participant>();
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }
    }
}