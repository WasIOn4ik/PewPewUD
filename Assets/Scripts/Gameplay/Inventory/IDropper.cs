using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PewComponents
{
	public interface IDropper
	{
		public event EventHandler OnDrop;
	}
}
