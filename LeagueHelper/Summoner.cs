using System;
using System.Collections.Generic;

namespace LeagueHelper
{
    public class Summoner
    {

        public long SummonerID { get; set; }

        public static class GAME_STATUS
        {
            public const String PLAYING = "inGame";
            public const String LOBBY = "outOfGame";
            public const String QUEUING = "inQueue";
            public const String SELECTING_CHAMP = "championSelect";
            public const String CREATE_CUSTOM = "hosting_Custom";
            public const String CREATE_NORMAL = "hosting_NORMAL";
            public const String CREATE_ARAM = "hosting_ARAM_UNRANKED_5x5";
            public const String CREATE_PRACTICE = "hosting_PRACTICETOOL";
        }

        private const String REGION_TW = "TW2";

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

        public String LastSelectedChampion { get; set; }

        public String GameStatus { get; set; }

        public int Level { get; set; }

        public Dictionary<string,string> AvailableChampionsNameIDPair { get; set; }


        //public methods
        public void ClearData()
        {
            Name = "";
            StatusMessage = "";
            Region = "";
            GameStatus = "";
            Level = -1;
        }

    }
}
