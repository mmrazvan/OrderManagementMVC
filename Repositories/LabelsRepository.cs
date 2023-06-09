﻿using Microsoft.EntityFrameworkCore;

using OrderManagementMVC.DataContext;
using OrderManagementMVC.Models;

namespace OrderManagementMVC.Repositories
{
    public class LabelsRepository
    {
        private readonly OrderManagementContext _context;
        public LabelsRepository(OrderManagementContext context)
        {
            _context = context;
        }

        public DbSet<LabelsModel> GetAllLabels()
        {
            return _context.Labels;
        }

        public LabelsModel GetLabelByName(string name)
        {
            return _context.Labels.FirstOrDefault(l => l.LabelName == name);
        }

        public LabelsModel GetLabelById(int id)
        {
            return _context.Labels.FirstOrDefault(x => x.LabelId == id);
        }

        public void AddLabel(LabelsModel label)
        {
            _context.Entry(label).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void UpdateLabel(int id)
        {
            var label = GetLabelById(id);   
            _context.Labels.Update(label);
            _context.SaveChanges();
        }

        public void DeleteLabel(int id)
        {
            var label = GetLabelById(id);
            _context.Labels.Remove(label);
            _context.SaveChanges();
        }
    }
}
