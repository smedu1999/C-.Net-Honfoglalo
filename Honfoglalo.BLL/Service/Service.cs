using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Honfoglalo.BLL.Interfaces;
using Honfoglalo.DAL;
using Honfoglalo.DAL.Model;

namespace Honfoglalo.BLL.Service
{
    public class Service :IGameService
    {
        private readonly HonfoglaloContext context;                     
        public Service(HonfoglaloContext con)
        {
            context = con;
        }
        public String InitGame()
        {                                                                               // Jatek elejen torlunk minden olyan tablat ami a jatek
            foreach (var r in context.UsersTable)                                            // alatt toltodik fel (users, history, diff, owning)
            {
                context.UsersTable.Remove(r);                                                //userek torlese
            }
            foreach (var r in context.HistoryTable)
            {
                context.HistoryTable.Remove(r);                                         //History torlese
            }
            foreach (var r in context.OwningTable)
            {
                context.OwningTable.Remove(r);                                          //Owning torlese
            }
            foreach (var q in context.DifficultyTable)
            {
                context.DifficultyTable.Remove(q);                                      //Diff torlese
            }
            context.SaveChanges();                                                      //Db frissitese
            
                var qlist=context.Questions.ToList();
                foreach (var q in qlist)
                {
                    var diff = q.Difficulty;
                    foreach (var f in context.Fields)                                           //Vegigmegyunk a mezokon
                    {

                        AddToDiffTable(q.Id, diff, f.Id, f.Rank);
                    }
                }
            

            context.SaveChanges();                                                      //Db frissitese
            return "You can start the game";
        }
        public void AddToDiffTable(int qId, int diff, int fId, int fRank)
        {   
            
            if (fRank == diff)                                                         //Diff feltoltese
            {
               // using (context)
                {
                    context.DifficultyTable.Add
                    (
                        new QuestionFieldLevel { FieldId = fId, QuestionId = qId }        //Uj elemet irunk a diff tablaba az adott ertekekkel
                    );
                }
            }
           
        }

        public Field GetField(int id)
        {
            return context.Fields.FirstOrDefault(r=>r.Id==id);
        }
        public Question GetQuestion(int id)
        {
            return context.Questions.FirstOrDefault(r=>r.Id==id);
        }
        public Question AttackField(Users attacker, Field field) 
        {
            var row = context.OwningTable.FirstOrDefault(r => r.FieldName == field.Name);   //Kikeressuk, hogy kie az adott field

            int defenderId = row.UserId;
            if (defenderId == attacker.Id)                                                  //Magunkat nem tamadhatjuk
            {
                throw (new Exception("Nem tamadhatod magad"));
            }

            return FindQuestionForField(field.Id);

        }

        public Question FindQuestionForField(int fId)
        {
            var row2 = context.DifficultyTable.Where(r => r.FieldId == fId);           //Kikeressuk, a mezohoz tartozo kerdeseket

            int random = new Random().Next(1, row2.ToList().Count() + 1);                  //..
            int qId = row2.ToList().ElementAt(random - 1).QuestionId;    
                                                                                        //Valasztunk egyet random kozuluk
            Question question = context.Questions.FirstOrDefault(r => r.Id == qId);
            return question;
        }
        public String GetAnswers(AnswersInfo ai)
        {
            String result;
            var question = context.Questions.FirstOrDefault(r => r.Id == ai.qId);       //Kikeressuk az adott kerdest
            if (String.Equals(ai.AssaulterAnswer, ai.DefenderAnswer) ||                 //Ha ugyan azok a valaszok akkor a vedekezo nyerte a kort
                String.Equals(ai.DefenderAnswer, question.CorrectAnswer))               //Ha a vedekezo jo valaszt adott, akkor nyerte a kort
            {
                result = UpdateDefenderWin(ai.defId, question.Id);
            }   
            else                                                                             //Ha a tamado jo valaszt adott, de a vedekezo nem
            {
                result = UpdateAttackerWin(ai.assId, ai.defId, ai.fId, question.Id);
            }
            context.SaveChanges();
            return result;

        }

        public String UpdateDefenderWin(int defId, int qId)
        {

            //using (context)
            {
                UpdateHistoryTable(qId, defId);
            }
            
            return "Gyozott:" + context.UsersTable.FirstOrDefault(r => r.Id == defId).Name;
        }
        public String UpdateAttackerWin(int assId, int defId, int fieldId, int qId)
        {
            UpdateHistoryTable(qId, assId);
            context.UsersTable.FirstOrDefault(p=>p.Id==assId).NumOfFields++;                             //..
            context.UsersTable.FirstOrDefault(p => p.Id==defId).NumOfFields--;                             //Frissitjuk az adatokat

            var row = context.Fields.FirstOrDefault(r => r.Id == fieldId);
            context.OwningTable.FirstOrDefault(r => r.FieldName == row.Name).UserId = assId;
            return "Gyozott:" + context.UsersTable.FirstOrDefault(p => p.Id==assId).Name;
        }
        public void UpdateHistoryTable(int qId, int uId)
        {
            context.HistoryTable.Add(new WinningHistory
            {
                QuestionId = qId,
                UserId = uId                         //Beirjuk a nyertest
            });
        }
        public Users GetUsers(Users user)
        {
            if (context.UsersTable.FirstOrDefault(p => p.Name == user.Name) != null)     //Alternatív kulcs hasznalata 
                throw (new Exception("A felhasználónév mar letezik"));
            context.UsersTable.Add(user);
            context.SaveChanges();
            if (context.UsersTable.Count() == 2)
            {
                dealFields(context.UsersTable.ToList());
            }
            return user;
        }
        private void dealFields(List<Users> list)                                         //Mezok kiosztasa
        {
            Field f =context.Fields.ToList().ElementAt(0);
            var r = context.Fields.Where(p => p.FieldLocation.Country == f.FieldLocation.Country);
            var r2 = context.Fields.Where(p => p.FieldLocation.Country != f.FieldLocation.Country);

            foreach (var field in r)
            {
                context.OwningTable.Add(new FieldOwner
                {
                    UserId = list[0].Id,
                    FieldName = field.Name
                });
                context.UsersTable.FirstOrDefault(r => r.Id == list[0].Id).NumOfFields++;
            }
            foreach (var field in r2)
            {
                context.OwningTable.Add(new FieldOwner
                {
                    UserId = list[1].Id,
                    FieldName = field.Name
                });
                context.UsersTable.FirstOrDefault(r => r.Id == list[1].Id).NumOfFields++;
            }

            context.SaveChanges();
        }

        public Question AddNewQuestion(Question q)
        {                                                                              
            if (context.Questions.FirstOrDefault(p => p.Id == q.Id) != null)
                throw (new Exception("A kerdes mar letezik"));
            context.Questions.Add(q);
            context.SaveChanges();
            var r = context.Questions.FirstOrDefault(r => r.Id == q.Id);
            foreach (var f in context.Fields.ToList())
            {
                AddToDiffTable(r.Id,r.Difficulty,f.Id,f.Rank);
            }
            context.SaveChanges();
            return q;
        }
        public Question DeleteQuestion(int id)
        {                                                                               
            var q = context.Questions.FirstOrDefault(p => p.Id == id);
            if (q == null)
                throw (new Exception("A kerdes nem letezik"));
            q.IsDeleted = true;
            context.SaveChanges();
            return q;
        }
        public String EndGame()
        {
            int max=context.UsersTable.Max(r => r.NumOfFields);
            Users u = context.UsersTable.FirstOrDefault(r => r.NumOfFields == max);
            return u.Name + " Won";
            
        }
    }
}
