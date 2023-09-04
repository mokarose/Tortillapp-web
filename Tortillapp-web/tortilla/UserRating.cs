using System;
using System.Collections.Generic;

namespace Tortillapp_web.tortilla
{
    public partial class UserRating
    {
        public ushort RecipeId { get; set; }
        public ushort UserId { get; set; }
        public ushort UserScore { get; set; }
        public DateTime ScoreAdded { get; set; }

        public virtual RecipeInfo Recipe { get; set; } = null!;
        public virtual UserData User { get; set; } = null!;
        public virtual Score UserScoreNavigation { get; set; } = null!;
    }
}
