using System;
using System.Web.Optimization;

namespace WebApplication.Business.Configuration
{
	public class BundleConfiguration : IApplicationConfiguration
	{
		#region Methods

		public virtual void Configure()
		{
			this.RegisterBundles(BundleTable.Bundles);
		}

		protected internal virtual void RegisterBundles(BundleCollection bundles)
		{
			if(bundles == null)
				throw new ArgumentNullException(nameof(bundles));

			BundleTable.EnableOptimizations = true;
			BundleTable.Bundles.FileExtensionReplacementList.Clear();

			bundles.Add(new StyleBundle("~/Style-bundle")
					.Include("~/Style/bootstrap.css", new CssRewriteUrlTransform())
					.Include("~/Style/bootstrap-flex.css", new CssRewriteUrlTransform())
					.Include("~/Style/bootstrap-grid.css", new CssRewriteUrlTransform())
					.Include("~/Style/bootstrap-reboot.css", new CssRewriteUrlTransform())
					.Include("~/Style/Main.css", new CssRewriteUrlTransform())
			);

			bundles.Add(new ScriptBundle("~/Script-bundle")
					.Include("~/Scripts/jquery-3.1.1.js")
					.Include("~/Scripts/bootstrap.js")
			);
		}

		#endregion
	}
}