using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tortillapp_web.Model
{
    public partial class UserRating
    {
        public ushort RecipeId { get; set; }
        public ushort UserId { get; set; }
        public ushort UserScore { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ScoreAdded { get; set; }

        public virtual RecipeInfo Recipe { get; set; } = null!;
        public virtual UserData User { get; set; } = null!;
        public virtual Score UserScoreNavigation { get; set; } = null!;
    }
}
