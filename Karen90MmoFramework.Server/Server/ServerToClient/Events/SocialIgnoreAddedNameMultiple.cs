﻿using Karen90MmoFramework.Rpc;
using Photon.SocketServer.Rpc;

namespace Karen90MmoFramework.Server.ServerToClient.Events
{
	public class SocialIgnoreAddedNameMultiple : GameEvent
	{
		/// <summary>
		/// Gets the event code
		/// </summary>
		public override byte EventCode
		{
			get
			{
				return (byte)ClientEventCode.SocialIgnoreAddedNameMultiple;
			}
		}
		
		/// <summary>
		/// Names
		/// </summary>
		[DataMember(Code = (byte)ParameterCode.Collection)]
		public string[] Names { get; set; }
	}
}