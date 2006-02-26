#region Common Public License Copyright Notice
/**************************************************************************\
* Neumont Object-Role Modeling Architect for Visual Studio                 *
*                                                                          *
* Copyright © Neumont University. All rights reserved.                     *
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

#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

#endregion

namespace Neumont.Tools.ORM.FactEditor
{
	/// <summary>
	/// A class which handles applying color to marked tokens
	/// </summary>
	public class FactColorizer : IVsColorizer
	{
		private FactParser myParser;

		/// <summary>
		/// Create a new colorizer object
		/// </summary>
		/// <param name="parser">The parser that knows how to parse the lines</param>
		public FactColorizer(FactParser parser)
		{
			myParser = parser;
		}

		#region IVsColorizer Members

		void IVsColorizer.CloseColorizer()
		{
			CloseColorizer();
		}
		/// <summary>
		/// Implements IVsColorizer.CloseColorizer
		/// </summary>
		protected static void CloseColorizer()
		{

		}

		// Colorize the given text.  For each character in the line of text
		// given, a matching index into the colorizer's syntax item array (+1) should
		// be placed in the color index array provided.  The colorizer should
		// start in the state provided, and return its current state (which will
		// be stored and provided as the start state when the following line is
		// colorized).  The index array given is gauranteed to be ONE ELEMENT
		// longer than the number of characters in the line.  This element is
		// used to determine the (background) color of the space to the right of
		// the last character on the line.
		// Color indexes are 1-BASED -- use 0 to specify default text color.
		// pText is NOT null-terminated.  iLength is the length of the line
		// MINUS the end-of-line marker (CR, LF, CRLF pair, or 0 (EOF)), which
		// will be present.
		//
		// NOTES:
		// The color of each character in the line is specified by setting a 'value'
		// in the provided pAttributes array.
		// The 'value' is really an index into an array of possible FactEditorColorizableItem,
		// plus the implicit DEFAULTITEMS.COLITEM_TEXT value (always 0).
		// Our own unique set of colors is implemented in the FactFontsAndColors.cs file.
		// 
		int IVsColorizer.ColorizeLine(int iLine, int iLength, IntPtr pszText, int iState, uint[] pAttributes)
		{
			return ColorizeLine(iLine, iLength, pszText, iState, pAttributes);
		}
		/// <summary>
		/// Implements IVsColorizer.ColorizeLine
		/// </summary>
		[CLSCompliant(false)]
		protected int ColorizeLine(int iLine, int iLength, IntPtr pszText, int iState, uint[] pAttributes)
		{
			if (null == pAttributes)
			{
				return VSConstants.E_INVALIDARG;
			}

			// set all colors to default or shell crashes
			int len = pAttributes.Length;
			for (int i = 0; i < len; ++i)
			{
				pAttributes[i] = (uint)DEFAULTITEMS.COLITEM_TEXT;
			}

			if (pszText == IntPtr.Zero)
			{
				return VSConstants.S_OK;
			}
			if (iLength <= 0)
			{
				return VSConstants.S_OK;
			}
			if (myParser == null)
			{
				return VSConstants.S_OK;
			}
			
			// Create a string from the "const" IntPtr param
			// call the Line method on the parse object
			string s = Marshal.PtrToStringUni(pszText);

			ParsedFact parsedFact = (myParser as IFactParser).ParseLine(s);
			for (int a = 0; a < pAttributes.Length; ++a)
			{
				pAttributes[a] = parsedFact.ColorAttributes[a];
			}
			pAttributes[iLength] = pAttributes[iLength - 1];
			return VSConstants.S_OK;
		}

		int IVsColorizer.GetStartState(out int piStartState)
		{
			return GetStartState(out piStartState);
		}
		/// <summary>
		/// Implements IVsColorizer.GetStartState
		/// </summary>
		/// <param name="piStartState"></param>
		/// <returns></returns>
		protected static int GetStartState(out int piStartState)
		{
			piStartState = 0;
			return VSConstants.S_OK;
		}

		int IVsColorizer.GetStateAtEndOfLine(int iLine, int iLength, IntPtr pText, int iState)
		{
			return GetStateAtEndOfLine(iLine, iLength, pText, iState);
		}
		/// <summary>
		/// Implements IVsColorizer.GetStateAtEndOfLine
		/// </summary>
		/// <param name="iLine"></param>
		/// <param name="iLength"></param>
		/// <param name="pText"></param>
		/// <param name="iState"></param>
		/// <returns></returns>
		protected static int GetStateAtEndOfLine(int iLine, int iLength, IntPtr pText, int iState)
		{
			return 0;
		}

		int IVsColorizer.GetStateMaintenanceFlag(out int pfFlag)
		{
			return GetStateMaintenanceFlag(out pfFlag);
		}
		/// <summary>
		/// Implements IVsColorizer.GetStateMaintenanceFlag
		/// </summary>
		/// <param name="pfFlag"></param>
		/// <returns></returns>
		protected static int GetStateMaintenanceFlag(out int pfFlag)
		{
			pfFlag = 0;
			return VSConstants.S_OK;
		}

		#endregion
	}
}
