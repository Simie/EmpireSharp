/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System.Globalization;
using FixMath.NET;

namespace EmpireSharp.Simulation
{

	/// <summary>
	/// A fixed-point math Vector2
	/// </summary>
	/// <remarks>Based on the MonoGame Vector2 implementation</remarks>
	public struct FixedVector2
	{

		#region Private Fields

		private static readonly FixedVector2 ZeroVector = new FixedVector2((Fix16)0);
		private static readonly FixedVector2 UnitVector = new FixedVector2((Fix16)1, (Fix16)1);
		private static readonly FixedVector2 UnitXVector = new FixedVector2((Fix16)1, (Fix16)0);
		private static readonly FixedVector2 UnitYVector = new FixedVector2((Fix16)0, (Fix16)1);

		#endregion Private Fields

		#region Public Fields

		public Fix16 X;

		public Fix16 Y;

		#endregion Public Fields

		#region Static Properties

		public static FixedVector2 Zero
		{
			get { return ZeroVector; }
		}

		public static FixedVector2 One
		{
			get { return UnitVector; }
		}

		public static FixedVector2 UnitX
		{
			get { return UnitXVector; }
		}

		public static FixedVector2 UnitY
		{
			get { return UnitYVector; }
		}

		#endregion Static Properties

		#region Constructors

		public FixedVector2(Fix16 x, Fix16 y)
		{
			X = x;
			Y = y;
		}

		public FixedVector2(Fix16 value)
		{
			X = Y = value;
		}

		public FixedVector2(int value) : this((Fix16)value) { }

		public FixedVector2(int x, int y) : this((Fix16)x, (Fix16)y) { }

		#endregion Constructors

		#region Public Methods

		public Fix16 Angle()
		{
			return AngleBetween(this, UnitX);
		}

		public static FixedVector2 FromAngle(Fix16 angle)
		{
			FixedVector2 direction = FixedVector2.Zero;
			direction.X = Fix16.Cos(angle);
			direction.Y = Fix16.Sin(angle);
			return direction;
		}

		public static Fix16 AngleBetween(FixedVector2 v1, FixedVector2 v2)
		{
			var dot = Dot(v1, v2);
			return Fix16.Acos(dot);
		}

