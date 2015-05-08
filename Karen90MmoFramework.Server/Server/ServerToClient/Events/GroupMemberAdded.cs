﻿using Karen90MmoFramework.Game;
using Karen90MmoFramework.Rpc;
using Photon.SocketServer.Rpc;

namespace Karen90MmoFramework.Server.ServerToClient.Events
{
	public class GroupMemberAdded : GameEvent
	{
		/// <summary>
		/// Gets the event code
		/// </summary>
		public override byte EventCode
		{
			get
			{
				return (byte)ClientEventCode.GroupMemberAdded;
			}
		}
		
		/// <summary>
		/// Group Member Data
		/// </summary>
		[DataMember(Code = (byte)ParameterCode.Data)]
		public GroupMemberStructure GroupMemberData { get; set; }
	}
}