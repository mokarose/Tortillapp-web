using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tortillapp_web.Model
{
    public partial class UserData
    {
        public UserData()
        {
            RecipeInfos = new HashSet<RecipeInfo>();
            UserFavorites = new HashSet<UserFavorite>();
            UserRatings = new HashSet<UserRating>();
        }

        public ushort UserId { get; set; }
        public string UserMail { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserPass { get; set; } = null!;
        public byte RoleId { get; set; }
        public string? ShowName { get; set; }
        public byte[]? ShowPic { get; set; }
        /// <summary>
        /// 1 = No, 2 = Si
        /// </summary>
        public sbyte RemenberMe { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime UserCreated { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime LastUpdated { get; set; }

        public virtual UserRole Role { get; set; } = null!;
        public virtual ICollection<RecipeInfo> RecipeInfos { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
