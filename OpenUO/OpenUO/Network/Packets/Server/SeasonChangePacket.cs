﻿/***************************************************************************
 *   SeasonChangePacket.cs
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
    public class SeasonChangePacket : RecvPacket
    {
        readonly byte _season;
        readonly byte _playSound;

        public byte Season
        {
            get { return _season; }
        }
        public byte PlaySound
        {
            get { return _playSound; }
        }

        public SeasonChangePacket(PacketReader reader)
            : base(0xBC, "Seasonal Information")
        {
            _season = reader.ReadByte();
            _playSound = reader.ReadByte();
        }
    }
}
