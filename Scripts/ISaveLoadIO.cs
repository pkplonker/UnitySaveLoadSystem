using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Save
{
	public interface ISaveLoadIO
	{
		public Dictionary<string, object> Load();

		public void Save(object data);
		public void Clear();
	}
}