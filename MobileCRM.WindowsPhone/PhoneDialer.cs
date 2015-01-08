using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using MobileCRM.Shared.Interfaces;
using MobileCRM.WindowsPhone;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]
namespace MobileCRM.WindowsPhone
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            PhoneCallTask phoneCallTask = new PhoneCallTask
            {
                PhoneNumber = number,
                DisplayName = "Phoneword"
            };

            phoneCallTask.Show();
            return true;
        }
    }
}
