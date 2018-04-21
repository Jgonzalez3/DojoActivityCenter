using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models{
    public class Participant{
        public int ParticipantId {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public int EventId {get;set;}
        public Event Event {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        public Participant(){
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }
    }
}