/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using Microsoft.Xna.Framework;

namespace EmpireSharp.Game.Modules.MonoGame
{

	/// <summary>
	/// Contains the camera location in simulation-space, and provides transforms to rendering to screen space.
	/// </summary>
	public class Camera
	{

		private Vector2 _simulationPosition;
		private Matrix _simulationTransform;
		private Matrix _inverseTransform;
		private float _scale;

		public bool IsDirty { get; set; }

		public Matrix Transform
		{
			get
			{
				if (IsDirty)
					Rebuild();
				return _simulationTransform;
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

		public float Scale
		{
			get { return _scale; }
			set { 
				_scale = value;
				IsDirty = true;
			}
		}

		public Vector2 TransformSimulationToView(Vector2 position)
		{

			Vector2.Transform(ref position, ref _simulationTransform, out position);

			return position;

		}
	
		public Vector2 TransformViewToSimulation(Vector2 position)
		{

			Vector2.Transform(ref position, ref _inverseTransform, out position);

			return position;

		}

		public void Rebuild()
		{

			var matrix = Matrix.Identity;

			matrix *= Matrix.CreateScale(Scale);
			matrix *= Matrix.CreateRotationZ(MathHelper.PiOver4);
			matrix *= Matrix.CreateScale(1.0f, 0.5f, 1.0f);

			_simulationTransform = matrix;
			Matrix.Invert(ref _simulationTransform, out _inverseTransform);
			IsDirty = false;

		}



	}

}
