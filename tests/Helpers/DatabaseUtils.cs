using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Models;

namespace SCJ.Booking.UnitTest.Helpers;

public class DatabaseUtils
{
    public static ApplicationDbContext GetDbContext(IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        string connectionString;
        string provider;

        if (configuration["ConnectionString"] != null)
        {
            connectionString = configuration["ConnectionString"];
            provider = ServiceConfig.DataProviderNpgsql;
        }
        else
        {
            provider = configuration[ServiceConfig.DataProviderKey.Replace("__", ":")];
            connectionString = configuration[ServiceConfig.ConnectionStringKey.Replace("__", ":")];
        }

        var applicationDbContext = new ApplicationDbContext(connectionString, provider);

        // clear the db
        applicationDbContext.Database.ExecuteSqlRaw(
            "DROP TABLE IF EXISTS \"ScTrialDateSelections\""
        );
        applicationDbContext.Database.ExecuteSqlRaw(
            "DROP TABLE IF EXISTS \"ScTrialBookingRequests\""
        );
        applicationDbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS \"ScLotteries\"");
        applicationDbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS \"BookingHistory\"");
        applicationDbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS \"EmailQueue\"");
        applicationDbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS \"Users\"");
        applicationDbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS \"DataProtectionKeys\"");
        applicationDbContext.Database.ExecuteSqlRaw(
            "DROP TABLE IF EXISTS \"__EFMigrationsHistory\""
        );

        // run the migrations
        applicationDbContext.Database.Migrate();

        return applicationDbContext;
    }

    public static void LoadCsvData(ApplicationDbContext dbContext, string filePath)
    {
        var testUser = new OidcUser
        {
            CredentialType = OidcUser.CredentialTypeLookup.KeycloakBceid,
            UniqueIdentifier = Guid.NewGuid().ToString(),
            LastLogin = DateTime.Now
        };
        dbContext.Users.AddAsync(testUser);
        dbContext.SaveChanges();

        var reader = new StreamReader(filePath);
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<CsvMapping>();

            foreach (var record in records)
            {
                var entity = new ScTrialBookingRequest
                {
                    FairUseBookingPeriodEndDate = record.FairUseBookingPeriodEndDate,
                    LotteryStartDate = record.LotteryStartDate,
                    BookHearingCode = record.BookHearingCode,
                    BookingLocationId = record.BookingLocationId,
                    CaseNumber = record.CaseNumber,
                    CaseRegistryCode = record.CaseRegistryCode,
                    CaseRegistryId = record.CaseRegistryId,
                    CeisPhysicalFileId = record.CeisPhysicalFileId,
                    CourtClassCode = record.CourtClassCode,
                    Email = record.Email,
                    FairUseSort = record.FairUseSort,
                    FairUseBookingPeriodStartDate = record.FairUseBookingPeriodStartDate,
                    CreationTimestamp = DateTime.Now,
                    HearingLength = record.HearingLength,
                    Phone = record.Phone,
                    RequestedByName = record.RequestedByName,
                    StyleOfCause = record.StyleOfCause,
                    TrialBookingId = record.TrialBookingId,
                    TrialLocationId = record.TrialLocationId,
                    TrialLocationName = record.TrialLocationName,
                    User = testUser
                };

                if (record.Selection1.HasValue)
                {
                    entity.TrialDateSelections.Add(
                        new ScTrialDateSelection
                        {
                            Rank = 1,
                            TrialStartDate = record.Selection1.Value
                        }
                    );
                }

                if (record.Selection2.HasValue)
                {
                    entity.TrialDateSelections.Add(
                        new ScTrialDateSelection
                        {
                            Rank = 2,
                            TrialStartDate = record.Selection2.Value
                        }
                    );
                }

                if (record.Selection3.HasValue)
                {
                    entity.TrialDateSelections.Add(
                        new ScTrialDateSelection
                        {
                            Rank = 3,
                            TrialStartDate = record.Selection3.Value
                        }
                    );
                }

                if (record.Selection4.HasValue)
                {
                    entity.TrialDateSelections.Add(
                        new ScTrialDateSelection
                        {
                            Rank = 4,
                            TrialStartDate = record.Selection4.Value
                        }
                    );
                }

                if (record.Selection5.HasValue)
                {
                    entity.TrialDateSelections.Add(
                        new ScTrialDateSelection
                        {
                            Rank = 5,
                            TrialStartDate = record.Selection5.Value
                        }
                    );
                }

                dbContext.ScTrialBookingRequests.Add(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
