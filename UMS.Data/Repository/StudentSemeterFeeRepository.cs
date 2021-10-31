using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Data.Data;
using UMS.Data.IRepository;
using UMS.Models.Models;

namespace UMS.Data.Repository
{
    public class StudentSemeterFeeRepository : Repository<StudentSemesterFee>, IStudentSemeterFeeRepository
    {
        private readonly ApplicationDbContext _db;
        public StudentSemeterFeeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(StudentSemesterFee studentSemesterFee)
        {
            var semesterFeeObj = await _db.StudentSemesterFees.
                FirstOrDefaultAsync(x => x.StudentId == studentSemesterFee.StudentId && x.SemesterId == studentSemesterFee.SemesterId);
            if(semesterFeeObj!=null)
            {
                semesterFeeObj.TotalPrice = studentSemesterFee.TotalPrice;
            }
        }
    }
}
