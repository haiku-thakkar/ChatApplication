
namespace SlackClone.Api.Resources
{
    public class MsgModel
    {
        public int Id { get; set; }

        public int Sender { get; set; }

        public int Receiver { get; set; }

        public string Content { get; set; }

        public string DateTime { get; set; }

    }
}
