using System;
using System.Collections.Generic;
using System.Text;

namespace Fourty5.Core.Utilities
{
	public static partial class Extensions
	{
		public static bool IsNullOrZero(
			this int value)
		{
			return value == null || value == 0;
		}

		public static bool IsNotNullOrZero(
			this int value)
		{
			return !value.IsNullOrZero();
		}

		public static bool IsNullOrZero(
			this int? value)
		{
			return value == null || value == 0;
		}

		public static bool IsNotNullOrZero(
			this int? value)
		{
			return !value.IsNullOrZero();
		}

		public static bool IsNullOrZero(
			this decimal? value)
		{
			return value == null || value == 0;
		}

		public static bool IsNotNullOrZero(
			this decimal? value)
		{
			return !value.IsNullOrZero();
		}
	}
}