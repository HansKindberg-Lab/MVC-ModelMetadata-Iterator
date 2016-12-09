using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;
using WebApplication.Business.Initialization;

[assembly: AssemblyCompany("Hans Kindberg - open source")]
[assembly: AssemblyCopyright("None")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("1.0.0-alpha")]
[assembly: AssemblyProduct("WebApplication")]
[assembly: AssemblyTitle("WebApplication")]
[assembly: AssemblyVersion("1.0.0")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: Guid("d1f455eb-f128-4eb4-860c-d51f4137bc4d")]
[assembly: InternalsVisibleTo("Tests")]
[assembly: PreApplicationStartMethod(typeof(Initializer), "Start")]