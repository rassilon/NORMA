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
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.VirtualTreeGrid;

namespace Neumont.Tools.Modeling.Shell.DynamicSurveyTreeGrid
{
	partial class MainList
	{
		partial class ListGrouper
		{
			private sealed class SimpleListShifter : IBranch
			{
				private readonly IBranch myBaseBranch;
				private readonly int myFirstItem;
				private readonly int myCount;

				/// <summary>
				/// Shows all elements of the branch from the certain point you 
				/// designate for a numbered amount that you request
				/// </summary>
				/// <param name="baseBranch">The List You Want To Shift</param>
				/// <param name="firstItemIndex">Where You Want To Start The Shift</param>
				/// <param name="count">Amount That You Want Displayed</param>
				public SimpleListShifter(IBranch baseBranch, int firstItemIndex, int count)
				{
					Debug.Assert(baseBranch != null);
					Debug.Assert(firstItemIndex >= 0);
					Debug.Assert(firstItemIndex < baseBranch.VisibleItemCount);

					myBaseBranch = baseBranch;
					myFirstItem = firstItemIndex;
					myCount = count;
					if (myFirstItem + myCount > myBaseBranch.VisibleItemCount)
					{
						myCount = myBaseBranch.VisibleItemCount - myFirstItem;
					}
				}

				#region IBranch Members

				public VirtualTreeLabelEditData BeginLabelEdit(int row, int column, VirtualTreeLabelEditActivationStyles activationStyle)
				{
					return myBaseBranch.BeginLabelEdit(row + myFirstItem, column, activationStyle);
				}
				public LabelEditResult CommitLabelEdit(int row, int column, string newText)
				{
					return myBaseBranch.CommitLabelEdit(row + myFirstItem, column, newText);
				}
				public BranchFeatures Features
				{
					get
					{
						return myBaseBranch.Features;
					}
				}
				public VirtualTreeAccessibilityData GetAccessibilityData(int row, int column)
				{
					return myBaseBranch.GetAccessibilityData(row + myFirstItem, column);
				}
				public VirtualTreeDisplayData GetDisplayData(int row, int column, VirtualTreeDisplayDataMasks requiredData)
				{
					return myBaseBranch.GetDisplayData(row + myFirstItem, column, requiredData);
				}
				public object GetObject(int row, int column, ObjectStyle style, ref int options)
				{
					return myBaseBranch.GetObject(row + myFirstItem, column, style, ref options);
				}
				public string GetText(int row, int column)
				{
					return myBaseBranch.GetText(row + myFirstItem, column);
				}
				public string GetTipText(int row, int column, ToolTipType tipType)
				{
					return myBaseBranch.GetTipText(row + myFirstItem, column, tipType);
				}
				public bool IsExpandable(int row, int column)
				{
					return false;
				}
				public LocateObjectData LocateObject(object obj, ObjectStyle style, int locateOptions)
				{
					LocateObjectData data = myBaseBranch.LocateObject(obj, style, locateOptions);
					data.Row -= myFirstItem;
					return data;
				}
				public event BranchModificationEventHandler OnBranchModification
				{
					add
					{
						myBaseBranch.OnBranchModification += value;
					}
					remove
					{
						myBaseBranch.OnBranchModification -= value;
					}
				}
				public void OnDragEvent(object sender, int row, int column, DragEventType eventType, DragEventArgs args)
				{
					myBaseBranch.OnDragEvent(sender, row + myFirstItem, column, eventType, args);
				}
				public void OnGiveFeedback(GiveFeedbackEventArgs args, int row, int column)
				{
					myBaseBranch.OnGiveFeedback(args, row + myFirstItem, column);
				}
				public void OnQueryContinueDrag(QueryContinueDragEventArgs args, int row, int column)
				{
					myBaseBranch.OnQueryContinueDrag(args, row + myFirstItem, column);
				}
				public VirtualTreeStartDragData OnStartDrag(object sender, int row, int column, DragReason reason)
				{
					return myBaseBranch.OnStartDrag(sender, row + myFirstItem, column, reason);
				}
				public StateRefreshChanges SynchronizeState(int row, int column, IBranch matchBranch, int matchRow, int matchColumn)
				{
					return myBaseBranch.SynchronizeState(row, column, matchBranch, matchRow + myFirstItem, matchColumn);
				}
				public StateRefreshChanges ToggleState(int row, int column)
				{
					return myBaseBranch.ToggleState(row + myFirstItem, column);
				}
				public int UpdateCounter
				{
					get
					{
						return myBaseBranch.UpdateCounter;
					}
				}
				public int VisibleItemCount
				{
					get
					{
						return myCount;
					}
				}
				#endregion
			}
		}
	}
}