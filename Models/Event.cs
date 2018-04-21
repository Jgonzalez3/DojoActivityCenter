using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models{
    public class Event{
        public int EventId {get;set;}
        public string title {get;set;}
        public string description {get;set;}
        public DateTime date {get;set;}
        public DateTime time {get;set;}
        public int duration {get;set;}
        public string durationunit {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public List<Participant> participants {get;set;}
        public Event(){
            participants = new List<Participant>();
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }
    }
}