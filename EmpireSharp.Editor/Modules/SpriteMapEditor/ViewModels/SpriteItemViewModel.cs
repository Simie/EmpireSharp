/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EmpireSharp.Data;

namespace EmpireSharp.Editor.Modules.SpriteMapEditor.ViewModels
{
	public class SpriteItemViewModel : Caliburn.Micro.PropertyChangedBase
	{

		public SpriteMap.SpriteMapItem Item
		{
			get { return _item; }
			private set { _item = value; }
		}

		private ImageSource _source;

		public ImageSource Source
		{
			get { return _source; }
			set
			{
				_source = value;
				NotifyOfPropertyChange(() => Source);
			}
		}

		private Int32Rect _sourceRect;

		public Int32Rect SourceRect
		{
			get { return _sourceRect; }
			set
			{
				_sourceRect = value;
				NotifyOfPropertyChange(() => SourceRect);
			}
		}

		private int _top;

		public int Top
		{
			get { return _top; }
			set
			{

				if (value + Height >= _internalSource.Height)
					throw new ArgumentOutOfRangeException();

				_top = value;
				NotifyOfPropertyChange(() => Top);
				UpdateSource();
			}
		}

		private int _left;

		public int Left
		{
			get { return _left; }
			set
			{

				if(value + Width >= _internalSource.Width)
					throw new ArgumentOutOfRangeException();

				_left = value;
				NotifyOfPropertyChange(() => Left);
				UpdateSource();
			}
		}

		private int _width;

		public int Width
		{
			get { return _width; }
			set
			{
				if (Left + value >= _internalSource.Width)
					throw new ArgumentOutOfRangeException();

				_width = value;
				NotifyOfPropertyChange(() => Width); 
				UpdateSource();
			}
		}

		private int _height;

		public int Height
		{
			get { return _height; }
			set
			{

				if (Top + value >= _internalSource.Height)
					throw new ArgumentOutOfRangeException();

				_height = value;
				NotifyOfPropertyChange(() => Height);
				UpdateSource();
			}
		}

		private BitmapSource _internalSource;
		private SpriteMap.SpriteMapItem _item;

		public SpriteItemViewModel(SpriteMap.SpriteMapItem item, BitmapSource map)
		{

			_internalSource = map;

			Item = item;

			_left = item.Rect.X;
			_top = item.Rect.Y;
			_width = item.Rect.Width;
			_height = item.Rect.Height;

			UpdateSource();

		}

		public override string ToString()
		{
			return base.ToString();
		}

		private void UpdateSource()
		{

			SourceRect = new Int32Rect(Left, Top, Width, Height);
			Source = new CroppedBitmap(_internalSource, SourceRect);

			_item.Rect = new SpriteMap.SpriteRect() {
				X = Left, Y = Top,
				Width = Width,
				Height = Height
			};

		}

	}
}