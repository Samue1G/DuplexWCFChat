using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TestWCF
{
    interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void RecieveMessage(string username, string message);
    }
}
