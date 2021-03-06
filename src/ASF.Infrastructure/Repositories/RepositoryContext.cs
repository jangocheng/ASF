﻿using ASF.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace ASF.Infrastructure.Repository
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<LogInfoModel> LogInfos { get; set; }
        public DbSet<PermissionModel> Permissions { get; set; }
        public DbSet<RoleModel> Roles { get; set; }

   
    }
}
