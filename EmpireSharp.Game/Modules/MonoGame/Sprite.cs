/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System;
using EmpireSharp.Data;
using Microsoft.Xna.Framework;

namespace EmpireSharp.Game.Modules.MonoGame
{

	public class Sprite
	{

		/// <summary>
		/// Position of this sprite, in simulation coords
		/// </summary>
		public Vector2 SimPosition { get; set; }

		/// <summary>
		/// Rotation of this sprite, in simulation space
		/// </summary>
		public float SimRotation { get; set; }

		public Data.SpriteMap SpriteMap { get; private set; }

		public SpriteMap.Clip Clip { get; private set; }

		public Data.SpriteMap.SpriteMapItem ActiveItem { get; private set; }

		public Rectangle WorldRect { get; private set; }

		private float _totalTime; // Total time it will take for this clip to play.
		private float _animTime; // Time from start to the end of the animation, but not the end of the clip
		private float _elapsedTime; // Elapsed time since the animation started

		public void SetClip(SpriteMap map, string clipKey)
		{

			SpriteMap = map;
			Clip = map.Clips.Find(p => p.Key == clipKey);
			_elapsedTime = 0;

			_animTime = Clip.Items.Count*Clip.PlaySpeed;
			_totalTime = _animTime + Clip.RepeatDelay;

		}

		public void SetClip(SpriteClipReference clip)
		{
			SetClip(clip.SpriteMap, clip.ClipKey);
		}

		internal void Update(float dt)
		{

			_elapsedTime += dt;

			if (_elapsedTime > _totalTime)
				_elapsedTime = 0;

			var frameTime = MathHelper.Clamp(_elapsedTime, 0, _animTime);

			var frame = (int)Math.Floor(frameTime/Clip.PlaySpeed);

			ActiveItem = SpriteMap.Items[Clip.Items[frame]];

			UpdateCullingRect();

		}

		private void UpdateCullingRect()
		{

			var worldPos = Translate.SimulationPointToWorld(SimPosition);
			worldPos -= new Vector2(ActiveItem.Origin.Width, ActiveItem.Origin.Height);

			WorldRect = new Rectangle((int)worldPos.X, (int)worldPos.Y, ActiveItem.Rect.Width, ActiveItem.Rect.Height);
			WorldRect.Inflate(1,1);


		}

	}

}
