using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Enums
{
    public enum ModType
    {
        [Description("Offensive Language")]
        Language,
        [Description("Threatening Speech")]
        Threatening,
        [Description("Hate Speech")]
        HateSpeech,
        [Description("Drug References")]
        Drugs,
        [Description("Targeted Shaming")]
        Shaming
    }
}
