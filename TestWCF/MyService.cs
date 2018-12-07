﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TestWCF
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class MyService : IMyService
    {
        Dictionary<IClient, string> _users = new Dictionary<IClient, string>();
        public void Join(string username)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IClient>();
            _users[connection] = username;
        }

        public void SendMessage(string message)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IClient>();
            string user;
            if (!_users.TryGetValue(connection, out user))
            {
                return;
            }

            foreach (var other in _users.Keys)
            {
                if (other == connection)
                {
                    continue;
                }
                other.RecieveMessage(user, message);
            }
        }
    }
}
