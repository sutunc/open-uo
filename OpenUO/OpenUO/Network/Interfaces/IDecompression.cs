﻿/***************************************************************************
 *   IDecompression.cs
 *   Part of OpenUO: http://code.google.com/p/OpenUO
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using System;
#endregion

namespace OpenUO.Network
{
    public interface IDecompression
    {
        void DecompressAll(ref byte[] src, int src_size, ref byte[] dest, ref int dest_size);
        bool DecompressOnePacket(ref byte[] src, int src_size, ref byte[] dest, ref int dest_size);
    }
}
