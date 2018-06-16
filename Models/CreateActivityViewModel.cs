using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models{
    public class CreateActivityViewModel{
        [MinLength(2, ErrorMessage="Title must have at least 2 characters")]
        [Required(ErrorMessage= "Title is required")]
        public string title {get;set;}
        [MinLength(10, ErrorMessage="Description must have at least 10 characters")]
        [Required(ErrorMessage= "Description is required")]
        public string description {get;set;}
        [Required(ErrorMessage = "Address required")]
        public string address {get;set;}
        [Required(ErrorMessage= "Duration is required")]
        public int? duration {get;set;}
        [Display(Name = "date")]
        [Required(ErrorMessage= "Date is required")]
        public DateTime? date {get;set;}
        
        [Required(ErrorMessage= "Time is required")]
        public DateTime? time {get;set;}
        public string durationunit {get;set;}
    }
}