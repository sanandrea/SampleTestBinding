using Android.App;
using Android.Widget;
using Android.OS;
using System;
//Wrapper Lib
using Com.Example.Thineventswrapper;
using Com.Example.Thineventswrapper.Wrapper.Events.Data;
using Com.Example.Thineventswrapper.Wrapper.Events;
//Original Lib
using Com.Example.Awesomelibrary.AP.Models;

namespace DemoApp
{
	[Activity(Label = "DemoApp", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, IPlayerEventListenerWrapper
	{
		int count = 1;
		private Button button;
		private IListenerRegistrationWrapper regHandler;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate { button.Text = $"{count++} clicks!"; };

			//Create the PlayerWrapper
			PlayerWrapper pv = new PlayerWrapper(null);

			//Register to events from the Wrapper Lib
			regHandler = pv.AsEventDispatcherWrapper().AddEventListener(this);
			var mv = new MediaLoadEventWrapper(null);
			//Read settings from the Original Lib
			PlayerSettings settings = pv.AsSettings();
			if (settings.Protection == DRMProtection.PlayReady) {
				Console.WriteLine("PlayerReady protection");
			}
				
		}

		protected override void OnDestroy()
		{
			regHandler.Remove();
			base.OnDestroy();
		}
		public void OnPlayerEvent(MediaUnLoadEventWrapper p0)
		{
			
		}
		
		public void OnPlayerEvent(MediaLoadEventWrapper p0)
		{
			RunOnUiThread(() =>
			{
				button.Text = "Loaded " + p0.Value;
			});
		}
	}
}

