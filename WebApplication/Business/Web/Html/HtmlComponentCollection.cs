using System;
using System.Collections;
using System.Collections.Generic;

namespace WebApplication.Business.Web.Html
{
	public class HtmlComponentCollection<T> : IList<T> where T : IHtmlComponent
	{
		#region Constructors

		public HtmlComponentCollection(IHtmlContainer parent)
		{
			if(parent == null)
				throw new ArgumentNullException(nameof(parent));

			this.Parent = parent;
		}

		#endregion

		#region Properties

		public virtual int Count => this.InternalList.Count;
		protected internal virtual IList<T> InternalList { get; } = new List<T>();
		public virtual bool IsReadOnly => this.InternalList.IsReadOnly;

		public virtual T this[int index]
		{
			get { return this.InternalList[index]; }
			set
			{
				if(value == null)
					throw new ArgumentNullException(nameof(value));

				this.InternalList[index] = value;

				value.Parent = this.Parent;
			}
		}

		protected internal virtual IHtmlContainer Parent { get; }

		#endregion

		#region Methods

		public virtual void Add(T item)
		{
			if(item == null)
				throw new ArgumentNullException(nameof(item));

			this.InternalList.Add(item);

			item.Parent = this.Parent;
		}

		public virtual void Clear()
		{
			this.InternalList.Clear();
		}

		public virtual bool Contains(T item)
		{
			return this.InternalList.Contains(item);
		}

		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			this.InternalList.CopyTo(array, arrayIndex);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual IEnumerator<T> GetEnumerator()
		{
			return this.InternalList.GetEnumerator();
		}

		public virtual int IndexOf(T item)
		{
			return this.InternalList.IndexOf(item);
		}

		public virtual void Insert(int index, T item)
		{
			if(item == null)
				throw new ArgumentNullException(nameof(item));

			this.InternalList.Insert(index, item);

			item.Parent = this.Parent;
		}

		public virtual bool Remove(T item)
		{
			var removed = this.InternalList.Remove(item);

			if(removed)
				item.Parent = null;

			return removed;
		}

		public virtual void RemoveAt(int index)
		{
			var item = this[index];

			this.InternalList.RemoveAt(index);

			item.Parent = null;
		}

		#endregion
	}
}