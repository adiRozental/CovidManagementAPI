using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myfirstAPI.Models;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;

/// <summary>

/// </summary>
/// 
/*
namespace myfirstAPI.Data
{
    public class MemberDbContext : DbContext
    {
        private readonly string _connectionString;

        public MemberDbContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MySqlConnection");
        }

        public DbSet<Member> Member { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(_connectionString);
        }
    }

    public class MemberRepository
    {
        private readonly MemberDbContext _context;

        public MemberRepository(MemberDbContext context)
        {
            _context = context;
        }

        public List<Member> GetAllMembers()
        {
            return _context.Member.ToList();
        }

        public Member GetMemberById(string id1)
        {
            return _context.Member.FirstOrDefault(m => m.id == id1);
        }
    }






}

*/