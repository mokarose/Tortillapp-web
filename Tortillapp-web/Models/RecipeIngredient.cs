using System;
using System.Collections.Generic;

namespace Tortillapp_web.Model
{
    public partial class RecipeIngredient
    {
        public ushort IngredientId { get; set; }
        public ushort RecipeId { get; set; }
        public string? IngredientName { get; set; }
        public short? IngredientAmount { get; set; }
        public string? IngredientUnit { get; set; }

        public virtual RecipeInfo Recipe { get; set; } = null!;
    }
}
