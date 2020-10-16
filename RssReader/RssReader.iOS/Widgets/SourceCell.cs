using Foundation;
using System;
using UIKit;

namespace RssReader.iOS
{
    public partial class SourceCell : UITableViewCell
    {
        public SourceCell (IntPtr handle) : base (handle)
        {
        }

        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value; }
        }
    }
}