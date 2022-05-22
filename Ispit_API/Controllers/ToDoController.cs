﻿using Ispit_API.Data.Migration;
using Ispit_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ispit_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoList>> GetAllLists()
        {
            try
            {
                var lists = _context.ToDoLists.ToList();
                return Ok(lists);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nije moguće izvršiti upit!");
            }
           
        }
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<ToDoList> GetList(int id)
        {
            try
            {
                var result = _context.ToDoLists.FirstOrDefault(x => x.Id == id);
                if(result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, " Rezultat nije pronađen!");
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                  return StatusCode(StatusCodes.Status500InternalServerError, "Nije moguce prikazati rezultat!");
            }
        }


        [HttpPost]
        public ActionResult InsertList(ToDoList toDoList)
        {
            try
            {
                if (toDoList.Id == 0)
                {
                    toDoList.Id = _context.ToDoLists.Max(x => x.Id) + 1;
                }

                var result = _context.ToDoLists.Add(toDoList);
                _context.SaveChanges();

                return Ok("Zapis je kreiran!");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        
        }
        [HttpPut("{id}")]
        public ActionResult PutList(int id, ToDoList update_list)
        {
            try
            {
                var result = _context.ToDoLists.FirstOrDefault(s => s.Id == id);    
                result.Title = update_list.Title;
                result.Description = update_list.Description;
                result.IsCompleted = update_list.IsCompleted;

                //var result2 = _context.ToDoLists.Update(update_list);

                _context.SaveChanges();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteMovie(int id)
        {
            try
            {
                var result = _context.ToDoLists.FirstOrDefault(r => r.Id == id);
                _context.ToDoLists.Remove(result);

                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
