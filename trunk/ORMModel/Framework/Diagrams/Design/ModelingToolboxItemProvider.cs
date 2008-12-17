#region Common Public License Copyright Notice
/**************************************************************************\
* Natural Object-Role Modeling Architect for Visual Studio                 *
*                                                                          *
* Copyright � Matthew Curland. All rights reserved.                        *
*                                                                          *
* The use and distribution terms for this software are covered by the      *
* Common Public License 1.0 (http://opensource.org/licenses/cpl) which     *
* can be found in the file CPL.txt at the root of this distribution.       *
* By using this software in any fashion, you are agreeing to be bound by   *
* the terms of this license.                                               *
*                                                                          *
* You must not remove this notice, or any other, from this software.       *
\**************************************************************************/
#endregion

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Design;

namespace Neumont.Tools.Modeling.Diagrams.Design
{
	#region IModelingToolboxItemProvider interface
	/// <summary>
	/// An interface used to initialize toolbox items. Implemented by a class returned by the
	/// <see cref="ModelingToolboxItemProviderAttribute"/>.
	/// </summary>
	public interface IModelingToolboxItemProvider
	{
		/// <summary>
		/// Create the toolbox items used by the diagrams in a <see cref="DomainModel"/>.
		/// Generally, this will defer directly to the ToolboxHelper code generated by the
		/// DSLTools templates. Additional custom attributes can also be added using helper
		/// methods from the <see cref="ToolboxHelperUtility"/> class.
		/// </summary>
		/// <param name="serviceProvider">The <see cref="IServiceProvider"/> instance</param>
		/// <returns>Initialized <see cref="ModelingToolboxItem"/> elements</returns>
		IList<ModelingToolboxItem> CreateToolboxItems(IServiceProvider serviceProvider);
		/// <summary>
		/// Get a fixed item offset to add to the positions of all toolbox items. Allows toolbox items
		/// from different domain models to appear in bunches on the same toolbox tab.
		/// </summary>
		int ToolboxItemPositionOffset { get;}
	}
	#endregion // IModelingToolboxItemProvider interface
	#region ModelingToolboxItemProviderAttribute class
	/// <summary>
	/// An attribute to attach to a <see cref="DomainModel"/> class to
	/// provide toolbox initialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class ModelingToolboxItemProviderAttribute : Attribute
	{
		private Type myToolboxItemProviderType;
		private string myNestedToolboxInitializerName;
		/// <summary>
		/// Associate an <see cref="IModelingToolboxItemProvider"/> implementation with a <see cref="DomainModel"/>-derived class
		/// </summary>
		/// <param name="providerType">A type that implements <see cref="IModelingToolboxItemProvider"/>
		/// and has a parameterless constructor</param>
		public ModelingToolboxItemProviderAttribute(Type providerType)
		{
			myToolboxItemProviderType = providerType;
		}
		/// <summary>
		/// Associate an <see cref="IModelingToolboxItemProvider"/> implementation with a <see cref="DomainModel"/>-derived class
		/// </summary>
		/// <param name="nestedProviderTypeName">The name of a nested class in the <see cref="DomainModel"/> that
		/// implements the  <see cref="IModelingToolboxItemProvider"/> and has a parameterless constructor</param>
		public ModelingToolboxItemProviderAttribute(string nestedProviderTypeName)
		{
			myNestedToolboxInitializerName = nestedProviderTypeName;
		}
		/// <summary>
		/// Create a list of <see cref="ModelingToolboxItem"/>s from a <see cref="IServiceProvider"/>
		/// </summary>
		/// <param name="serviceProvider">The context <see cref="IServiceProvider"/></param>
		/// <param name="domainModelType">The type of the <see cref="DomainModel"/> associated with this attribute</param>
		/// <returns>List of <see cref="ModelingToolboxItem"/> elements.</returns>
		public IList<ModelingToolboxItem> CreateToolboxItems(IServiceProvider serviceProvider, Type domainModelType)
		{
			Type createType = myToolboxItemProviderType;
			if (createType == null)
			{
				string[] nestedTypeNames = myNestedToolboxInitializerName.Split(new char[] { '.', '+' }, StringSplitOptions.RemoveEmptyEntries);
				createType = domainModelType;
				for (int i = 0; i < nestedTypeNames.Length; ++i)
				{
					createType = createType.GetNestedType(nestedTypeNames[i], BindingFlags.NonPublic | BindingFlags.Public);
				}
			}
			if (createType != null)
			{
				IModelingToolboxItemProvider initializer = (IModelingToolboxItemProvider)Activator.CreateInstance(createType, true);
				IList<ModelingToolboxItem> items = initializer.CreateToolboxItems(serviceProvider);
				int itemCount;
				if (items != null &&
					0 != (itemCount = items.Count))
				{
					int offsetAdjustment = initializer.ToolboxItemPositionOffset;
					if (offsetAdjustment != 0)
					{
						for (int i = 0; i < itemCount; ++i)
						{
							ModelingToolboxItem item = items[i];
							items[i] = new ModelingToolboxItem(item.Id, item.Position + offsetAdjustment, item.DisplayName, item.Bitmap, item.TabNameId, item.TabName, item.ContextSensitiveHelpKeyword, item.Description, item.Prototype, item.Filter);
						}
					}
					return items;
				}
			}
			return null;
		}
	}
	#endregion // ModelingToolboxItemProviderAttribute class
	#region ToolboxHelperUtility class
	/// <summary>
	/// A set of helper methods designed for use in implementing <see cref="IModelingToolboxItemProvider"/>
	/// </summary>
	public static class ToolboxHelperUtility
	{
		/// <summary>
		/// Given a list of <see cref="ModelingToolboxItem"/>s, create a dictionary
		/// mapping the item identifiers to indices in the list.
		/// </summary>
		public static IDictionary<string, int> CreateIdentifierToIndexMap(IList<ModelingToolboxItem> toolboxItems)
		{
			Dictionary<string, int> retVal = new Dictionary<string, int>(toolboxItems.Count);
			for (int i = 0; i < toolboxItems.Count; i++)
			{
				retVal[toolboxItems[i].Id] = i;
			}
			return retVal;
		}
		/// <summary>
		/// Add a filter to the specified <see cref="ModelingToolboxItem"/>
		/// </summary>
		/// <param name="items">A list of existing items</param>
		/// <param name="itemIndexDictionary">A dictionary mapping from the item identifier to an index in the
		/// <paramref name="items"/> list. The dictionary should be created with <see cref="CreateIdentifierToIndexMap"/></param>
		/// <param name="itemId">The identifer of the item to modify</param>
		/// <param name="filterAttribute">The filter attribute to add</param>
		public static void AddFilterAttribute(IList<ModelingToolboxItem> items, IDictionary<string, int> itemIndexDictionary, string itemId, ToolboxItemFilterAttribute filterAttribute)
		{
			int itemIndex;
			if (itemIndexDictionary.TryGetValue(itemId, out itemIndex))
			{
				ModelingToolboxItem itemBase = items[itemIndex];
				ICollection baseFilters = itemBase.Filter;
				int baseFilterCount = (baseFilters != null) ? baseFilters.Count : 0;
				ToolboxItemFilterAttribute[] newFilters = new ToolboxItemFilterAttribute[baseFilterCount + 1];
				if (baseFilterCount != 0)
				{
					baseFilters.CopyTo(newFilters, 0);
				}
				newFilters[baseFilterCount] = filterAttribute;
				itemBase.Filter = newFilters;
			}
		}
		/// <summary>
		/// Remove a filter from the specified <see cref="ModelingToolboxItem"/>
		/// </summary>
		/// <param name="items">A list of existing items</param>
		/// <param name="itemIndexDictionary">A dictionary mapping from the item identifier to an index in the
		/// <paramref name="items"/> list. The dictionary should be created with <see cref="CreateIdentifierToIndexMap"/></param>
		/// <param name="itemId">The identifer of the item to modify</param>
		/// <param name="filterString">The filter string for the filter to remove</param>
		public static void RemoveFilterAttribute(IList<ModelingToolboxItem> items, IDictionary<string, int> itemIndexDictionary, string itemId, string filterString)
		{
			int itemIndex;
			if (itemIndexDictionary.TryGetValue(itemId, out itemIndex))
			{
				ModelingToolboxItem itemBase = items[itemIndex];
				ICollection baseFilters = itemBase.Filter;
				int baseFilterCount = baseFilters.Count;
				if (baseFilterCount != 0)
				{
					int removeCount = 0;
					foreach (object filter in baseFilters)
					{
						ToolboxItemFilterAttribute filterAttribute = filter as ToolboxItemFilterAttribute;
						if (filterAttribute == null || filterAttribute.FilterString == filterString)
						{
							++removeCount;
						}
					}
					if (removeCount == baseFilterCount)
					{
						itemBase.Filter = null;
					}
					else
					{
						ToolboxItemFilterAttribute[] newFilters = new ToolboxItemFilterAttribute[baseFilterCount - removeCount];
						int nextIndex = 0;
						foreach (object filter in baseFilters)
						{
							ToolboxItemFilterAttribute filterAttribute = filter as ToolboxItemFilterAttribute;
							if (filterAttribute != null && filterAttribute.FilterString != filterString)
							{
								newFilters[nextIndex] = filterAttribute;
								++nextIndex;
							}
						}
						itemBase.Filter = newFilters;
					}
				}
			}
		}
	}
	#endregion // ToolboxHelperUtility class
}
