using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StickDownWindows.Services.Data
{
    [JsonObject(Title = "Note")]
    public class Note
    {
        public string Text;
        public string FileName;
        public string ResourceId;
        public bool Archived;
        public string UserId;
        public string Id;
        public string Version;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
        public bool Deleted;
    }
}
