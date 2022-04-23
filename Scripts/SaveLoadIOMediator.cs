using UnityEngine;

namespace Save
{
	public class SaveLoadIOMediator 
	{
		/// <summary>
		/// Method <c>ClearSave</c> Public function to swap out savefilehandler for any class implementing ISaveLoadExternal if you want to plug and play alternative external interface. Such as server API
		/// </summary>
		public ISaveLoadIO CreateSaveLoadExternal(string fileName)
		{
			return new SaveFileHandler(Application.persistentDataPath, fileName);
		}
	}
}
