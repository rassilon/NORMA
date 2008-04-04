using System;
using System.Reflection;
using Neumont.Tools.ORM.SDK.TestEngine;
using Neumont.Tools.ORM.ObjectModel;
using Microsoft.VisualStudio.Modeling;
using NUnit.Framework;
using NUnitCategory = NUnit.Framework.CategoryAttribute;
using Neumont.Tools.Modeling.Design;
namespace RelationalTests.FullRegeneration
{
	/// <summary>
	/// Tests to verify stability of full generating of abstraction
	/// and relational models from an existing ORM file.
	/// </summary>
	[ORMTestFixture]
	[TestFixture(Description="Relational model initial generation")]
	public class Tests
	{
		#region Boilerplate code for ORMTestDriver integration
		private IORMToolServices myServices;
		private IORMToolTestServices myTestServices;
		public Tests(IORMToolServices services)
		{
			// Cache the services for future use
			myServices = services;
			// The services from the test tool can be retrieved
			// from the code services service provider.
			myTestServices = (IORMToolTestServices)services.ServiceProvider.GetService(typeof(IORMToolTestServices));
		}
		#endregion // Boilerplate code for ORMTestDriver integration
		#region Additional boilerplate code for NUnit integration
		public Tests() : this(Suite.CreateServices()) { }
		#endregion // Additional boilerplate code for NUnit integration
		#region Relational Load Tests
		/// <summary>
		/// NUnit
		/// </summary>
		[Test(Description = "Relational Load")]
		[NUnitCategory("Relational")]
		[NUnitCategory("FullRegeneration")]
		public void Test1()
		{
			// Forward the call
			Suite.RunNUnitTest(this, myTestServices);
		}
		/// <summary>
		/// Test full regeneration of a medium-sized ORM model. This
		/// is an ORM model with the Abstraction/ConceptualDatabase
		/// extension hand-added to the top of the file. All other
		/// required extensions should load automatically.
		/// </summary>
		[ORMTest("Relational", "FullRegeneration")]
		public void Test1(Store store)
		{
		}
		/// <summary>
		/// NUnit
		/// </summary>
		[Test(Description = "Relational Load")]
		[NUnitCategory("Relational")]
		[NUnitCategory("FullRegeneration")]
		public void Test2()
		{
			// Forward the call
			Suite.RunNUnitTest(this, myTestServices);
		}
		/// <summary>
		/// Test full regeneration of a simple model. This model
		/// used to generate an extra table for the cmValue valueType.
		/// The load file is an ORM model with the Abstraction/ConceptualDatabase
		/// extension hand-added to the top of the file. All other
		/// required extensions should load automatically.
		/// </summary>
		[ORMTest("Relational", "FullRegeneration")]
		public void Test2(Store store)
		{
			myTestServices.Compare(store, (MethodInfo)MethodInfo.GetCurrentMethod(), "WithIndependent");
			ORMModel model = store.ElementDirectory.FindElements<ORMModel>()[0];
			ObjectType objectType = (ObjectType)model.ObjectTypesDictionary.GetElement("SomeLength").FirstElement;
			DomainTypeDescriptor.CreatePropertyDescriptor(objectType, ObjectType.IsIndependentDomainPropertyId).SetValue(objectType, false);
		}
		/// <summary>
		/// NUnit
		/// </summary>
		[Test(Description = "Relational Load")]
		[NUnitCategory("Relational")]
		[NUnitCategory("FullRegeneration")]
		public void Test3()
		{
			// Forward the call
			Suite.RunNUnitTest(this, myTestServices);
		}
		/// <summary>
		/// Test full regeneration of a model featuring non-absorbed composite
		/// preferred identifiers.
		/// </summary>
		[ORMTest("Relational", "FullRegeneration")]
		public void Test3(Store store)
		{
			myTestServices.Compare(store, (MethodInfo)MethodInfo.GetCurrentMethod(), "OriginalOrder");
			ORMModel model = store.ElementDirectory.FindElements<ORMModel>()[0];
			ObjectType objectType = (ObjectType)model.ObjectTypesDictionary.GetElement("A").FirstElement;
			myTestServices.LogMessage("Reorder columns on composite pid with no referenced composite elements");
			using (Transaction t = store.TransactionManager.BeginTransaction("Reorder columns on pid with no referenced composite elements"))
			{
				objectType.PreferredIdentifier.RoleCollection.Move(0, 1);
				t.Commit();
			}
			objectType = (ObjectType)model.ObjectTypesDictionary.GetElement("E").FirstElement;
			myTestServices.Compare(store, (MethodInfo)MethodInfo.GetCurrentMethod(), "AfterReorder");

			myTestServices.LogMessage("Reorder columns on composite pid with referenced composite elements");
			using (Transaction t = store.TransactionManager.BeginTransaction("Reorder columns on pid with no referenced composite elements"))
			{
				objectType.PreferredIdentifier.RoleCollection.Move(0, 1);
				t.Commit();
			}
		}
		#endregion // Relational Load tests
	}
}
