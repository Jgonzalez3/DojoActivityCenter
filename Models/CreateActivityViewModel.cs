using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models{
    public class CreateActivityViewModel{
        [Required(ErrorMessage= "Title is required")]
        [MinLength(2, ErrorMessage="Title must have at least 2 characters")]
        public string title {get;set;}
        [Required(ErrorMessage= "description is required")]
        [MinLength(10, ErrorMessage="Description must have at least 10 characters")]
        public string description {get;set;}
        [Required(ErrorMessage = "Address required")]
        public string address {get;set;}
        [Required(ErrorMessage= "Duration is required")]
        public int duration {get;set;}
        [Required(ErrorMessage= "Date is required")]
        public DateTime date {get;set;}
        [Required(ErrorMessage= "Time is required")]
        public DateTime time {get;set;}
        public string durationunit {get;set;}
    }
}