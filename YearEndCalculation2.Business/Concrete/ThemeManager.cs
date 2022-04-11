using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace YearEndCalculation.Business.Concrete
{
    public class ThemeManager
    {

		private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

		private const string RegistryValueName = "AppsUseLightTheme";

		private enum WindowsTheme
		{
			Light,
			Dark
		}

		public int WatchTheme()
		{
			int theme = 1;
			var currentUser = WindowsIdentity.GetCurrent();
			string query = string.Format(
				CultureInfo.InvariantCulture,
				@"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
				currentUser.User.Value,
				RegistryKeyPath.Replace(@"\", @"\\"),
				RegistryValueName);

			try
			{
				var watcher = new ManagementEventWatcher(query);
				watcher.EventArrived += (sender, args) =>
				{
					//WindowsTheme newWindowsTheme = GetWindowsTheme();
					theme = GetWindowsTheme();
					// React to new theme
				};

				// Start listening for events
				watcher.Start();
			}
			catch (Exception)
			{
				// This can fail on Windows 7
			}

			//WindowsTheme initialTheme = GetWindowsTheme();
			theme = GetWindowsTheme();
			return theme;
		}

		private static int GetWindowsTheme()
		{
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
			{
				object registryValueObject = key?.GetValue(RegistryValueName);
				if (registryValueObject == null)
				{
					//return WindowsTheme.Light;
					return 1;
				}

				int registryValue = (int)registryValueObject;

				//return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
				return registryValue;
			}
		}


	}
}
