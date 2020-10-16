using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using RssReader.Core.Entities;
using RssReader.Core.Exceptions;
using RssReader.Core.Services;

namespace RssReader.Droid.Activities
{
    [Activity(Label = "Ajouter une source", Theme = "@style/AppTheme")]
    public class AddActivity: AppCompatActivity
    {
        private SourceService _sourceService;

        private TextInputLayout _titleTextInputLayout;
        private TextInputLayout _urlTextInputLayout;

        private EditText _edittextTitle;
        private EditText _edittextUrl;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            _sourceService = new SourceService(Constants.ConnectionString);
            
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_add);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Add RSS Item";
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var btnSave = FindViewById<Button>(Resource.Id.add_button_save);

            _titleTextInputLayout = FindViewById<TextInputLayout>(Resource.Id.add_edittext_title_layout);
            _urlTextInputLayout = FindViewById<TextInputLayout>(Resource.Id.add_edittext_url_layout);

            _edittextTitle = FindViewById<EditText>(Resource.Id.add_edittext_title);
            _edittextUrl = FindViewById<EditText>(Resource.Id.add_edittext_url);
            // var listviewSource = FindViewById<Button>(Resource.Id.listview_source);
            
            btnSave.Click += BtnSaveOnClick;
        }

        private void BtnSaveOnClick(object sender, EventArgs e)
        {
            try
            {
                var id = _sourceService.Add(_edittextTitle?.Text, _edittextUrl?.Text);

                var addResultIntent = new Intent();
                addResultIntent.PutExtra(nameof(Constants.CREATE_SOURCE_ID), id);
                SetResult(Result.Ok, addResultIntent);

                Finish();
            }
            catch (AddRssSourceTitleRequiredException)
            {
                _titleTextInputLayout.Error = "Le champs est requis";
            }
            catch (AddRssSourceUrlRequiredException)
            {
                _urlTextInputLayout.Error = "Le champs est requis ou invalide";
            }
            catch (Exception)
            {
                var alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert
                    .SetMessage("Une erreur s'est produite")
                    .SetPositiveButton("Ok", (s, _) => { })
                    .Show();
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem? item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}