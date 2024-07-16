using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebA.Models;

namespace WebA.Controllers
{
    //[Authorize]
    public class RollCallStudentsController : Controller
    {
        private readonly NestStudentContext _context;

        // 建構函數，注入資料庫上下文
        public RollCallStudentsController(NestStudentContext context)
        {
            _context = context;
        }

        // 顯示所有學生紀錄，支援根據條件進行查詢
        public async Task<IActionResult> Index(string search, string searchby, DateTime? searchdate)
        {
            // 先查詢所有紀錄
            IQueryable<RollcallStudent> query = _context.RollcallStudents;

            // 根據 StudentId 進行查詢
            if (searchby == "StudentId" && int.TryParse(search, out int StudentId))
            {
                query = query.Where(e => e.StudentId == StudentId);
            }
            // 根據 Rollcall 進行查詢，支援前綴匹配
            else if (searchby == "Rollcall" && !string.IsNullOrEmpty(search))
            {
                query = query.Where(e => e.Rollcall.StartsWith(search));
            }
            // 根據 Datetime 進行查詢，支援日期範圍查詢
            else if (searchby == "Datetime" && searchdate.HasValue)
            {
                DateTime startDate = searchdate.Value.Date; // 起始日期
                DateTime endDate = startDate.AddDays(1).AddTicks(-1); // 結束日期
                query = query.Where(e => e.Datetime >= startDate && e.Datetime <= endDate);
            }

            // 執行查詢並將結果轉換為列表
            List<RollcallStudent> rollCallStudents = await query.ToListAsync();

            // 如果查詢結果為空，則設置 ViewBag.message 提示用戶未找到紀錄
            if (rollCallStudents.Count == 0)
            {
                ViewBag.message = "未找到紀錄";
            }

            // 返回視圖，傳遞查詢結果
            return View(rollCallStudents);
        }
        // 顯示特定學生的詳細信息
        public async Task<IActionResult> Details(int? id)
        {
            // 如果 id 為空，則返回 404 錯誤
            if (id == null)
            {
                return NotFound();
            }

            // 查詢特定的學生紀錄
            var rollCallStudent = await _context.RollcallStudents.FirstOrDefaultAsync(m => m.RollcallId == id);
            // 如果未找到紀錄，則返回 404 錯誤
            if (rollCallStudent == null)
            {
                return NotFound();
            }

            // 返回視圖，傳遞查詢結果
            return View(rollCallStudent);
        }
        // 顯示創建新學生紀錄的表單
        public IActionResult Create()
        {
            return View();
        }
        // 處理創建新學生紀錄的表單提交
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RollcallId,StudentId,Rollcall,Datetime")] RollcallStudent rollCallStudent)
        {
            // 如果表單數據有效，則保存到資料庫
            if (ModelState.IsValid)
            {
                // 檢查是否有重複的點名記錄
                var existingRollcall = await _context.RollcallStudents
                    .Where(r => r.StudentId == rollCallStudent.StudentId &&
                                r.Datetime.Date == rollCallStudent.Datetime.Date)
                    .FirstOrDefaultAsync();

                if (existingRollcall != null)
                {
                    // 如果有重複記錄，返回錯誤訊息
                    ModelState.AddModelError("", "已點過名");
                    return View(rollCallStudent);
                }

                _context.Add(rollCallStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // 如果表單數據無效，則返回表單視圖，顯示錯誤信息
            return View(rollCallStudent);
        }
        // 顯示編輯特定學生紀錄的表單
        public async Task<IActionResult> Edit(int? id)
        {
            // 如果 StudentId 為空，則返回 404 錯誤
            if (id == null)
            {
                return NotFound();
            }

            // 查詢特定的學生紀錄
            var rollCallStudent = await _context.RollcallStudents.FirstOrDefaultAsync(m => m.RollcallId == id);
            // 如果未找到紀錄，則返回 404 錯誤
            if (rollCallStudent == null)
            {
                return NotFound();
            }
            // 返回視圖，傳遞查詢結果
            return View(rollCallStudent);
        }
        // 處理編輯學生紀錄的表單提交
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RollcallId,Rollcall,Datetime")] RollcallStudent rollCallStudent)
        {
            // 如果 id 與紀錄的 RollcallId 不匹配，則返回 404 錯誤
            if (id != rollCallStudent.RollcallId)
            {
                return NotFound();
            }

            // 如果表單數據有效，則更新資料庫
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rollCallStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // 如果更新過程中出現並發異常，檢查紀錄是否存在
                    if (!RollCallStudentExists(rollCallStudent.RollcallId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // 更新成功後重定向到 Index 視圖
                return RedirectToAction(nameof(Index));
            }
            // 如果表單數據無效，則返回表單視圖，顯示錯誤信息
            return View(rollCallStudent);
        }
        // 顯示刪除特定學生紀錄的確認頁面
        public async Task<IActionResult> Delete(int? id)
        {
            // 如果 id 為空，則返回 404 錯誤
            if (id == null)
            {
                return NotFound();
            }

            // 查詢特定的學生紀錄
            var rollCallStudent = await _context.RollcallStudents
                .FirstOrDefaultAsync(m => m.RollcallId == id);
            // 如果未找到紀錄，則返回 404 錯誤
            if (rollCallStudent == null)
            {
                return NotFound();
            }

            // 返回視圖，傳遞查詢結果
            return View(rollCallStudent);
        }
        // 處理刪除學生紀錄的表單提交
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // 查找並刪除特定的學生紀錄
            var rollCallStudent = await _context.RollcallStudents.FindAsync(id);
            if (rollCallStudent != null)
            {
                _context.RollcallStudents.Remove(rollCallStudent);
            }

            // 保存更改到資料庫
            await _context.SaveChangesAsync();
            // 刪除成功後重定向到 Index 視圖
            return RedirectToAction(nameof(Index));
        }
        // 檢查特定學生紀錄是否存在
        private bool RollCallStudentExists(int id)
        {
            return _context.RollcallStudents.Any(e => e.RollcallId == id);
        }
    }
}
