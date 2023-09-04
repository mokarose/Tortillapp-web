using System;
using System.Collections.Generic;

namespace Tortillapp_web.tortilla
{
    public partial class Tag
    {
        public Tag()
        {
            RecipeTags = new HashSet<RecipeTag>();
        }

        public ushort TagId { get; set; }
        public string TagName { get; set; } = null!;
        public DateTime TagCreated { get; set; }

        public virtual ICollection<RecipeTag> RecipeTags { get; set; }
    }
}
