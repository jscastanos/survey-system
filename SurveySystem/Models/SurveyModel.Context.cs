﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SurveySystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SurveyEntities : DbContext
    {
        public SurveyEntities()
            : base("name=SurveyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tAssignedPlace> tAssignedPlaces { get; set; }
        public virtual DbSet<tBrgy> tBrgies { get; set; }
        public virtual DbSet<tCategory> tCategories { get; set; }
        public virtual DbSet<tCategoryByDist> tCategoryByDists { get; set; }
        public virtual DbSet<tCategoryByMunCity> tCategoryByMunCities { get; set; }
        public virtual DbSet<tChoice> tChoices { get; set; }
        public virtual DbSet<tEnumerator> tEnumerators { get; set; }
        public virtual DbSet<tMunCity> tMunCities { get; set; }
        public virtual DbSet<tSurvey> tSurveys { get; set; }
        public virtual DbSet<tUser> tUsers { get; set; }
        public virtual DbSet<vAssignedLoc> vAssignedLocs { get; set; }
        public virtual DbSet<vSurvey> vSurveys { get; set; }
        public virtual DbSet<vSurveyByDist> vSurveyByDists { get; set; }
        public virtual DbSet<vSurveyByMunCity> vSurveyByMunCities { get; set; }
        public virtual DbSet<tPrk> tPrks { get; set; }
    }
}
