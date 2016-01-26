using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Floe.Net;

namespace Floe.UI
{
	public partial class App : Application
	{
		private static HashSet<string> _uncluttered = new HashSet<string>();

		public static void AddUncluttered(string channelName)
		{
			string element = channelName.ToUpperInvariant();
			if (!_uncluttered.Contains(element))
			{
				_uncluttered.Add(element);
			}
			SaveUncluttered();
		}

		public static bool RemoveUncluttered(string channelName)
		{
			string element = channelName.ToUpperInvariant();
			if (_uncluttered.Remove(element))
			{
				SaveUncluttered();
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool ExistsInUnclutteredList(IrcTarget target)
		{
			return _uncluttered.Contains(target.Name.ToUpperInvariant());
		}

		public static bool IsUncluttered(IrcTarget target)
		{
			if (target == null || !target.IsChannel)
			{
				return false;
			}

			if(App.Settings.Current.Formatting.HideAllClutter)
			{
				return true;
			}

			return ExistsInUnclutteredList(target);
		}

		private static void LoadUncluttered()
		{
			_uncluttered.Clear();
			foreach (var i in App.Settings.Current.Uncluttered.Split(','))
			{
				if (i.Trim().Length < 1)
				{
					continue;
				}
				_uncluttered.Add(i.ToUpperInvariant());
			}
		}

		private static void SaveUncluttered()
		{
			App.Settings.Current.Uncluttered = string.Join(",",_uncluttered);
		}
	}
}
