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
using System.ComponentModel;
using System.Windows.Media;
using Caliburn.Micro;
using EmpireSharp.Data;
using Gemini.Framework;
using Papyrus.Studio.Framework;

namespace EmpireSharp.Editor.Modules.SpriteMapEditor.ViewModels
{

	class SpriteMapEditorViewModel : Document, IRecordDocument, ISaveAware
	{

		private ImageSource _image;

		public ImageSource Image
		{
			get { return _image; }
			set
			{
				_image = value;
				NotifyOfPropertyChange(() => Image);
			}
		}

		public override string DisplayName
		{
			get { return RecordModel.RecordID + (IsDirty ? "*" : ""); }
		}

		public SpriteMapRecordViewModel RecordModel { get; private set; }

		Papyrus.DataTypes.Record IRecordDocument.Record
		{
			get { return RecordModel.Record; }
		}

		public bool IsDirty
		{
			get { return RecordModel.IsDirty; }
		}

		public void Open(SpriteMap map)
		{
			
			RecordModel = new SpriteMapRecordViewModel();
			RecordModel.Open(map);

			RecordModel.PropertyChanged += RecordModelOnPropertyChanged;

		}

		public IEnumerable<IResult> Save()
		{

			RecordModel.Save();

			yield break;

		}

		private void RecordModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{

			switch (propertyChangedEventArgs.PropertyName) {
				case "RecordID":
					NotifyOfPropertyChange(() => DisplayName);
					break;
				case "IsDirty":
					NotifyOfPropertyChange(() => DisplayName);
					NotifyOfPropertyChange(() => IsDirty);
					break;

			}

		}

	}

}