using System;

namespace SecretSanta.Data
{
    public class Assignment
    {
        public User Giver { get; set;}
        public User Receiver { get; set;}

        public int Id{get;set;}
        public DateTime GiftDue{get;set;}

        public Assignment(User giver, User recipient)
        {
            Giver = giver ?? throw new ArgumentNullException(nameof(giver));
            Receiver = recipient ?? throw new ArgumentNullException(nameof(recipient));
            Id = 0;
            GiftDue = System.DateTime.Now;
            GiftDue = GiftDue.AddDays(7);
        }
        public Assignment()
        {
            this.Giver = new User();
            this.Receiver = new User();
            this.Id = 0;
            GiftDue = System.DateTime.Now;
            GiftDue = GiftDue.AddDays(7);
        }

        public void AddDaysToGiftDate(int days)
        {
            GiftDue = GiftDue.AddDays(days);
        }
    }
}
