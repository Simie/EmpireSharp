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
	public static class Translate
	{

		private static Matrix _transform;
		private static Matrix _inverseTransform;

		private static Matrix _directionTransform = Matrix.CreateRotationZ(MathHelper.PiOver4);
		private static Matrix _inverseDirectionTransform = Matrix.Invert(Matrix.CreateRotationZ(MathHelper.PiOver4));

		static Translate()
		{

			Rebuild();

		}

		public static void Rebuild()
		{

			_transform = Matrix.Identity;
			const float scale = 67.7f;
			_transform *= Matrix.CreateScale(scale, scale, 1f);
			_transform *= _directionTransform;
			_transform *= Matrix.CreateScale(1.0f, 0.5f, 1);
			//_transform *= Matrix.CreateScale(48.5f, 24.5f, 1.0f); // Scale by half size of a tile


			_inverseTransform = Matrix.Invert(_transform);

		}

		public static Vector2 SimulationPointToWorld(Vector2 vec)
		{
			Rebuild();
			return Vector2.Transform(vec, _transform);
		}

		public static Vector2 WorldPointToSimulation(Vector2 vec)
		{
			return Vector2.Transform(vec, _inverseTransform);
		}

		public static Vector2 SimulationDirectionToWorld(Vector2 vec)
		{
			return Vector2.Transform(vec, _directionTransform);
		}

		public static Vector2 WorldDirectionToSimulation(Vector2 vec)
		{
			return Vector2.Transform(vec, _inverseDirectionTransform);
		}

	}
}
