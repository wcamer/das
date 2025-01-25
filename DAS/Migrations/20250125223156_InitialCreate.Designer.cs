﻿// <auto-generated />
using System;
using DAS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAS.Migrations
{
    [DbContext(typeof(DASContext))]
    [Migration("20250125223156_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("DAS.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppointSetterId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AppointmentId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("DAS.Models.DoctorNote", b =>
                {
                    b.Property<int>("DoctorNoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("DoctorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.HasKey("DoctorNoteId");

                    b.ToTable("DoctorNote");
                });

            modelBuilder.Entity("DAS.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<string>("InsuranceCompany")
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<string>("InsurancePolicyNumber")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<int?>("PatientGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProfileId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PatientId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("DAS.Models.Profile", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("ProfileId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("DAS.Models.ServiceProviders", b =>
                {
                    b.Property<int>("ServiceProvidersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("ServiceProvidersId");

                    b.ToTable("ServiceProviders");
                });
#pragma warning restore 612, 618
        }
    }
}
