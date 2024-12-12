using ChocolateFactory.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Messages
{
    public class ItemChangedMessage : ValueChangedMessage<ItemModel>
    {
        public ItemChangedMessage(ItemModel value) : base(value)
        {
        }
    }
}
