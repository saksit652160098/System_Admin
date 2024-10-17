
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Memcach.Model;
using Memcach.Data;
using System;
using System.Diagnostics; // เพิ่มการนำเข้าคลาส Stopwatch
using System.Threading.Tasks;

namespace Memcach.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly MemcachContext _context;

        public CreateModel(MemcachContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {
            // คำสั่งนี้จะทำเมื่อมีการเรียกหน้า Create.cshtml
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // สร้าง Stopwatch เพื่อจับเวลา
            var stopwatch = new Stopwatch();
            stopwatch.Start(); // เริ่มจับเวลา

            // ตั้งค่า CreatedAt เป็นเวลาปัจจุบัน
            User.CreatedAt = DateTime.Now;

            // จำลองความล่าช้า (delay) 10 วินาที


            // บันทึกผู้ใช้ใหม่ลงในฐานข้อมูล
            _context.User.Add(User);
            await _context.SaveChangesAsync(); // บันทึกข้อมูลแรก

            stopwatch.Stop(); // หยุดจับเวลา

            // เก็บเวลาที่ใช้ในฟิลด์ TimeTaken
            User.TimeTaken = (decimal)stopwatch.Elapsed.TotalMilliseconds; // เก็บเป็นมิลลิวินาที

            // อัปเดต TimeTaken ในฐานข้อมูล
            _context.User.Update(User);
            await _context.SaveChangesAsync(); // บันทึกการอัปเดต

            return RedirectToPage("./Index");
        }
    }
}
