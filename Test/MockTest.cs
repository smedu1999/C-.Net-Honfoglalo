using NUnit.Framework;
using Honfoglalo.BLL.Service;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Honfoglalo.DAL;
using Moq;
using Microsoft.EntityFrameworkCore;
using Honfoglalo.DAL.Model;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Test
{
    public class Tests
    {

        Service s;
        Field f;
        Users att;
        Users def;
        Question question;
        Mock<HonfoglaloContext> mockContext;
        List<WinningHistory> winHistory;
        List<QuestionFieldLevel> diffList;

        [SetUp]
        public void Setup()
        {
            //System.Diagnostics.Debugger.Launch();

            question = new Question()
            {
                WrongAns = new WrongAnswers { Id = 1, Answer1 = "testA1", Answer2 = "testA2", Answer3 = "testA3", Question1 = "testQ1" },
                CorrectAnswer = "testCa",
                Difficulty = 1,//new Rank { Difficulty=1,IsChildrenGame=false},
                Id = 1,
                IsDeleted = false,
                Question1 = "testQ1"
            };
            var questions = new List<Question> { question }.AsQueryable();
            var mockQuestionSet = new Mock<DbSet<Question>>();
            mockQuestionSet.As<IQueryable<Question>>().Setup(m=> m.Provider).Returns(questions.Provider);
            mockQuestionSet.As<IQueryable<Question>>().Setup(m => m.Expression).Returns(questions.Expression);
            mockQuestionSet.As<IQueryable<Question>>().Setup(m => m.ElementType).Returns(questions.ElementType);
            mockQuestionSet.As<IQueryable<Question>>().Setup(m => m.GetEnumerator()).Returns(() => questions.GetEnumerator());

            f = new Field() { Id = 1, Name = "testF1", Rank = 1, FieldLocation=new Location{Country=Countries.Italy,Region="testRegion"} };
            var fields = new List<Field> { f }.AsQueryable();
            var mockFieldSet = new Mock<DbSet<Field>>();
            mockFieldSet.As<IQueryable<Field>>().Setup(m => m.Provider).Returns(fields.Provider);
            mockFieldSet.As<IQueryable<Field>>().Setup(m => m.Expression).Returns(fields.Expression);
            mockFieldSet.As<IQueryable<Field>>().Setup(m => m.ElementType).Returns(fields.ElementType);
            mockFieldSet.As<IQueryable<Field>>().Setup(m => m.GetEnumerator()).Returns(() => fields.GetEnumerator());

            att = new Users() { Id = 1, Name = "testAttacker", NumOfFields = 2 };
            def = new Users() { Id = 2, Name = "testDefender", NumOfFields = 2 };
            var users = new List<Users> { att, def }.AsQueryable();
            var mockUserSet = new Mock<DbSet<Users>>();
            mockUserSet.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(users.Provider);
            mockUserSet.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(users.Expression);
            mockUserSet.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockUserSet.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(() => users.GetEnumerator());

            winHistory = new List<WinningHistory>();
            var history = winHistory.AsQueryable();
            var mockHistorySet = new Mock<DbSet<WinningHistory>>();
            mockHistorySet.As<IQueryable<WinningHistory>>().Setup(m => m.Provider).Returns(history.Provider);
            mockHistorySet.As<IQueryable<WinningHistory>>().Setup(m => m.Expression).Returns(history.Expression);
            mockHistorySet.As<IQueryable<WinningHistory>>().Setup(m => m.ElementType).Returns(history.ElementType);
            mockHistorySet.Setup(m => m.Add(It.IsAny<WinningHistory>())).Callback<WinningHistory>((s) => winHistory.Add(s));
            mockHistorySet.As<IQueryable<WinningHistory>>().Setup(m => m.GetEnumerator()).Returns(() => history.GetEnumerator());

            FieldOwner fo = new FieldOwner() { FieldName = "testF1", Id = 1, UserId = 1 };
            var owners = new List<FieldOwner> { fo }.AsQueryable();
            var mockFOSet = new Mock<DbSet<FieldOwner>>();
            mockFOSet.As<IQueryable<FieldOwner>>().Setup(m => m.Provider).Returns(owners.Provider);
            mockFOSet.As<IQueryable<FieldOwner>>().Setup(m => m.Expression).Returns(owners.Expression);
            mockFOSet.As<IQueryable<FieldOwner>>().Setup(m => m.ElementType).Returns(owners.ElementType);
            mockFOSet.As<IQueryable<FieldOwner>>().Setup(m => m.GetEnumerator()).Returns(() => owners.GetEnumerator());

            
            diffList = new List<QuestionFieldLevel>();
            var diff = diffList.AsQueryable();
            var mockQFLSet = new Mock<DbSet<QuestionFieldLevel>>();
            mockQFLSet.As<IQueryable<QuestionFieldLevel>>().Setup(m => m.Provider).Returns(diff.Provider);
            mockQFLSet.As<IQueryable<QuestionFieldLevel>>().Setup(m => m.Expression).Returns(diff.Expression);
            mockQFLSet.As<IQueryable<QuestionFieldLevel>>().Setup(m => m.ElementType).Returns(diff.ElementType);
            //mockQFLSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockQFLSet.Object);
            mockQFLSet.Setup(m => m.Add(It.IsAny<QuestionFieldLevel>())).Callback<QuestionFieldLevel>((s) => diffList.Add(s));
            mockQFLSet.As<IQueryable<QuestionFieldLevel>>().Setup(m => m.GetEnumerator()).Returns(() => diff.GetEnumerator());

            mockContext = new Mock<HonfoglaloContext>();
            mockContext.Setup(m => m.UsersTable).Returns(mockUserSet.Object);
            mockContext.Setup(m => m.Questions).Returns(mockQuestionSet.Object);
            mockContext.Setup(m => m.Fields).Returns(mockFieldSet.Object);
            mockContext.Setup(m => m.HistoryTable).Returns(mockHistorySet.Object);
            mockContext.Setup(m => m.OwningTable).Returns(mockFOSet.Object);
            mockContext.Setup(m => m.DifficultyTable).Returns(mockQFLSet.Object);
            s = new Service(mockContext.Object);


        }

        [Test]
        public void GetAnswersSameAnswerTest()
        {
            AnswersInfo ai = new AnswersInfo() { assId = 1, defId = 2, qId = 1, fId = 1, AssaulterAnswer = "testA1", DefenderAnswer = "testA1" };
            string answer = s.GetAnswers(ai);
            Assert.AreEqual("Gyozott:testDefender", answer);
        }

        [Test]
        public void AttackSelfFieldTest()
        {
            Assert.Throws<Exception>(() => s.AttackField(att, f));
        }

        [Test]
        public void DeleteQuestionExistsTest()
        {
            Question q = s.DeleteQuestion(1);
            Assert.IsTrue(q.IsDeleted);
        }
        [Test]
        public void UpdateHistoryTest()
        {
            s.UpdateHistoryTable(1, 1);
            WinningHistory wh = new WinningHistory() { Id = 0, QuestionId = 1, UserId = 1 };
            Assert.AreEqual(1, winHistory.Count());
        }
        [Test]
        public void UpdateAttackerWinTest()
        {
            s.UpdateAttackerWin(att.Id, def.Id, f.Id, question.Id);
            Assert.AreEqual(3,att.NumOfFields);
        }
        [Test]
        public void UpdateDefenderWinTest()
        {
            String answer=s.UpdateDefenderWin(def.Id,question.Id);
            Assert.AreEqual("Gyozott:testDefender", answer);
        }
        [Test]
        public void AddToDiffTableTest()
        {
            s.AddToDiffTable(question.Id,1,f.Id, f.Rank);

            Assert.AreEqual(1, diffList.Count());
        }

        [Test]
        public void GetFieldsTest()
        {
            Field fi = s.GetField(f.Id);
            Assert.AreEqual(fi,f);
        }
        [Test]
        public void GetQuestionsTest()
        {
            Question qu = s.GetQuestion(question.Id);
            Assert.AreEqual(qu, question);
        }
        [Test]
        public void RegisterExsistingUserTest()
        {
            Assert.Throws<Exception>(() => s.GetUsers(att));
        }
    }
}
