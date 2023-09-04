using System;
using System.Collections.Generic;

namespace Tortillapp_web.tortilla
{
    public partial class RecipeTag
    {
        public ushort RecipeId { get; set; }
        public ushort TagId { get; set; }
        public DateTime? TagAdded { get; set; }

        public virtual RecipeInfo Recipe { get; set; } = null!;
        public virtual Tag Tag { get; set; } = null!;
    }
}
