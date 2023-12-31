﻿using UnityEngine;
using UnityEngine.Events;

namespace Origins
{
	public interface ICommonCameraAPI
	{
		void RegisterCustomCamera(OWCamera OWCamera);
		(OWCamera, Camera) CreateCustomCamera(string name);
		UnityEvent<PlayerTool> EquipTool();
		UnityEvent<PlayerTool> UnequipTool();
		void ExitCamera(OWCamera OWCamera);
		void EnterCamera(OWCamera OWCamera);
	}
}
