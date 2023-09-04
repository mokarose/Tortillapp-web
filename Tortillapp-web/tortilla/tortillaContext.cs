using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tortillapp_web.tortilla
{
    public partial class tortillaContext : DbContext
    {
        public tortillaContext()
        {
        }

        public tortillaContext(DbContextOptions<tortillaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RecipeInfo> RecipeInfos { get; set; } = null!;
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;
        public virtual DbSet<RecipeStep> RecipeSteps { get; set; } = null!;
        public virtual DbSet<RecipeTag> RecipeTags { get; set; } = null!;
        public virtual DbSet<Score> Scores { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<UserData> UserData { get; set; } = null!;
        public virtual DbSet<UserFavorite> UserFavorites { get; set; } = null!;
        public virtual DbSet<UserRating> UserRatings { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;port=3306;uid=root;pwd=Lord0Rings;database=tortilla;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeInfo>(entity =>
            {
                entity.HasKey(e => e.RecipeId)
                    .HasName("PRIMARY");

                entity.ToTable("recipe_info");

                entity.HasIndex(e => e.UserId, "recipe_info_user_idx");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("last_updated")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Published)
                    .HasColumnType("timestamp")
                    .HasColumnName("published");

                entity.Property(e => e.RecipePic)
                    .HasColumnType("blob")
                    .HasColumnName("recipe_pic");

                entity.Property(e => e.RecipePortion).HasColumnName("recipe_portion");

                entity.Property(e => e.RecipeStatus)
                    .HasColumnName("recipe_status")
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 = Editando, 2 = En revision, 3 = Publicada");

                entity.Property(e => e.RecipeTime)
                    .HasColumnType("time")
                    .HasColumnName("recipe_time");

                entity.Property(e => e.RecipeTips)
                    .HasColumnType("text")
                    .HasColumnName("recipe_tips");

                entity.Property(e => e.RecipeTitle)
                    .HasMaxLength(50)
                    .HasColumnName("recipe_title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RecipeInfos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_info_user");
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PRIMARY");

                entity.ToTable("recipe_ingredients");

                entity.HasIndex(e => e.RecipeId, "recipe_ingredients_idpk_1_idx");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.IngredientAmount).HasColumnName("ingredient_amount");

                entity.Property(e => e.IngredientName)
                    .HasMaxLength(50)
                    .HasColumnName("ingredient_name");

                entity.Property(e => e.IngredientUnit)
                    .HasColumnType("set('Litro','Mililitro','Kilo','Gramo','Cucharada','Cucharadita','Taza')")
                    .HasColumnName("ingredient_unit");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_ingredients_idpk_1");
            });

            modelBuilder.Entity<RecipeStep>(entity =>
            {
                entity.HasKey(e => e.StepId)
                    .HasName("PRIMARY");

                entity.ToTable("recipe_steps");

                entity.HasIndex(e => e.RecipeId, "recipe_id");

                entity.Property(e => e.StepId).HasColumnName("step_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.StepDescrp)
                    .HasColumnType("text")
                    .HasColumnName("step_descrp");

                entity.Property(e => e.StepPos).HasColumnName("step_pos");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeSteps)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_steps_idffk_1");
            });

            modelBuilder.Entity<RecipeTag>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.TagId })
                    .HasName("PRIMARY");

                entity.ToTable("recipe_tags");

                entity.HasIndex(e => e.RecipeId, "recipe_id");

                entity.HasIndex(e => e.TagId, "recipe_tags");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.TagAdded)
                    .HasColumnType("timestamp")
                    .HasColumnName("tag_added");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeTags)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("recipe_tags_recipe");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.RecipeTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_tags_tags");
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.ToTable("score");

                entity.Property(e => e.ScoreId).HasColumnName("score_id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.ScoreModified)
                    .HasColumnType("timestamp")
                    .HasColumnName("score_modified");

                entity.Property(e => e.ScorePoints).HasColumnName("score_points");

                entity.Property(e => e.ScoreStatus)
                    .HasColumnName("score_status")
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 = Oculto, 2 = Visible");

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tags");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.TagCreated)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("tag_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.TagName)
                    .HasMaxLength(25)
                    .HasColumnName("tag_name");
            });

            modelBuilder.Entity<UserData>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("user_data");

                entity.HasIndex(e => e.RoleId, "role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("last_updated")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.RemenberMe)
                    .HasColumnName("remenber_me")
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 = No, 2 = Si");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.ShowName)
                    .HasMaxLength(25)
                    .HasColumnName("show_name");

                entity.Property(e => e.ShowPic)
                    .HasColumnType("blob")
                    .HasColumnName("show_pic");

                entity.Property(e => e.UserCreated)
                    .HasColumnType("timestamp")
                    .HasColumnName("user_created");

                entity.Property(e => e.UserMail)
                    .HasMaxLength(99)
                    .HasColumnName("user_mail");

                entity.Property(e => e.UserName)
                    .HasMaxLength(25)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPass)
                    .HasMaxLength(40)
                    .HasColumnName("user_pass");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserData)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_data_roles");
            });

            modelBuilder.Entity<UserFavorite>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("user_favorites");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserAdded)
                    .HasColumnType("timestamp")
                    .HasColumnName("user_added");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.UserFavorites)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_favorites_recipe");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFavorites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_favorites_user");
            });

            modelBuilder.Entity<UserRating>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.UserId })
                    .HasName("PRIMARY");

                entity.ToTable("user_rating");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.HasIndex(e => e.UserScore, "user_rating_score_idx");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.ScoreAdded)
                    .HasColumnType("timestamp")
                    .HasColumnName("score_added");

                entity.Property(e => e.UserScore).HasColumnName("user_score");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_rating_recipe");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("user_rating_user");

                entity.HasOne(d => d.UserScoreNavigation)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.UserScore)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_rating_score");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PRIMARY");

                entity.ToTable("user_roles");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("last_updated")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.RoleCreated)
                    .HasColumnType("timestamp")
                    .HasColumnName("role_created");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(15)
                    .HasColumnName("role_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
