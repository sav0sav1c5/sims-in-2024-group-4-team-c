using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Proxies;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using BookingApp.Domain.Model;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading;
using System.Reflection.Emit;
using System.Threading.Channels;
using System.Windows.Media.Media3D;


namespace BookingApp.Resources.DBAccess
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<AccommodationReservation> AccommodationReservations { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Tourist> Tourists { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<CurrentDate> CurrentDates { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<TourReservation> TourReservations { get; set; }
        public DbSet<TourParticipant> TourParticipants { get; set; }
        public DbSet<TourAttendance> TourAttendances { get; set; }
        public DbSet<AccommodationReservationReview> AccommodationReservationReviews { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<AccommodationReservationModificationRequest> AccommodationReservationModificationRequests { get; set; }
        public DbSet<TourReview> TourReviews { get; set; }
        public DbSet<TourRequest> TourRequests { get; set; }
        public DbSet<VoucherUse> VoucherUses { get; set; }
        public DbSet<Renovation> Renovations { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Forum> Forums { get; set; }
        
        public DbSet<ForumAlert> ForumAlert { get; set; }
        
        public DbSet<ComplexTourRequest> ComplexTourRequests { get; set; }
        public DbSet<ComplexTourPart> ComplexTourParts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(GlobalConfig.CnnVal("bookingAppData.db"));
            //optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Lets us use a string array seperated by commas
            //modelBuilder.Entity<AccommodationReservationReview>()
            //    .Property(e => e.ReviewImages)
            //    .HasConversion(
            //    .HasConversion(
            //        v => string.Join(',', v),
            //        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            //        );

            modelBuilder.Entity<ComplexTourPart>()
                .HasOne(cp => cp.ComplexTourRequest)
                .WithMany(c => c.Parts)
                .HasForeignKey(cp => cp.ComplexTourRequestId)
                .OnDelete(DeleteBehavior.Cascade);
            SeedData(modelBuilder);

            EstablishEntityRelations(modelBuilder);
        }


        private void SeedData(ModelBuilder modelBuilder)
        {
            SeedUser(modelBuilder);
            SeedGuest(modelBuilder);
            SeedTourist(modelBuilder);
            SeedLocation(modelBuilder);
            SeedAccommodation(modelBuilder);
            SeedAccommodationReservation(modelBuilder);
            SeedTour(modelBuilder);
            SeedCheckpoint(modelBuilder);
            SeedCurrentDate(modelBuilder);
            SeedGuide(modelBuilder);
            SeedOwner(modelBuilder);
            SeedTourReservation(modelBuilder);
            SeedTourParticipant(modelBuilder);
            SeedTourAttendance(modelBuilder);
            SeedAccommodationReservationReview(modelBuilder);
            SeedVouchers(modelBuilder);
            SeedAccommodationReservationModificationRequest(modelBuilder);
            SeedTourReviews(modelBuilder);
            SeedTourRequests(modelBuilder);
            SeedVoucherUses(modelBuilder);
            SeedRenovations(modelBuilder);
            SeedForums(modelBuilder);
            SeedComments(modelBuilder);
            SeedForumAlerts(modelBuilder);
            SeedComplexTourRequests(modelBuilder); 
            SeedComplexTourParts(modelBuilder);
        }

        private static void EstablishEntityRelations(ModelBuilder modelBuilder)
        {
            //Establish relationships 
            //BLAME: Nikola Radojcic for deleting the relationships and breaking the reservation feature
            //BLAME: Nikola Radojcic must establish relationships before creating migrations or updating the database

            //Establish relationships
            modelBuilder.Entity<Accommodation>()
            .HasOne(e => e.Location);

            modelBuilder.Entity<Tour>()
                .HasOne(e => e.Location);

            modelBuilder.Entity<Tour>()
                .HasMany(t => t.Checkpoints)
                .WithOne(c => c.Tour)
                .HasForeignKey(c => c.TourId);

            modelBuilder.Entity<Accommodation>()
                .HasMany(e => e.AccommodationReservations)
                .WithOne(e => e.Accommodation)
                .HasForeignKey(e => e.AccommodationId);

            //modelBuilder.Entity<Guest>()
            //    .HasMany(e => e.AccommodationReservations)
            //    .WithOne(e => e.Guest)
            //    .HasForeignKey(e => e.GuestId);

            modelBuilder.Entity<Checkpoint>()
                .HasOne(e => e.Location)
                .WithMany()
                .HasForeignKey(e => e.LocationId);

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Guide)
                .WithMany()
                .HasForeignKey(t => t.GuideId);

            //modelBuilder.Entity<AccommodationReservationModificationRequest>()
            //    .HasOne(e => e.AccommodationReservation)
            //    .WithMany(e => e.ModificationRequests)
            //    .HasForeignKey(e => e.AccommodationReservationId);


            //Note: Eager loading will be used
            modelBuilder.Entity<AccommodationReservationReview>()
                .HasOne(e => e.Guest);
            modelBuilder.Entity<AccommodationReservationReview>()
                .HasOne(e => e.Owner);
            modelBuilder.Entity<AccommodationReservationReview>()
                .HasOne(e => e.AccommodationReservation);
            modelBuilder.Entity<AccommodationReservationReview>()
                .HasOne(e => e.Accommodation);

        }

        private static void SeedForums(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Forum>().HasKey(key => key.Id);
            modelBuilder.Entity<Forum>().HasData(
                    new Forum
                    {
                        Id = 1,
                        Name = "Prenoćišta u Novom Sadu",
                        LocationId = 1,
                        Description = "Priuštiva prenoćišta u Novom Sadu",
                        AuthorId = 2
                    }

                );
        }

        private static void SeedForumAlerts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ForumAlert>().HasKey(key => key.Id);
            modelBuilder.Entity<ForumAlert>().HasData(
                    new ForumAlert
                    {
                        Id = 1,
                        ForumId = 1,
                        OwnerId = 11,
                        isOpened = false
                    }
                );
        }

        

        private static void SeedAccommodation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accommodation>().HasKey(key => key.Id);
            modelBuilder.Entity<Accommodation>().HasData(
                    new Accommodation
                    {
                        Id = 1,
                        Name = "Aparatmani Lučić",
                        LocationId = 1,
                        AccommodationType = AccommodationTypes.Type.Apartment,
                        MaxGuests = 5,
                        MinReservationDays = 1,
                        CancelationDeadline = 1,
                        Images = "/Resources/Images/house.png",
                        OwnerId = 12
                    },
                    new Accommodation
                    {
                        Id = 2,
                        Name = "Konak Živojin Mišić",
                        LocationId = 2,
                        AccommodationType = AccommodationTypes.Type.Cabin,
                        MaxGuests = 4,
                        MinReservationDays = 3,
                        CancelationDeadline = 5,
                        Images = "/Resources/Images/house.png,/Resources/Images/house2.jpg",
                        OwnerId = 12
                    }
                );
        }


        private static void SeedComments(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Comment>().HasKey(key => key.Id);
            modelBuilder.Entity<Comment>().HasData(
                    new Comment
                    {
                        Id = 1,
                        ForumId = 1,
                        UserId = 2,
                        PublishedDate = DateTime.ParseExact("18-03-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        IsOwnerComment = false,
                        CommentText = "Preporucujem konak u rumenackoj ulici, ne sjecam se kako se zove.",
                        NumOfReports = 0
                    }

                );
            
        }

        private static void SeedRenovations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Renovation>().HasKey(key => key.Id);
            modelBuilder.Entity<Renovation>().ToTable("Renovation");

            modelBuilder.Entity<Renovation>().HasData(
                    new
                    {
                        Id = 1,
                        AccommodationId = 2,
                        StartDate = DateOnly.ParseExact("18-03-2024", "dd-MM-yyyy", null),
                        EndDate = DateOnly.ParseExact("28-03-2024", "dd-MM-yyyy", null),
                        EstimatedDuration = 5,
                        State = true,
                        isCanceled = false
                    }
                );

        }


        private static void SeedUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<User>().HasData(
                    new //TODO: change this to some other class
                    {
                        Id = 1,
                        //UserType = UserType.Owner,  
                        Username = "pera",
                        Password = "peric",
                        FirstName = "Pera",
                        LastName = "Peric",
                        Email = "peraperic@gmail.com"
                    }
                );
            modelBuilder.Entity<User>().ToTable("User");
        }

        private static void SeedLocation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().HasKey(key => key.Id);

            modelBuilder.Entity<Location>().HasData(
                    new Location
                    {
                        Id = 1,
                        City = "Novi Sad",
                        Country = "Serbia"
                    },
                    new Location
                    {
                        Id = 2,
                        City = "Fruska gora",
                        Country = "Serbia"
                    },
                    new Location
                    {
                        Id = 3,
                        City = "Danilovgrad",
                        Country = "Crna Gora"
                    },
                    new Location
                    {
                        Id = 4,
                        City = "Sarajevo",
                        Country = "Bosnia and Herzegovina"
                    }
                );

            modelBuilder.Entity<Location>().ToTable("Location");
        }

        private void SeedAccommodationReservationReview(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccommodationReservationReview>().HasKey(key => key.Id);
            modelBuilder.Entity<AccommodationReservationReview>().HasData(
                    new
                    {
                        Id = 2,
                        AccommodationId = 1,
                        OwnerId = 12,
                        GuestId = 420,
                        AccommodationReservationId = 4,
                        Direction = true,
                        Comment = "Jako dobar gost svaka preporuka",
                        Cleanliness = 4,
                        Correctness = 1,
                        //owner:
                        RuleCompliance = 5,
                        //guest:
                        IsInNeedOfRenovation = false,
                        RenovationNeed = 0,
                        //ReviewImages = new[] { string.Empty },
                        Images = string.Empty 
                    },
                    new
                    {
                        Id = 3,
                        AccommodationId = 1,
                        OwnerId = 12,
                        GuestId = 420,
                        AccommodationReservationId = 4,
                        Direction = false,
                        Comment = "Dobar vlasnik",
                        Cleanliness = 5,
                        Correctness = 5,
                        //owner:
                        RuleCompliance = 0,
                        //guest:
                        IsInNeedOfRenovation = false,
                        RenovationNeed = 0,
                        //ReviewImages = new[] { string.Empty }
                        Images = string.Empty
                    }

                    //Guest test cases
                    ,
                    new
                    {
                        Id = 4,
                        AccommodationId = 1,
                        OwnerId = 12,
                        GuestId = 2,
                        AccommodationReservationId = 5,
                        Direction = false,
                        Comment = "Los vlasnik",
                        Cleanliness = 2,
                        Correctness = 2,
                        //owner:
                        RuleCompliance = 0,
                        //guest:
                        IsInNeedOfRenovation = true,
                        RenovationNeed = 2,
                        //ReviewImages = new[] { "/data/img/slika.jpg", "/data/img/house.jpg" }
                        Images = "/data/img/slika.jpg,/data/img/house.jpg"
                    },
                    new
                    {
                        Id = 5,
                        AccommodationId = 1,
                        OwnerId = 12,
                        GuestId = 2,
                        AccommodationReservationId = 6,
                        Direction = false,
                        Comment = "Ok vlasnik",
                        Cleanliness = 3,
                        Correctness = 4,
                        //owner:
                        RuleCompliance = 0,
                        //guest:
                        IsInNeedOfRenovation = false,
                        RenovationNeed = 0,
                        //ReviewImages = new[] { "/data/img/house.jpg" }
                        Iamges = "/data/img/slika.jpg"
                    },
                    new
                    {
                        Id = 6,
                        AccommodationId = 1,
                        OwnerId = 12,
                        GuestId = 2,
                        AccommodationReservationId = 6,
                        Direction = true,
                        Comment = "Prosjecan gost",
                        Cleanliness = 5,
                        Correctness = 5,
                        //owner:
                        RuleCompliance = 1,
                        //guest:
                        IsInNeedOfRenovation = false,
                        RenovationNeed = 0,
                        //ReviewImages = new[] { string.Empty }
                        Images = string.Empty 
                    }
                    //end of guest test cases




                );

            modelBuilder.Entity<AccommodationReservationReview>().ToTable("AccommodationReservationReview");
        }
        private void SeedAccommodationReservation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccommodationReservation>().HasKey(e => e.Id);

            modelBuilder.Entity<AccommodationReservation>().HasData(
                    new
                    {
                        Id = 1,
                        AccommodationId = 1,
                        GuestId = 2,
                        ReservationState = ReservationState.State.Reserved,
                        StartDate = DateTime.ParseExact("18-03-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 1
                    },
                    new
                    {
                        Id = 2,
                        AccommodationId = 1,
                        GuestId = 2,
                        ReservationState = ReservationState.State.Reserved,
                        StartDate = DateTime.ParseExact("25-03-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 2,
                        StayLength = 2
                    },
                    new
                    {
                        Id = 3,
                        AccommodationId = 1,
                        GuestId = 420,
                        ReservationState = ReservationState.State.Reserved,
                        StartDate = DateTime.ParseExact("28-03-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 3,
                        StayLength = 4
                    },
                    new
                    {
                        Id = 4,
                        AccommodationId = 1,
                        GuestId = 420,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("12-03-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 3,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 5,
                        AccommodationId = 1,
                        GuestId = 2,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("01-03-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 1
                    },
                    new
                    {
                        Id = 6,
                        AccommodationId = 1,
                        GuestId = 2,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("03-03-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 4,
                        StayLength = 4
                    },
                    new
                    {
                        Id = 7,
                        AccommodationId = 1,
                        GuestId = 2,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("15-03-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 4,
                        StayLength = 1
                    },
                    //The following are used for testing the super guest functionality
                    new
                    {
                        Id = 8,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("01-01-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 9,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("05-01-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 10,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("09-01-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 11,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("13-01-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 12,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("17-01-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 13,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("21-01-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 14,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("25-01-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 15,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("29-01-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 16,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("02-02-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 17,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("06-02-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    },
                    new
                    {
                        Id = 18,
                        AccommodationId = 2,
                        GuestId = 421,
                        ReservationState = ReservationState.State.Ended,
                        StartDate = DateTime.ParseExact("10-02-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 1,
                        StayLength = 3
                    }
                );

            modelBuilder.Entity<AccommodationReservation>().ToTable("AccommodationReservation");

        }

        private static void SeedAccommodationReservationModificationRequest(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccommodationReservationModificationRequest>().HasKey(e => e.Id);

            modelBuilder.Entity<AccommodationReservationModificationRequest>().HasData(
                    new
                    {
                        Id = 4,
                        AccommodationReservationId = 2,
                        RequestState = ReservationModificationRequestState.State.Pending,
                        StartDate = DateTime.ParseExact("01-04-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 2,
                        StayLength = 2,
                        OwnerComment = string.Empty
                    },
                    new
                    {
                        Id = 5,
                        AccommodationReservationId = 3,
                        RequestState = ReservationModificationRequestState.State.Pending,
                        StartDate = DateTime.ParseExact("02-04-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 3,
                        StayLength = 4,
                        OwnerComment = string.Empty
                    },
                    new
                    {
                        Id = 6,
                        AccommodationReservationId = 1,
                        RequestState = ReservationModificationRequestState.State.Denied,
                        StartDate = DateTime.ParseExact("03-04-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                        NumberOfGuests = 2,
                        StayLength = 3,
                        OwnerComment = "Sorry but no!"
                    }
            );

            modelBuilder.Entity<AccommodationReservationModificationRequest>().ToTable("AccommodationReservationModificationRequest");
        }

        private static void SeedCurrentDate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentDate>().HasKey(currentDate => currentDate.Id);
            modelBuilder.Entity<CurrentDate>().HasData(
                    new CurrentDate
                    {
                        Id = 1,
                        Date = DateTime.ParseExact("17-03-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null)

                    }
                );
            modelBuilder.Entity<CurrentDate>().ToTable("CurrentDate");

        }

        private void SeedGuest(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>().HasData(
                    new Guest
                    {
                        Id = 2,
                        //UserType = UserType.Guest,    
                        Username = "dusko",
                        Password = "radic",
                        FirstName = "Dusko",
                        LastName = "Radic",
                        Email = "lopD@gmail.com",
                        IsSuperGuest = true,
                        SuperGuestDiscountCouponCount = 1
                    },
                    new Guest
                    {
                        Id = 420,
                        //UserType = UserType.Guest,    
                        Username = "radenko",
                        Password = "radic",
                        FirstName = "Radenko",
                        LastName = "Radic",
                        Email = "radandecko@gmail.com",
                        IsSuperGuest = false,
                        SuperGuestDiscountCouponCount = 0
                    },
                    new Guest
                    {
                        Id = 421,
                        //UserType = UserType.Guest,    
                        Username = "superdusko",
                        Password = "radic",
                        FirstName = "Dusko",
                        LastName = "Radic",
                        Email = "superdusko@gmail.com",
                        IsSuperGuest = false,
                        SuperGuestDiscountCouponCount = 0
                    }
                ) ;
        }

        private void SeedTourist(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tourist>().HasData(
                    new Tourist
                    {
                        Id = 3,
                        //UserType = UserType.Guest,    
                        Username = "dragan",
                        Password = "123",
                        FirstName = "Dragan",
                        LastName = "Vucicevic",
                        Email = "gagi@gmail.com"
                    },
                    new Tourist
                    {
                        Id = 4,
                        //UserType = UserType.Guest,    
                        Username = "savo",
                        Password = "ftn",
                        FirstName = "Savo",
                        LastName = "Savic",
                        Email = "savic@gmail.com"
                    },
                    new Tourist
                    {
                        Id = 5,
                        //UserType = UserType.Guest,    
                        Username = "filip",
                        Password = "a",
                        FirstName = "Filip",
                        LastName = "Filipovic",
                        Email = "filip@gmail.com"
                    },
                    new Tourist
                    {
                        Id = 6,
                        //UserType = UserType.Guest,    
                        Username = "marko",
                        Password = "456",
                        FirstName = "Marko",
                        LastName = "Kuburic",
                        Email = "maremagare@gmail.com"
                    },
                    new Tourist
                    {
                        Id = 7,
                        //UserType = UserType.Guest,    
                        Username = "Milos",
                        Password = "def",
                        FirstName = "Milos",
                        LastName = "Trifkovic",
                        Email = "losmi@gmail.com"
                    },
                    new Tourist
                    {
                        Id = 8,
                        //UserType = UserType.Guest,    
                        Username = "sav0",
                        Password = "s",
                        FirstName = "Savo",
                        LastName = "Savic",
                        Email = "savic0120@gmail.com"
                    }
                );
        }

        private static void SeedTour(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>().HasKey(e => e.Id);

            modelBuilder.Entity<Tour>().HasData(
                    new Tour
                    {
                        Id = 1,
                        Name = "Parkovi Novog Sada",
                        LocationId = 1,
                        Description = "Obilazak parkova u Novom Sadu",
                        Language = "English",
                        MaxTouristNumber = 15,
                        AvailableSeats = 2,
                        GuideId = 9,
                        Duration = 2,
                        Images = "https://gradskeinfo.rs/wp-content/uploads/2022/04/dunavski-park-1.jpg"
                    },
                    new Tour
                    {
                        Id = 2,
                        Name = "Fruskogorski manastiri",
                        LocationId = 2,
                        Description = "Obilazak fruskogorskih manastira. Na prostoru 50 kilometara dužine i 10 kilometara širine na Fruškoj Gori je smešteno šesnaest srpskih pravoslavnih manastira.",
                        Language = "Serbian",
                        MaxTouristNumber = 20,
                        AvailableSeats = 0,
                        GuideId = 9,
                        Duration = 2,
                        Images = "https://www.idemonaput.rs/wp-content/uploads/2020/11/cov17-768x403.jpg"
                    },
                    new Tour
                    {
                        Id = 3,
                        Name = "Manastir Ostrog",
                        LocationId = 3,
                        Description = "Hodočašće u manastir Ostrog",
                        Language = "Spanish",
                        MaxTouristNumber = 25,
                        AvailableSeats = 0,
                        GuideId = 10,
                        Duration = 2,
                        Images = "https://i0.wp.com/www.durmitor.rs/wp-content/uploads/2014/03/ostrog.jpg?w=800&ssl=1"
                    },
                    new Tour
                    {
                        Id = 4,
                        Name = "Visit Sarajevo",
                        LocationId = 4,
                        Description = "You have the chance to visit the capital of Bosnia and Herzegovina as well as its greatest sights",
                        Language = "German",
                        MaxTouristNumber = 15,
                        AvailableSeats = 0,
                        GuideId = 9,
                        Duration = 2,
                        Images = "https://a.cdn-hotels.com/gdcs/production32/d702/0efac108-32eb-435f-811e-59d516d808ac.jpg?impolicy=fcrop&w=800&h=533&q=medium"
                    }
                );

            modelBuilder.Entity<Tour>().ToTable("Tour");
        }

        private static void SeedCheckpoint(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Checkpoint>().HasKey(e => e.Id);

            modelBuilder.Entity<Checkpoint>().HasData(
                    new Checkpoint
                    {
                        Id = 1,
                        Name = "Limanski park",
                        LocationId = 1,
                        TourId = 1
                    },
                    new Checkpoint
                    {
                        Id = 2,
                        Name = "Dunavski park",
                        LocationId = 1,
                        TourId = 1
                    },
                    new Checkpoint
                    {
                        Id = 3,
                        Name = "Fruskogorski manastiri",
                        LocationId = 2,
                        TourId = 2
                    },
                    new Checkpoint
                    {
                        Id = 4,
                        Name = "Manastir Ostrog",
                        LocationId = 3,
                        TourId = 3,
                        IsActive = false
                    },
                    new Checkpoint
                    {
                        Id = 5,
                        Name = "Manastir Krušedol",
                        LocationId = 2,
                        TourId = 2
                    },
                    new Checkpoint
                    {
                        Id = 6,
                        Name = "Park na Grbavici",
                        LocationId = 1,
                        TourId = 1
                    },
                    new Checkpoint
                    {
                        Id = 7,
                        Name = "Železnički park",
                        LocationId = 1,
                        TourId = 1
                    },
                    new Checkpoint
                    {
                        Id = 8,
                        Name = "Kamenički park",
                        LocationId = 1,
                        TourId = 1
                    },
                    new Checkpoint
                    {
                        Id = 9,
                        Name = "Petkovica",
                        LocationId = 2,
                        TourId = 2
                    },
                    new Checkpoint
                    {
                        Id = 10,
                        Name = "Mala Remeta",
                        LocationId = 2,
                        TourId = 2
                    },
                    new Checkpoint
                    {
                        Id = 11,
                        Name = "Bascarsija",
                        LocationId = 4,
                        TourId = 4
                    },
                    new Checkpoint
                    {
                        Id = 12,
                        Name = "Trebevic",
                        LocationId = 4,
                        TourId = 4
                    }
                );

            modelBuilder.Entity<Checkpoint>().ToTable("Checkpoint");
        }

        private void SeedGuide(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guide>().HasData(
                    new Guide
                    {
                        Id = 9,
                        //UserType = UserType.Guide,    
                        Username = "a",
                        Password = "a",
                        FirstName = "Milovan",
                        LastName = "Drecun",
                        Email = "mojaavlija@gmail.com"
                    },
                    new Guide
                    {
                        Id = 10,
                        //UserType = UserType.Guide,    
                        Username = "spopalko",
                        Password = "ftn",
                        FirstName = "Milan",
                        LastName = "Topalovic",
                        Email = "spopadanTopalko@gmail.com"
                    }
                );
        }

        public void SeedOwner(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().HasData(
                    new Owner
                    {
                        Id = 11,
                        Username = "dragan",
                        Password = "misic",
                        FirstName = "Dragan",
                        LastName = "Misic",
                        Email = "draganmisic59@gmail.com"
                    },

                    new Owner
                    {
                        Id = 12,
                        Username = "daniel",
                        Password = "misic",
                        FirstName = "Daniel",
                        LastName = "Misic",
                        Email = "danielmisic@gmail.com"
                    }
                  );
        }

        private static void SeedTourReservation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourReservation>().HasKey(e => e.Id);

            modelBuilder.Entity<TourReservation>().HasData(
                    new TourReservation
                    {
                        Id = 1,
                        TourId = 1,
                        GuideId = 9,
                        TouristsNumber = 4,
                        TourState = TourStates.Type.Finished,
                        DateAndTime = DateTime.ParseExact("28-05-2024 16:30:00", "dd-MM-yyyy HH:mm:ss", null)
                    },
                    new TourReservation
                    {
                        Id = 2,
                        TourId = 2,
                        GuideId = 10,
                        TouristsNumber = 2,
                        TourState = TourStates.Type.Finished,
                        DateAndTime = DateTime.ParseExact("27-05-2024 12:30:00", "dd-MM-yyyy HH:mm:ss", null)
                    },
                    new TourReservation
                    {
                        Id = 3,
                        TourId = 1,
                        GuideId = 9,
                        TouristsNumber = 1,
                        TourState = TourStates.Type.Pending,
                        DateAndTime = DateTime.ParseExact("23-05-2024 17:00:00", "dd-MM-yyyy HH:mm:ss", null)
                    },
                    new TourReservation
                    {
                        Id = 4,
                        TourId = 2,
                        GuideId = 9,
                        TouristsNumber = 1,
                        TourState = TourStates.Type.Finished,
                        DateAndTime = DateTime.ParseExact("07-05-2024 11:30:00", "dd-MM-yyyy HH:mm:ss", null)
                    },
                    new TourReservation
                    {
                        Id = 5,
                        TourId = 3,
                        GuideId = 10,
                        TouristsNumber = 1,
                        TourState = TourStates.Type.Finished,
                        DateAndTime = DateTime.ParseExact("28-05-2024 13:30:00", "dd-MM-yyyy HH:mm:ss", null)
                    },
                    new TourReservation
                    {
                        Id = 6,
                        TourId = 2,
                        GuideId = 9,
                        TouristsNumber = 0,
                        TourState = TourStates.Type.Pending,
                        DateAndTime = DateTime.ParseExact("18-05-2024 14:30:00", "dd-MM-yyyy HH:mm:ss", null)
                    },
                    new TourReservation
                    {
                        Id = 7,
                        TourId = 1,
                        GuideId = 9,
                        TouristsNumber = 0,
                        TourState = TourStates.Type.Finished,
                        DateAndTime = DateTime.ParseExact("28-03-2024 17:30:00", "dd-MM-yyyy HH:mm:ss", null)
                    },
                    new TourReservation
                    {
                        Id = 8,
                        TourId = 4,
                        GuideId = 9,
                        TouristsNumber = 0,
                        TourState = TourStates.Type.Pending,
                        DateAndTime = DateTime.ParseExact("16-05-2024 17:30:00", "dd-MM-yyyy HH:mm:ss", null)
                    }
                );

            modelBuilder.Entity<TourReservation>().ToTable("TourReservation");
        }

        private static void SeedTourParticipant(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourParticipant>().HasKey(ta => ta.Id);

            modelBuilder.Entity<TourParticipant>().HasData(
                    new TourParticipant
                    {
                        Id = 1,
                        TourReservationId = 1,
                        FirstName = "Nikola",
                        LastName = "Radojcic",
                        Email = "volemzenerumunke@gmail.com",
                        IsPresent = false,
                        //CheckpointJoinedId = 1,
                        Age = 22
                    },
                    new TourParticipant
                    {
                        Id = 2,
                        TourReservationId = 1,
                        FirstName = "Dragan",
                        LastName = "Misic",
                        Email = "bosanac02@gmail.com",
                        IsPresent = false,
                        Age = 13
                    },
                    new TourParticipant
                    {
                        Id = 3,
                        TourReservationId = 3,
                        FirstName = "Ana",
                        LastName = "Petrovic",
                        Email = "anapetrovic@gmail.com",
                        IsPresent = false,
                        Age = 55,
                    },
                    new TourParticipant
                    {
                        Id = 4,
                        TourReservationId = 1,
                        FirstName = "Marko",
                        LastName = "Jovanovic",
                        Email = "markojovanovic@gmail.com",
                        IsPresent = false,
                        Age = 33
                    },
                    new TourParticipant
                    {
                        Id = 5,
                        TourReservationId = 2,
                        FirstName = "Milos",
                        LastName = "Nikolic",
                        Email = "milosnikolic@gmail.com",
                        IsPresent = false,
                        Age = 14
                    },
                    new TourParticipant
                    {
                        Id = 6,
                        TourReservationId = 2,
                        FirstName = "Ivana",
                        LastName = "Djokovic",
                        Email = "ivanadjokovic@gmail.com",
                        IsPresent = false,
                        Age = 25
                    },
                    new TourParticipant
                    {
                        Id = 7,
                        TourReservationId = 2,
                        FirstName = "Milana",
                        LastName = "Djokovic",
                        Email = "milanadjokovic@gmail.com",
                        IsPresent = false,
                        Age = 56
                    },
                    new TourParticipant
                    {
                        Id = 8,
                        TourRequestId = 1,
                        FirstName = "Milanda",
                        LastName = "Djokovidsc",
                        Email = "milanadjokodsavic@gmail.com",
                        IsPresent = false,
                        Age = 33
                    }
                );

            modelBuilder.Entity<TourParticipant>().ToTable("TourParticipant");
        }

        private static void SeedTourAttendance(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourAttendance>().HasKey(x => x.Id);
            modelBuilder.Entity<TourAttendance>().HasData(
                new TourAttendance
                {
                    Id = 1,
                    TouristId = 3,
                    TourReservationId = 1,
                    IsPresent = false,
                    IsConfirmed = false,
                    Age = 22

                },
                new TourAttendance
                {
                    Id = 2,
                    TouristId = 8,
                    TourReservationId = 3,
                    IsPresent = false,
                    IsConfirmed = false,
                    Age = 21

                },
                new TourAttendance
                {
                    Id = 3,
                    TouristId = 8,
                    TourReservationId = 4,
                    IsPresent = true,
                    IsConfirmed = true,
                    Age = 21,
                    CheckpointJoinedId = 1
                },
                new TourAttendance
                {
                    Id = 4,
                    TouristId = 8,
                    TourReservationId = 5,
                    IsPresent = true,
                    IsConfirmed = false,
                    Age = 21
                },
                new TourAttendance
                {
                    Id = 5,
                    TouristId = 3,
                    TourReservationId = 6,
                    IsPresent = false,
                    IsConfirmed = false,
                    Age = 27
                },
                new TourAttendance
                {
                    Id = 6,
                    TouristId = 3,
                    TourReservationId = 7,
                    IsPresent = false,
                    IsConfirmed = false,
                    Age = 27,
                    CheckpointJoinedId = 2
                }
            );

            modelBuilder.Entity<TourAttendance>().ToTable("TourAttendance");
        }

        private static void SeedVouchers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Voucher>().HasKey(x => x.Id);

            modelBuilder.Entity<Voucher>().HasData(
                new Voucher
                {
                    Id = 1,
                    Name = "20% discount",
                    ExpirationDate = DateTime.Today.AddMonths(-2),
                    TouristId = 8
                },
                new Voucher
                {
                    Id = 2,
                    Name = "Summer starter",
                    ExpirationDate = DateTime.Today.AddMonths(-6),
                    TouristId = 8
                },
                new Voucher
                {
                    Id = 3,
                    Name = "Summer starter",
                    ExpirationDate = DateTime.Today.AddMonths(-6),
                    TouristId = 8
                },
                new Voucher
                {
                    Id = 4,
                    Name = "Winter Wonderland",
                    ExpirationDate = DateTime.Today.AddMonths(-4),
                    TouristId = 8
                },
                new Voucher
                {
                    Id = 5,
                    Name = "Family Pack",
                    ExpirationDate = DateTime.Today.AddMonths(-3),
                    TouristId = 8
                },
                new Voucher
                {
                    Id = 6,
                    Name = "Adventure Trip",
                    ExpirationDate = DateTime.Today.AddMonths(-5),
                    TouristId = 8
                }
            );
            modelBuilder.Entity<Voucher>().ToTable("Voucher");
        }

        private static void SeedTourReviews(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourReview>().HasKey(x => x.Id);

            modelBuilder.Entity<TourReview>().HasData(
                 new TourReview
                 {
                     Id = 1,
                     Knowledge = 4,
                     Language = 5,
                     Interestingness = 4,
                     Comment = "Great experience!",
                     ReviewImages = "",
                     GuideId = 9,
                     TourReservationId = 6,
                     TourAttendanceId = 6,
                     IsValid = true
                 }/*,
                 new TourReview
                 {
                     Id = 2,
                     Knowledge = 5,
                     Language = 5,
                     Interestingness = 4,
                     Comment = "Great experience!",
                     GuideId = 9,
                     TourReservationId = 7,
                     TourAttendanceId = 6,
                     iSValid = true
                 }*/
            );
            modelBuilder.Entity<TourReview>().ToTable("TourReview");
        }
        private static void SeedTourRequests(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourRequest>().HasKey(x => x.Id);

            modelBuilder.Entity<TourRequest>().HasData(
                 new TourRequest
                 {
                     Id = 1,
                     TouristId = 8,
                     TourName = "Recorrido por las Ruinas Mayas",
                     StartDate = DateTime.Today.AddYears(-2).AddDays(-75),
                     EndDate = DateTime.Today.AddYears(-2).AddDays(-72),
                     Country = "Mexico",
                     City = "Chichen Itza",
                     Language = "Spanish",
                     Description = "Descubre las antiguas ruinas mayas y su fascinante historia.",
                     NumberOfParticipants = 2,
                     TourRequestState = TourRequestStates.Type.Accepted,
                 },
                 new TourRequest
                 {
                     Id = 2,
                     TouristId = 8,
                     TourName = "Tour of Ancient Rome",
                     StartDate = DateTime.Today.AddYears(-2).AddDays(-29),
                     EndDate = DateTime.Today.AddYears(-2).AddDays(-27),
                     Country = "Italy",
                     City = "Rome",
                     Language = "Italian",
                     Description = "Experience the glory of ancient Rome with a knowledgeable guide.",
                     NumberOfParticipants = 3,
                     TourRequestState = TourRequestStates.Type.Accepted,
                 },
                 new TourRequest
                 {
                     Id = 3,
                     TouristId = 8,
                     TourName = "Hiking in the Swiss Alps",
                     StartDate = DateTime.Today.AddYears(-2).AddDays(-45),
                     EndDate = DateTime.Today.AddYears(-2).AddDays(-40),
                     Country = "Switzerland",
                     City = "Interlaken",
                     Language = "English",
                     Description = "Embark on an adventurous hike through the breathtaking Swiss Alps.",
                     NumberOfParticipants = 2,
                     TourRequestState = TourRequestStates.Type.Declined,
                 },
                 new TourRequest
                 {
                     Id = 4,
                     TouristId = 8,
                     TourName = "Wine Tasting in Bordeaux",
                     StartDate = DateTime.Today.AddYears(-1).AddDays(-50),
                     EndDate = DateTime.Today.AddYears(-1).AddDays(-48),
                     Country = "France",
                     City = "Bordeaux",
                     Language = "French",
                     Description = "Indulge in the finest wines of Bordeaux and explore its picturesque vineyards.",
                     NumberOfParticipants = 1,
                     TourRequestState = TourRequestStates.Type.Declined,
                 },
                 new TourRequest
                 {
                     Id = 5,
                     TouristId = 8,
                     TourName = "Visiting Bordeaux",
                     StartDate = DateTime.Today.AddYears(-1).AddDays(-20),
                     EndDate = DateTime.Today.AddYears(-1).AddDays(-18),
                     Country = "France",
                     City = "Bordeaux",
                     Language = "English",
                     Description = "Indulge in the finest wines of Bordeaux and explore its picturesque vineyards.",
                     NumberOfParticipants = 4,
                     TourRequestState = TourRequestStates.Type.Accepted,
                 },
                 new TourRequest
                 {
                     Id = 6,
                     TouristId = 8,
                     TourName = "Aventura en la Selva Amazónica",
                     StartDate = DateTime.Today.AddYears(-1).AddDays(-10),
                     EndDate = DateTime.Today.AddYears(-1).AddDays(-8),
                     Country = "Peru",
                     City = "Iquitos",
                     Language = "Spanish",
                     Description = "Sumérgete en la exuberante selva amazónica y descubre su increíble biodiversidad.",
                     NumberOfParticipants = 1,
                     TourRequestState = TourRequestStates.Type.Declined,
                 },
                 new TourRequest
                 {
                     Id = 7,
                     TouristId = 8,
                     TourName = "Obilazak Petrovaradina",
                     StartDate = DateTime.Today.AddDays(6),
                     EndDate = DateTime.Today.AddDays(12),
                     Country = "Serbia",
                     City = "Novi Sad",
                     Language = "English",
                     Description = "Obilazak Petrovaradinske tvrdjave i njenih tunela",
                     NumberOfParticipants = 1,
                 },
                 new TourRequest
                 {
                     Id = 8,
                     TouristId = 8,
                     TourName = "Obilazak Crkvi Novog Sada",
                     StartDate = DateTime.Today.AddDays(8),
                     EndDate = DateTime.Today.AddDays(13),
                     Country = "Serbia",
                     City = "Novi Sad",
                     Language = "Spanish",
                     Description = "Crkve Starog grada",
                     NumberOfParticipants = 1
                 },
                 new TourRequest
                 {
                     Id = 9,
                     TouristId = 8,
                     TourName = "Posetimo Bosnu",
                     StartDate = DateTime.Today.AddDays(-7),
                     EndDate = DateTime.Today.AddDays(-5),
                     Country = "Bosnia and Herzegovina",
                     City = "Brcko DS",
                     Language = "German",
                     Description = "Visit the most beautiful beauties of Bosnia with us.",
                     NumberOfParticipants = 1,
                     TourRequestState = TourRequestStates.Type.Declined,
                 }
            );
            modelBuilder.Entity<TourRequest>().ToTable("TourRequest");
        }

        private static void SeedVoucherUses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VoucherUse>().HasKey(vu => new { vu.VoucherId, vu.TourReservationId });
            modelBuilder.Entity<VoucherUse>().HasData(
                 new VoucherUse
                 {
                    VoucherId = 3,
                    TourReservationId = 1
                 }
            );

            modelBuilder.Entity<VoucherUse>().ToTable("VoucherUse");
        }

        private static void SeedComplexTourRequests(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComplexTourRequest>().HasKey(x => x.Id);
            modelBuilder.Entity<ComplexTourRequest>().HasData(
                new ComplexTourRequest
                {
                    Id = 1,
                    Name = "Upoznaj Beograd i Novi Sad",
                    TouristId = 8
                },
                new ComplexTourRequest
                {
                     Id = 2,
                     Name = "Obilazak zapada",
                     TouristId = 8
                 }
            );
            modelBuilder.Entity<ComplexTourRequest>().ToTable("ComplexTourRequest");
        }

        private static void SeedComplexTourParts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComplexTourPart>().HasKey(x => x.Id);
            modelBuilder.Entity<ComplexTourPart>().HasData(
                new ComplexTourPart
                {
                    Id = 1,
                    TourName = "Zanimljivosti Beograda",
                    StartDate = DateTime.ParseExact("21-06-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                    EndDate = DateTime.ParseExact("23-06-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                    ApprovedDate = DateTime.ParseExact("23-06-2024 11:00:00", "dd-MM-yyyy HH:mm:ss", null),
                    Country = "Serbia",
                    City = "Belgrade",
                    Language = "English",
                    Description = "A simple tour part in Belgrade.",
                    NumberOfParticipants = 10,
                    NotifyTourist = false,
                    TourRequestState = TourRequestStates.Type.Pending, // Pretpostavljam da je ovo validna vrednost
                    ComplexTourRequestId = 1 // ID gore navedene ture
                },
                new ComplexTourPart
                {
                    Id = 2,
                    TourName = "Zanimljivosti Novog Sada",
                    StartDate = DateTime.ParseExact("24-06-2024 09:00:00", "dd-MM-yyyy HH:mm:ss", null),
                    EndDate = DateTime.ParseExact("26-06-2024 18:00:00", "dd-MM-yyyy HH:mm:ss", null),
                    ApprovedDate = DateTime.ParseExact("26-06-2024 18:00:00", "dd-MM-yyyy HH:mm:ss", null),
                    Country = "Serbia",
                    City = "Novi Sad",
                    Language = "English",
                    Description = "A simple tour part in Novi Sad.",
                    NumberOfParticipants = 15,
                    NotifyTourist = false,
                    TourRequestState = TourRequestStates.Type.Pending, 
                    ComplexTourRequestId = 1 
                },
                new ComplexTourPart
                {
                    Id = 3,
                    TourName = "Obilazak Malog Zvornika",
                    StartDate = DateTime.ParseExact("22-06-2024 09:00:00", "dd-MM-yyyy HH:mm:ss", null),
                    EndDate = DateTime.ParseExact("24-06-2024 18:00:00", "dd-MM-yyyy HH:mm:ss", null),
                    ApprovedDate = DateTime.ParseExact("24-06-2024 18:00:00", "dd-MM-yyyy HH:mm:ss", null),
                    Country = "Serbia",
                    City = "Mali Zvornik",
                    Language = "English",
                    Description = "Ae da ronimo po Drini gnjurci",
                    NumberOfParticipants = 10,
                    NotifyTourist = false,
                    TourRequestState = TourRequestStates.Type.Pending,
                    ComplexTourRequestId = 2
                }
            );
            modelBuilder.Entity<ComplexTourPart>().ToTable("ComplexTourPart");
        }
    }
}