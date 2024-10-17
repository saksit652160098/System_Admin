using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Memcach.Model;
using Memcach.Data;
using Microsoft.Extensions.Caching.Memory; // นำเข้าการใช้ Memory Cache
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Internal;

namespace Memcach.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly MemcachContext _context;
        private readonly IMemoryCache _memoryCache;

        public CreateModel(MemcachContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
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

            var stopwatch = new Stopwatch();
            stopwatch.Start(); // เริ่มจับเวลา

            // ลองดึงข้อมูลจากแคชก่อน
            if (_memoryCache.TryGetValue(User.ID.ToString(), out User cachedUser))
            {
                // หากมีข้อมูลในแคช ให้ใช้ข้อมูลจากแคชโดยไม่ต้องบันทึกลงฐานข้อมูลอีกครั้ง
                User = cachedUser;

                stopwatch.Stop(); // หยุดจับเวลา
                Console.WriteLine($"Time taken to retrieve from cache: {stopwatch.Elapsed.TotalMilliseconds} ms");

                return RedirectToPage("./Index");
            }

            // หากไม่มีข้อมูลในแคช ให้บันทึกข้อมูลใหม่
            stopwatch.Restart(); // เริ่มจับเวลาใหม่สำหรับการบันทึกข้อมูล

            User.CreatedAt = DateTime.Now;

            // บันทึกผู้ใช้ใหม่ลงในฐานข้อมูล
            _context.User.Add(User);
            await _context.SaveChangesAsync();

            stopwatch.Stop(); // หยุดจับเวลา
            User.TimeTaken = (decimal)stopwatch.Elapsed.TotalMilliseconds;

            // อัปเดต TimeTaken ในฐานข้อมูล
            _context.User.Update(User);
            await _context.SaveChangesAsync();

            _memoryCache.Set(User.ID.ToString(), User, new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7), // ข้อมูลจะถูกเก็บสูงสุด 7 วัน
                SlidingExpiration = TimeSpan.FromHours(1) // รีเฟรชทุก 1 ชั่วโมงหากมีการเข้าถึงข้อมูล
            });

            Console.WriteLine($"Time taken to save to database: {User.TimeTaken} ms");

            return RedirectToPage("./Index");
    }
}
}