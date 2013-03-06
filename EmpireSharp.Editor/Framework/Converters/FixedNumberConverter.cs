/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EmpireSharp.Editor.Framework.Converters
{
	class FixedNumberConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{

			if (!(value is FixMath.NET.Fix16))
				return Binding.DoNothing;

			var num = (FixMath.NET.Fix16) value;

			if (targetType == typeof (double) || targetType == typeof(object)) {
				return (double) num;
			}

			if (targetType == typeof (float)) {
				return (float) num;
			}

			if (targetType == typeof (decimal)) {
				return (decimal) num;
			}

			if (targetType == typeof (string)) {
				return num.ToString(culture);
			}

			return value;

		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{

			if (targetType != typeof (FixMath.NET.Fix16))
				return Binding.DoNothing;

			if (value is string) {
				return (FixMath.NET.Fix16) double.Parse((string) value);
			}

			var val = (double) value;

			return (FixMath.NET.Fix16) val;

		}

	}
}
