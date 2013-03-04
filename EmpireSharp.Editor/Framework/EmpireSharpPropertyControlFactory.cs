/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System.Windows;
using System.Windows.Data;
using EmpireSharp.Editor.Framework.Controls;
using PropertyTools.Wpf;

namespace EmpireSharp.Editor.Framework
{

	public class EmpireSharpPropertyControlFactory : Papyrus.Studio.Framework.PapyrusPropertyControlFactory
	{

		public override FrameworkElement CreateControl(PropertyItem property, PropertyControlFactoryOptions options)
		{

			if (property.ActualPropertyType == typeof (Data.SpriteClipReference))
				return CreateSpriteClipControl(property);
			
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



	}

}
