﻿/*
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

			_transform = Matrix.Identity;

			//_transform *= Matrix.CreateTranslation(SimulationPosition.X, SimulationPosition.Y, 0);
			_transform *= Matrix.CreateScale(62);
			_transform *= _directionTransform;
			_transform *= Matrix.CreateScale(1.0f, 0.5f, 1.0f);

			_inverseTransform = Matrix.Invert(_transform);

		}

		public static Vector2 SimulationPointToWorld(Vector2 vec)
		{
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
