/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EmpireSharp.Editor.Framework.Controls;
using EmpireSharp.Editor.Framework.Converters;
using PropertyTools.Wpf;

namespace EmpireSharp.Editor.Framework
{

	public class EmpireSharpPropertyControlFactory : Papyrus.Studio.Framework.PapyrusPropertyControlFactory
	{

		public override FrameworkElement CreateControl(PropertyItem property, PropertyControlFactoryOptions options)
		{

			if (property.ActualPropertyType == typeof (Data.SpriteClipReference))
				return CreateSpriteClipControl(property);

			if (property.ActualPropertyType == typeof (FixMath.NET.Fix16)) {

				return CreateFixedNumberControl(property);

			}

			return base.CreateControl(property, options);

		}

		public FrameworkElement CreateSpriteClipControl(PropertyItem item)
		{

			var c = new Controls.SpriteClipReferenceControl();

			var binding = item.CreateBinding();
			binding.Mode = BindingMode.TwoWay;
			c.SetBinding(SpriteClipReferenceControl.SpriteClipReferenceProperty, binding);

			return c;

		}

		public FrameworkElement CreateFixedNumberControl(PropertyItem property)
		{

			var tb = new TextBoxEx {
				IsReadOnly = property.IsReadOnly,
				BorderThickness = new Thickness(0),
				HorizontalContentAlignment = ConvertHorizontalAlignment(property.HorizontalAlignment),
				VerticalContentAlignment = VerticalAlignment.Center
			};
			var b1 = property.CreateBinding();
			b1.Converter = new FixedNumberConverter();
			tb.SetBinding(TextBox.TextProperty, b1);

			var c = new SpinControl {
				Maximum = double.PositiveInfinity,
				Minimum = 0,
				SmallChange = 0.1,
				LargeChange = 1,
				Content = tb
			};

			var b2 = property.CreateBinding();
			b2.Converter = new FixedNumberConverter();
			c.SetBinding(SpinControl.ValueProperty, b2);
			return c;

		}



	}

}
