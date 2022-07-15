using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LoginChuchu.Models;

namespace LoginChuchu.Services
{
    public class NoteService
    {/*
        private NoteModelContext getContext()
        {
            return new NoteModelContext();
        }
        public int CreateNote(NoteModel obj)
        {
            var _dbContext = getContext();
            _dbContext.Notes.Add(obj);
            int c = _dbContext.SaveChanges();
            return c;
        }
        
        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            var _dbContext = getContext();
            var res = await _dbContext.Employees.ToListAsync();
            return res;
        }

        public async Task<int> UpdateEmployee(EmployeeModel obj)
        {
            var _dbContext = getContext();
            _dbContext.Employees.Update(obj);
            int c = await _dbContext.SaveChangesAsync();
            return c;
        }

        public int DeleteEmployee(EmployeeModel obj)
        {

            var _dbContext = getContext();
            _dbContext.Employees.Remove(obj);
            int c = _dbContext.SaveChanges();
            return c;
        }
        */
    }
}
