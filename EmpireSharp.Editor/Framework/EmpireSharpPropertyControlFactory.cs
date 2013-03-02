/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System.Windows;
using PropertyTools.Wpf;

namespace EmpireSharp.Editor.Framework
{

	public class EmpireSharpPropertyControlFactory : Papyrus.Studio.Framework.PapyrusPropertyControlFactory
	{

		public override FrameworkElement CreateControl(PropertyItem property, PropertyControlFactoryOptions options)
		{

			//if (property.ActualPropertyType == typeof (Data.SpriteMap))
			//	return CreateSpriteEditorControl(property);
			
			return base.CreateControl(property, options);

		}

		/*public FrameworkElement CreateSpriteEditorControl(PropertyItem item)
		{

			var viewModel = Caliburn.Micro.IoC.Get<Modules.SpriteMapEditor.ViewModels.SpriteMapEditorView>();
			viewModel.op

		}*/



	}

}
