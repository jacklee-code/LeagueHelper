using System;
using System.Collections.Generic;

namespace LeagueHelper
{
    public class Summoner
    {

        public int SummonerID { get; set; }

        public static class GAME_STATUS
        {
            public const String PLAYING = "inGame";
            public const String LOBBY = "outOfGame";
            public const String QUEUING = "inQueue";
            public const String SELECTING_CHAMP = "championSelect";
        }

        private const String REGION_TW = "TW";

        public String Name { get; set; }
        public String StatusMessage { get; set; }

        private String _region;
        public String Region
        {
            get { return _region; }
            set
            {
                if (value == REGION_TW)
                    _region = "台港澳服";
                else
                    _region = value;
            }
        }

        public String GameStatus { get; set; }

        public int Level { get; set; }

        public Dictionary<string,string> AvailableChampionsNameIDPair { get; set; }

    }
}
