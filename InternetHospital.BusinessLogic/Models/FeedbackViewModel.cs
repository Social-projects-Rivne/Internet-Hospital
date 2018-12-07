using System;

namespace InternetHospital.BusinessLogic.Models
{
    public class FeedbackViewModel
    {
        public int Id {get;set;}
        public string Text { get; set; }
        public int FeedbackTypeId { set; get; }
        public string FeedbackTypeName { set; get; }
        public DateTime dateTime { set; get; }

        public int UserId { set; get; }
        public string UsersEmail { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersSecondName { get; set; }
        public string UsersThirdName { get; set; }
        public string UsersAvatarURL { get; set; }
        public DateTime? UsersBirthDate { get; set; }
        public bool? IsViewed { set; get; }
    }
}
