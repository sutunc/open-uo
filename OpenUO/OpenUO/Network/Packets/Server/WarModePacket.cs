﻿/***************************************************************************
 *   WarModePacket.cs
 *   Part of OpenUO: http://code.google.com/p/OpenUO
 *   
 *   begin                : May 31, 2009
 *   email                : poplicola@OpenUO.com
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using OpenUO.Network;
#endregion

namespace OpenUO.Network.Packets.Server
{
    public class WarModePacket : RecvPacket
    {
        readonly byte _warmode;

        public bool WarMode
        {
            get { return (_warmode == 0x01); }
        }

        public WarModePacket(PacketReader reader)
            : base(0x72, "Request War Mode")
        {
            _warmode = reader.ReadByte();

            reader.ReadByte(); // always 0x00
            reader.ReadByte(); // always 0x32
            reader.ReadByte(); // always 0x00
        }
    }
}
