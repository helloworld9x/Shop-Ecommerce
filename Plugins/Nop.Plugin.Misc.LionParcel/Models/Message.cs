using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.LionParcel.Models
{
    public class Message
    {
        public string message { get; set; }

        public string id_pos { get; set; }
    }

    public class MessageResponse
    {
        public MessageResponse()
        {
            message=new Message();
        }

        public Message message { get; set; }

        public bool status { get; set; }
    }
}
