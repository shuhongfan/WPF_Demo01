﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class User
    {
        public string UserName { get; set; }
        public readonly IChatServiceCallback callback;

        public User(string userName, IChatServiceCallback callback)
        {
            this.UserName = userName;
            this.callback = callback;
        }
    }
}
