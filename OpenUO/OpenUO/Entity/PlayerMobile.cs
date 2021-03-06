﻿/***************************************************************************
 *   PlayerMobile.cs
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
using Microsoft.Xna.Framework;
using OpenUO.TileEngine;
#endregion

namespace OpenUO.Entity
{
    public class PlayerMobile : Mobile
    {
        public PlayerMobile(Serial serial)
            : base(serial)
        {

        }

        public override string ToString()
        {
            return base.ToString() + " | " + Name;
        }

        public override void Update(GameTime gameTime)
        {
            _movement.PlayerMobile_CheckForMoveEvent();

            base.Update(gameTime);
        }
    }
}
