using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCRM.Shared.Interfaces
{
    public interface IDialer
    {
        bool Dial(string number);
    }
}
