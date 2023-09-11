using System;
using System.Collections.Generic;

namespace Tortillapp_web.Model
{
    public partial class RecipeStep
    {
        public ushort StepId { get; set; }
        public ushort RecipeId { get; set; }
        public byte? StepPos { get; set; }
        public string? StepDescrp { get; set; }

        public virtual RecipeInfo Recipe { get; set; } = null!;
    }
}
