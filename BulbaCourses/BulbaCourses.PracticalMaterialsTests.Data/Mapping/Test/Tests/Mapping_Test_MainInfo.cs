﻿using BulbaCourses.PracticalMaterialsTests.Data.Models.Test;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BulbaCourses.PracticalMaterialsTests.Data.Mapping.Test
{
    public class Mapping_Test_MainInfo : EntityTypeConfiguration<MTest_MainInfoDb>
    {
        public Mapping_Test_MainInfo()
        {
            ToTable("Test_MainInfo");

            HasKey(i => i.Id);

            Property(i => i.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(i => i.Name)
                .HasColumnName("Name")
                .HasMaxLength(250)
                .IsRequired();

            Property(i => i.Description)
                .HasColumnName("Description")
                .HasMaxLength(500)
                .IsRequired();

            Property(i => i.DateCreate)
                .HasColumnName("DateCreate")                
                .IsRequired();

            this.HasMany(g => g.Questions_ChoosingAnswerFromList)
                .WithRequired(s => s.Test_MainInfoDb)
                .HasForeignKey<int>(s => s.Test_MainInfoDb_Id)
                .WillCascadeOnDelete();

            this.HasMany(g => g.Questions_SetOrder)
                .WithRequired(s => s.Test_MainInfoDb)
                .HasForeignKey<int>(s => s.Test_MainInfoDb_Id)
                .WillCascadeOnDelete();

            this.HasMany(g => g.ResultsOfTheTest)
                .WithRequired(s => s.Test_MainInfo)
                .HasForeignKey<int>(s => s.Test_MainInfoDb_Id)
                .WillCascadeOnDelete();
        }
    }
}
