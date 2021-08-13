using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueHelper
{
    class RunePage
    {
        [JsonProperty]
        private List<int> autoModifiedSelections = new List<int>();

        [JsonProperty]
        private bool current = true;

        [JsonProperty]
        private bool isActive = true;

        [JsonProperty]
        private bool isDeletable = true;

        [JsonProperty]
        private bool isEditable = true;

        [JsonProperty]
        private bool isValid = true;

        /*[JsonProperty]
        private int lastModified = 0;*/

        [JsonProperty]
        private int order = 0;

        [JsonProperty]
        public int id = 0;

        [JsonProperty]
        public String name;

        [JsonProperty]
        public int primaryStyleId;

        [JsonProperty]
        public List<int> selectedPerkIds = new List<int>();

        [JsonProperty]
        public int subStyleId;

        public RunePage(String pageName = "英雄聯盟小助手")
        {
            name = pageName;
        }
    }
}
