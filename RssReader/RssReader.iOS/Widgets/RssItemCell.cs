using Foundation;
using System;
using UIKit;

namespace RssReader.iOS
{
    public partial class RssItemCell : UITableViewCell
    {
        public RssItemCell (IntPtr handle) : base (handle)
        {
        }

        //public string Title
        //{
        //    get { return titl.Text; }
        //    set { titleLabel.Text = value; }
        //}

        //public string Subtitle
        //{
        //    get { return subtitleLabel.Text; }
        //    set { subtitleLabel.Text = value; }
        //}

        //public string ImageUrl
        //{
        //    get { return "https://www.lequipe.fr/rss/RSS/cover_podcast_flashlequipe.jpg"; }
        //    set { subtitleLabel.Text = "https://www.lequipe.fr/rss/RSS/cover_podcast_flashlequipe.jpg"; }
        //}
    }
}