﻿/***************************************************************************
 *   MapObjectCorpse.cs
 *   Part of OpenUO: http://code.google.com/p/OpenUO
 *   Based on code from ClintXNA's renderer: http://www.runuo.com/forums/xna/92023-hi.html
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using Microsoft.Xna.Framework;
using OpenUO.Entity;
using OpenUO.Graphics;
#endregion

namespace OpenUO.TileEngine
{
    public class MapObjectCorpse : MapObjectItem
    {
        public int BodyID { get; set; }
        public int FrameIndex { get; set; }

        public new int SortZ
        {
            get { return (int)Z; }
        }

        public int Layer
        {
            get { return SortTiebreaker; }
            set { SortTiebreaker = value; }
        }

        private bool _noDraw = false;

        public MapObjectCorpse(Position3D position, int direction, BaseEntity ownerEntity, int nHue, int bodyID, float frame)
            : base(0x2006, position, direction, ownerEntity, nHue)
        {
            BodyID = bodyID;
            FrameIndex = (int)(frame * UltimaData.BodyConverter.DeathAnimationFrameCount(bodyID));

            UltimaData.FrameXNA iFrame = getFrame();
            if (iFrame == null)
            {
                _noDraw = true;
                return;
            }
            _draw_texture = iFrame.Texture;
            _draw_width = _draw_texture.Width;
            _draw_height = _draw_texture.Height;
            _draw_X = iFrame.Center.X - 22;
            _draw_Y = iFrame.Center.Y + (int)(Z * 4) + _draw_height - 22;
            _draw_hue = Utility.GetHueVector(Hue);
            _pickType = PickTypes.PickObjects;
            _draw_flip = false;
        }

        internal override bool Draw(SpriteBatch3D sb, Vector3 drawPosition, MouseOverList molist, PickTypes pickType, int maxAlt)
        {
            if (_noDraw)
                return false;
            return base.Draw(sb, drawPosition, molist, pickType, maxAlt);
        }

        private UltimaData.FrameXNA getFrame()
        {
            UltimaData.FrameXNA[] iFrames = UltimaData.AnimationsXNA.GetAnimation(BodyID, UltimaData.BodyConverter.DeathAnimationIndex(BodyID), Facing, Hue);
            if (iFrames == null)
                return null;
            if (iFrames[FrameIndex].Texture == null)
                return null;
            return iFrames[FrameIndex];
        }

        public override string ToString()
        {
            return string.Format("MapObjectCorpse\n   BodyID:{1} Layer:{2}\n{0}", base.ToString(), BodyID, Layer);
        }
    }
}
