/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using EmpireSharp.Data;
using Papyrus;
using PropertyTools.Wpf;

namespace EmpireSharp.Editor.Framework.Controls
{

	public class SpriteClipReferenceControl : Control
	{

		public static readonly DependencyProperty SpriteClipReferenceProperty =
			DependencyProperty.Register("SpriteClipReference", typeof (Data.SpriteClipReference), typeof (SpriteClipReferenceControl), new PropertyMetadata(default(Data.SpriteClipReference), SpriteClipChangedCallback));


		public Data.SpriteClipReference SpriteClipReference
		{
			get { return (Data.SpriteClipReference) GetValue(SpriteClipReferenceProperty); }
			set { SetValue(SpriteClipReferenceProperty, value); }
		}


		public static readonly DependencyProperty ClipSourceProperty =
			DependencyProperty.Register("ClipSource", typeof (BindableCollection<string>), typeof (SpriteClipReferenceControl), new PropertyMetadata(default(BindableCollection<string>)));

		public BindableCollection<string> ClipSource
		{
			get { return (BindableCollection<string>) GetValue(ClipSourceProperty); }
			set { SetValue(ClipSourceProperty, value); }
		}

		public ICommand RefreshCommand { get; set; }

		public SpriteClipReferenceControl()
		{

			RefreshCommand = new DelegateCommand(RefreshClips);

		}

		protected void SpriteClipChanged(DependencyPropertyChangedEventArgs args)
		{
			
			RefreshClips();

		}

		protected void RefreshClips()
		{

			if(ClipSource == null)
				ClipSource = new BindableCollection<string>();

			ClipSource.Clear();

			if (SpriteClipReference.SpriteMap.IsValid) {

				ClipSource.AddRange(SpriteClipReference.SpriteMap.Value.Clips.Select(p => p.Key));

				if (!ClipSource.Contains(SpriteClipReference.ClipKey))
					SpriteClipReference.ClipKey = null;

			}


		}

		private static void SpriteClipChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			((SpriteClipReferenceControl)dependencyObject).SpriteClipChanged(dependencyPropertyChangedEventArgs);
		}


	}

}
