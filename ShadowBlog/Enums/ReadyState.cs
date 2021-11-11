using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Enums
{
    public enum ReadyState
    {
        //This enum when in use means the post isn't ready
        Incomplete,
        //When in use it means its ready to be viewed by the public
        ProductionReady,
        //When in use it means it is in preview mode
        InPreview
    }
}
