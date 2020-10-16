using System;
using System.Collections.Generic;
using Foundation;
using RssReader.Core.Entities;
using UIKit;

namespace RssReader.iOS.Adapters
{
    public class RssItemAdapter: UITableViewSource
    {
        public const string CellId = "CellId";
        public Action<int> OnRowSelected;

        private readonly UIViewController controller;
        private readonly List<RssItem> data;

        public RssItemAdapter(UIViewController controller, List<RssItem> data)
        {
            this.controller = controller;
            this.data = data;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // Uses a builtin cell
            //var cell = tableView.DequeueReusableCell(nameof(RssItemCell)) as RssItemCell;
            var cell = tableView.DequeueReusableCell(nameof(RssItemCell));
            cell.TextLabel.Text = data[indexPath.Row].Title;
            cell.DetailTextLabel.Text = data[indexPath.Row].PublishDate.ToShortDateString();
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return data.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            OnRowSelected?.Invoke(data[indexPath.Row].Id);
        }
    }
}
