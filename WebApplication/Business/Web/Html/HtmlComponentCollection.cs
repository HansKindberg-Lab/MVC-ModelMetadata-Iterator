using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace WebApplication.Business.Web.Html
{
	public class HtmlComponentCollection<T> : IList<T> where T : IHtmlNode
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

				var htmlChild = this.CastToHtmlChild(value);

				this.InternalList[index] = value;

				htmlChild.ParentInternal = this.Parent;
			}
		}

		protected internal virtual IHtmlContainer Parent { get; }

		#endregion

		#region Methods

		public virtual void Add(T item)
		{
			if(item == null)
				throw new ArgumentNullException(nameof(item));

			var htmlChild = this.CastToHtmlChild(item);

			this.InternalList.Add(item);

			htmlChild.ParentInternal = this.Parent;
		}

		protected internal virtual IHtmlChild CastToHtmlChild(T htmlNode)
		{
			if(htmlNode == null)
				throw new ArgumentNullException(nameof(htmlNode));

			try
			{
				return (IHtmlChild) htmlNode;
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The html-node of type \"{0}\" must implement \"{1}\".", typeof(T), typeof(IHtmlChild)), exception);
			}
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

			var htmlChild = this.CastToHtmlChild(item);

			this.InternalList.Insert(index, item);

			htmlChild.ParentInternal = this.Parent;
		}

		public virtual bool Remove(T item)
		{
			var htmlChild = this.CastToHtmlChild(item);

			var removed = this.InternalList.Remove(item);

			if(removed)
				htmlChild.ParentInternal = null;

			return removed;
		}

		public virtual void RemoveAt(int index)
		{
			var item = this[index];

			var htmlChild = this.CastToHtmlChild(item);

			this.InternalList.RemoveAt(index);

			htmlChild.ParentInternal = null;
		}

		#endregion
	}
}