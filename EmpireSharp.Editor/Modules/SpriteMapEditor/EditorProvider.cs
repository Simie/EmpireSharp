/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using EmpireSharp.Data;
using EmpireSharp.Editor.Modules.SpriteMapEditor.ViewModels;
using Papyrus.DataTypes;
using Papyrus.Studio.Framework;
using Papyrus.Studio.Framework.Services;

namespace EmpireSharp.Editor.Modules.SpriteMapEditor
{

	[Export(typeof(IRecordEditorProvider))]
	class EditorProvider : IRecordEditorProvider
	{

		public Type PrimaryType { get { return typeof (Data.SpriteMap); } }

		public bool Handles(Record record)
		{
			return record is Data.SpriteMap;
		}

		public IRecordDocument Create(Record record)
		{

			var editor = new ViewModels.SpriteMapEditorViewModel();
			IoC.BuildUp(editor);
			editor.Open((SpriteMap)record);

			return editor;

		}

		public bool IsInstanceOf(IRecordDocument existingEditor)
		{
			return existingEditor is SpriteMapEditorViewModel;
		}

	}
}
