using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System_uznawania_przychodow.Data;

namespace Unit_tests;

public class TestDbContext
{
    public static AppDbContext Create()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;
 
        var dbContext = new AppDbContext(options);
        dbContext.Database.EnsureCreated();
        return dbContext;
    }
}