using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using MobileCRM.Shared.Interfaces;
using MobileCRM.WindowsPhone;
using MobileCRM.WindowsPhone.Renderers;
using Xamarin.Forms;


[assembly: Dependency(typeof(PhoneDialer))]
namespace MobileCRM.WindowsPhone.Renderers
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            //Since this is a demo we're not going to dial the actual number.  This is a temporary toll-free number we've set up.
            number = "8555826555";

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
