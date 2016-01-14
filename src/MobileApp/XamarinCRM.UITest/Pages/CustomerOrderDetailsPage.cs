using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class CustomerOrderDetailsPage : BasePage
    {
        readonly Query SaveButton;
        readonly Query ExitButton = x => x.Marked("Exit");
        readonly Query DeliverOrderButton = x => x.Marked("Deliver Order");
        readonly Query ConfirmDeliveryButton = x => x.Marked("Yes, Deliver");
        readonly Query ProductField;
        readonly Query PriceField;
        readonly Query DateField;

        public CustomerOrderDetailsPage(IApp app, Platform platform)
            : base(app, platform, "Company", "Company")
        {
            if (OnAndroid)
            {
                SaveButton = x => x.Marked("Save");
                ProductField = x => x.Class("EntryEditText").Descendant(0);
                PriceField = x => x.Class("EntryEditText").Descendant(1);
                DateField = x => x.Class("EditText").Index(0);
            }
            if (OniOS)
            {
                ProductField = x => x.Class("UITextField").Index(0);
                PriceField = x => x.Class("UITextField").Index(1);
                DateField = x => x.Class("UITextField").Index(2);
                SaveButton = x => x.Id("save.png");
            }
        }

        public CustomerOrderDetailsPage ChangeProduct()
        {
            app.Tap(ProductField);
            return this;
        }

        public CustomerOrderDetailsPage ChangePrice(int amount)
        {
            app.Tap(PriceField);
            app.ClearText();
            app.EnterText(amount.ToString());
            if (OnAndroid)
                app.PressEnter();
            if (OniOS)
                app.DismissKeyboard();

            return this;
        }

        public CustomerOrderDetailsPage ChangeDate()
        {
            //TODO: Need to Add Logic
            if (OnAndroid)
            {
            }

            if (OniOS)
            {
            }

            return this;
        }

        public void SaveAndExit()
        {
            app.Tap(SaveButton);
            app.Screenshot("Dialog Appears");
            app.Tap("Save");
        }

        public void DeliverOrder()
        {
            app.Tap(DeliverOrderButton);
            app.Screenshot("Dialog Appears");
            app.Tap(ConfirmDeliveryButton);

        }
    }
}

