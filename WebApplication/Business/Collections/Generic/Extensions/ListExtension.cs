using System;
using System.Collections.Generic;

namespace WebApplication.Business.Collections.Generic.Extensions
{
	public static class ListExtension
	{
		#region Methods

		public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
		{
			if(list == null)
				throw new ArgumentNullException(nameof(list));

			if(collection == null)
				throw new ArgumentNullException(nameof(collection));

			var concreteList = list as List<T>;

			// ReSharper disable UseNullPropagation
			// ReSharper disable PossibleMultipleEnumeration
			if(concreteList != null)
			{
				concreteList.AddRange(collection);
			}
			else
			{
				foreach(var item in collection)
				{
					list.Add(item);
				}
			}
			// ReSharper restore PossibleMultipleEnumeration
			// ReSharper restore UseNullPropagation
		}

		#endregion
	}
}