﻿using Karen90MmoFramework.Rpc;
using Photon.SocketServer.Rpc;

namespace Karen90MmoFramework.Server.ServerToClient.Events
{
	public class GroupMemberRemoved : GameEvent
	{
		/// <summary>
		/// Gets the event code
		/// </summary>
		public override byte EventCode
		{
			get
			{
				return (byte)ClientEventCode.GroupMemberRemoved;
			}
		}
		
		/// <summary>
		/// Object Id
		/// </summary>
		[DataMember(Code = (byte)ParameterCode.ObjectId)]
		public long ObjectId { get; set; }
	}
}