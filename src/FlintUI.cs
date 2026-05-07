using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FlintUI;

public class FlintUI : ResourceDictionary
{
	public FlintUI()
	{
		MergedDictionaries.Add(new ResourceDictionary
		{
			Source = new Uri("pack://application:,,,/FlintUI;component/FlintUI.xaml", UriKind.Absolute)
		});
	}
}