		public static FixedVector2 Add(FixedVector2 value1, FixedVector2 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		public static void Add(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		public static FixedVector2 Clamp(FixedVector2 value1, FixedVector2 min, FixedVector2 max)
		{
			return new FixedVector2(
				Fix16.Clamp(value1.X, min.X, max.X),
				Fix16.Clamp(value1.Y, min.Y, max.Y));
		}

		public static void Clamp(ref FixedVector2 value1, ref FixedVector2 min, ref FixedVector2 max, out FixedVector2 result)
		{
			result = new FixedVector2(
				Fix16.Clamp(value1.X, min.X, max.X),
				Fix16.Clamp(value1.Y, min.Y, max.Y));
		}

		public static Fix16 Distance(FixedVector2 value1, FixedVector2 value2)
		{
			Fix16 v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return Fix16.Sqrt((v1 * v1) + (v2 * v2));
		}

		public static void Distance(ref FixedVector2 value1, ref FixedVector2 value2, out Fix16 result)
		{
			Fix16 v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = Fix16.Sqrt((v1 * v1) + (v2 * v2));
		}

		public static Fix16 DistanceSquared(FixedVector2 value1, FixedVector2 value2)
		{
			Fix16 v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (v1 * v1) + (v2 * v2);
		}

		public static void DistanceSquared(ref FixedVector2 value1, ref FixedVector2 value2, out Fix16 result)
		{
			Fix16 v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = (v1 * v1) + (v2 * v2);
		}

		public static FixedVector2 Divide(FixedVector2 value1, FixedVector2 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		public static void Divide(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		public static FixedVector2 Divide(FixedVector2 value1, Fix16 divider)
		{
			Fix16 factor = Fix16.One / divider;
			value1.X *= factor;
			value1.Y *= factor;
			return value1;
		}

		public static void Divide(ref FixedVector2 value1, Fix16 divider, out FixedVector2 result)
		{
			Fix16 factor = Fix16.One / divider;
			result.X = value1.X * factor;
			result.Y = value1.Y * factor;
		}

		public static Fix16 Dot(FixedVector2 value1, FixedVector2 value2)
		{
			return (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		public static void Dot(ref FixedVector2 value1, ref FixedVector2 value2, out Fix16 result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		public override bool Equals(object obj)
		{
			if (obj is FixedVector2) {
				return Equals((FixedVector2)obj);
			}

			return false;
		}

		public bool Equals(FixedVector2 other)
		{
			return (X == other.X) && (Y == other.Y);
		}

		public static FixedVector2 Reflect(FixedVector2 vector, FixedVector2 normal)
		{
			FixedVector2 result;
			Fix16 val = (Fix16)2 * ((vector.X * normal.X) + (vector.Y * normal.Y));
			result.X = vector.X - (normal.X * val);
			result.Y = vector.Y - (normal.Y * val);
			return result;
		}

		public static void Reflect(ref FixedVector2 vector, ref FixedVector2 normal, out FixedVector2 result)
		{
			Fix16 val = (Fix16)2 * ((vector.X * normal.X) + (vector.Y * normal.Y));
			result.X = vector.X - (normal.X * val);
			result.Y = vector.Y - (normal.Y * val);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode();
		}

		public Fix16 Length()
		{
			return Fix16.Sqrt((X * X) + (Y * Y));
		}

		public Fix16 LengthSquared()
		{
			return (X * X) + (Y * Y);
		}

		public static FixedVector2 Lerp(FixedVector2 value1, FixedVector2 value2, Fix16 amount)
		{
			return new FixedVector2(
				FixedMathHelper.Lerp(value1.X, value2.X, amount),
				FixedMathHelper.Lerp(value1.Y, value2.Y, amount));
		}

		public static void Lerp(ref FixedVector2 value1, ref FixedVector2 value2, Fix16 amount, out FixedVector2 result)
		{
			result = new FixedVector2(
				FixedMathHelper.Lerp(value1.X, value2.X, amount),
				FixedMathHelper.Lerp(value1.Y, value2.Y, amount));
		}

		public static FixedVector2 Max(FixedVector2 value1, FixedVector2 value2)
		{
			return new FixedVector2(value1.X > value2.X ? value1.X : value2.X,
							   value1.Y > value2.Y ? value1.Y : value2.Y);
		}

		public static void Max(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
		{
			result.X = value1.X > value2.X ? value1.X : value2.X;
			result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
		}

		public static FixedVector2 Min(FixedVector2 value1, FixedVector2 value2)
		{
			return new FixedVector2(value1.X < value2.X ? value1.X : value2.X,
							   value1.Y < value2.Y ? value1.Y : value2.Y);
		}

		public static void Min(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
		{
			result.X = value1.X < value2.X ? value1.X : value2.X;
			result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
		}

		public static FixedVector2 Multiply(FixedVector2 value1, FixedVector2 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		public static FixedVector2 Multiply(FixedVector2 value1, Fix16 scaleFactor)
		{
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			return value1;
		}

		public static void Multiply(ref FixedVector2 value1, Fix16 scaleFactor, out FixedVector2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		public static void Multiply(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		public static FixedVector2 Negate(FixedVector2 value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		public static void Negate(ref FixedVector2 value, out FixedVector2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		public void Normalize()
		{
			Fix16 val = Fix16.One / Fix16.Sqrt((X * X) + (Y * Y));
			X *= val;
			Y *= val;
		}

		public static FixedVector2 Normalize(FixedVector2 value)
		{
			Fix16 val = Fix16.One / Fix16.Sqrt((value.X * value.X) + (value.Y * value.Y));
			value.X *= val;
			value.Y *= val;
			return value;
		}

		public static void Normalize(ref FixedVector2 value, out FixedVector2 result)
		{
			Fix16 val = Fix16.One / Fix16.Sqrt((value.X * value.X) + (value.Y * value.Y));
			result.X = value.X * val;
			result.Y = value.Y * val;
		}

		public static FixedVector2 Subtract(FixedVector2 value1, FixedVector2 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		public static void Subtract(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}


		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1}}}", new object[] { 
				this.X.ToString(currentCulture), this.Y.ToString(currentCulture) });
		}

		#endregion Public Methods

		#region Operators

		public static FixedVector2 operator -(FixedVector2 value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}


		public static bool operator ==(FixedVector2 value1, FixedVector2 value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y;
		}


		public static bool operator !=(FixedVector2 value1, FixedVector2 value2)
		{
			return value1.X != value2.X || value1.Y != value2.Y;
		}


		public static FixedVector2 operator +(FixedVector2 value1, FixedVector2 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}


		public static FixedVector2 operator -(FixedVector2 value1, FixedVector2 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}


		public static FixedVector2 operator *(FixedVector2 value1, FixedVector2 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}


		public static FixedVector2 operator *(FixedVector2 value, Fix16 scaleFactor)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}


		public static FixedVector2 operator *(Fix16 scaleFactor, FixedVector2 value)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}


		public static FixedVector2 operator /(FixedVector2 value1, FixedVector2 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}


		public static FixedVector2 operator /(FixedVector2 value1, Fix16 divider)
		{
			Fix16 factor = Fix16.One / divider;
			value1.X *= factor;
			value1.Y *= factor;
			return value1;
		}

		#endregion Operators

	}

}
