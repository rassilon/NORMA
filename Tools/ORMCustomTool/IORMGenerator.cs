#region Common Public License Copyright Notice
/**************************************************************************\
* Natural Object-Role Modeling Architect for Visual Studio                 *
*                                                                          *
* Copyright � Neumont University. All rights reserved.                     *
* Copyright � ORM Solutions, LLC. All rights reserved.                     *
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
#if VISUALSTUDIO_10_0
using Microsoft.Build.Construction;
using BuildItem = Microsoft.Build.Construction.ProjectItemElement;
using BuildItemGroup = Microsoft.Build.Construction.ProjectItemGroupElement;
#else // VISUALSTUDIO_10_0
using Microsoft.Build.BuildEngine;
#endif // VISUALSTUDIO_10_0

namespace ORMSolutions.ORMArchitect.ORMCustomTool
{
	/// <summary>
	/// Implementations of this interface generate a specific output format based on requested input formats.
	/// </summary>
	/// <remarks>
	/// TODO: Document how to register IORMGenerator implementations via the registry.
	/// If anyone is currently looking for this information, take a look in the "ORMGenerators.cs" file.
	/// </remarks>
	public interface IORMGenerator
	{
		/// <summary>
		/// The non-localized official name of this <see cref="IORMGenerator"/>.
		/// </summary>
		/// <remarks>
		/// This name must match the name of the <see cref="Microsoft.Win32.RegistryKey"/> in which this
		/// <see cref="IORMGenerator"/> is registered.
		/// </remarks>
		string OfficialName
		{
			get;
		}
		/// <summary>
		/// The localized name of this <see cref="IORMGenerator"/> for display in the user interface.
		/// </summary>
		[Localizable(true)]
		string DisplayName
		{
			get;
		}
		/// <summary>
		/// The localized description of this <see cref="IORMGenerator"/> for display in the user interface.
		/// </summary>
		[Localizable(true)]
		string DisplayDescription
		{
			get;
		}

		/// <summary>
		/// Return true if the file generated by this format is only necessary as
		/// a dependency. A dependency file is turned off automatically when it is
		/// no longer needed.
		/// </summary>
		bool GeneratesSupportFile
		{
			get;
		}

		/// <summary>
		/// Return true if file generated by this format should be generated once
		/// and not regenerated as the model changes.
		/// </summary>
		bool GeneratesOnce
		{
			get;
		}

		/// <summary>
		/// The official name of the output format provided by this <see cref="IORMGenerator"/>.
		/// </summary>
		string ProvidesOutputFormat
		{
			get;
		}
		
		/// <summary>
		/// The official name(s) of the output format(s) required by this <see cref="IORMGenerator"/> as input.
		/// </summary>
		IList<string> RequiresInputFormats
		{
			get;
		}

		/// <summary>
		/// The official name(s) of the output format(s) that need to be generated along
		/// with this generator. Companion formats differ from input formats in that
		/// companions are not used as input. Generators for two output formats may
		/// reference each other without causing a cycle. The output format may not
		/// be one of the companion formats.
		/// </summary>
		IList<string> RequiresCompanionFormats
		{
			get;
		}

		/// <summary>
		/// If <see cref="ProvidesOutputFormat"/> is part of the set of
		/// <see cref="RequiresInputFormats"/> then this generator is used
		/// to modify a previously generated instance of the same format.
		/// If more than one intra-format modifier is registered, then
		/// this property is used to determine the execution order. Lower
		/// numbers execute first, and 0 is the default priority.
		/// </summary>
		int FormatModifierPriority
		{
			get;
		}

		/// <summary>
		/// True if the generator modifies a format instead of initially
		/// creating it.
		/// </summary>
		bool IsFormatModifier
		{
			get;
		}

		/// <summary>
		/// Get required extensions for an associated input format. The extensions ensure that the input formats
		/// meet additional content requirements not expressible based solely on file format.
		/// </summary>
		/// <param name="inputFormat">The official name of the required input format, retrieved from <see cref="RequiresInputFormats"/>.</param>
		/// <returns>An enumerable of required extensions. The extensions must be enabled by the generator chosen to produce the given input format.</returns>
		IEnumerable<string> GetRequiredExtensionsForInputFormat(string inputFormat);

		/// <summary>
		/// Returns the default name of the file generated for a specific source file name.
		/// </summary>
		/// <param name="sourceFileName">A <see cref="String"/> containing the name (without file extension) of the source ORM file.</param>
		string GetOutputFileDefaultName(string sourceFileName);

#if VISUALSTUDIO_10_0
		/// <summary>
		/// Adds a <see cref="ProjectItemElement"/> for the generated file to <paramref name="itemGroup"/>.
		/// </summary>
		/// <param name="itemGroup">The <see cref="ProjectItemGroupElement"/> to which the <see cref="ProjectItemElement"/> for the generated file should be added.</param>
		/// <param name="sourceFileName">The name of the source ORM file. This will usually be used as the value for the &lt;DependentUpon&gt; item metadata.</param>
		/// <param name="outputFileName">The name of the generated file. This will usually be used as the value for the Include attribute of the <see cref="ProjectItemElement"/> for the generated file.</param>
		/// <returns>The <see cref="ProjectItemElement"/> for the generated file.</returns>
		/// <remarks>
		/// If this <see cref="IORMGenerator"/> generates compilable output that is useful at design time, the &lt;DesignTime&gt;
		/// item metadata should be set to the value "True" (without the quotes).
		/// </remarks>
		ProjectItemElement AddGeneratedFileItem(ProjectItemGroupElement itemGroup, string sourceFileName, string outputFileName);

		/// <summary>
		/// Generates the output for <paramref name="itemElement"/> to <paramref name="outputStream"/>, using the read-only <see cref="Stream"/>s
		/// contained in <paramref name="inputFormatStreams"/> as input.
		/// </summary>
		/// <param name="itemElement">The <see cref="ProjectItemElement"/> for which output is to be generated.</param>
		/// <param name="outputStream">The <see cref="Stream"/> to which output is to be generated.</param>
		/// <param name="inputFormatStreams">A read-only <see cref="IDictionary{String,Stream}"/> containing pairs of official output format names and read-only <see cref="Stream"/>s containing the output in that format.</param>
		/// <param name="defaultNamespace">A <see cref="String"/> containing the default namespace that should be used in the generated output, as appropriate.</param>
		/// <param name="itemProperties">An implementation of <see cref="IORMGeneratorItemProperties"/> to allow retrieval of additional properties</param>
		/// <remarks>
		/// <para><paramref name="inputFormatStreams"/> is guaranteed to contain the output <see cref="Stream"/>s for
		/// the "ORM" format and any formats returned by this <see cref="IORMGenerator"/>'s implementation of
		/// <see cref="IORMGenerator.RequiresInputFormats"/>.</para>
		/// <para>Implementations of this method are responsible for resetting the <see cref="Stream.Position"/> of any
		/// <see cref="Stream"/> obtained from <paramref name="inputFormatStreams"/> to the beginning of that <see cref="Stream"/>
		/// if they directly or indirectly alter that <see cref="Stream.Position"/>. This does not apply to <paramref name="outputStream"/>.
		/// See below for an example of how to reset the position of a <see cref="Stream"/> in C#.</para>
		/// <para><example>Stream oialStream = inputFormatStreams[ORMOutputFormat.OIAL];
		/// ...
		/// oialStream.Seek(0, SeekOrigin.Begin);</example></para>
		/// </remarks>
		void GenerateOutput(ProjectItemElement itemElement, Stream outputStream, IDictionary<string, Stream> inputFormatStreams, string defaultNamespace, IORMGeneratorItemProperties itemProperties);
#else // VISUALSTUDIO_10_0
		/// <summary>
		/// Adds a <see cref="BuildItem"/> for the generated file to <paramref name="buildItemGroup"/>.
		/// </summary>
		/// <param name="buildItemGroup">The <see cref="BuildItemGroup"/> to which the <see cref="BuildItem"/> for the generated file should be added.</param>
		/// <param name="sourceFileName">The name of the source ORM file. This will usually be used as the value for the &lt;DependentUpon&gt; item metadata.</param>
		/// <param name="outputFileName">The name of the generated file. This will usually be used as the value for the Include attribute of the <see cref="BuildItem"/> for the generated file.</param>
		/// <returns>The <see cref="BuildItem"/> for the generated file.</returns>
		/// <remarks>
		/// If this <see cref="IORMGenerator"/> generates compilable output that is useful at design time, the &lt;DesignTime&gt;
		/// item metadata should be set to the value "True" (without the quotes).
		/// </remarks>
		BuildItem AddGeneratedFileItem(BuildItemGroup buildItemGroup, string sourceFileName, string outputFileName);

		/// <summary>
		/// Generates the output for <paramref name="buildItem"/> to <paramref name="outputStream"/>, using the read-only <see cref="Stream"/>s
		/// contained in <paramref name="inputFormatStreams"/> as input.
		/// </summary>
		/// <param name="buildItem">The <see cref="BuildItem"/> for which output is to be generated.</param>
		/// <param name="outputStream">The <see cref="Stream"/> to which output is to be generated.</param>
		/// <param name="inputFormatStreams">A read-only <see cref="IDictionary{String,Stream}"/> containing pairs of official output format names and read-only <see cref="Stream"/>s containing the output in that format.</param>
		/// <param name="defaultNamespace">A <see cref="String"/> containing the default namespace that should be used in the generated output, as appropriate.</param>
		/// <param name="itemProperties">An implementation of <see cref="IORMGeneratorItemProperties"/> to allow retrieval of additional properties</param>
		/// <remarks>
		/// <para><paramref name="inputFormatStreams"/> is guaranteed to contain the output <see cref="Stream"/>s for
		/// the "ORM" format and any formats returned by this <see cref="IORMGenerator"/>'s implementation of
		/// <see cref="IORMGenerator.RequiresInputFormats"/>.</para>
		/// <para>Implementations of this method are responsible for resetting the <see cref="Stream.Position"/> of any
		/// <see cref="Stream"/> obtained from <paramref name="inputFormatStreams"/> to the beginning of that <see cref="Stream"/>
		/// if they directly or indirectly alter that <see cref="Stream.Position"/>. This does not apply to <paramref name="outputStream"/>.
		/// See below for an example of how to reset the position of a <see cref="Stream"/> in C#.</para>
		/// <para><example>Stream oialStream = inputFormatStreams[ORMOutputFormat.OIAL];
		/// ...
		/// oialStream.Seek(0, SeekOrigin.Begin);</example></para>
		/// </remarks>
		void GenerateOutput(BuildItem buildItem, Stream outputStream, IDictionary<string, Stream> inputFormatStreams, string defaultNamespace, IORMGeneratorItemProperties itemProperties);
#endif // VISUALSTUDIO_10_0
	}
	/// <summary>
	/// An interface used for generators to retrieve additional properties about the orm item
	/// being generated and its containing project.
	/// </summary>
	public interface IORMGeneratorItemProperties
	{
		/// <summary>
		/// Retrieve a named property from the project item that is the
		/// initial input to the generation process.
		/// </summary>
		/// <param name="propertyName">The name of a property</param>
		/// <returns>Property value or empty string</returns>
		string GetItemProperty(string propertyName);
		/// <summary>
		/// Retrieve a named property from the project item that is the
		/// initial input to the generation process.
		/// </summary>
		/// <param name="propertyName">The name of a property</param>
		/// <returns>Property value or empty string</returns>
		string GetProjectProperty(string propertyName);
		/// <summary>
		/// Ensure that the containing project contains a reference to the specified assembly
		/// </summary>
		/// <param name="referencedNamespace">The root namespace for the assembly</param>
		/// <param name="assemblyName">The assembly name</param>
		/// <returns>True if the item is added or exists</returns>
		bool EnsureProjectReference(string referencedNamespace, string assemblyName);
	}
}
