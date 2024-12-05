using Microsoft.EntityFrameworkCore;
using SharedLibrary.Backend.DataAccess;

namespace SharedLibrary.Backend.BusinessLogic;

public class CaseService
{
    private readonly AppDbContext _context;

    public CaseService(AppDbContext context)
    {
        _context = context;
    }

    public List<Case> GetAllCases()
    {
        return _context.Cases.Include(c => c.Department).ToList();
    }

    public void AddCase(Case caseEntity)
    {
        if (!_context.Departments.Any(d => d.DepartmentID == caseEntity.DepartmentID))
        {
            throw new Exception("Invalid department ID");
        }

        caseEntity.Department = null;
        _context.Cases.Add(caseEntity);
        _context.SaveChanges();
    }

    public void UpdateCase(Case caseEntity)
    {
        var existingCase = _context.Cases.Find(caseEntity.CaseID);
        if (existingCase == null)
            throw new Exception("Case not found");

        existingCase.Title = caseEntity.Title;
        existingCase.Description = caseEntity.Description;
        existingCase.DepartmentID = caseEntity.DepartmentID;

        _context.Cases.Attach(caseEntity);
        _context.Entry(caseEntity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteCase(int id)
    {
        var caseEntity = _context.Cases.Find(id);
        if (caseEntity == null)
            throw new Exception("Case not found");

        _context.Cases.Remove(caseEntity);
        _context.SaveChanges();
    }
}