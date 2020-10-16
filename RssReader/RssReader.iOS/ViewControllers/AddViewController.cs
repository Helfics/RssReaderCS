using Foundation;
using RssReader.Core.Services;
using System;
using UIKit;

namespace RssReader.iOS
{
    public partial class AddViewController : UIViewController
    {
        SourceService sourceService = null;

        public Action<int> OnSuccess { get; set; }

        public AddViewController (IntPtr handle) : base (handle)
        {
            sourceService = new SourceService(Constants.ConnectionString);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            saveButton.TouchUpInside += SaveButton_TouchUpInside;
        }

        private void SaveButton_TouchUpInside(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(titleTextField.Text) && !string.IsNullOrWhiteSpace(urlTextField.Text))
            {
                var id = this.sourceService.Add(titleTextField.Text, urlTextField.Text);
                NavigationController.PopViewController(true);
                OnSuccess?.Invoke(id);
            }
        }
    }
}