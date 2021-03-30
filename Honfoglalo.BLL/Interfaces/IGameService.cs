using Honfoglalo.BLL.Service;
using Honfoglalo.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Honfoglalo.BLL.Interfaces
{
    public interface IGameService
    {
        Field GetField(int id);
        Question GetQuestion(int id);
        Question AddNewQuestion(Question q);
        Users GetUsers(Users user);
        Question AttackField(Users u, Field f);
        String GetAnswers(AnswersInfo ai);
        String InitGame();
        String EndGame();
        Question DeleteQuestion(int id);
    }
}
