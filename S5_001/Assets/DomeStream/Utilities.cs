////////////////////////////////////////////////////////////////////////////////////////
//
// COPYRIGHT (C) Evans & Sutherland Computer Corporation
// All rights reserved.
//
////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

namespace ES
{
	public static class Utilities
	{
		public static bool TryFindObjectOfType<T>(out T found) where T : UnityEngine.Object
		{
			found = UnityEngine.Object.FindObjectOfType<T>();
			return found != null;
		}

		public static bool TryGetComponent<T>(this GameObject source, out T found) where T : Component
		{
			found = source.GetComponent<T>();
			return found != null;
		}

		public static bool TryGetComponent<T>(this GameObject source, bool enabledOnly, out T found) where T : Behaviour
		{
			var allFound = source.GetComponents<T>();
			for (int i = 0; i < allFound.Length; i++)
			{
				if (allFound[i].enabled)
				{
					found = allFound[i];
					return true;
				}
			}

			found = null;
			return false;
		}

		public static bool HasComponent<T>(this GameObject source) => source.GetComponent<T>() != null;

		public static T GetOrAddComponent<T>(this GameObject source) where T : Component
		{
			var component = source.GetComponent<T>();
			if (component == null)
				component = source.AddComponent<T>();

			return component;
		}

		public static string FirstNullOrWhiteSpace(params string[] values)
		{
			foreach (var value in values)
				if (!string.IsNullOrWhiteSpace(value))
					return value;

			return null;
		}
	}
}
