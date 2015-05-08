﻿using Karen90MmoFramework.Rpc;

namespace Karen90MmoFramework.Server.ServerToClient.Events
{
	public class GroupDisbanded : GameEvent
	{
		/// <summary>
		/// Gets the event code
		/// </summary>
		public override byte EventCode
		{
			get
			{
				return (byte)ClientEventCode.GroupDisbanded;
			}
		}
	}
}