using Foundation;
using RssReader.Core.APIs;
using RssReader.Core.Entities;
using RssReader.iOS.Adapters;
using System;
using System.Collections.Generic;
using UIKit;

namespace RssReader.iOS
{
    public partial class StreamViewController : UIViewController
    {
        private RssItemAdapter _adapter = null;

        public string UrlToParse = null;

        public StreamViewController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (!string.IsNullOrWhiteSpace(UrlToParse))
            {
                try
                {
                    var items = await new RssApi().GetAsync(UrlToParse);
                _adapter = new RssItemAdapter(this, items);
                }
                catch (Exception ex)
                {
                    var alert = UIAlertController.Create("", "L'url du flux n'est pas valide. Supprimer l'élément?", UIAlertControllerStyle.Alert);
                    //alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (aa) => aa.));
                    alert.AddAction(UIAlertAction.Create("No", UIAlertActionStyle.Default, null));
                    this.NavigationController.PresentModalViewController(alert, true);
                }

            }
            else
            {
                _adapter = new RssItemAdapter(this, new List<RssItem>());
            }

            rssitemsTableView.RegisterClassForCellReuse(typeof(UITableViewCell), RssItemAdapter.CellId);
            rssitemsTableView.Source = _adapter;

        }
    }
}