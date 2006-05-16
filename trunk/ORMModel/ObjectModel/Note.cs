#region Common Public License Copyright Notice
/**************************************************************************\
* Neumont Object-Role Modeling Architect for Visual Studio                 *
*                                                                          *
* Copyright � Neumont University. All rights reserved.                     *
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
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagnostics;
using System.ComponentModel;

namespace Neumont.Tools.ORM.ObjectModel
{
	#region INoteOwner interface
	/// <summary>
	/// An interface to implement for any Note owner
	/// </summary>
	public interface INoteOwner
	{
		/// <summary>
		/// Return the name of the note owner
		/// </summary>
		string Name { get;}
		/// <summary>
		/// The current text value of a note
		/// </summary>
		string NoteText { get;}
		/// <summary>
		/// A property descriptor corresponding to the NoteText property
		/// </summary>
		PropertyDescriptor NoteTextPropertyDescriptor { get;}
	}
	#endregion // INoteOwner interface
	public partial class Note
	{
		#region NoteChangeRule class
		/// <summary>
		/// Enforces Change Rules
		/// </summary>
		[RuleOn(typeof(Note))]
		private class NoteChangeRule : ChangeRule
		{
			/// <summary>
			/// Handle custom storage attributes on Note
			/// </summary>
			public override void ElementAttributeChanged(ElementAttributeChangedEventArgs e)
			{
				Guid attributeGuid = e.MetaAttribute.Id;
				// If what was changed was the note's Text property,
				if (attributeGuid == Note.TextMetaAttributeGuid)
				{
					// cache the value.
					string newText = (string)e.NewValue;
					// Get the note
					Note note = e.ModelElement as Note;
					// and if the text is blank, 
					if (string.IsNullOrEmpty(newText))
					{
						// get rid of the note.
						note.Remove();
					}
				}
			}
		}
		#endregion // NoteChangeRule class
	}
	public partial class FactType : INoteOwner
	{
		#region INoteOwner Implementation
		/// <summary>
		/// Implements INoteOwner.NoteTextPropertyDescriptor
		/// </summary>
		protected PropertyDescriptor NoteTextPropertyDescriptor
		{
			get
			{
				return CreatePropertyDescriptor(Store.MetaDataDirectory.FindMetaAttribute(NoteTextMetaAttributeGuid), this);
			}
		}
		PropertyDescriptor INoteOwner.NoteTextPropertyDescriptor
		{
			get
			{
				return NoteTextPropertyDescriptor;
			}
		}
		#endregion // INoteOwner Implementation
	}
	public partial class ObjectType : INoteOwner
	{
		#region INoteOwner Implementation
		/// <summary>
		/// Implements INoteOwner.NoteTextPropertyDescriptor
		/// </summary>
		protected PropertyDescriptor NoteTextPropertyDescriptor
		{
			get
			{
				return CreatePropertyDescriptor(Store.MetaDataDirectory.FindMetaAttribute(NoteTextMetaAttributeGuid), this);
			}
		}
		PropertyDescriptor INoteOwner.NoteTextPropertyDescriptor
		{
			get
			{
				return NoteTextPropertyDescriptor;
			}
		}
		#endregion // INoteOwner Implementation
	}
}
