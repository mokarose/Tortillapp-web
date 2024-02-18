using System;
using System.Collections.Generic;

namespace Tortillapp_web.Models
{
    public partial class UserRating
    {
        public ushort ScoreId { get; set; }
        public ushort RecipeId { get; set; }
        public ushort UserId { get; set; }
        public ushort ScorePoints { get; set; }
        public string? ScoreComment { get; set; }
        /// <summary>
        /// 1 = Oculto, 2 = Visible
        /// </summary>
        public short ScoreStatus { get; set; }
        public DateTime ScoreAdded { get; set; }
        public DateTime ScoreModified { get; set; }

        public virtual RecipeInfo Recipe { get; set; } = null!;
        public virtual UserData User { get; set; } = null!;
    }
}
