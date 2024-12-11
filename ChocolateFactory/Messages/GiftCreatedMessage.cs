using ChocolateFactory.Data;

namespace ChocolateFactory.Messages
{
    public class GiftCreatedMessage
    {
        public Gift NewGift { get; }

        public GiftCreatedMessage(Gift newGift)
        {
            NewGift = newGift;
        }
    }
}
