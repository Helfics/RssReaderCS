using Foundation;
using RssReader.Core.Entities;
using RssReader.Core.Services;
using RssReader.iOS.Adapters;
using System;
using System.Collections.Generic;
using UIKit;

namespace RssReader.iOS
{
    public partial class ViewController : UIViewController
    {
        SourceService sourceService = null;
        SourceAdapter adapter = null;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            sourceService = new SourceService(Constants.ConnectionString);
            adapter = new SourceAdapter(this, sourceService.Get());

            adapter.OnRowSelected += OnRowSelected;

            sourceTableView.RegisterClassForCellReuse(typeof(UITableViewCell), SourceAdapter.CellId);
            sourceTableView.Source = adapter;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            var viewController = segue.DestinationViewController as AddViewController;

            viewController.OnSuccess += OnSuccess;

            base.PrepareForSegue(segue, sender);
        }

        private void OnRowSelected(int id)
        {
            var dvc = Storyboard.InstantiateViewController(nameof(StreamViewController)) as StreamViewController;

            dvc.UrlToParse = sourceService.GetById(id)?.Uri;

            this.NavigationController.PushViewController(dvc, true);
        }

        private void OnSuccess(int id) {
            var matchingSource = sourceService.GetById(id);
            adapter.Add(matchingSource);
            sourceTableView.ReloadData();
        }
    }
}