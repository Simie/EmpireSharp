/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System;
using Microsoft.Xna.Framework;

namespace EmpireSharp.Game.Modules.MonoGame
{

	/// <summary>
	/// Contains the camera location in simulation-space, and provides transforms to rendering to screen space.
	/// </summary>
	public class Camera
	{

		private Vector2 _simulationPosition = Vector2.Zero;
		private Matrix _transform;
		private Matrix _inverseTransform;
		private float _zoom = 1;

		private Rectangle _screenRect;
		public bool IsDirty { get; set; }

		public Rectangle Screen 
		{ 
			set {
				_screenRect = value;
				IsDirty = true; 
			}
			get { return _screenRect; }
		}

		/// <summary>
		/// Transform from the simulation space to world space.
		/// </summary>
		public Matrix Transform
		{
			get
			{
				if (IsDirty)
					Rebuild();
				return _transform;
			}
		}

		public Matrix InverseTransform
		{
			get
			{
				if(IsDirty)
					Rebuild();
				return _inverseTransform;
			}
		}

		/// <summary>
		/// Position of the camera in simulation space.
		/// </summary>
		public Vector2 SimulationPosition
		{

			get { return _simulationPosition; }
			set { 

				_simulationPosition = value;
				IsDirty = true;

			}

		}

		public float Zoom
		{
			get { return _zoom; }
			set {
				_zoom = MathHelper.Clamp(value, 0.001f, float.MaxValue);
				IsDirty = true;
			}
		}

		public Vector2 TransformWorldtoScreen(Vector2 position)
		{
			return Vector2.Transform(position, Transform);
		}
	
		public Vector2 TransformScreenToWorld(Vector2 position)
		{
			return Vector2.Transform(position, InverseTransform);
		}

		public void Rebuild()
		{

			var viewMatrix = Matrix.Identity;
			var cameraPos = Translate.SimulationPointToWorld(SimulationPosition);
			viewMatrix *= Matrix.CreateTranslation(cameraPos.X, cameraPos.Y, 0);
			viewMatrix *= Matrix.CreateScale(Zoom);
			viewMatrix *= Matrix.CreateTranslation(Screen.Width*0.5f, Screen.Height*0.5f, 0);

			_transform = viewMatrix;
			Matrix.Invert(ref _transform, out _inverseTransform);

			IsDirty = false;

		}



	}

}
