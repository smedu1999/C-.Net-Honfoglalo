using Microsoft.EntityFrameworkCore;
using System;
using Honfoglalo.DAL.Model;
using System.Collections.Generic;

namespace Honfoglalo.DAL
{
    public class HonfoglaloContext    : DbContext                                             //Az adatbazisunk eleresehez hasznalt osztaly<
    {
        public HonfoglaloContext(DbContextOptions<HonfoglaloContext> options): base(options)
        {

        }
        public HonfoglaloContext() { }

        public virtual DbSet<Question> Questions { get; set; }                                                  //..
        public virtual DbSet<Field> Fields { get; set; }                                                        //..
        public virtual DbSet<Users> UsersTable { get; set; }                                                          //Tablak letrehozasa
        public virtual DbSet<WinningHistory> HistoryTable { get; set; }                                         //..
        public virtual DbSet<FieldOwner> OwningTable { get; set; }                                              //..
        public virtual DbSet<QuestionFieldLevel> DifficultyTable { get; set; }                                  //..
        public virtual DbSet<WrongAnswers> WrongAnswersTable { get; set; }                                  //..
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseHiLo("DBSequenceHiLo");                                                     //HILO
            modelBuilder.HasSequence<int>("QuestionHiLo").StartsAt(100).IncrementsBy(2);
            modelBuilder.Entity<Question>().Property(p => p.Id).UseHiLo("QuestionHiLo");

            //var data = GenerateQuestionTables();

            

            for (int i = 0; i < 50; i++)
            {
                int r = new Random().Next(1, 6);
                modelBuilder.Entity<Question>(q =>
                {
                    q.ToTable("Questions");
                    q.Property(w => w.Question1).HasColumnName("Question1");
                    q.HasData(new Question 
                    { 
                        Id=i+1,
                        Question1="Question "+ (i + 1),
                        CorrectAnswer="Correct Answer"+ (i + 1),
                        Difficulty=r,
                        IsDeleted=false
                    });
                    q.HasOne(w => w.WrongAns).WithOne().HasForeignKey<WrongAnswers>(w => w.Id);

                });

                modelBuilder.Entity<WrongAnswers>(wa =>
                {
                    wa.ToTable("Questions");
                    wa.Property(w => w.Question1).HasColumnName("Question1");
                    wa.HasData(new
                    {
                        Id = i+1,
                        Question1 = "Question "+(i + 1),
                        Answer1 = "Answer1_" + (i + 1),
                        Answer2 = "Answer2_" + (i + 1),
                        Answer3 = "Answer3_" + (i + 1)
                    });
                }   
                );
            }

            modelBuilder.Entity<Users>().HasAlternateKey(p => p.Name).HasName("AK_User");               //Alternate key hozzaadasa
            
            modelBuilder.Entity<Question>().HasIndex(p => p.Difficulty).HasName("Index_Difficulty");    //Index letrehozasa
            modelBuilder.Entity<Question>().HasQueryFilter(p => !p.IsDeleted);


            modelBuilder.Entity<Field>(p =>
            {
                for (int i = 0; i < 20; i++)
                {
                    p.HasData(new Field { Id = i+1, Name = "f1"+(i+1), Rank = new Random().Next(1, 6) });
                    p.OwnsOne(l => l.FieldLocation).HasData(new { FieldId = i+1, Country = i%3==0? Countries.Hungary : Countries.Germany, Region = "R1"+i });
                }
            });

            modelBuilder.Entity<Field>().HasAlternateKey(p => p.Name).HasName("AK_Name");               //Alternate key hozzaadasa
            modelBuilder.Entity<Field>().HasIndex(p => p.Rank).HasName("Index_Rank");                   //Index letrehozasa
            
            modelBuilder.Entity<Field>(c =>
            {
                c.OwnsOne(e => e.FieldLocation, b =>
                {
                    b.Property(e => e.Country).HasConversion
                    (
                        o => o.ToString(), o => (Countries)Enum.Parse(typeof(Countries), o)
                    );
                });
            });
        }

        protected (Question[], WrongAnswers[]) GenerateQuestionTables()
        {
            Question[] q = new Question[50];
            WrongAnswers[] wa = new WrongAnswers[50];
            for (int i = 0; i < 50; i++)
            {
                int r = new Random().Next(1, 6);

                WrongAnswers tempWa = new WrongAnswers
                {
                    Id = i + 1,
                    Question1 = "Question" + (i + 1),
                    Answer1 = "Answer" + (i + 1) + "_1",
                    Answer2 = "Answer" + (i + 1) + "_2",
                    Answer3 = "Answer" + (i + 1) + "_3"
                };
                wa[i] = tempWa;

                q[i] = new Question
                {
                    Id = i + 1,
                    Question1 = "Question" + (i + 1),
                    WrongAns = tempWa,
                    CorrectAnswer = "Correct Answer" + (i + 1),
                    Difficulty = r,
                    IsDeleted = false
                };

            }
            return (q, wa);     
        }
        

        
    }
}
