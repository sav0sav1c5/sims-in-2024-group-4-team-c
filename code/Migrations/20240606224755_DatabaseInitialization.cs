using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingApp.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentDate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForumAlert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ForumId = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    isOpened = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumAlert", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Renovation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EstimatedDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<bool>(type: "INTEGER", nullable: false),
                    isCanceled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renovation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    IsSuperGuest = table.Column<bool>(type: "INTEGER", nullable: true),
                    SuperGuestDiscountCouponCount = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accommodation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AccommodationType = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    MinReservationDays = table.Column<int>(type: "INTEGER", nullable: false),
                    CancelationDeadline = table.Column<int>(type: "INTEGER", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accommodation_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComplexTourRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TouristId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexTourRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplexTourRequest_User_TouristId",
                        column: x => x.TouristId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsClosed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsUseful = table.Column<bool>(type: "INTEGER", nullable: false),
                    OwnerComments = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestComments = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forum_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forum_User_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    MaxTouristNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    AvailableSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    GuideId = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tour_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tour_User_GuideId",
                        column: x => x.GuideId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TouristId = table.Column<int>(type: "INTEGER", nullable: false),
                    TourName = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfParticipants = table.Column<int>(type: "INTEGER", nullable: false),
                    NotifyTourist = table.Column<bool>(type: "INTEGER", nullable: false),
                    TourRequestState = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourRequest_User_TouristId",
                        column: x => x.TouristId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TouristId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsGlobal = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voucher_User_TouristId",
                        column: x => x.TouristId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    StayLength = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservationState = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationReservation_Accommodation_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccommodationReservation_User_GuestId",
                        column: x => x.GuestId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComplexTourPart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TourName = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfParticipants = table.Column<int>(type: "INTEGER", nullable: false),
                    NotifyTourist = table.Column<bool>(type: "INTEGER", nullable: false),
                    GuideId = table.Column<int>(type: "INTEGER", nullable: false),
                    TourRequestState = table.Column<int>(type: "INTEGER", nullable: false),
                    ComplexTourRequestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexTourPart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplexTourPart_ComplexTourRequest_ComplexTourRequestId",
                        column: x => x.ComplexTourRequestId,
                        principalTable: "ComplexTourRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ForumId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsOwnerComment = table.Column<bool>(type: "INTEGER", nullable: false),
                    CommentText = table.Column<string>(type: "TEXT", nullable: false),
                    NumOfReports = table.Column<int>(type: "INTEGER", nullable: false),
                    IsUseful = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Forum_ForumId",
                        column: x => x.ForumId,
                        principalTable: "Forum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checkpoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    TourId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkpoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkpoint_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checkpoint_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TourId = table.Column<int>(type: "INTEGER", nullable: false),
                    GuideId = table.Column<int>(type: "INTEGER", nullable: false),
                    TouristsNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TourState = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourReservation_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourReservation_User_GuideId",
                        column: x => x.GuideId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationReservationModificationRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccommodationReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestState = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    StayLength = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerComment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationReservationModificationRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationReservationModificationRequest_AccommodationReservation_AccommodationReservationId",
                        column: x => x.AccommodationReservationId,
                        principalTable: "AccommodationReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationReservationReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccommodationId = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    AccommodationReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cleanliness = table.Column<int>(type: "INTEGER", nullable: true),
                    RuleCompliance = table.Column<int>(type: "INTEGER", nullable: true),
                    Correctness = table.Column<int>(type: "INTEGER", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    Direction = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsInNeedOfRenovation = table.Column<bool>(type: "INTEGER", nullable: true),
                    RenovationNeed = table.Column<int>(type: "INTEGER", nullable: true),
                    Images = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationReservationReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationReservationReview_AccommodationReservation_AccommodationReservationId",
                        column: x => x.AccommodationReservationId,
                        principalTable: "AccommodationReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccommodationReservationReview_Accommodation_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccommodationReservationReview_User_GuestId",
                        column: x => x.GuestId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccommodationReservationReview_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TouristId = table.Column<int>(type: "INTEGER", nullable: false),
                    TourReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsPresent = table.Column<bool>(type: "INTEGER", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CheckpointJoinedId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourAttendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourAttendance_Checkpoint_CheckpointJoinedId",
                        column: x => x.CheckpointJoinedId,
                        principalTable: "Checkpoint",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TourAttendance_TourReservation_TourReservationId",
                        column: x => x.TourReservationId,
                        principalTable: "TourReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourAttendance_User_TouristId",
                        column: x => x.TouristId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourParticipant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TourReservationId = table.Column<int>(type: "INTEGER", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    IsPresent = table.Column<bool>(type: "INTEGER", nullable: false),
                    CheckpointJoinedId = table.Column<int>(type: "INTEGER", nullable: true),
                    TourRequestId = table.Column<int>(type: "INTEGER", nullable: true),
                    ComplexTourPartId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourParticipant_ComplexTourPart_ComplexTourPartId",
                        column: x => x.ComplexTourPartId,
                        principalTable: "ComplexTourPart",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TourParticipant_TourRequest_TourRequestId",
                        column: x => x.TourRequestId,
                        principalTable: "TourRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TourParticipant_TourReservation_TourReservationId",
                        column: x => x.TourReservationId,
                        principalTable: "TourReservation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VoucherUse",
                columns: table => new
                {
                    VoucherId = table.Column<int>(type: "INTEGER", nullable: false),
                    TourReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherUse", x => new { x.VoucherId, x.TourReservationId });
                    table.ForeignKey(
                        name: "FK_VoucherUse_TourReservation_TourReservationId",
                        column: x => x.TourReservationId,
                        principalTable: "TourReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherUse_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Knowledge = table.Column<int>(type: "INTEGER", nullable: false),
                    Language = table.Column<int>(type: "INTEGER", nullable: false),
                    Interestingness = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    ReviewImages = table.Column<string>(type: "TEXT", nullable: false),
                    GuideId = table.Column<int>(type: "INTEGER", nullable: false),
                    TourReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsValid = table.Column<bool>(type: "INTEGER", nullable: false),
                    TourAttendanceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourReview_TourAttendance_TourAttendanceId",
                        column: x => x.TourAttendanceId,
                        principalTable: "TourAttendance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourReview_TourReservation_TourReservationId",
                        column: x => x.TourReservationId,
                        principalTable: "TourReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CurrentDate",
                columns: new[] { "Id", "Date" },
                values: new object[] { 1, new DateTime(2024, 3, 17, 11, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ForumAlert",
                columns: new[] { "Id", "ForumId", "OwnerId", "isOpened" },
                values: new object[] { 1, 1, 11, false });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City", "Country" },
                values: new object[,]
                {
                    { 1, "Novi Sad", "Serbia" },
                    { 2, "Fruska gora", "Serbia" },
                    { 3, "Danilovgrad", "Crna Gora" },
                    { 4, "Sarajevo", "Bosnia and Herzegovina" }
                });

            migrationBuilder.InsertData(
                table: "Renovation",
                columns: new[] { "Id", "AccommodationId", "EndDate", "EstimatedDuration", "StartDate", "State", "isCanceled" },
                values: new object[] { 1, 2, new DateOnly(2024, 3, 28), 5, new DateOnly(2024, 3, 18), true, false });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 1, "User", "peraperic@gmail.com", "Pera", "Peric", "peric", "pera" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "IsSuperGuest", "LastName", "Password", "SuperGuestDiscountCouponCount", "Username" },
                values: new object[] { 2, "Guest", "lopD@gmail.com", "Dusko", true, "Radic", "radic", 1, "dusko" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { 3, "Tourist", "gagi@gmail.com", "Dragan", "Vucicevic", "123", "dragan" },
                    { 4, "Tourist", "savic@gmail.com", "Savo", "Savic", "ftn", "savo" },
                    { 5, "Tourist", "filip@gmail.com", "Filip", "Filipovic", "a", "filip" },
                    { 6, "Tourist", "maremagare@gmail.com", "Marko", "Kuburic", "456", "marko" },
                    { 7, "Tourist", "losmi@gmail.com", "Milos", "Trifkovic", "def", "Milos" },
                    { 8, "Tourist", "savic0120@gmail.com", "Savo", "Savic", "s", "sav0" },
                    { 9, "Guide", "mojaavlija@gmail.com", "Milovan", "Drecun", "a", "a" },
                    { 10, "Guide", "spopadanTopalko@gmail.com", "Milan", "Topalovic", "ftn", "spopalko" },
                    { 11, "Owner", "draganmisic59@gmail.com", "Dragan", "Misic", "misic", "dragan" },
                    { 12, "Owner", "danielmisic@gmail.com", "Daniel", "Misic", "misic", "daniel" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "IsSuperGuest", "LastName", "Password", "SuperGuestDiscountCouponCount", "Username" },
                values: new object[,]
                {
                    { 420, "Guest", "radandecko@gmail.com", "Radenko", false, "Radic", "radic", 0, "radenko" },
                    { 421, "Guest", "superdusko@gmail.com", "Dusko", false, "Radic", "radic", 0, "superdusko" }
                });

            migrationBuilder.InsertData(
                table: "Accommodation",
                columns: new[] { "Id", "AccommodationType", "CancelationDeadline", "Images", "LocationId", "MaxGuests", "MinReservationDays", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, 0, 1, "/Resources/Images/house.png", 1, 5, 1, "Aparatmani Lučić", 12 },
                    { 2, 2, 5, "/Resources/Images/house.png,/Resources/Images/house2.jpg", 2, 4, 3, "Konak Živojin Mišić", 12 }
                });

            migrationBuilder.InsertData(
                table: "ComplexTourRequest",
                columns: new[] { "Id", "Name", "TouristId" },
                values: new object[,]
                {
                    { 1, "Upoznaj Beograd i Novi Sad", 8 },
                    { 2, "Obilazak zapada", 8 }
                });

            migrationBuilder.InsertData(
                table: "Forum",
                columns: new[] { "Id", "AuthorId", "Description", "GuestComments", "IsClosed", "IsUseful", "LocationId", "Name", "OwnerComments" },
                values: new object[] { 1, 2, "Priuštiva prenoćišta u Novom Sadu", 0, false, false, 1, "Prenoćišta u Novom Sadu", 0 });

            migrationBuilder.InsertData(
                table: "Tour",
                columns: new[] { "Id", "AvailableSeats", "Description", "Duration", "GuideId", "Images", "Language", "LocationId", "MaxTouristNumber", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Obilazak parkova u Novom Sadu", 2, 9, "https://gradskeinfo.rs/wp-content/uploads/2022/04/dunavski-park-1.jpg", "English", 1, 15, "Parkovi Novog Sada" },
                    { 2, 0, "Obilazak fruskogorskih manastira. Na prostoru 50 kilometara dužine i 10 kilometara širine na Fruškoj Gori je smešteno šesnaest srpskih pravoslavnih manastira.", 2, 9, "https://www.idemonaput.rs/wp-content/uploads/2020/11/cov17-768x403.jpg", "Serbian", 2, 20, "Fruskogorski manastiri" },
                    { 3, 0, "Hodočašće u manastir Ostrog", 2, 10, "https://i0.wp.com/www.durmitor.rs/wp-content/uploads/2014/03/ostrog.jpg?w=800&ssl=1", "Spanish", 3, 25, "Manastir Ostrog" },
                    { 4, 0, "You have the chance to visit the capital of Bosnia and Herzegovina as well as its greatest sights", 2, 9, "https://a.cdn-hotels.com/gdcs/production32/d702/0efac108-32eb-435f-811e-59d516d808ac.jpg?impolicy=fcrop&w=800&h=533&q=medium", "German", 4, 15, "Visit Sarajevo" }
                });

            migrationBuilder.InsertData(
                table: "TourRequest",
                columns: new[] { "Id", "City", "Country", "Description", "EndDate", "Language", "NotifyTourist", "NumberOfParticipants", "StartDate", "TourName", "TourRequestState", "TouristId" },
                values: new object[,]
                {
                    { 1, "Chichen Itza", "Mexico", "Descubre las antiguas ruinas mayas y su fascinante historia.", new DateTime(2022, 3, 27, 0, 0, 0, 0, DateTimeKind.Local), "Spanish", false, 2, new DateTime(2022, 3, 24, 0, 0, 0, 0, DateTimeKind.Local), "Recorrido por las Ruinas Mayas", 1, 8 },
                    { 2, "Rome", "Italy", "Experience the glory of ancient Rome with a knowledgeable guide.", new DateTime(2022, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), "Italian", false, 3, new DateTime(2022, 5, 9, 0, 0, 0, 0, DateTimeKind.Local), "Tour of Ancient Rome", 1, 8 },
                    { 3, "Interlaken", "Switzerland", "Embark on an adventurous hike through the breathtaking Swiss Alps.", new DateTime(2022, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), "English", false, 2, new DateTime(2022, 4, 23, 0, 0, 0, 0, DateTimeKind.Local), "Hiking in the Swiss Alps", 2, 8 },
                    { 4, "Bordeaux", "France", "Indulge in the finest wines of Bordeaux and explore its picturesque vineyards.", new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Local), "French", false, 1, new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), "Wine Tasting in Bordeaux", 2, 8 },
                    { 5, "Bordeaux", "France", "Indulge in the finest wines of Bordeaux and explore its picturesque vineyards.", new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Local), "English", false, 4, new DateTime(2023, 5, 18, 0, 0, 0, 0, DateTimeKind.Local), "Visiting Bordeaux", 1, 8 },
                    { 6, "Iquitos", "Peru", "Sumérgete en la exuberante selva amazónica y descubre su increíble biodiversidad.", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Local), "Spanish", false, 1, new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Local), "Aventura en la Selva Amazónica", 2, 8 },
                    { 7, "Novi Sad", "Serbia", "Obilazak Petrovaradinske tvrdjave i njenih tunela", new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Local), "English", false, 1, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Local), "Obilazak Petrovaradina", 0, 8 },
                    { 8, "Novi Sad", "Serbia", "Crkve Starog grada", new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Local), "Spanish", false, 1, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Local), "Obilazak Crkvi Novog Sada", 0, 8 },
                    { 9, "Brcko DS", "Bosnia and Herzegovina", "Visit the most beautiful beauties of Bosnia with us.", new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), "German", false, 1, new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Local), "Posetimo Bosnu", 2, 8 }
                });

            migrationBuilder.InsertData(
                table: "Voucher",
                columns: new[] { "Id", "ExpirationDate", "IsGlobal", "Name", "TouristId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Local), false, "20% discount", 8 },
                    { 2, new DateTime(2023, 12, 7, 0, 0, 0, 0, DateTimeKind.Local), false, "Summer starter", 8 },
                    { 3, new DateTime(2023, 12, 7, 0, 0, 0, 0, DateTimeKind.Local), false, "Summer starter", 8 },
                    { 4, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local), false, "Winter Wonderland", 8 },
                    { 5, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Local), false, "Family Pack", 8 },
                    { 6, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Local), false, "Adventure Trip", 8 }
                });

            migrationBuilder.InsertData(
                table: "AccommodationReservation",
                columns: new[] { "Id", "AccommodationId", "GuestId", "NumberOfGuests", "ReservationState", "StartDate", "StayLength" },
                values: new object[,]
                {
                    { 1, 1, 2, 1, 1, new DateTime(2024, 3, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 1, 2, 2, 1, new DateTime(2024, 3, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 1, 420, 3, 1, new DateTime(2024, 3, 28, 11, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 4, 1, 420, 3, 3, new DateTime(2024, 3, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 5, 1, 2, 1, 3, new DateTime(2024, 3, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, 1, 2, 4, 3, new DateTime(2024, 3, 3, 11, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 7, 1, 2, 4, 3, new DateTime(2024, 3, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, 2, 421, 1, 3, new DateTime(2024, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 9, 2, 421, 1, 3, new DateTime(2024, 1, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 10, 2, 421, 1, 3, new DateTime(2024, 1, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 11, 2, 421, 1, 3, new DateTime(2024, 1, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 12, 2, 421, 1, 3, new DateTime(2024, 1, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 13, 2, 421, 1, 3, new DateTime(2024, 1, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 14, 2, 421, 1, 3, new DateTime(2024, 1, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 15, 2, 421, 1, 3, new DateTime(2024, 1, 29, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 16, 2, 421, 1, 3, new DateTime(2024, 2, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 17, 2, 421, 1, 3, new DateTime(2024, 2, 6, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 18, 2, 421, 1, 3, new DateTime(2024, 2, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Checkpoint",
                columns: new[] { "Id", "IsActive", "LocationId", "Name", "TourId" },
                values: new object[,]
                {
                    { 1, false, 1, "Limanski park", 1 },
                    { 2, false, 1, "Dunavski park", 1 },
                    { 3, false, 2, "Fruskogorski manastiri", 2 },
                    { 4, false, 3, "Manastir Ostrog", 3 },
                    { 5, false, 2, "Manastir Krušedol", 2 },
                    { 6, false, 1, "Park na Grbavici", 1 },
                    { 7, false, 1, "Železnički park", 1 },
                    { 8, false, 1, "Kamenički park", 1 },
                    { 9, false, 2, "Petkovica", 2 },
                    { 10, false, 2, "Mala Remeta", 2 },
                    { 11, false, 4, "Bascarsija", 4 },
                    { 12, false, 4, "Trebevic", 4 }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "CommentText", "ForumId", "IsOwnerComment", "IsUseful", "NumOfReports", "PublishedDate", "UserId" },
                values: new object[] { 1, "Preporucujem konak u rumenackoj ulici, ne sjecam se kako se zove.", 1, false, false, 0, new DateTime(2024, 3, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "ComplexTourPart",
                columns: new[] { "Id", "ApprovedDate", "City", "ComplexTourRequestId", "Country", "Description", "EndDate", "GuideId", "Language", "NotifyTourist", "NumberOfParticipants", "StartDate", "TourName", "TourRequestState" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 23, 11, 0, 0, 0, DateTimeKind.Unspecified), "Belgrade", 1, "Serbia", "A simple tour part in Belgrade.", new DateTime(2024, 6, 23, 11, 0, 0, 0, DateTimeKind.Unspecified), 0, "English", false, 10, new DateTime(2024, 6, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), "Zanimljivosti Beograda", 0 },
                    { 2, new DateTime(2024, 6, 26, 18, 0, 0, 0, DateTimeKind.Unspecified), "Novi Sad", 1, "Serbia", "A simple tour part in Novi Sad.", new DateTime(2024, 6, 26, 18, 0, 0, 0, DateTimeKind.Unspecified), 0, "English", false, 15, new DateTime(2024, 6, 24, 9, 0, 0, 0, DateTimeKind.Unspecified), "Zanimljivosti Novog Sada", 0 },
                    { 3, new DateTime(2024, 6, 24, 18, 0, 0, 0, DateTimeKind.Unspecified), "Mali Zvornik", 2, "Serbia", "Ae da ronimo po Drini gnjurci", new DateTime(2024, 6, 24, 18, 0, 0, 0, DateTimeKind.Unspecified), 0, "English", false, 10, new DateTime(2024, 6, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), "Obilazak Malog Zvornika", 0 }
                });

            migrationBuilder.InsertData(
                table: "TourParticipant",
                columns: new[] { "Id", "Age", "CheckpointJoinedId", "ComplexTourPartId", "Email", "FirstName", "IsPresent", "LastName", "TourRequestId", "TourReservationId" },
                values: new object[] { 8, 33, null, null, "milanadjokodsavic@gmail.com", "Milanda", false, "Djokovidsc", 1, null });

            migrationBuilder.InsertData(
                table: "TourReservation",
                columns: new[] { "Id", "DateAndTime", "GuideId", "TourId", "TourState", "TouristsNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 28, 16, 30, 0, 0, DateTimeKind.Unspecified), 9, 1, 2, 4 },
                    { 2, new DateTime(2024, 5, 27, 12, 30, 0, 0, DateTimeKind.Unspecified), 10, 2, 2, 2 },
                    { 3, new DateTime(2024, 5, 23, 17, 0, 0, 0, DateTimeKind.Unspecified), 9, 1, 0, 1 },
                    { 4, new DateTime(2024, 5, 7, 11, 30, 0, 0, DateTimeKind.Unspecified), 9, 2, 2, 1 },
                    { 5, new DateTime(2024, 5, 28, 13, 30, 0, 0, DateTimeKind.Unspecified), 10, 3, 2, 1 },
                    { 6, new DateTime(2024, 5, 18, 14, 30, 0, 0, DateTimeKind.Unspecified), 9, 2, 0, 0 },
                    { 7, new DateTime(2024, 3, 28, 17, 30, 0, 0, DateTimeKind.Unspecified), 9, 1, 2, 0 },
                    { 8, new DateTime(2024, 5, 16, 17, 30, 0, 0, DateTimeKind.Unspecified), 9, 4, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "AccommodationReservationModificationRequest",
                columns: new[] { "Id", "AccommodationReservationId", "NumberOfGuests", "OwnerComment", "RequestState", "StartDate", "StayLength" },
                values: new object[,]
                {
                    { 4, 2, 2, "", 1, new DateTime(2024, 4, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, 3, 3, "", 1, new DateTime(2024, 4, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 6, 1, 2, "Sorry but no!", 3, new DateTime(2024, 4, 3, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "AccommodationReservationReview",
                columns: new[] { "Id", "AccommodationId", "AccommodationReservationId", "Cleanliness", "Comment", "Correctness", "Direction", "GuestId", "Images", "IsInNeedOfRenovation", "OwnerId", "RenovationNeed", "RuleCompliance" },
                values: new object[,]
                {
                    { 2, 1, 4, 4, "Jako dobar gost svaka preporuka", 1, true, 420, "", false, 12, 0, 5 },
                    { 3, 1, 4, 5, "Dobar vlasnik", 5, false, 420, "", false, 12, 0, 0 },
                    { 4, 1, 5, 2, "Los vlasnik", 2, false, 2, "/data/img/slika.jpg,/data/img/house.jpg", true, 12, 2, 0 },
                    { 5, 1, 6, 3, "Ok vlasnik", 4, false, 2, null, false, 12, 0, 0 },
                    { 6, 1, 6, 5, "Prosjecan gost", 5, true, 2, "", false, 12, 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "TourAttendance",
                columns: new[] { "Id", "Age", "CheckpointJoinedId", "IsConfirmed", "IsPresent", "TourReservationId", "TouristId" },
                values: new object[,]
                {
                    { 1, 22, null, false, false, 1, 3 },
                    { 2, 21, null, false, false, 3, 8 },
                    { 3, 21, 1, true, true, 4, 8 },
                    { 4, 21, null, false, true, 5, 8 },
                    { 5, 27, null, false, false, 6, 3 },
                    { 6, 27, 2, false, false, 7, 3 }
                });

            migrationBuilder.InsertData(
                table: "TourParticipant",
                columns: new[] { "Id", "Age", "CheckpointJoinedId", "ComplexTourPartId", "Email", "FirstName", "IsPresent", "LastName", "TourRequestId", "TourReservationId" },
                values: new object[,]
                {
                    { 1, 22, null, null, "volemzenerumunke@gmail.com", "Nikola", false, "Radojcic", null, 1 },
                    { 2, 13, null, null, "bosanac02@gmail.com", "Dragan", false, "Misic", null, 1 },
                    { 3, 55, null, null, "anapetrovic@gmail.com", "Ana", false, "Petrovic", null, 3 },
                    { 4, 33, null, null, "markojovanovic@gmail.com", "Marko", false, "Jovanovic", null, 1 },
                    { 5, 14, null, null, "milosnikolic@gmail.com", "Milos", false, "Nikolic", null, 2 },
                    { 6, 25, null, null, "ivanadjokovic@gmail.com", "Ivana", false, "Djokovic", null, 2 },
                    { 7, 56, null, null, "milanadjokovic@gmail.com", "Milana", false, "Djokovic", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "VoucherUse",
                columns: new[] { "TourReservationId", "VoucherId" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "TourReview",
                columns: new[] { "Id", "Comment", "GuideId", "Interestingness", "IsValid", "Knowledge", "Language", "ReviewImages", "TourAttendanceId", "TourReservationId" },
                values: new object[] { 1, "Great experience!", 9, 4, true, 4, 5, "", 6, 6 });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_LocationId",
                table: "Accommodation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_OwnerId",
                table: "Accommodation",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationReservation_AccommodationId",
                table: "AccommodationReservation",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationReservation_GuestId",
                table: "AccommodationReservation",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationReservationModificationRequest_AccommodationReservationId",
                table: "AccommodationReservationModificationRequest",
                column: "AccommodationReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationReservationReview_AccommodationId",
                table: "AccommodationReservationReview",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationReservationReview_AccommodationReservationId",
                table: "AccommodationReservationReview",
                column: "AccommodationReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationReservationReview_GuestId",
                table: "AccommodationReservationReview",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationReservationReview_OwnerId",
                table: "AccommodationReservationReview",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkpoint_LocationId",
                table: "Checkpoint",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkpoint_TourId",
                table: "Checkpoint",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ForumId",
                table: "Comment",
                column: "ForumId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplexTourPart_ComplexTourRequestId",
                table: "ComplexTourPart",
                column: "ComplexTourRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplexTourRequest_TouristId",
                table: "ComplexTourRequest",
                column: "TouristId");

            migrationBuilder.CreateIndex(
                name: "IX_Forum_AuthorId",
                table: "Forum",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Forum_LocationId",
                table: "Forum",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_GuideId",
                table: "Tour",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_LocationId",
                table: "Tour",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TourAttendance_CheckpointJoinedId",
                table: "TourAttendance",
                column: "CheckpointJoinedId");

            migrationBuilder.CreateIndex(
                name: "IX_TourAttendance_TouristId",
                table: "TourAttendance",
                column: "TouristId");

            migrationBuilder.CreateIndex(
                name: "IX_TourAttendance_TourReservationId",
                table: "TourAttendance",
                column: "TourReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_TourParticipant_ComplexTourPartId",
                table: "TourParticipant",
                column: "ComplexTourPartId");

            migrationBuilder.CreateIndex(
                name: "IX_TourParticipant_TourRequestId",
                table: "TourParticipant",
                column: "TourRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TourParticipant_TourReservationId",
                table: "TourParticipant",
                column: "TourReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_TourRequest_TouristId",
                table: "TourRequest",
                column: "TouristId");

            migrationBuilder.CreateIndex(
                name: "IX_TourReservation_GuideId",
                table: "TourReservation",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_TourReservation_TourId",
                table: "TourReservation",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourReview_TourAttendanceId",
                table: "TourReview",
                column: "TourAttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TourReview_TourReservationId",
                table: "TourReview",
                column: "TourReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_TouristId",
                table: "Voucher",
                column: "TouristId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUse_TourReservationId",
                table: "VoucherUse",
                column: "TourReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccommodationReservationModificationRequest");

            migrationBuilder.DropTable(
                name: "AccommodationReservationReview");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CurrentDate");

            migrationBuilder.DropTable(
                name: "ForumAlert");

            migrationBuilder.DropTable(
                name: "Renovation");

            migrationBuilder.DropTable(
                name: "TourParticipant");

            migrationBuilder.DropTable(
                name: "TourReview");

            migrationBuilder.DropTable(
                name: "VoucherUse");

            migrationBuilder.DropTable(
                name: "AccommodationReservation");

            migrationBuilder.DropTable(
                name: "Forum");

            migrationBuilder.DropTable(
                name: "ComplexTourPart");

            migrationBuilder.DropTable(
                name: "TourRequest");

            migrationBuilder.DropTable(
                name: "TourAttendance");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "Accommodation");

            migrationBuilder.DropTable(
                name: "ComplexTourRequest");

            migrationBuilder.DropTable(
                name: "Checkpoint");

            migrationBuilder.DropTable(
                name: "TourReservation");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
