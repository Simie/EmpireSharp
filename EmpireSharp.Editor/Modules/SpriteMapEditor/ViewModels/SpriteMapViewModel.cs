//Sample license text.

using System.Windows.Media;
using EmpireSharp.Data;

namespace EmpireSharp.Editor.Modules.SpriteMapEditor.ViewModels
{

	public class SpriteMapViewModel : Papyrus.Studio.Framework.RecordViewModel<SpriteMap>
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

		public override void Open(SpriteMap record)
		{
			base.Open(record);
		}

	}

}