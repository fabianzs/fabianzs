﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.Controllers
{
    [Route("todo")]
    public class TodoController : Controller
    {
        private readonly ApplicationContext applicationContext;

        public TodoController(ApplicationContext app)
        {
            applicationContext = app;
        }

        [HttpGet]
        [Route("simple")]
        [Route("simple/list")]
        public IActionResult SimpleList()
        {
            List<Todo> todoList = new List<Todo>()
            {
                new Todo("Start the day"),
                new Todo("Finish H2 workshop1"),
                new Todo("Finish JPA workshop2"),
                new Todo("Create a CRUD project")
            };
            return View(todoList);
            //return Content("This is my first todo");
        }

        [HttpGet]
        [Route("")]
        [Route("list")]
        public IActionResult List([FromQuery] string isActive)
        {
            // Create a SQL query in the background
            var todos = new List<Todo>();
            if (isActive == null)
            {
                todos = applicationContext.Todos.ToList();
            }

            if (isActive != null)
            {
                if (isActive.Equals("true"))
                {
                    todos = applicationContext.Todos.Where(t => !t.IsDone).ToList();
                }
                if (isActive.Equals("false"))
                {
                    todos = applicationContext.Todos.Where(t => t.IsDone).ToList();
                }
            }
            
            return View(todos);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Save(Todo todo)
        {
            applicationContext.Add(todo);
            applicationContext.SaveChanges();
            return Redirect("list");
        }

    }
}
