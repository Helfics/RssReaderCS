using System;
using System.Collections.Generic;
using Foundation;
using RssReader.Core.Entities;
using UIKit;

namespace RssReader.iOS.Adapters
{
    public class SourceAdapter: UITableViewSource
    {
        public const string CellId = "CellId";
        public Action<int> OnRowSelected;

        private readonly UIViewController controller;
        private readonly List<Source> data;

        public SourceAdapter(UIViewController controller, List<Source> data)
        {
            this.controller = controller;
            this.data = data;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // Uses a builtin cell
            //var cell = tableView.DequeueReusableCell(CellId);

            // Uses a custom cell
            var cell = tableView.DequeueReusableCell(nameof(SourceCell)) as SourceCell;
            cell.Title = data[indexPath.Row].Title;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return data.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //var alert = UIAlertController.Create("Selection", $"{data[indexPath.Row]}", UIAlertControllerStyle.Alert);

            //alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

            //controller.ShowViewController(alert, controller);

            OnRowSelected?.Invoke(data[indexPath.Row].Id);
        }

        public void Add(Source source)
        {
            data.Insert(0, source);
        }
    }
}
