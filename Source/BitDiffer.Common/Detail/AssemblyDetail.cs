using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Xml;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class AssemblyDetail : ParentDetail, ICloneable
	{
		private string _location;

		public AssemblyDetail()
		{
		}

		public AssemblyDetail(Assembly assembly)
		{
			_name = "";
			_location = assembly.Location;

			_children.Add(new TraitDetail(this, "FullName", assembly.FullName));
			_children.Add(new TraitDetail(this, "Version", assembly.GetName().Version.ToString()));
			_children.Add(new TraitDetail(this, "RuntimeVersion", assembly.ImageRuntimeVersion));
			_children.Add(new TraitDetail(this, "PublicKeyToken", GenericUtility.GetHashText(assembly.GetName().GetPublicKeyToken())));
			_children.Add(new TraitDetail(this, "Flags", assembly.GetName().Flags.ToString()));

			AttributesDetail attributes = new AttributesDetail(this);
			_children.Add(attributes);
			foreach (CustomAttributeData cad in CustomAttributeData.GetCustomAttributes(assembly))
			{
				AttributeDetail ad = new AttributeDetail(attributes, cad);
				ad.Visibility = Visibility.Exported;
				attributes.Children.Add(ad);
			}

			ResourcesDetail resources = new ResourcesDetail(this);
			_children.Add(resources);
			foreach (string resource in assembly.GetManifestResourceNames())
			{
				resources.Children.Add(new ResourceDetail(resources, resource, GenericUtility.ReadStream(assembly, resource)));
			}

			ReferencesDetail references = new ReferencesDetail(this);
			_children.Add(references);
			foreach (AssemblyName an in assembly.GetReferencedAssemblies())
			{
				references.Children.Add(new ReferenceDetail(references, an));
			}

			Type[] types = assembly.GetTypes();

			foreach (Type type in types)
			{
				if (!type.IsNested)
				{
					NamespaceDetail ns = FindOrCreateNamespace(type.Namespace);

					if (type.IsEnum)
					{
						ns.Children.Add(new EnumDetail(ns, type));
					}
					else if (type.IsInterface)
					{
						ns.Children.Add(new InterfaceDetail(ns, type));
					}
					else if (type.IsClass)
					{
						ns.Children.Add(new ClassDetail(ns, type));
					}
				}
			}
		}

		private NamespaceDetail FindOrCreateNamespace(string ns)
		{
			if (ns == null)
			{
				ns = "*";
			}

			foreach (NamespaceDetail nsd in FilterChildren<NamespaceDetail>())
			{
				if (nsd.Name == ns)
				{
					return nsd;
				}
			}

			NamespaceDetail newns = new NamespaceDetail(this, ns);
			_children.Add(newns);
			return newns;
		}

		public object Clone()
		{
			using (MemoryStream ms = new MemoryStream())
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(ms, this);

				ms.Seek(0, SeekOrigin.Begin);

				return bf.Deserialize(ms);
			}
		}

		public string Location
		{
			get { return _location; }
			set { _location = value; }
		}

		protected override bool SuppressBreakingChangesInChildren
		{
			get { return false; }
		}

		public override bool ExcludeFromReport
		{
			get { return true; }
		}

		protected override string SerializeGetElementName()
		{
			return "Assembly";
		}

		protected override void SerializeWriteRawContent(XmlWriter writer)
		{
			base.SerializeWriteRawContent(writer);

			writer.WriteAttributeString("Location", _location);
		}

		protected override void SerializeWriteContent(XmlWriter writer)
		{
			base.SerializeWriteContent(writer);

			writer.WriteAttributeString("Location", _location);
		}

		public override string ToString()
		{
			return _location;
		}
	}
}
