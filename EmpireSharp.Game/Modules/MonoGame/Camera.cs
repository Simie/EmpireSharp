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
		private Matrix _simulationTransform;
		private Matrix _inverseTransform;
		private float _scale;

		private Matrix _directionTransform = Matrix.CreateRotationZ(MathHelper.PiOver4);
		private Matrix _inverseDirectionTransform = Matrix.Invert(Matrix.CreateRotationZ(MathHelper.PiOver4));

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

		public Matrix DirectionTransform
		{
			get { return _directionTransform; }
		}

		public Matrix InverseDirectionTransform { get { return _inverseDirectionTransform; } }

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
			return Vector2.Transform(position, Transform);
		}
	
		public Vector2 TransformViewToSimulation(Vector2 position)
		{
			return Vector2.Transform(position, InverseTransform);
		}

		public Vector2 TransformSimulationDirectionToView(Vector2 direction)
		{
			Vector2.Transform(ref direction, ref _directionTransform, out direction);
			return direction;
		}

		public Vector2 TransformViewDirectionToSimulation(Vector2 direction)
		{
			Vector2.Transform(ref direction, ref _inverseDirectionTransform, out direction);
			return direction;
		}

		public void Rebuild()
		{

			var matrix = Matrix.Identity;

			matrix *= Matrix.CreateTranslation(SimulationPosition.X, SimulationPosition.Y, 0);
			matrix *= Matrix.CreateScale(Scale);
			matrix *= DirectionTransform;
			matrix *= Matrix.CreateScale(1.0f, 0.5f, 1.0f);

			_simulationTransform = matrix;
			Matrix.Invert(ref _simulationTransform, out _inverseTransform);
			IsDirty = false;

		}



	}

}
