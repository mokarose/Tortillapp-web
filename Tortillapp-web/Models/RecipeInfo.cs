using System;
using System.Collections.Generic;

namespace Tortillapp_web.Models
{
    public partial class RecipeInfo
    {
        public RecipeInfo()
        {
            RecipeIngredients = new HashSet<RecipeIngredient>();
            RecipeSteps = new HashSet<RecipeStep>();
            RecipeTags = new HashSet<RecipeTag>();
            UserFavorites = new HashSet<UserFavorite>();
            UserRatings = new HashSet<UserRating>();
        }

        public ushort RecipeId { get; set; }
        public ushort UserId { get; set; }
        public string? RecipeTitle { get; set; }
        public DateTime Published { get; set; }
        public short? RecipePortion { get; set; }
        public TimeSpan? RecipeTime { get; set; }
        public byte[]? RecipePic { get; set; }
        public string? RecipeTips { get; set; }
        /// <summary>
        /// 1 = Editando, 2 = En revision, 3 = Publicada
        /// </summary>
        public byte RecipeStatus { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual UserData User { get; set; } = null!;
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual ICollection<RecipeStep> RecipeSteps { get; set; }
        public virtual ICollection<RecipeTag> RecipeTags { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
