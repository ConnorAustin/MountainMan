using UnityEngine;
using System.Collections;

public class GrassGround : PassPoint 
{
	protected override void PassedPoint()
	{
		GameManager.manager.ChangeEnvironment();

		base.PassedPoint();
	}
}
