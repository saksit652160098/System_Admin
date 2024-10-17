using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Memcach.Data;
using Memcach.Model;
using System.Diagnostics; // เพิ่มการนำเข้าคลาส Stopwatch

namespace Memcach.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly MemcachContext _context;

        public EditModel(MemcachContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(User).State = EntityState.Modified;

            // สร้าง Stopwatch เพื่อจับเวลา
            var stopwatch = new Stopwatch();
            stopwatch.Start(); // เริ่มจับเวลา

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // หยุดจับเวลาเมื่อแก้ไขเสร็จ
            stopwatch.Stop();
            User.TimeTaken = (decimal)stopwatch.Elapsed.TotalMilliseconds;

            // บันทึกเวลาที่ใช้ในการแก้ไขข้อมูล
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }
    }
}