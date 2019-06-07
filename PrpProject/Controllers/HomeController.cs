using PrpProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrpProject.Controllers
{
    public class HomeController : Controller
    {
        ProjectContext context = new ProjectContext();

        static User user;
        static MoneyManagerItem item;

        public ActionResult Index()
        {
            ForViewBug();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Login, string Password)
        {
            // получаем из бд все объекты User
            IEnumerable<User> users = context.Users;
            //List<User> us = context.Users.ToList<User>();
            // передаем все полученный объекты в динамическое свойство Users в ViewBag
            ViewBag.Users = users;
            foreach (var items in ViewBag.Users)
            {
                if (items.Login == Login && items.Password == Password)
                {
                    user = items;

                    ForViewBug();

                    return View("Index");    
                }
            }
            // возвращаем представление
            return View("Login");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(string name, string surname, string Login, string Password, string PasswordS)
        {
            IEnumerable<User> users = context.Users;
            ViewBag.Users = users;

            foreach (var items in ViewBag.Users)
            {
                if (items.Login == Login)
                    return View("Registration");
            }
            if (Password == PasswordS)
            {
                context.Users.Add(new User { Login = Login, Password = Password, Name = name, Surname = surname, Money = 0 });
                context.SaveChanges();

                ViewBag.RegistrationMessage = "Вы успешно зарегистрировались";
                return View();
            }
            else
            {
                ViewBag.RegistrationMessage = "Пароли не совпадают";
                return View("Registration");
            }
        }

        public ActionResult AddIncome()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddIncome(string name)
        {
            MoneyManagerItem item = new MoneyManagerItem();
            item.Name = name;
            item.State = true;
            item.UserId = user.Id;
            context.MoneyManagerItems.Add(item);
            context.SaveChanges();

            ForViewBug();

            return View("Index");
        }

        public ActionResult AddRashod()
        {
            ForViewBug();
            return View();
        }
        [HttpPost]
        public ActionResult AddRashod(string name)
        {
            MoneyManagerItem item = new MoneyManagerItem();
            item.Name = name;
            item.State = false;
            item.UserId = user.Id;
            context.MoneyManagerItems.Add(item);
            context.SaveChanges();

            ForViewBug();

            return View("Index");
        }

        public ActionResult Operation(int id)
        {
            item = (MoneyManagerItem)context.MoneyManagerItems.FirstOrDefault(item => item.Id == id);
            ViewBag.Income = context.MoneyManagerItems.Where(item => item.Id == id);
            if (item.State)
            {
                ViewBag.Operation = "доход";
            }
            else
            {
                ViewBag.Operation = "расход";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Operation(string income)
        {
            if (item.State)
            {
                user.Money += Convert.ToInt32(income);
            }
            else
            {
                user.Money -= Convert.ToInt32(income);
            }
            foreach (var b in context.Users)
            {
                if (b.Id == user.Id)
                    b.Money = user.Money;
            }

            Hystory hystory = new Hystory();
            hystory.MoneyManagerItemId = item.Id;
            hystory.Name = item.Name;
            hystory.Operation = item.State ? "+" + income : "-" + income;
            hystory.UserId = user.Id;
            hystory.Date = DateTime.Now;

            context.Hystories.Add(hystory);
            context.SaveChanges();

            ForViewBug();

            return View("Index");
        }

        public void ForViewBug()
        {
            ViewBag.Hystory = context.Hystories.Where(hys => hys.UserId == user.Id);
            ViewBag.Items = context.MoneyManagerItems.Where(it => it.UserId == user.Id);
            ViewBag.User = user;
        }
    }
}