using System;
using System.Collections.Generic;

namespace Tortillapp_web.tortilla
{
    public partial class UserFavorite
    {
        public ushort RecipeId { get; set; }
        public ushort UserId { get; set; }
        public DateTime UserAdded { get; set; }

        public virtual RecipeInfo Recipe { get; set; } = null!;
        public virtual UserData User { get; set; } = null!;
    }
}
