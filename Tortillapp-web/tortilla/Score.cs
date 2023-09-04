using System;
using System.Collections.Generic;

namespace Tortillapp_web.tortilla
{
    public partial class Score
    {
        public Score()
        {
            UserRatings = new HashSet<UserRating>();
        }

        public ushort ScoreId { get; set; }
        public float ScorePoints { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        public DateTime ScoreModified { get; set; }
        /// <summary>
        /// 1 = Oculto, 2 = Visible
        /// </summary>
        public sbyte ScoreStatus { get; set; }

        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
