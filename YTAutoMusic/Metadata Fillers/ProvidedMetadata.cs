﻿using System.Diagnostics;
using System.Globalization;
using static YTAutoMusic.MetadataFillerExtensions;

namespace YTAutoMusic.Metadata_Fillers
{
    /// <summary>
    /// Auto generated by YT
    /// </summary>
    internal class ProvidedMetadata : MetadataBase
    {
        public override int Priority => 0;

        public override string Name => "Provided by YT";

        /*
         * Provided to YouTube by INSERTCOMPANYHERE
         * 
         * Title · Artist
         * 
         * Album
         * 
         * ℗
         * 
         * Released on: YYYY-MM-DD
         * 
         * ...
         * 
         * Auto-generated by YouTube.
         */

        public override bool Fill(TagLib.File tagFile, string title, string description)
        {
            if (description.Contains("Provided to YouTube by"))
            {
                string[] lines = LineifyDescription(description);

                Debug.Assert(lines[0].Contains("Provided to YouTube by"));

                tagFile.Tag.Title = title;

                string[] performers = lines[1].Split('·')[1..];
                for (int i = 0; i < performers.Length; i++)
                {
                    performers[i] = performers[i].Trim();
                }

                tagFile.Tag.Performers = performers;
                tagFile.Tag.Album = lines[2].Trim();

                tagFile.Tag.Copyright = lines[3].Trim();

                tagFile.Tag.Year = (uint)DateTime.ParseExact(lines[4].Trim()[13..], "yyyy-MM-dd", CultureInfo.InvariantCulture).Year;

                return true;
            }

            return false;
        }
    }
}
